namespace Algorithms.AlgebraicAlgorithms;

public static class Power {
    public static double PowIterative(double a, int n) {
        ArgumentOutOfRangeException.ThrowIfNegative(n);

        switch (a) {
            case 0 when n == 0:
            case 1:
                return 1;
        }

        double result = 1;
        for (var i = 0; i < n; i++) {
            result *= a;
        }

        return result;
    }
}