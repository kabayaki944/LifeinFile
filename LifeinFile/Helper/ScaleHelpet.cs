namespace LifeinFile.Helper
{
    public static class ScaleHelper
    {
        public static (double x, double y) GetSlimeScale(double inputX, double sensibility)
        {
            double z = Math.Sin(inputX) * sensibility;

            double resultX = 1d + z ;
            double resultY = 1d - z;
            
            //Debug.WriteLine("ScaleX" + z);
            return (resultX, resultY);
        }
    }
}