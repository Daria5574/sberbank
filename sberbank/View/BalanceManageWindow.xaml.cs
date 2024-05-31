using Microsoft.EntityFrameworkCore;
using sberbank.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace sberbank.View
{
    /// <summary>
    /// Логика взаимодействия для BalanceManageWindow.xaml
    /// </summary>
    public partial class BalanceManageWindow : Window
    {
        private SberContext db = new SberContext();
        Deposit currentDeposit;
        Deposit currentDeposit1;

        Client currentClient;
        Client currentClient1;


        Account currentAccount;
        Account currentAccount1;
        public BalanceManageWindow(Account currentAccount)
        {
            InitializeComponent();
            //cA = currentAccount;
            currentDeposit = db.Deposits.FirstOrDefault(d => d.IdDeposit == currentAccount.IdDeposit);
            currentClient = db.Clients.FirstOrDefault(c => c.IdClient == currentAccount.IdClient);
            //db.Accounts.Attach(currentAccount);
            currentAccount1 = currentAccount;
            currentAccount = currentAccount1;
            nameLabel.Content = currentDeposit.Name;
            nameLabel1.Content = currentDeposit.Name;
            balanceLabel.Content = currentAccount.Balance.ToString("F2") + " " + currentDeposit.Currency;
            balanceLabel1.Content = currentAccount.Balance.ToString("F2") + " " + currentDeposit.Currency;

            currentDeposit1 = currentDeposit;
            currentClient1 = currentClient;
            if (currentDeposit.PossibilityOfRemoval == 0)
                minusBalanseGrid.Visibility = Visibility.Collapsed;

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
                MainClientWindow wMain = new MainClientWindow(currentClient);
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
            currentAccount = currentAccount1;
            try
            {
                decimal plus = decimal.Parse(plusTextBox.Text);

                currentAccount.Balance += plus;
                db.Entry(currentAccount).State = EntityState.Modified; 
                db.SaveChanges();

                Operation operation = new Operation
                {
                    IdAccount = currentAccount.IdAccount,
                    IdClient = currentAccount.IdClient,
                    Type = "Пополнение баланса",
                    Date = DateTime.Now,
                    Sum = plus,
                    Description = "Пополнение баланса через приложение."
                };
                db.Operations.Add(operation);
                db.SaveChanges();

                MessageBox.Show("Пополнение прошло успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                DepositDetailsWindow depositDetailsWindow = new DepositDetailsWindow(currentDeposit, currentAccount);
                depositDetailsWindow.Show();
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите сумму.");
            }
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            currentClient = currentClient1;
            currentDeposit = currentDeposit1;
            currentAccount = currentAccount1;

            decimal minus = decimal.Parse(minusTextBox.Text);
            if (currentAccount.Balance >= minus)
            {
                currentAccount.Balance -= minus;
                db.Entry(currentAccount).State = EntityState.Modified; 
                db.SaveChanges();

                Operation operation = new Operation
                {
                    IdAccount = currentAccount.IdAccount,
                    IdClient = currentAccount.IdClient,
                    Type = "Снятие средств",
                    Date = DateTime.Now,
                    Sum = minus,
                    Description = "Снятие средств через приложение."
                };

                db.Operations.Add(operation);
                db.SaveChanges();

                MessageBox.Show("Снятие средств прошло успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                DepositDetailsWindow depositDetailsWindow = new DepositDetailsWindow(currentDeposit, currentAccount);
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
            if (App.UserRole == 2)
            {
                ClientsWindow mClW = new ClientsWindow();
                mClW.Show();
                Close();
            }
            if (App.UserRole == 1)
            {
                MainClientWindow wMain = new MainClientWindow(currentClient);
                wMain.Show();
                Close();
            }
        }
    }
}
