namespace zad1{
class Producer {
    public bool running = true;
    public Mutex ?mutex = null;
    public int number = 0;
    public int sleepTime;
    public List<int> ?data = null;

    public Producer(int number, Mutex mutex, int sleepTime, List<int> data) {
        this.number = number;
        this.mutex = mutex;
        this.sleepTime = sleepTime;
        this.data = data;
    }

    public void Start() {
        while(running){
            Thread.Sleep(sleepTime);
            ProduceData();
        }
        Console.WriteLine("Producer " + number + " finished work.");
    }
    public void ProduceData() {
        mutex.WaitOne();
        data.Add(this.number);
        mutex.ReleaseMutex();
    }
}


class Consumer {
    public bool running = true;
    public Mutex ?mutex = null;
    public int number = 0;
    public int sleepTime;
    public List<int> ?data = null;
    public List<int> ?consumed = new List<int>();

    public Consumer(int number, Mutex mutex, int sleepTime, List<int> data) {
        this.number = number;
        this.mutex = mutex;
        this.sleepTime = sleepTime;
        this.data = data;
    }

    public void Start() {
        while(running){
            Thread.Sleep(sleepTime);
            ConsumeData();
        }
        Console.WriteLine("Consumer " + number + " consumed:\n" + ToString());
    }
    public void ConsumeData() {
        mutex.WaitOne();  
        if(data.Count == 0){
            mutex.ReleaseMutex();
            return;
        }
        consumed.Add(data[0]);
        data.RemoveAt(0);
        mutex.ReleaseMutex();
    }
    public override string ToString(){
        Dictionary<int, int> dict = new Dictionary<int, int>();
        foreach(var x in consumed){
            if(dict.ContainsKey(x)){
                dict[x] += 1;
            }
            else{
                dict.Add(x, 0);
            }
        }
        dict = dict.OrderBy(x => x.Key).ToDictionary();
        return string.Join("\n", dict.Select(x => "\tproducer " + x.Key.ToString() + ": " + x.Value.ToString()));
    }
}



class ProducerConsumer
{    
    public List<Producer> producers = new List<Producer>();
    public List<Consumer> consumers = new List<Consumer>();
    public List<int> data = new List<int>();
    public Mutex mutex = new Mutex();
    public int p_count = 0;
    public int c_count = 0;
    public Random random = new Random();

    public ProducerConsumer(int p, int c) {
        p_count = p;
        c_count = c;
    }

    public void Start(){
        for(int i = 0; i<p_count; i++) {
            Producer producer = new Producer(i, mutex, random.Next(100,1000), data);
            producers.Add(producer);
            Thread thread = new Thread(new ThreadStart(producer.Start));
            thread.Start();
            Console.WriteLine("Producer " + i + " started");
        }
        for(int i = 0; i<c_count; i++) {
            Consumer consumer = new Consumer(i, mutex, random.Next(100,1000), data);
            consumers.Add(consumer);
            Thread thread = new Thread(new ThreadStart(consumer.Start));
            thread.Start();
            Console.WriteLine("Consumer " + i + " started");
        }

        Console.WriteLine("Press 'Q' to quit");
        bool running = true;
        while(running) {
            if(Console.ReadKey().Key == ConsoleKey.Q){
                running = false;
            }
        }
        foreach(Producer p in producers) {
            p.running = false;
        }
        foreach(Consumer c in consumers) {
            c.running = false;
        }
    }
    static void Main(){
        ProducerConsumer program = new ProducerConsumer(5,4);
        program.Start();
    }
}

}