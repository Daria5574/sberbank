using sberbank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace sberbank.View
{
    /// <summary>
    /// Логика взаимодействия для DepositDetailsWindow.xaml
    /// </summary>
    public partial class DepositDetailsWindow : Window
    {
        Account currentAccount;
        Account currentAccount1;
        Client currentClient;
        Client currentClient1;

        SberContext db = new SberContext();
        public DepositDetailsWindow(Deposit currentDeposit)
        {
            InitializeComponent();

            currentAccount = db.Accounts.FirstOrDefault(a => a.IdDeposit == currentDeposit.IdDeposit);
            currentAccount1 = currentAccount;

            currentClient = db.Clients.FirstOrDefault(c => c.IdClient == currentAccount.IdClient);
            currentClient1 = currentClient;

            nameLabel.Content = currentDeposit.Name;
            balanceLabel.Content = currentAccount.Balance.ToString("F2") + " " + currentDeposit.Currency;
            procentLabel.Content = "Ставка " + currentDeposit.InterestRate.ToString("F1") + "% годовых";

            UpdateOperations();
        }
        public void UpdateOperations()
        {
            currentAccount = currentAccount1;
            var listViewData = from operation in db.Operations
                               where operation.IdAccount == currentAccount.IdAccount
                               orderby operation.Date descending
                               select new
                               {
                                   Typee = operation.Type,
                                   Summa = operation.Sum,
                                   Data = operation.Date
                               };

            lvOperations.ItemsSource = listViewData.ToList();
        }
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is System.Windows.Controls.ListViewItem listViewItem)
            {
                Operation currentOperation = null;
                using (SberContext db = new SberContext())
                {
                    var selectedItem = listViewItem.Content as dynamic;
                    DateTime data = selectedItem.Data;

                    currentOperation = db.Operations.FirstOrDefault(b => b.Date == data);

                    OperationDetailsWindow wOperationDetails = new OperationDetailsWindow(currentOperation);
                    wOperationDetails.Show();
                    Close();
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow mClW = new ClientsWindow();
            mClW.Show();
            Close();
        }
        private void DepositCategoresButton_Click(object sender, RoutedEventArgs e)
        {
            DepositWindow depositWindow = new DepositWindow();
            depositWindow.Show();
            Close();
        }

        private void MoneyButton_Click(object sender, RoutedEventArgs e)
        {
            currentAccount = currentAccount1;
            BalanceManageWindow balanceManageWindow = new BalanceManageWindow(currentAccount);
            balanceManageWindow.Show();
            Close();
        }
        public void sberImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBoxResult result = MessageBox.Show("Вы действительно хотите закрыть данный счет?", "Подтверждение закрытия счета", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if (result == MessageBoxResult.Yes)
            //{
            //    MainClientWindow mainClientWindow = new MainClientWindow();
            //    mainClientWindow.Show();
            //    Close();
            //}
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            currentClient1 = currentClient;
            MainClientWindow wMainClient = new MainClientWindow(currentClient);
            wMainClient.Show();
            Close();
        }
    }
}
