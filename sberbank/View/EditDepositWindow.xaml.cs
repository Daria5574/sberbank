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
    /// Логика взаимодействия для EditDepositWindow.xaml
    /// </summary>
    public partial class EditDepositWindow : Window
    {
        public EditDepositWindow()
        {
            InitializeComponent();
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
        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, 0) || e.Text == "." || e.Text == ",")
            {
                if ((e.Text == "." || e.Text == ",") && ((sender as TextBox).Text.Contains(".") || (sender as TextBox).Text.Contains(",")))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void currencyTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (currencyTextBox.Text.Length >= 3)
            {
                e.Handled = true;
            }

            else if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[A-Z]$"))
            {
                e.Handled = true;
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DepositWindow depositWindow = new DepositWindow();
            depositWindow.Show();
            Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вклад успешно изменен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            DepositWindow depositWindow = new DepositWindow();
            depositWindow.Show();
            Close();
        }
    }
}
