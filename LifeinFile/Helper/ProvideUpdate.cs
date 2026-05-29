using System.Reactive;
using System.Windows.Threading;

namespace LifeinFile.Helper
{
    static class ProvideUpdate
    {
        const int TICK_INTERVAL = 50;

        static DispatcherTimer _timer = new DispatcherTimer();
        
        static List<IUpdate> _updates = new List<IUpdate>();
        static List<IUpdateLate> _updateLates = new List<IUpdateLate>();

        static IObservable<Unit> OnUpdate { get; }
        static IObservable<Unit> OnUpdateLate { get; }
        
        
        public static void Start()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(TICK_INTERVAL);
            _timer.Tick += (s, e) => OnTick();
            _timer.Start();
        }

        static void OnTick()
        {
            foreach (var update in _updates)
            {
                update.OnUpdate();
            }
            foreach (var updateLate in _updateLates)
            {
                updateLate.OnUpdateLate();
            }
        }

        public static void AddUpdate(IUpdate mover) => _updates.Add(mover);
        public static void RemoveUpdate(IUpdate mover) => _updates.Remove(mover);
        public static void AddUpdateLate(IUpdateLate mover) => _updateLates.Add(mover);
        public static void RemoveUpdateLate(IUpdateLate mover) => _updateLates.Remove(mover);

    }
}
