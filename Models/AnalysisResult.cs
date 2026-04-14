using GraphOptimizer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphOptimizer.Models
{
    public record AnalysisResult (
        AnalysisMode? Mode,
        List<uint> VertexCoverIds,
        int VertexCount,
        double ExecutionTimeMs,
        long OperationCount
    )
    {
        public string FormattedTime => FormatDuration(ExecutionTimeMs);

        private static string FormatDuration(double ms)
        {
            if (ms < 0.001) return $"{ms * 1_000_000:N2} нс";
            if (ms < 1) return $"{ms * 1_000:N2} мкс";

            TimeSpan t = TimeSpan.FromMilliseconds(ms);

            if (t.TotalSeconds < 60)
            {
                return $"{ms:N2} мс";
            }

            if (t.TotalMinutes < 60)
            {
                return $"{(int)t.TotalMinutes} мин {t.Seconds} сек";
            }

            return $"{(int)t.TotalHours} ч {t.Minutes} мин";
        }
    }
}
