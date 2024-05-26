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
    /// Логика взаимодействия для BalanceManageWindow.xaml
    /// </summary>
    public partial class BalanceManageWindow : Window
    {
        SberContext db = new SberContext();
        Deposit currentDeposit;
        Deposit currentDeposit1;

        Client currentClient;
        Client currentClient1;


        Account cA;
        public BalanceManageWindow(Account currentAccount)
        {
            InitializeComponent();
            cA = currentAccount;
            currentDeposit = db.Deposits.FirstOrDefault(d => d.IdDeposit == cA.IdDeposit);
            currentClient = db.Clients.FirstOrDefault(c => c.IdClient == cA.IdClient);

            nameLabel.Content = currentDeposit.Name;
            nameLabel1.Content = currentDeposit.Name;
            balanceLabel.Content = cA.Balance.ToString("F2") + " " + currentDeposit.Currency;
            balanceLabel1.Content = cA.Balance.ToString("F2") + " " + currentDeposit.Currency;

            currentDeposit1 = currentDeposit;
            currentClient1 = currentClient;

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

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            currentClient = currentClient1;
            currentDeposit = currentDeposit1;

            decimal plus = decimal.Parse(plusTextBox.Text);

            cA.Balance += plus;
            db.Accounts.Update(cA);
            db.SaveChanges();

            Operation operation = new Operation
            {
                IdAccount = cA.IdAccount,
                IdClient = cA.IdClient,
                Type = "Пополнение баланса",
                Date = DateTime.Now,
                Sum = plus,
                Description = "Пополнение баланса через приложение."
            };
            db.Operations.Add(operation);
            db.SaveChanges();

            MessageBox.Show("Пополнение прошло успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            DepositDetailsWindow depositDetailsWindow = new DepositDetailsWindow(currentDeposit);
            depositDetailsWindow.Show();
            Close();
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            currentClient = currentClient1;
            currentDeposit = currentDeposit1;

            decimal minus = decimal.Parse(minusTextBox.Text);
            if (cA.Balance >= minus)
            {
                cA.Balance -= minus;
                db.Accounts.Update(cA);
                db.SaveChanges();

                Operation operation = new Operation
                {
                    IdAccount = cA.IdAccount,
                    IdClient = cA.IdClient,
                    Type = "Снятие средств",
                    Date = DateTime.Now,
                    Sum = minus,
                    Description = "Снятие средств через приложение."
                };

                db.Operations.Add(operation);
                db.SaveChanges();

                MessageBox.Show("Снятие средств прошло успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                DepositDetailsWindow depositDetailsWindow = new DepositDetailsWindow(currentDeposit);
                depositDetailsWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Недостаточно средств для снятия.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void sberImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
            Close();
        }
    }
}
