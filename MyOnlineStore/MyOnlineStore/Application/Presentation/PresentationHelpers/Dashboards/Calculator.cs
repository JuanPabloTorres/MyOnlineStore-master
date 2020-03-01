using System;

namespace MyOnlineStore.Utilities
{
    public static class Calculator
    {
        public static double GetPercent(double value, double totaltoreach)
        {
            double result = 0;
            double percent = 0;
            if (value != 0)
            {

                result = value / totaltoreach;
                percent = Math.Round(result, 2) * 100;
            }
            else
            {
                percent = 0;
            }

            return percent;
        }

        public static double GetTotalToReached(double val, int days)
        {

            double totaltoreach = 0;

            totaltoreach = val * days;
            return totaltoreach;
        }
    }
}
