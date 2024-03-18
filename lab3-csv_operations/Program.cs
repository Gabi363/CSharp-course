using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.RegularExpressions;

public class Tweet {
    public string ?Text { get; set; }
    public string ?UserName { get; set; }
    public string ?LinkToTweet { get; set; }
    public string ?FirstLinkUrl { get; set; }
    public string ?CreatedAt { get; set; }
    public string ?TweetEmbedCode { get; set; }
    public override string? ToString() {
        return ">>>Tweet\n" + "Text:\n" + Text + "\nUsername:\n" + UserName + "\nLink to tweet:\n" + LinkToTweet 
        + "\nLink URL:\n" + FirstLinkUrl + "\nCreated at:\n" + CreatedAt + "\nTweet embed code:\n" + TweetEmbedCode + "\n";
    }
}

public class Tweets {
    public List<Tweet> data { get; set; }

    public override string? ToString() {
        string to_return = "";
        foreach(Tweet t in this.data) {
            to_return += t.ToString();
        }
        return to_return;
    }

    public void to_xml() {

        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(this.GetType());
        using (StreamWriter writer = File.CreateText("C:\\Users\\Gabi\\OneDrive\\Dokumenty\\studia\\4 semestr\\PZ2\\lab3\\data.xml")) {
            x.Serialize(writer, this);
        }

        Tweets? tweets1 = null;
        using (StreamReader reader = new StreamReader("C:\\Users\\Gabi\\OneDrive\\Dokumenty\\studia\\4 semestr\\PZ2\\lab3\\data.xml")) {
            tweets1 = (Tweets)x.Deserialize(reader);
        }

        foreach (Tweet o in tweets1.data)
            Console.WriteLine(o.ToString());
    }


    public Tweets sort_tweets_by_username() {

        Tweets tweets = new Tweets {
            data = new List<Tweet>(this.data)
        };
        tweets.data.Sort((x,y) => x.UserName.CompareTo(y.UserName));
        // foreach(Tweet t in tweets.data)
        //     Console.WriteLine(">>>" + t.UserName + "\n" + t.Text);

        return tweets;
    }

    public void sort_by_date() {

        string format = @"MMMM dd, yyyy \a\t hh:mmtt";
        IFormatProvider provider = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
        this.data.Sort((x,y) => DateTime.ParseExact(x.CreatedAt, format, provider).CompareTo(DateTime.ParseExact(y.CreatedAt, format, provider)));
    }


    public List<string> sort_users_by_date() {

        List<string> users = new List<string>();
        Tweets tweets = new Tweets {
            data = new List<Tweet>(this.data)
        };
        tweets.sort_by_date();

        foreach(Tweet t in tweets.data) {
            users.Add(t.UserName);
            // Console.WriteLine(t.UserName + "\n" + t.CreatedAt + "\n");
        }
        return users;

    }


    public Dictionary<string, List<Tweet>> create_dict_of_users() {

        Dictionary<string, List<Tweet>> dict = new Dictionary<string, List<Tweet>>();
        foreach(Tweet t in this.data) {
            if(dict.ContainsKey(t.UserName)) {
                dict[t.UserName].Add(t);
            }
            else {
                dict.Add(t.UserName, new List<Tweet>{t} );
            }
        }
        return dict;
    }


    public Dictionary<string, int> words_frequency() {

        Dictionary<string, int> dict = new Dictionary<string, int>();
        var wordPattern = new Regex(@"[a-zA-Z]+");

        foreach(Tweet t in this.data) {

            foreach (Match match in wordPattern.Matches(t.Text)) {
                int currentCount;
                dict.TryGetValue(match.Value.ToLower(), out currentCount);
                currentCount++;
                dict[match.Value.ToLower()] = currentCount;
            }
        }
        return dict;
    }


    public void most_frequent_words() {

        Dictionary<string, int> dict = this.words_frequency();
        foreach(var x in dict.Where(pair => pair.Key.Count() < 5).ToList()) {
            dict.Remove(x.Key);
        }
        Dictionary<string, int> sortedDict = dict.OrderByDescending(pair => pair.Value)
                                                .Take(10)
                                                .ToDictionary(pair => pair.Key, pair => pair.Value);

        foreach(KeyValuePair<string, int> el in sortedDict) {
            Console.WriteLine(el.Key + " " + el.Value);
        }
    }


    public void compute_idf() {

        Dictionary<string, double> dict = new Dictionary<string, double>();
        var wordPattern = new Regex(@"[a-zA-Z]+");

        foreach(Tweet t in this.data) {

            List<string> words = wordPattern.Matches(t.Text).Select(match => match.Value).ToList()
                                            .ConvertAll(x => x.ToLower()).Distinct().ToList();
            foreach(string word in words) {
                if(dict.ContainsKey(word)) {
                    dict[word] ++;
                }
                else {
                    dict.Add(word, 1);
                }
            }

        }
        int number_of_doc = this.data.Count;
        foreach(KeyValuePair<string, double> pair in dict) {
            dict[pair.Key] = Math.Log(number_of_doc / pair.Value, Math.E);
        }
        dict = dict.OrderByDescending(pair => pair.Value)
                                    .Take(10)
                                    .ToDictionary(pair => pair.Key, pair => pair.Value);
        foreach(KeyValuePair<string, double> pair in dict) {
            Console.WriteLine(pair.Key + " " + pair.Value);
        }
    }
}





public class Program {

    public static void Main(string[] args){

        // read json file to variable
        String jsonString = System.IO.File.ReadAllText("C:\\Users\\Gabi\\OneDrive\\Dokumenty\\studia\\4 semestr\\PZ2\\lab3\\data.json");        
        Tweets ?tweets = JsonSerializer.Deserialize<Tweets>(jsonString);
        // foreach (Tweet t in tweets.data)
            // Console.WriteLine(t.ToString());

        // read from / write to xml
        tweets.to_xml();

        // sort tweets by username
        Console.WriteLine(tweets.sort_tweets_by_username());

        // sort users by date
        foreach(var x in tweets.sort_users_by_date()) {
            Console.WriteLine(x);
        }

        // the latest tweet
        tweets.sort_by_date();
        Console.WriteLine(tweets.data[tweets.data.Count-1]);

        // the oldest tweet
        Console.WriteLine(tweets.data[0]);

        // dict of tweets written by given users
        Dictionary<string, List<Tweet>> dict = tweets.sort_tweets_by_username().create_dict_of_users();
        foreach(KeyValuePair<string, List<Tweet>> el in dict) {
              Console.WriteLine(">>>>>>>>>>>" + el.Key);
              foreach(Tweet t in el.Value) Console.WriteLine(t);
          }

        // frequency of words
        Dictionary<string, int> dict1 = tweets.words_frequency();
        foreach(KeyValuePair<string, int> el in dict1) {
              Console.WriteLine(el.Key + " " + el.Value);
          }
        Console.WriteLine(dict1["song"]);

        // 10 most frequent words
        tweets.most_frequent_words();

        // idf for words
        tweets.compute_idf();

    }
}

