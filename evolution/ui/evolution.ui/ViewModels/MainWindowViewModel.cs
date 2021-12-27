using Prism.Mvvm;

namespace evolution.ui.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Life - WPF Presenter";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
