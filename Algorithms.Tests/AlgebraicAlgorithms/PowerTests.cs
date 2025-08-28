using Algorithms.AlgebraicAlgorithms;

namespace Algorithms.Tests.AlgebraicAlgorithms;

public class PowerTests {
    [Theory]
    [InlineData(2.0, 3, 8.0)]
    [InlineData(0.0, 5, 0.0)]
    [InlineData(5.0, 0, 1.0)]
    [InlineData(-2.0, 4, 16.0)]
    [InlineData(-2.0, 3, -8.0)]
    [InlineData(0.0, 0, 1.0)]
    [InlineData(1.0, 100, 1.0)]
    [InlineData(-1.0, 101, -1.0)]
    [InlineData(-1.0, 100, 1.0)]
    [InlineData(10.0, 6, 1000000.0)]
    [InlineData(3.0, 5, 243.0)]
    public void PowIterative_DoubleBase_IntExponent_ReturnsExpected(double baseValue, int exponent, double expected) {
        double result = Power.PowIterative(baseValue, exponent);
        Assert.True(Math.Abs(result - expected) < 1e-9, $"Expected {expected}, got {result}");
    }

    [Fact]
    public void PowIterative_NegativeExponent_ThrowsArgumentOutOfRangeException() {
        Assert.Throws<ArgumentOutOfRangeException>(() => Power.PowIterative(2.0, -1));
    }
}