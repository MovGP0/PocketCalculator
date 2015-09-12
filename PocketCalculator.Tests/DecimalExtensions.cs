namespace PocketCalculator.Tests
{
    internal static class DecimalExtensions
    {
        internal static bool IsApproximatlyZero(this decimal value)
        {
            return value.IsApproximatly(0m);
        }

        internal static bool IsApproximatly(this decimal value, decimal other)
        {
            const decimal epsilon = 0.0000000001m;
            return value < other + epsilon 
                && value > other - epsilon;
        }
    }
}
