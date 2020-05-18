namespace FinanceLib
{
    public static class LinearInterpolator
    {
        public static double SolveForY(double x, double x0, double x1, double y0, double y1)
        {
            var slope = (y1 - y0) / (x1 - x0);
            var y = slope * (x - x0) + y0;

            return y;
        }
    }
}
