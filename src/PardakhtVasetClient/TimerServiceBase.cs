using System;
using System.Threading;

namespace Septa.PardakhtVaset.Client
{
    public abstract class TimerServiceBase : IDisposable
    {
        private readonly Timer _timer = null;
        private bool _isStarted = false;
        private bool _isDisposed = false;

        public bool IsStarted => _isStarted;

        public TimerServiceBase()
        {
            _timer = new Timer(InternalTick, null, 0, 0);
        }

        private void InternalTick(object state)
        {
            if (!_isDisposed && _isStarted)
            {
                OnTick();
            }
        }

        public abstract void OnTick();

        public void Start(TimeSpan interval)
        {
            if (_isDisposed)
                throw new InvalidOperationException("Object is disposed.");

            if (!_isStarted)
            {
                _isStarted = true;
                _timer.Change(interval, interval);
            }
        }

        public void Stop()
        {
            if (_isDisposed)
                throw new InvalidOperationException("Object is disposed.");

            if (_isStarted)
            {
                _isStarted = false;
                _timer.Change(0, 0);
            }
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _timer.Dispose();
                _isDisposed = true;
            }
        }
    }
}
