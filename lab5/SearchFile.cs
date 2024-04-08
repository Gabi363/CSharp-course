namespace zad3{

public class SearchFile{
    string path;
    string search_for;
    string ?found;
    public SearchFile(string path, string search_for){
        this.path = path;
        this.search_for = search_for;
    }
    public void Start(){
        Thread searchThread = new Thread(new ThreadStart(this.SearchThread));
        searchThread.Start();
        Console.WriteLine("Searching for " + search_for + "...");
        while(true){
            if(found != null){
                Console.WriteLine("The result: " + found);
                break;
            }
        }
    }

    public void SearchThread() {
        foreach(string file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)){
            if(Path.GetFileName(file).Contains(search_for)){
                found = file;
            }
        }
        found = "Not found!";
    }

    public static void Main(){
        SearchFile program = new SearchFile("./", "at");
        program.Start();
    }
}

}