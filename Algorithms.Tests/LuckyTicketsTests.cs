namespace Algorithms.Tests;

public class LuckyTicketsTests {
    [Theory]
    [InlineData(1, 10)]
    [InlineData(2, 670)]
    [InlineData(3, 55252)]
    [InlineData(4, 4816030)]
    [InlineData(5, 432457640)]
    [InlineData(6, 39581170420)]
    [InlineData(7, 3671331273480)]
    [InlineData(8, 343900019857310)]
    [InlineData(9, 32458256583753952)]
    [InlineData(10, 3081918923741896840)]
    public void GetLuckyTicketsAmount_Always_ReturnsCorrectAmount(int n, long expectedAmount) {
        Assert.Equal(expectedAmount, new LuckyTickets().GetLuckyTicketsAmount(n));
    }
}