using GraphOptimizer.Interfaces;
using GraphOptimizer.Enums;
using GraphOptimizer.ViewModels.GraphCore;
using System;

namespace GraphOptimizer.ViewModels
{
    //public enum AnalysisMode { Greedy, Approx, Backtracking, ComparisonAll }
    public class HeaderViewModel: ViewModelBase
    {
        public GraphViewModel GraphVM { get; init; }

        public IAppState AppState { get; init; }

        private bool _isAnalysisModeListExpanded = false;
        public bool IsAnalysisModeListExpanded
        {
            get => _isAnalysisModeListExpanded;
            set => SetProperty(ref _isAnalysisModeListExpanded, value);
        }

        private AnalysisMode? _selectedAnalysisMode = null; 
        public AnalysisMode? SelectedAnalysisMode
        {
            get => _selectedAnalysisMode;
            set => SetProperty(ref _selectedAnalysisMode, value);
        }

        //public enum AnalysisMode { Greedy, Approx, Backtracking, ComparisonAll }

        // В HeaderViewModel
        //public event Action<AnalysisMode>? AnalysisRequested;

        //public event Action? StartAnalysisRequested;
        //public event Action? StopAnalysisRequested;
        public event Action<AnalysisMode>? StartAnalysisRequested;
        public event Action StopAnalysisRequested;

        public HeaderViewModel(GraphViewModel graphVM, IAppState appState)
        {
            GraphVM = graphVM;
            AppState = appState;
        }

        public void HandleAnalysisModeListExpandButtonClick()
        {
            IsAnalysisModeListExpanded = !IsAnalysisModeListExpanded;
        }

        public void HandleActionButtonClick()
        {
            if (SelectedAnalysisMode == null)
            {
                return;
            }

            if (AppState.IsAnalysisActive)
            {
                StopAnalysisRequested?.Invoke();
            }
            else
            {
                StartAnalysisRequested?.Invoke(SelectedAnalysisMode.Value);
            }
        }

        public void HandleAnalysisModeItemClick(AnalysisMode mode)
        {
            IsAnalysisModeListExpanded = false;
            SelectedAnalysisMode = mode;
        }
    }
}
