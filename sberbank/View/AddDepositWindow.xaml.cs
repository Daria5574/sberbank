using sberbank.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для AddDepositWindow.xaml
    /// </summary>
    public partial class AddDepositWindow : Window
    {
        SberContext db;
        public AddDepositWindow()
        {
            InitializeComponent();
            db = new SberContext();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (App.UserRole == 2)
            {
                ClientsWindow mClW = new ClientsWindow();
                mClW.Show();
                Close();
            }
        }
        private void DepositCategoresButton_Click(object sender, RoutedEventArgs e)
        {
            DepositWindow depositWindow = new DepositWindow();
            depositWindow.Show();
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
        public void sberImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (App.UserRole == 2)
            {
                ClientsWindow mClW = new ClientsWindow();
                mClW.Show();
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DepositWindow depositWindow = new DepositWindow();
            depositWindow.Show();
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text.Trim();
            string currency = currencyTextBox.Text.Trim();
            decimal minimumDeposit = decimal.Parse(minimumDepositTextBox.Text.Trim(), CultureInfo.InvariantCulture);
            int depositTerm = int.Parse(depositTermTextBox.Text.Trim());
            decimal interestRate = decimal.Parse(interestRateTextBox.Text.Trim(), CultureInfo.InvariantCulture);


            int interestPeriod = int.Parse(interestPeriodTextBox.Text.Trim());

            int possibilityOfRemoval;

            if (possibilityOfRemovalCheckBox.IsChecked == true)
            {
                possibilityOfRemoval = 1;
            }
            else
            {
                possibilityOfRemoval = 0;
            }

            Deposit deposit = new Deposit
            {
                Name = name,
                Currency = currency,
                MinimumDeposit = minimumDeposit,
                DepositTerm = depositTerm,
                InterestRate = interestRate,
                InterestPeriod = interestPeriod,
                PossibilityOfRemoval = possibilityOfRemoval,
                IsActivity = 1
            };

            db.Deposits.Add(deposit);
            db.SaveChanges();

            MessageBox.Show("Вклад успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            DepositWindow depositWindow = new DepositWindow();
            depositWindow.Show();
            Close();
        }
    }
}
