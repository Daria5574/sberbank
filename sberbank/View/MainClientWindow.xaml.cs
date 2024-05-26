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
    /// Логика взаимодействия для MainClientWindow.xaml
    /// </summary>
    public partial class MainClientWindow : Window
    {

        SberContext db = new SberContext();
        Client currentClient = new Client();
        Client client1 = new Client();
        public MainClientWindow(Client currentClient)
        {
            InitializeComponent();

            var listViewData = from client in db.Clients
                               join account in db.Accounts on client.IdClient equals account.IdClient
                               join deposit in db.Deposits on account.IdDeposit equals deposit.IdDeposit
                               where client.IdClient == currentClient.IdClient
                               select new
                               {
                                   name = deposit.Name,
                                   balance = account.Balance + " " + deposit.Currency,
                               };

            depositListView.ItemsSource = listViewData.ToList();

            client1 = currentClient;
        }
        public void myProfileImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            currentClient = client1;
            MyProfileWindow myProfWind = new MyProfileWindow(currentClient);
            myProfWind.Show();
            Close();
        }

        public void sberImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
            Close();
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
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem listViewItem)
            {

                Deposit currentDeposit = new Deposit();

                var selectedItem = listViewItem.Content as dynamic;
                string name = selectedItem.name + " " + selectedItem.balance;

                currentDeposit = db.Deposits
     .Join(db.Accounts,
         deposit => deposit.IdDeposit,
         account => account.IdDeposit,
         (deposit, account) => new { deposit, account })
     .Where(x => x.deposit.Name + " " + x.account.Balance + " " + x.deposit.Currency == name)
     .Select(x => x.deposit)
     .FirstOrDefault();

                DepositDetailsWindow DepositDetailsW = new DepositDetailsWindow(currentDeposit);
                DepositDetailsW.Show();
                Close();
            }
        }

        private void NewDepositButton_Click(object sender, RoutedEventArgs e)
        {

            DepositWindow depositWindow = new DepositWindow();
            depositWindow.Show();
            Close();
        }
    }
}
