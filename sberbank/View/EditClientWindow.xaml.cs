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
            lnameTextBox.Text = c.Lname;
            snameTextBox.Text = c.Sname;
            phoneTextBox.Text = c.Phone;
            emailTextBox.Text = c.Email;
            countryTextBox.Text = c.Country;
            cityTextBox.Text = c.City;
            streetTextBox.Text = c.Street;
            houseTextBox.Text = c.House;
            roomTextBox.Text = c.Room;
            passportTextBox.Text = c.Passport;
            dobTextBox.Text = c.DateOfBirthday.Value.ToString("dd.MM.yyyy");
            loginTextBox.Text = c.Login;
            passwordTextBox.Text = c.Password;

            if (c.IsActive == 1)
            {
                statusCheckBox.IsChecked = true;
            }
            else
            {
                statusCheckBox.IsChecked = false;
            }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !("01".Contains(e.Text));
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
            c.Fname = fnameTextBox.Text.Trim();
            c.Lname = lnameTextBox.Text.Trim();
            c.Sname = snameTextBox.Text.Trim();
            c.Phone = phoneTextBox.Text.Trim();
            c.Email = emailTextBox.Text.Trim();
            c.Country = countryTextBox.Text.Trim();
            c.City = cityTextBox.Text.Trim();
            c.Street = streetTextBox.Text.Trim();
            c.House = houseTextBox.Text.Trim();
            c.Room = roomTextBox.Text.Trim();
            c.Passport = passportTextBox.Text.Trim();
            c.DateOfBirthday = DateOnly.Parse(dobTextBox.Text.Trim());
            c.Login = loginTextBox.Text.Trim();
            c.Password = passwordTextBox.Text.Trim();
            if (statusCheckBox.IsChecked == true)
            {
                c.IsActive = 1;
            }
            else
            {
                c.IsActive = 0;
            }
            db.Clients.Update(c);
            db.SaveChanges();


            MessageBox.Show("Клиент успешно изменен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            MyProfileWindow clientsWindow = new MyProfileWindow(c);
            clientsWindow.Show();
            Close();
        }
    }
}
