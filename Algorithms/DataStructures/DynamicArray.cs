namespace Algorithms.DataStructures;

public class DynamicArray<T>(int capacity = 4) {
    private int _insertIndex;
    private T[] _array = new T[Math.Max(0, capacity)];

    public int Count => _insertIndex;

    public void Add(T item) {
        EnsureCapacity(_insertIndex + 1);
        _array[_insertIndex++] = item;
    }

    public void Remove(int index) {
        if ((uint)index >= (uint)_insertIndex)
            throw new ArgumentOutOfRangeException(nameof(index));

        var move = _insertIndex - index - 1;
        if (move > 0)
            Array.Copy(_array, index + 1, _array, index, move);

        _array[--_insertIndex] = default!;
    }

    public T this[int index] {
        get => (uint)index >= (uint)_insertIndex ? throw new ArgumentOutOfRangeException(nameof(index)) : _array[index];
        set {
            if ((uint)index >= (uint)_insertIndex) throw new ArgumentOutOfRangeException(nameof(index));
            _array[index] = value;
        }
    }

    public void Clear() {
        _insertIndex = 0;
    }

    private void EnsureCapacity(int min) {
        if (_array.Length >= min) return;

        var newCapacity = _array.Length == 0 ? 4 : _array.Length * 2;
        if (newCapacity < min) newCapacity = min;

        const int maxArrayLength = 0x7FFFFFC7;
        if (newCapacity > maxArrayLength) newCapacity = Math.Max(min, maxArrayLength);

        Array.Resize(ref _array, newCapacity);
    }
}