using System.Runtime.CompilerServices;

namespace GasNetwork.Extensions
{
    public static class StringExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this string? a, string? b)
        {
            return string.CompareOrdinal(a, b) == 0;
        }

        public static string ConvetToIconPath(this string str)
        {
            string fullPath = "C:\\Workflow\\GasNetwork\\GasNetwork\\Assets\\img\\Warning\\";

            return $"{fullPath}{str}.png";
        }
    }
}
