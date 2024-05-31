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
    /// Логика взаимодействия для ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : System.Windows.Window
    {
        SberContext db = new SberContext();

        List<Client> allClients;
        List<Client> filteredClients;
        List<Client> clients;
        Client currentClient;
        public ClientsWindow()
        {
            InitializeComponent();

            allClients = db.Clients.ToList();
            clients = db.Clients.ToList();

            lvClients.ItemsSource = clients;

            filteredClients = new List<Client>(allClients);
            UpdateClients();

        }
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string filterText = searchTextBox.Text.Trim();

            filteredClients = allClients.Where(c =>
     c.Lname.Trim().ToLower().Contains(filterText.Trim().ToLower()) ||
     c.Fname.Trim().ToLower().Contains(filterText.Trim().ToLower()) ||
     (c.Sname != null && c.Sname.Trim().ToLower().Contains(filterText.Trim().ToLower()))
 ).ToList();

            UpdateClients();
        }
        private void SearchTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchButton_Click(sender, e);
            }
        }
        private void UpdateClients()
        {
            var listViewData = from Client in filteredClients
                               select new
                               {
                                   Name = Client.Lname + " " + Client.Fname + " " + Client.Sname,
                                   DOB = Client.DateOfBirthday,
                                   StatusAct = Client.IsActive,
                                   Adress = Client.Country + ", г. " + Client.City + ", " + Client.Street + ", д. " + Client.House + ", кв. " + Client.Room,
                                   Passport = Client.Passport
                               };

            lvClients.ItemsSource = listViewData.ToList();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is System.Windows.Controls.ListViewItem listViewItem)
            {
                Client currentClient = null;
                using (SberContext db = new SberContext())
                {
                    var selectedItem = listViewItem.Content as dynamic;
                    string pass = selectedItem.Passport;

                    currentClient = db.Clients.FirstOrDefault(b => b.Passport == pass);

                    MainClientWindow wMainClient = new MainClientWindow(currentClient);
                    wMainClient.Show();
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

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClWin = new AddClientWindow();
            addClWin.Show();
            Close();
        }

        
        public void sberImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            filteredClients = new List<Client>(allClients); 
            UpdateClients();
        }

    }
}
