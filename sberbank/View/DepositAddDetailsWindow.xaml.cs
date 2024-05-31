using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.Logging;
using sberbank.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace sberbank.View
{
    /// <summary>
    /// Логика взаимодействия для DepositAddDetailsWindow.xaml
    /// </summary>
    public partial class DepositAddDetailsWindow : Window
    {
        Deposit d = new Deposit();
        SberContext db = new SberContext();

        public DepositAddDetailsWindow(Deposit currentDeposit)
        {
            InitializeComponent();

            d = currentDeposit;

            if (App.UserRole == 1)
            {
                editButtonGrid.Visibility = Visibility.Collapsed;
            }

            if (d.IsActivity == 0 || App.UserRole == 2)
            {
                openAccountGrid.Visibility = Visibility.Collapsed;
            }

            nameLabel.Content = $"Вклад \"{d.Name}\" в {d.Currency}";
            minimumLabel.Content = "Минимальная сумма вклада: " + d.MinimumDeposit.ToString("F2") + " " + d.Currency;
            depositTermLabel.Content = "Срок вклада: " + d.DepositTerm + " месяцев";

            switch (d.InterestPeriod)
            {
                case 1:
                    depositPeriodLabel.Content = $"Период начисления процентов: раз в месяц";
                    break;
                case 4:
                    depositPeriodLabel.Content = "Период начисления процентов: каждый квартал (раз в 4 месяца)";
                    break;
                case 12:
                    depositPeriodLabel.Content = "Период начисления процентов: раз в год";
                    break;
            }

            switch (d.PossibilityOfRemoval)
            {
                case 1:
                    withdrawalLabel.Content = "Возможность частичного снятия: да";
                    break;
                case 0:
                    withdrawalLabel.Content = "Возможность частичного снятия: нет";
                    break;
            }
            procentLabel.Content = "Процентная ставка: " + d.InterestRate.ToString("F1") + "% годовых";

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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            DepositWindow depositWindow = new DepositWindow();
            depositWindow.Show();
            Close();
        }
        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, 0) || e.Text == ".")
            {
                if ((e.Text == ".") && ((sender as TextBox).Text.Contains(".") || (sender as TextBox).Text.Contains(",")))
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

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(sumTextBox.Text, out decimal sum))
            {
                if (sum > d.MinimumDeposit || sum == d.MinimumDeposit)
                {
                    Account account = new Account
                    {
                        IdClient = App.currentClient.IdClient,
                        IdDeposit = d.IdDeposit,
                        DepositAmount = sum,
                        OpeningDate = DateOnly.FromDateTime(DateTime.Now),
                        ClosingDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(d.DepositTerm)),
                        Balance = sum,
                        Status = 1,
                    };
                    db.Accounts.Add(account);
                    db.SaveChanges();

                    MessageBox.Show("Вклад открыт успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                    MainClientWindow wMyProfile = new MainClientWindow(App.currentClient);
                    wMyProfile.Show();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Неверный формат суммы. Введите числовое значение.");
            }

        }

        private void editDepositButton_Click(object sender, RoutedEventArgs e)
        {
            EditDepositWindow wEdit = new EditDepositWindow(d);
            wEdit.Show();
            Close();
        }
    }
}
