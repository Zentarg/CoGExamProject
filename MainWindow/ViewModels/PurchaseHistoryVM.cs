using MainWindow.Annotations;
using MainWindow.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow.ViewModels
{
    class PurchaseHistoryVM : INotifyPropertyChanged
    {
        private AccountPurchase _selectedPurchase;

        public PurchaseHistoryVM()
        { 
            _selectedPurchase = null;
        }

        public AccountPurchase SelectedPurchase { get { return _selectedPurchase; } set { _selectedPurchase = value; OnPropertyChanged(); } }

        public ObservableCollection<AccountPurchase> PurchaseHistory { get { return AccountHandler.AccountDetail.PurchaseHistory; } }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
