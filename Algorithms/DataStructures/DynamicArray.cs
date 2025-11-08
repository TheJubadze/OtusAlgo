namespace Algorithms.DataStructures;

public class DynamicArray<T>(int n = 42) {
    private int _insertIndex;
    private T[] _array = new T[n];

    public void Add(T n) {
        if (_array.Length == _insertIndex) {
            var newArray = new T[_array.Length * 2];
            Array.Copy(_array, 0, newArray, 0, _array.Length);
            _array = newArray;
        }

        _array[_insertIndex] = n;
        _insertIndex++;
    }

    public void Remove(int index) {
        if (index < 0 || _insertIndex <= index) throw new ArgumentOutOfRangeException(nameof(index));

        for (int i = index; i < _insertIndex; i++) {
            _array[i] = _array[i + 1];
        }

        _insertIndex--;
    }

    public T this[int index] {
        get => (uint)index >= (uint)_insertIndex ? throw new ArgumentOutOfRangeException(nameof(index)) : _array[index];
        set {
            if ((uint)index >= (uint)_insertIndex)
                throw new ArgumentOutOfRangeException(nameof(index));
            _array[index] = value;
        }
    }
}