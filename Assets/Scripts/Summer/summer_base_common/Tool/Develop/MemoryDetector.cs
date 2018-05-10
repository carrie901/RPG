
using System.Text;
using UnityEngine.Profiling;

namespace Summer
{
    public class MemoryDetector
    {

        private const string TOTAL_ALLOC_MEMROY_FORMATION = "Alloc Memory : {0}";
        private const string TOTAL_RESERVED_MEMORY_FORMATION = "Reserved Memory : {0}M";
        private const string TOTAL_UNUSED_RESERVED_MEMORY_FORMATION = "Unused Reserved: {0}M";
        private const string MONO_HEAP_FORMATION = "Mono Heap : {0}M";
        private const string MONO_USED_FORMATION = "Mono Used : {0}M";
        private const float BYTE_TO_M = 0.000001f;

        private StringBuilder sb = new StringBuilder();
        public string OnExcute()
        {
            sb.Remove(0, sb.Length);
            sb.AppendFormat(TOTAL_ALLOC_MEMROY_FORMATION, Profiler.GetTotalAllocatedMemoryLong() * BYTE_TO_M);
            sb.AppendLine();
            sb.AppendFormat(TOTAL_RESERVED_MEMORY_FORMATION, Profiler.GetTotalReservedMemoryLong() * BYTE_TO_M);
            sb.AppendLine();
            sb.AppendFormat(TOTAL_UNUSED_RESERVED_MEMORY_FORMATION, Profiler.GetTotalUnusedReservedMemoryLong() * BYTE_TO_M);
            sb.AppendLine();
            sb.AppendFormat(MONO_HEAP_FORMATION, Profiler.GetMonoHeapSizeLong() * BYTE_TO_M);
            sb.AppendLine();
            sb.AppendFormat(MONO_USED_FORMATION, Profiler.GetMonoUsedSizeLong() * BYTE_TO_M);
            sb.AppendLine();
            LogManager.Log(sb.ToString());
            return sb.ToString();
        }
    }
}

