namespace Septa.PardakhtVaset.Client
{
    public interface ITimerService
    {
        void Start();
        void Stop();
        bool IsStarted { get; }
        void OnTick();
    }
}
