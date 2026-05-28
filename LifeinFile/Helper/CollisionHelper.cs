using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Windows;


namespace LifeinFile.Helper
{
    public struct CollisionResult
    {
        public bool hitX;
        public bool hitY;
        public double newX;
        public double newY;
    }
    public static class CollisionHelper
    {
        // Windowをそのまま受け取る窓口（Rectに変換してパスする）
        public static CollisionResult CheckAndClamp(Window target, Window area)
        {
            Rect targetRect = new Rect(target.Left, target.Top, target.Width, target.Height);
            Rect areaRect = new Rect(area.Left, area.Top, area.Width, area.Height);

            return CheckAndClamp(targetRect, areaRect);
        }
        public static CollisionResult CheckAndClamp(Window target, Rect area)
        {
            // Target(Pet)だけをRectに変換して、大本命にパス！
            Rect targetRect = new Rect(target.Left, target.Top, target.Width, target.Height);
            return CheckAndClamp(targetRect, area);
        }

        // --- 大本命：Rect同士を受け取って計算する窓口 ---
        public static CollisionResult CheckAndClamp(Rect target, Rect area)
        {
            bool hitX = false;
            bool hitY = false;
            double newX = target.X;
            double newY = target.Y;

            // X軸（左右）の壁チェック
            if (newX <= area.Left) // areaMinX の代わりに area.Left が使える！
            {
                newX = area.Left;
                hitX = true;
            }
            else if (newX + target.Width >= area.Right) // areaMaxX を計算しなくても area.Right が使える！
            {
                newX = area.Right - target.Width;
                hitX = true;
            }

            // Y軸（上下）の壁チェック
            if (newY <= area.Top)
            {
                newY = area.Top;
                hitY = true;
            }
            else if (newY + target.Height >= area.Bottom) // area.Bottom が使える！
            {
                newY = area.Bottom - target.Height;
                hitY = true;
            }

            return new CollisionResult
            {
                hitX = hitX,
                hitY = hitY,
                newX = newX,
                newY = newY
            };
        }

        /// <summary>
        /// Target(Petなど)の中心点が、Area(Cageなど)の中に含まれているかを判定します。
        /// </summary>
        public static bool IsCenterInside(Window target, Window area)
        {
            Point center = new Point(target.Left + (target.Width / 2), target.Top + (target.Height / 2));
            Rect areaRect = new Rect(area.Left, area.Top, area.Width, area.Height);

            return areaRect.Contains(center);
        }
    }
}
