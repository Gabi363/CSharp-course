using System.Reactive.Linq;

Random random = new Random();
var randomNumber = Observable.Interval(TimeSpan.FromSeconds(1))
                        .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(20)))
                        .Select(x => random.NextDouble()*2 -1);
var scanMax = randomNumber.Scan(-1.0, (acc, current) => Math.Max(acc, current));
scanMax.Subscribe(x => Console.WriteLine("max " + x), 
                    () => Console.WriteLine("completed"));
// randomNumber.Subscribe(
//     Console.WriteLine, 
//     () => Console.WriteLine("completed"));

var sinus = Observable.Interval(TimeSpan.FromSeconds(1))
                    .Where(x => Math.Sin(x * 0.01) >= 0 && Math.Sin(x * 0.01) <= 0.03)
                    .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(20)))
                    .Select(x => Math.Sin(x * 0.01));
// sinus.Subscribe(
//     Console.WriteLine, 
//     () => Console.WriteLine("completed"));

scanMax.Merge(sinus).Subscribe(
    Console.WriteLine, 
    () => Console.WriteLine("completed")
);
Console.ReadLine();