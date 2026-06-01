namespace LifeinFile.Helper
{
    public static class EasingHelper
    {
        // start(現在値) から end(目標値) へ、amount(0.0~1.0の割合) だけ近づけた値を返す
        public static double Lerp(double start, double end, double amount)
        {
            return start + (end - start) * amount;
        }
    }
}