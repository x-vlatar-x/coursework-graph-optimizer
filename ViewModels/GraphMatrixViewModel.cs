using GraphOptimizer.ViewModels.GraphCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphOptimizer.ViewModels
{
    public class GraphMatrixViewModel: ViewModelBase
    {
        private readonly GraphViewModel _graph;
        public GraphMatrixViewModel(GraphViewModel graph) 
        {
            _graph = graph;
        }
    }
}
