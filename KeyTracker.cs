using System.Reactive.Subjects;

namespace KeyLogger
{
    public class KeyEvent
    {
        public char Key { get; set; }
    }

    public class KeyTracker : IDisposable
    {
        public ISubject<KeyEvent> KeySubject { get; set; }
        private bool _isCompleted;

        public KeyTracker()
        {
            KeySubject = new Subject<KeyEvent>();
            _isCompleted = false;
        }

        public void Track(ConsoleKeyInfo keyInfo)
        {
            if (_isCompleted) return;

            // Проверяем на 0
            if (keyInfo.Key == ConsoleKey.D0)
            {
                Complete();
                return;
            }

            KeySubject.OnNext(new KeyEvent 
            { 
                Key = keyInfo.KeyChar,
            });
        }

        public void Complete()
        {
            if (!_isCompleted)
            {
                _isCompleted = true;
                KeySubject.OnCompleted();
            }
        }

        public void Dispose()
        {
            Complete();
            (KeySubject as IDisposable)?.Dispose();
        }
    }
}
