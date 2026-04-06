using GraphOptimizer.Interfaces;
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

        public IAppState AppState { get; init; }

        private bool _isAlgorithmListExpanded = false;
        public bool IsAlgorithmListExpanded 
        {
            get => _isAlgorithmListExpanded;
            set => SetProperty(ref _isAlgorithmListExpanded, value);
        }

        //public enum AnalysisMode { Greedy, Approx, Backtracking, ComparisonAll }

        // В HeaderViewModel
        //public event Action<AnalysisMode>? AnalysisRequested;

        public event Action? StartAnalysisRequested;
        public event Action? StopAnalysisRequested;

        public HeaderViewModel(GraphViewModel graphVM, IAppState appState)
        {
            GraphVM = graphVM;
            AppState = appState;
        }

        public void HandleAlgorithmListExpandButtonClick()
        {
            IsAlgorithmListExpanded = !IsAlgorithmListExpanded;
        }

        public void HandleActionButtonClick()
        {
            if (AppState.IsAnalysisActive)
            {
                StopAnalysisRequested?.Invoke();
            } else
            {
                StartAnalysisRequested?.Invoke();
            }
        }

        //public void HandleStartButtonClick()
        //{
        //    StartAnalysisRequested?.Invoke();
        //}

        //public void HandleStopButtonClick()
        //{
        //    StopAnalysisRequested?.Invoke();
        //}
    }
}
