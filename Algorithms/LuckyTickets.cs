namespace Algorithms;

public class LuckyTickets {
    private int _n;

    public long GetLuckyTicketsAmount(int n) {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
        
        _n = n;
        var mat = new long[10, 10];
        for (var i = 0; i < 10; i++) mat[i, i] = 1;
        return Helper(2, mat);
    }

    private long Helper(int n, long[,] matrix) {
        if (n > _n) return GetTotalLine(matrix).Select(x => x * x).Sum();

        var mat = new long[10, 9 * n + 1];
        var line = GetTotalLine(matrix);

        for (var i = 0; i < 10; i++) {
            for (var j = 0; j < line.Length; j++) {
                mat[i, i + j] = line[j];
            }
        }

        return Helper(n + 1, mat);
    }

    private static long[] GetTotalLine(long[,] matrix) {
        var line = new long [matrix.GetLength(1)];
        for (var i = 0; i < matrix.GetLength(0); i++) {
            for (var j = 0; j < matrix.GetLength(1); j++) {
                line[j] += matrix[i, j];
            }
        }

        return line;
    }
}