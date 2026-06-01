using System;
using System.Reactive.Subjects;
using System.Windows.Threading;

namespace LifeinFile.Helper
{
    public static class ProvideUpdate
    {
        const int TICK_INTERVAL = 50;

        static DispatcherTimer _timer = new DispatcherTimer();

        private static readonly Subject<double> _updateSubject = new Subject<double>();
        private static readonly Subject<double> _lateUpdateSubject = new Subject<double>();

        public static IObservable<double> UpdateAsObservable => _updateSubject;
        public static IObservable<double> LateUpdateAsObservable => _lateUpdateSubject;

        private static DateTime _lastTime;

        public static void Start()
        {
            _lastTime = DateTime.Now;

            _timer.Interval = TimeSpan.FromMilliseconds(TICK_INTERVAL);
            _timer.Tick += (s, e) => OnTick();
            _timer.Start();
        }

        static void OnTick()
        {
            DateTime now = DateTime.Now;
            double deltaTime = (now - _lastTime).TotalSeconds;
            
            _lastTime = now;

            _updateSubject.OnNext(deltaTime);
            _lateUpdateSubject.OnNext(deltaTime);
        }
    }
}