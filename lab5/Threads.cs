namespace zad4{

    public class MiniThread {
        public int number = 0;
        public Threads parent;
        public MiniThread(Threads parent, int number) {
            this.parent = parent;
            this.number = number;
        }
        public void Start() {
            Interlocked.Increment(ref parent.numberOfThreads);
            Console.WriteLine(number + " started");

            parent.ewhChild.WaitOne();
            Interlocked.Decrement(ref parent.numberOfThreads);

            parent.ewhParent.Set();
            Console.WriteLine(number + " finished");
        }

    }

    public class Threads{
        public long numberOfThreads = 0;
        public int maxNumberOfThreads;
        public EventWaitHandle ewhChild;
        public EventWaitHandle ewhParent;

        public Threads(int maxNumberOfThreads) {
            this.maxNumberOfThreads = maxNumberOfThreads;
            ewhChild = new EventWaitHandle(false, EventResetMode.AutoReset);
            ewhParent = new EventWaitHandle(false, EventResetMode.AutoReset);
        }
        public void Start() {
            Console.WriteLine("Initializing threads");
            for(int i=0; i<maxNumberOfThreads; i++){
                MiniThread m = new MiniThread(this, i);
                Thread t = new Thread(new ThreadStart(m.Start));
                t.Start();
            }
            while(Interlocked.Read(ref numberOfThreads) != maxNumberOfThreads){
                Thread.Sleep(100);
            }
            Console.WriteLine("All threads started");
            while(Interlocked.Read(ref numberOfThreads) > 0) {
                WaitHandle.SignalAndWait(ewhChild, ewhParent);
            }
            Console.WriteLine("All threads finished");
            Console.WriteLine("Main thread finished");
        }

        public static void Main(){
            Threads program = new Threads(500);
            program.Start();
        }
    }


}