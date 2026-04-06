using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphOptimizer.Interfaces
{
    public interface IAppState
    {
        bool IsAnalysisActive { get; set; }
    }
}
