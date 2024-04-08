namespace zad2{

class FileWatcher{
    public string ?path;
    public FileSystemWatcher fwatcher;
    public FileWatcher(string path) {
        this.path = path;
        fwatcher = new FileSystemWatcher(path);
    }
    void Start() {
        Console.WriteLine("Watching: " + path);

        fwatcher.EnableRaisingEvents = true;
        fwatcher.IncludeSubdirectories = false;
        fwatcher.Changed += (sender, e) => Console.WriteLine("Changed: " + e.Name);
        fwatcher.Created += (sender, e) => Console.WriteLine("Created: " + e.Name);
        fwatcher.Deleted += (sender, e) => Console.WriteLine("Deleted: " + e.Name);
        fwatcher.Renamed += (sender, e) => Console.WriteLine("Renamed " + e.OldName + " for: " + e.Name);

        Thread thread = new Thread(new ThreadStart(this.ThreadStart));
        thread.Start();
        
    }

    public void ThreadStart(){
        Console.WriteLine("Press 'Q' to quit");
        bool running = true;
        while(running) {
            if(Console.ReadKey().Key == ConsoleKey.Q){
                running = false;
                fwatcher.EnableRaisingEvents = false;
                fwatcher.Dispose();
            }
        }
        Console.WriteLine("\nStopped watching");
    }
    public static void Main() {
        FileWatcher program = new FileWatcher("./");
        program.Start();
    }
}


}