using System;
using System.Threading;

namespace Septa.PardakhtVaset.Client
{
    public abstract class TimerServiceBase : ITimerService, IDisposable
    {
        private readonly Timer _timer = null;
        private bool _isStarted = false;
        private bool _isDisposed = false;

        public bool IsStarted => _isStarted;

        public TimerServiceBase(TimeSpan interval)
        {
            _timer = new Timer(InternalTick, null, TimeSpan.Zero, interval);
        }

        private void InternalTick(object state)
        {
            if (!_isDisposed && _isStarted)
            {
                OnTick();
            }
        }

        public abstract void OnTick();

        public void Start()
        {
            if (_isDisposed)
                throw new InvalidOperationException("Object is disposed.");

            if (!_isStarted)
            {
                _isStarted = true;
                throw new NotImplementedException();
            }
        }

        public void Stop()
        {
            if (_isDisposed)
                throw new InvalidOperationException("Object is disposed.");

            if (_isStarted)
            {
                _isStarted = false;
                throw new NotImplementedException();
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
