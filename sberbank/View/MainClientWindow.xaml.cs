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
            client1 = currentClient;
            if (App.UserRole == 1)
            {
                archiveGrid.Visibility = Visibility.Collapsed;
            }
            if (App.UserRole == 2) 
            {
                openButtonGrid.Visibility = Visibility.Collapsed;
            }
            
            var listViewData = from client in db.Clients
                               join account in db.Accounts on client.IdClient equals account.IdClient
                               join deposit in db.Deposits on account.IdDeposit equals deposit.IdDeposit
                               where client.IdClient == currentClient.IdClient && account.Status == 1 && deposit.IsActivity == 1
                               select new
                               {
                                   name = deposit.Name,
                                   balance = account.Balance + " " + deposit.Currency,
                                   idAccount = account.IdAccount,
                                   idDeposit = deposit.IdDeposit,
                               };

            depositListView.ItemsSource = listViewData.ToList();

            client1 = currentClient;

            var listViewData1 = from client in db.Clients
                                join account in db.Accounts on client.IdClient equals account.IdClient
                                join deposit in db.Deposits on account.IdDeposit equals deposit.IdDeposit
                                where client.IdClient == currentClient.IdClient && (deposit.IsActivity == 0 || account.Status == 0)
                                select new
                                {
                                    name = deposit.Name,
                                    balance = account.Balance + " " + deposit.Currency,
                                    idAccount = account.IdAccount,
                                    idDeposit = deposit.IdDeposit,
                                };

            depositListViewArchive.ItemsSource = listViewData1.ToList();
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
            if (App.UserRole == 2)
            {
                ClientsWindow mClW = new ClientsWindow();
                mClW.Show();
                Close();
            }
            if (App.UserRole == 1)
            {
                MainClientWindow wMain = new MainClientWindow(App.currentClient);
                wMain.Show();
                Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (App.UserRole == 2)
            {
                ClientsWindow mClW = new ClientsWindow();
                mClW.Show();
                Close();
            }
            if (App.UserRole == 1)
            {
                MainClientWindow wMain = new MainClientWindow(App.currentClient);
                wMain.Show();
                Close();
            }
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
                Account currentAccount = new Account();
                Deposit currentDeposit = new Deposit();

                var selectedItem = listViewItem.Content as dynamic;
                string name = selectedItem.name + " " + selectedItem.balance;

                int idAcc = selectedItem.idAccount;
                int idDep = selectedItem.idDeposit;

                currentAccount = db.Accounts.FirstOrDefault(x => x.IdAccount == idAcc);
                currentDeposit = db.Deposits.FirstOrDefault(d => d.IdDeposit == idDep);

                DepositDetailsWindow DepositDetailsW = new DepositDetailsWindow(currentDeposit, currentAccount);
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
