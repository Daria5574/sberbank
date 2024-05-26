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
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        SberContext db;
        public AddClientWindow()
        {
            InitializeComponent();
            db = new SberContext();
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string fname = fnameTextBox.Text.Trim();
            string lname = lnameTextBox.Text.Trim();
            string sname = snameTextBox.Text.Trim();
            string phone = phoneTextBox.Text.Trim();
            string email = emailTextBox.Text.Trim();
            string country = countryTextBox.Text.Trim();
            string city = cityTextBox.Text.Trim();
            string street = streetTextBox.Text.Trim();
            string house = houseTextBox.Text.Trim();
            string room = roomTextBox.Text.Trim();
            string passport = passportTextBox.Text.Trim();
            DateOnly dob = DateOnly.Parse(dobTextBox.Text.Trim());
            string login = loginTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            Client Cl = new Client
            {
                Fname = fname,
                Lname = lname,
                Sname = sname,
                Phone = phone,
                Email = email,
                Country = country,
                City = city,
                Street = street,
                House = house,
                Room = room,
                Passport = passport,
                DateOfBirthday = dob,
                Login = login,
                Password = password,
            };
            db.Clients.Add(Cl);
            db.SaveChanges();


            MessageBox.Show("Клиент добавлен успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

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
    }
}
