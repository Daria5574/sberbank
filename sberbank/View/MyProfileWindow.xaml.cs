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
    /// Логика взаимодействия для MyProfileWindow.xaml
    /// </summary>
    public partial class MyProfileWindow : Window
    {
        SberContext db = new SberContext();
        Client c;
        public MyProfileWindow(Client currentClient)
        {
            InitializeComponent();

            if (App.UserRole == 1)
            {
                editButton.Visibility = Visibility.Collapsed;
            }

            c = currentClient;
            fioLabel.Content = "ФИО: " + c.Lname + " " + c.Fname + " " + c.Sname;
            dobLabel.Content = "Дата рождения: " + c.DateOfBirthday;
            phoneLabel.Content = "Номер телефона: " + c.Phone;
            emailLabel.Content = "Email: " + c.Email;
            adressLabel.Content = "Адрес проживания: " + c.Country + ", г. " + c.City + ", " + c.Street + ", д. " + c.House + ", кв. " + c.Room;
            passportLabel.Content = "Паспортные данные: " + c.Passport;
        }
        private void DepositCategoresButton_Click(object sender, RoutedEventArgs e)
        {
            DepositWindow depositWindow = new DepositWindow();
            depositWindow.Show();
            Close();
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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainClientWindow wMainClient = new MainClientWindow(c);
            wMainClient.Show();
            Close();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            EditClientWindow wEditClient = new EditClientWindow(c);
            wEditClient.Show();
            Close();
        }
    }
}
