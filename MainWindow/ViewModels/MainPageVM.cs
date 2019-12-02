using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using MainWindow.Annotations;

namespace MainWindow.ViewModels
{
    class MainPageVM : INotifyPropertyChanged
    {
        private bool _isPaneOpen = false;


        public RelayCommand DoTogglePane { get; set; }

        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set
            {
                _isPaneOpen = value;
                OnPropertyChanged();
            }
        }

        public MainPageVM()
        {
            DoTogglePane = new RelayCommand(TogglePane);
        }

        private void TogglePane()
        {
            IsPaneOpen = !IsPaneOpen;// == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
