using System;
using System.Reactive;
using System.Reactive.Subjects;
using System.Windows.Threading;

namespace LifeinFile.Helper
{
    public static class ProvideUpdate
    {
        const int TICK_INTERVAL = 50;

        static DispatcherTimer _timer = new DispatcherTimer();

        // 【追加】Rxの心臓部。イベントを発火するスイッチ（Subject）
        // Unit は「値を持たない純粋な通知」という意味です（voidのRx版）
        private static readonly Subject<Unit> _updateSubject = new Subject<Unit>();
        private static readonly Subject<Unit> _lateUpdateSubject = new Subject<Unit>();

        // 【追加】外部に公開するストリーム。外のクラスはこれをSubscribe（購読）する
        // Subjectをそのまま公開すると外から勝手に発火されてしまうため、IObservableとして隠蔽します
        public static IObservable<Unit> UpdateAsObservable => _updateSubject;
        public static IObservable<Unit> LateUpdateAsObservable => _lateUpdateSubject;

        public static void Start()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(TICK_INTERVAL);
            _timer.Tick += (s, e) => OnTick();
            _timer.Start();
        }

        static void OnTick()
        {
            // 購読している全員（PetModelなど）に「Updateのタイミングだよ！」と一斉通知
            _updateSubject.OnNext(Unit.Default);

            // 続けて「LateUpdateだよ！」と通知
            _lateUpdateSubject.OnNext(Unit.Default);
        }
    }
}