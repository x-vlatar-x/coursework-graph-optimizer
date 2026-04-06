using GraphOptimizer.ViewModels.GraphCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GraphOptimizer.ViewModels
{
    public class HeaderViewModel: ViewModelBase
    {
        public GraphViewModel GraphVM { get; init; }

        private bool _isAlgorithmListExpanded = false;
        public bool IsAlgorithmListExpanded 
        {
            get => _isAlgorithmListExpanded;
            set => SetProperty(ref _isAlgorithmListExpanded, value);
        }

        public HeaderViewModel(GraphViewModel graphVM)
        {
            GraphVM = graphVM;
        }

        public void HandleAlgorithmListExpandButtonClick()
        {
            IsAlgorithmListExpanded = !IsAlgorithmListExpanded;
        }
    }
}
