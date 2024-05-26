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
    /// Логика взаимодействия для OperationDetailsWindow.xaml
    /// </summary>
    public partial class OperationDetailsWindow : Window
    {
        SberContext db = new SberContext();

        Account currentAccount;
        Account currentAccount1;
        Deposit currentDeposit;
        Deposit currentDeposit1;

        Operation o;
        public OperationDetailsWindow(Operation currentOperation)
        {
            InitializeComponent();
            o = currentOperation;

            currentAccount = db.Accounts.FirstOrDefault(a => a.IdAccount == o.IdAccount);
            currentAccount1 = currentAccount;

            currentDeposit = db.Deposits.FirstOrDefault(d => d.IdDeposit == currentAccount.IdDeposit);
            currentDeposit1 = currentDeposit;



            switch (currentOperation.Type)
            {
                case "Пополнение баланса":
                case "Зачисление процентов":
                    balanceLabel.Content = $"+{currentOperation.Sum.ToString("F2") + " " + currentDeposit.Currency}";
                    balanceLabel.Foreground = Brushes.Green;
                    break;
                case "Снятие средств":
                    balanceLabel.Content = $"-{currentOperation.Sum.ToString("F2") + " " + currentDeposit.Currency}";
                    break;
                default:
                    balanceLabel.Content = currentOperation.Sum.ToString("F2") + " " + currentDeposit.Currency;
                    break;
            }

            depositLabel.Content = currentDeposit.Name + " в " + currentDeposit.Currency;
            dateLabel.Content = string.Format("{0:dd.MM.yyyy HH:mm:ss}", o.Date);
            descriptionLabel.Content = o.Description;
            operationLabel.Content = o.Type;

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


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            currentDeposit1 = currentDeposit;
            DepositDetailsWindow wDepositdetails = new DepositDetailsWindow(currentDeposit);
            wDepositdetails.Show();
            Close();

        }
    }
}
