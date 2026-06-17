namespace LifeinFile.Windows
{
    public interface IPetWindow
    {
        public (double x, double y) GetSquishScale();
        public (double x, double y) GetDirectionScale();
        public (double x, double y) GetTrans();

        public void SetTrans(double x, double y);
        public void SetSquishScale(double x, double y);

        public void SetDirectionScale(double x, double y);

        public void AddScale(double amountX, double amountY);
        public void SetSprite(string spriteName);
    }
}