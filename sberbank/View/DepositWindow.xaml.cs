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
    /// Логика взаимодействия для DepositWindow.xaml
    /// </summary>
    public partial class DepositWindow : Window
    {
        SberContext db = new SberContext();
        List<Deposit> deposits;
        List<Deposit> archiveDeposits;
        public DepositWindow()
        {
            InitializeComponent();
            
            deposits = db.Deposits.ToList();
            archiveDeposits = db.Deposits.ToList();

            lvDeposit.ItemsSource = deposits;
            lvDepositArchive.ItemsSource = archiveDeposits;
            UpdateDeposit();
        }

        public void UpdateDeposit()
        {
            var listViewData = from deposit in db.Deposits
                               where deposit.IsActivity == 1
                               select new
                               {
                                   DepositName = deposit.Name + " в " + deposit.Currency,
                                   Id = deposit.IdDeposit
                               };

            lvDeposit.ItemsSource = listViewData.ToList();

            var listViewData1 = from deposit in db.Deposits
                               where deposit.IsActivity == 0
                               select new
                               {
                                   DepositName = deposit.Name + " в " + deposit.Currency,
                                   Id = deposit.IdDeposit
                               };

            lvDepositArchive.ItemsSource = listViewData1.ToList();

        }

        private void DepositCategoresButton_Click(object sender, RoutedEventArgs e)
        {
            DepositWindow depositWindow = new DepositWindow();
            depositWindow.Show();
            Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow mClW = new ClientsWindow();
            mClW.Show();
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddDepositWindow depositWindow = new AddDepositWindow();
            depositWindow.Show();
            Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditDepositWindow depositWindow = new EditDepositWindow();
            depositWindow.Show();
            Close();
        }
        public void sberImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
            Close();
        }

        private void ArchiveButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is System.Windows.Controls.ListViewItem listViewItem)
            {
                Deposit currentDeposit = null;
                using (SberContext db = new SberContext())
                {

                    var selectedItem = listViewItem.Content as dynamic;
                    int id = selectedItem.Id;
                    currentDeposit = db.Deposits.FirstOrDefault(d => d.IdDeposit == id);
                    DepositAddDetailsWindow wAddDetails = new DepositAddDetailsWindow(currentDeposit);
                    wAddDetails.Show();
                    Close();
                    //        string pass = selectedItem.Passport;

                    //        currentClient = db.Clients.FirstOrDefault(b => b.Passport == pass);

                    //        MainClientWindow wMainClient = new MainClientWindow(currentClient);
                    //        wMainClient.Show();
                    //        Close();
                }
            }
        }
    }
}
