using System;
using System.Windows;
using System.Windows.Threading;

namespace LifeinFile.Scripts.PetSystem
{ 
    public class PetMover
    {
        private Window targetWindow;
        private Window myCage;

        private DispatcherTimer moveTimer;
        private double speedX = 2.0;
        private double speedY = 2.0;

        // コンストラクタ（誕生した時の設定）
        public PetMover(Window window, Window cage)
        {
            targetWindow = window;
            myCage = cage;

            moveTimer = new DispatcherTimer();
            moveTimer.Interval = TimeSpan.FromMilliseconds(50);
            moveTimer.Tick += MoveTimer_Tick;
            moveTimer.Start();
        }

        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            // 左右の壁の判定
            if (targetWindow.Left + targetWindow.Width >= myCage.Left + myCage.Width)
            {
                speedX = -2.0;
            }
            else if (targetWindow.Left <= myCage.Left)
            {
                speedX = 2.0;
            }

            // 上下の壁の判定
            if (targetWindow.Top + targetWindow.Height >= myCage.Top + myCage.Height)
            {
                speedY = -2.0;
            }
            else if (targetWindow.Top <= myCage.Top)
            {
                speedY = 2.0;
            }

            // ウィンドウの座標を動かす
            targetWindow.Left += speedX;
            targetWindow.Top += speedY;
        }
    }
}