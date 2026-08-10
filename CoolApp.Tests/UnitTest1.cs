namespace CoolApp.Tests;

public class BasicTests
{
    [Fact]
    public void AdditionWorks()
    {
        int a = 2;
        int b = 3;
        int result = a + b;
        Assert.Equal(5, result);
    }
}