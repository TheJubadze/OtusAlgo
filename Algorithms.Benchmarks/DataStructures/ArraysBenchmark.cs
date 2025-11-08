using Algorithms.DataStructures;
using BenchmarkDotNet.Attributes;

namespace Algorithms.Benchmarks.DataStructures {
    // ─────────────────────────────────────────────────────────────
    // СЦЕНАРИИ ДОБАВЛЕНИЯ
    // ─────────────────────────────────────────────────────────────
    [MemoryDiagnoser]
    public class DynamicArrayAddBenchmarks {
        [Params(100_000, 1_000_000)] public int N;

        // 1) Рост с маленькой начальной ёмкости (тестируем EnsureCapacity + копирования)
        [Benchmark(Description = "Add: grows from small capacity")]
        public int Add_WithResize() {
            var arr = new DynamicArray<int>();
            for (var i = 0; i < N; i++) arr.Add(i);
            // Возвращаем значение, чтобы JIT не выкинул цикл
            return arr[N - 1];
        }

        // 2) Без роста (чистая стоимость записи и проверок границ)
        [Benchmark(Baseline = true, Description = "Add: no resize (preallocated)")]
        public int Add_NoResize() {
            var arr = new DynamicArray<int>(N);
            for (var i = 0; i < N; i++) arr.Add(i);
            return arr[N - 1];
        }

        // 3) Переиспользуем буфер между итерациями без shrink (дешёвый Clear)
        private DynamicArray<int> _reused = null!;

        [GlobalSetup(Target = nameof(Add_ReusedBuffer_ClearEachIteration))]
        public void GS_Reused() => _reused = new DynamicArray<int>(N);

        [IterationSetup(Target = nameof(Add_ReusedBuffer_ClearEachIteration))]
        public void IS_Reused() => _reused.Clear();

        [Benchmark(Description = "Add: reused buffer + Clear per-iteration")]
        public int Add_ReusedBuffer_ClearEachIteration() {
            for (var i = 0; i < N; i++) _reused.Add(i);
            return _reused[N - 1];
        }
    }

    // ─────────────────────────────────────────────────────────────
    // ЧТЕНИЕ/УДАЛЕНИЕ (отдельный класс с другими Params)
    // ─────────────────────────────────────────────────────────────
    [MemoryDiagnoser]
    public class DynamicArrayOtherBenchmarks {
        // Более скромные размеры, чтобы Remove-циклы не были O(N^2) слишком долго
        [Params(10_000, 50_000)] public int N;

        private DynamicArray<int> _forReads = null!;
        private DynamicArray<int> _forRemoveStart = null!;
        private DynamicArray<int> _forRemoveEnd = null!;

        [GlobalSetup]
        public void GlobalSetup() {
            // Общий заполненный массив для чтений
            _forReads = new DynamicArray<int>(N);
            for (var i = 0; i < N; i++) _forReads.Add(i);

            // Отдельные экземпляры под remove-сценарии
            _forRemoveStart = new DynamicArray<int>(N);
            _forRemoveEnd = new DynamicArray<int>(N);
        }

        [IterationSetup(Targets = [nameof(Remove_AtStart_All), nameof(Remove_AtEnd_All)])]
        public void IterationSetup_Removes() {
            _forRemoveStart.Clear();
            _forRemoveEnd.Clear();
            for (var i = 0; i < N; i++) {
                _forRemoveStart.Add(i);
                _forRemoveEnd.Add(i);
            }
        }

        // 1) Последовательное чтение через индексатор
        [Benchmark(Description = "Indexer: sequential read")]
        public long Indexer_ReadSequential() {
            long sum = 0;
            for (var i = 0; i < N; i++) sum += _forReads[i];
            return sum; // защищаемся от Dead Code Elimination
        }

        // 2) Удаляем с начала до опустошения (худший случай по сдвигам)
        [Benchmark(Description = "Remove: at start, remove all")]
        public int Remove_AtStart_All() {
            // Возвращаем сумму, чтобы не выкинули код
            var checksum = 0;
            while (_forRemoveStart.Count > 0) {
                // Берём текущий первый элемент (до сдвига) и удаляем index 0
                checksum ^= _forRemoveStart[0];
                _forRemoveStart.Remove(0);
            }

            return checksum;
        }

        // 3) Удаляем с конца до опустошения (почти O(1) на шаг)
        [Benchmark(Description = "Remove: at end, remove all")]
        public int Remove_AtEnd_All() {
            var checksum = 0;
            while (_forRemoveEnd.Count > 0) {
                var idx = _forRemoveEnd.Count - 1;
                checksum ^= _forRemoveEnd[idx];
                _forRemoveEnd.Remove(idx);
            }

            return checksum;
        }
    }
}