using Algorithms.DataStructures;

namespace Algorithms.Tests.DataStructures;

public class DynamicArrayTests {
    [Fact]
    public void Indexer_Get_OutOfRange_Throws() {
        var arr = new DynamicArray<int>(n: 2);
        Assert.Throws<ArgumentOutOfRangeException>(() => {
            var _ = arr[0];
        });
        Assert.Throws<ArgumentOutOfRangeException>(() => {
            var _ = arr[-1];
        });
    }

    [Fact]
    public void Add_Then_Indexer_Get_Returns_Same_Values() {
        var arr = new DynamicArray<int>(n: 2);
        const int N = 100;

        for (int i = 0; i < N; i++)
            arr.Add(i);

        for (int i = 0; i < N; i++)
            Assert.Equal(i, arr[i]);

        Assert.Throws<ArgumentOutOfRangeException>(() => {
            var _ = arr[N];
        });
    }

    [Fact]
    public void Indexer_Set_Overwrites_Value() {
        var arr = new DynamicArray<string>(n: 1);
        arr.Add("a");
        arr.Add("b");

        arr[1] = "B";
        Assert.Equal("a", arr[0]);
        Assert.Equal("B", arr[1]);
    }

    [Fact]
    public void Capacity_Grows_When_Full_Adding_Many_Items_Does_Not_Throw() {
        var arr = new DynamicArray<int>(n: 1);
        for (int i = 0; i < 10_000; i++)
            arr.Add(i);

        Assert.Equal(0, arr[0]);
        Assert.Equal(9_999, arr[9_999]);
        Assert.Throws<ArgumentOutOfRangeException>(() => {
            var _ = arr[10_000];
        });
    }

    [Fact]
    public void Remove_Middle_Shifts_Left_And_Shortens_Length() {
        var arr = new DynamicArray<int>(n: 4);
        arr.Add(10);
        arr.Add(20);
        arr.Add(30);

        // remove element at index 1 (value 20)
        arr.Remove(1);

        // should shift 30 left
        Assert.Equal(10, arr[0]);
        Assert.Equal(30, arr[1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => {
            var _ = arr[2];
        });
    }

    [Fact]
    public void Remove_First_Shifts_All_Left() {
        var arr = new DynamicArray<int>(n: 4);
        arr.Add(1);
        arr.Add(2);
        arr.Add(3);

        arr.Remove(0);

        Assert.Equal(2, arr[0]);
        Assert.Equal(3, arr[1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => {
            var _ = arr[2];
        });
    }

    [Fact]
    public void Remove_Last_Just_Shortens_Length() {
        var arr = new DynamicArray<int>(n: 4);
        arr.Add(1);
        arr.Add(2);
        arr.Add(3);

        arr.Remove(2); // remove last

        Assert.Equal(1, arr[0]);
        Assert.Equal(2, arr[1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => {
            var _ = arr[2];
        });
    }

    [Fact]
    public void Remove_OutOfRange_Throws() {
        var arr = new DynamicArray<int>(n: 3);
        arr.Add(1);
        arr.Add(2);

        Assert.Throws<ArgumentOutOfRangeException>(() => arr.Remove(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => arr.Remove(2)); // equals current length
        Assert.Throws<ArgumentOutOfRangeException>(() => arr.Remove(5));
    }
}