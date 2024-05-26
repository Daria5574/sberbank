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
    /// Логика взаимодействия для EditClientWindow.xaml
    /// </summary>
    public partial class EditClientWindow : Window
    {
        SberContext db = new SberContext();
        private Client c;
        public EditClientWindow(Client currentClient)
        {
            InitializeComponent();
            c = currentClient;
            fnameTextBox.Text = c.Fname;
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
        public void sberImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
            Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Клиент успешно изменен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
            Close();
        }
    }
}
