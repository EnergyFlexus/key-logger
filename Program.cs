using System.Reactive.Linq;

namespace KeyLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            using var keyTracker = new KeyTracker();
            using var fileWriter = File.CreateText("log.txt");
            fileWriter.AutoFlush = true;

            var completed = false;

            // Подписываемся на все нажатия для логирования
            keyTracker.KeySubject.Subscribe(
                keyEvent =>
                {
                    var message = $"Pressed {keyEvent.Key}";
                    fileWriter.WriteLine(message);
                },
                ex => Console.WriteLine($"Error: {ex.Message}"),
                () => completed = true
            );

            Console.WriteLine("Press 0 to exit.");

            while (!completed)
            {
                if (Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(true);
                    keyTracker.Track(keyInfo);
                }
            }
        }
    }
}
