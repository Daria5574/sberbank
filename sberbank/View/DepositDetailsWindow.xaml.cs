using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;
using sberbank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Логика взаимодействия для DepositDetailsWindow.xaml
    /// </summary>
    public partial class DepositDetailsWindow : Window
    {
        private System.Timers.Timer interestTimer;
        Models.Account currentAccount;
        Models.Account currentAccount1;
        private DateOnly lastInterestDate;
        Client currentClient;
        Client currentClient1;
        Deposit currentDeposit1;

        SberContext db = new SberContext();

        public DepositDetailsWindow(Deposit currentDeposit, Models.Account currentAccount)
        {
            InitializeComponent();
            //interestTimer = new System.Timers.Timer(100000);
            //interestTimer.Elapsed += InterestTimer_Elapsed;
            //interestTimer.Start();

            currentDeposit1 = currentDeposit;
            this.currentAccount = currentAccount;
            lastInterestDate = currentAccount.OpeningDate;
            nameLabel.Content = currentDeposit.Name;
            balanceLabel.Content = currentAccount.Balance.ToString("F2") + " " + currentDeposit.Currency;
            procentLabel.Content = "Ставка " + currentDeposit.InterestRate.ToString("F1") + "% годовых";

            UpdateOperations();
            if (App.UserRole == 2)
            {
                manageBox.Visibility = Visibility.Collapsed;
            }

            if (App.UserRole == 1)
            {
                procentGrid.Visibility = Visibility.Collapsed;
            }
            
        }
        //private void InterestTimer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    currentAccount = currentAccount1;
        //    currentAccount1 = currentAccount;
        //    DateTime startDate = DateTime.Now;
        //    int daysInPeriod = 0;
        //    switch (currentDeposit1.InterestPeriod)
        //    {
        //        case 1:
        //            daysInPeriod = GetDaysInPeriod(1, startDate);
        //            break;
        //        case 4:
        //            daysInPeriod = GetDaysInPeriod(4, startDate);
        //            break;
        //        case 12:
        //            daysInPeriod = GetDaysInPeriod(12, startDate);
        //            break;
        //    }
        //    if (currentAccount1 != null)
        //    {
        //        if (ShouldCalculateInterest(currentAccount1.OpeningDate, currentDeposit1.InterestPeriod))
        //        {
        //            decimal sumProcent = currentAccount1.Balance * (currentDeposit1.InterestRate / 100) * (daysInPeriod / 365m);
        //            currentAccount1.Balance += sumProcent;

        //            db.Update(currentAccount);
        //            //db.Entry(currentAccount1).State = EntityState.Modified;
        //            db.SaveChanges();

        //            Operation operation = new Operation
        //            {
        //                IdAccount = currentAccount1.IdAccount,
        //                IdClient = currentAccount1.IdClient,
        //                Type = "Зачисление процентов",
        //                Date = DateTime.Now,
        //                Sum = sumProcent,
        //                Description = "Зачисление процентов за период."
        //            };
        //            db.Operations.Add(operation);
        //            db.SaveChanges();

        //            lastInterestDate = lastInterestDate.AddMonths(currentDeposit1.InterestPeriod);
        //        }

        //    }
        //}
        //private bool ShouldCalculateInterest(DateOnly openingDate, int interestPeriod)
        //{
        //    return DateOnly.FromDateTime(DateTime.Now) >= lastInterestDate.AddMonths(interestPeriod);
        //}


        public static int GetDaysInPeriod(int periodMonths, DateTime startDate)
        {
            DateTime endDate = startDate.AddMonths(periodMonths);
            int daysInPeriod = (endDate - startDate).Days;
            return daysInPeriod;
        }

        private void procentButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = DateTime.Now;
            int daysInPeriod = 0;
            switch (currentDeposit1.InterestPeriod)
            {
                case 1:
                    daysInPeriod = GetDaysInPeriod(1, startDate);
                    break;
                case 4:
                    daysInPeriod = GetDaysInPeriod(4, startDate);
                    break;
                case 12:
                    daysInPeriod = GetDaysInPeriod(12, startDate);
                    break;
            }
            decimal sumProcent = currentAccount.Balance * (currentDeposit1.InterestRate / 100) * (daysInPeriod / 365m);
            currentAccount.Balance += sumProcent;
            db.Update(currentAccount);
            db.SaveChanges();

            Operation operation = new Operation
            {
                IdAccount = currentAccount.IdAccount,
                IdClient = currentAccount.IdClient,
                Type = "Зачисление процентов",
                Date = DateTime.Now,
                Sum = sumProcent,
                Description = "Зачисление процентов за период."
            };
            db.Operations.Add(operation);
            db.SaveChanges();
            MessageBox.Show("Зачисление процентов прошло успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            DepositDetailsWindow wDepositdetails = new DepositDetailsWindow(currentDeposit1, currentAccount);
            wDepositdetails.Show();
            Close();

        }
        public void UpdateOperations()
        {
            currentAccount1 = currentAccount;

            var listViewData = from operation in db.Operations
                               where operation.IdAccount == currentAccount.IdAccount
                               orderby operation.Date descending
                               select new
                               {
                                   Typee = operation.Type,
                                   Summa = operation.Sum,
                                   Data = operation.Date
                               };



            lvOperations.ItemsSource = listViewData.ToList();
        }
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is System.Windows.Controls.ListViewItem listViewItem)
            {
                Operation currentOperation = null;
                using (SberContext db = new SberContext())
                {
                    var selectedItem = listViewItem.Content as dynamic;
                    DateTime data = selectedItem.Data;

                    currentOperation = db.Operations.FirstOrDefault(b => b.Date == data);

                    OperationDetailsWindow wOperationDetails = new OperationDetailsWindow(currentOperation);
                    wOperationDetails.Show();
                    Close();
                }
            }
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

        private void MoneyButton_Click(object sender, RoutedEventArgs e)
        {
            currentAccount = currentAccount1;
            BalanceManageWindow balanceManageWindow = new BalanceManageWindow(currentAccount);
            balanceManageWindow.Show();
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            currentAccount = currentAccount1;
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите закрыть данный счет?", "Подтверждение закрытия счета", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                currentAccount.Status = 0;
                currentAccount.ClosingDate = DateOnly.FromDateTime(DateTime.Now);

                db.Accounts.Update(currentAccount);
                db.SaveChanges();

                MainClientWindow mainClientWindow = new MainClientWindow(App.currentClient);
                mainClientWindow.Show();
                Close();
            }
        }


    }
}
