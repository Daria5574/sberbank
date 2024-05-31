using OfficeOpenXml;
using sberbank.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
            Close();
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            currentDeposit1 = currentDeposit;
            DepositDetailsWindow wDepositdetails = new DepositDetailsWindow(currentDeposit, currentAccount);
            wDepositdetails.Show();
            Close();

        }

        private void importButton_Click(object sender, RoutedEventArgs e)
        {
            if (o == null || currentAccount == null || currentDeposit == null)
            {
                MessageBox.Show("Ошибка: Не удалось загрузить данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Excel файл (*.xlsx)|*.xlsx";
            saveFileDialog.DefaultExt = ".xlsx";

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string fileName = saveFileDialog.FileName;
                FileInfo newFile = new FileInfo(fileName);

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                ExcelPackage excelPackage;
                if (newFile.Exists)
                {
                    excelPackage = new ExcelPackage(newFile);
                }
                else
                {
                    excelPackage = new ExcelPackage();
                }

                ExcelWorksheet worksheet;
                if (excelPackage.Workbook.Worksheets.Count > 0)
                {
                    worksheet = excelPackage.Workbook.Worksheets[0];
                    worksheet.Cells.Clear();
                }
                else
                {
                    worksheet = excelPackage.Workbook.Worksheets.Add("DetailProduct");
                }

                worksheet.Cells[1, 1].Value = "Операция:";
                worksheet.Cells[1, 2].Value = o.Type;

                worksheet.Cells[2, 1].Value = "На сумму:";
                worksheet.Cells[2, 2].Value = o.Sum.ToString("F2") + " " + currentDeposit.Currency;

                worksheet.Cells[3, 1].Value = "По вкладу:";
                worksheet.Cells[3, 2].Value = currentDeposit.Name + " в " + currentDeposit.Currency;

                worksheet.Cells[4, 1].Value = "Номер счета:";
                worksheet.Cells[4, 2].Value = currentAccount.IdAccount;

                worksheet.Cells[5, 1].Value = "Дата и время совершения операции:";
                worksheet.Cells[5, 2].Value = string.Format("{0:dd.MM.yyyy HH:mm:ss}", o.Date);

                worksheet.Cells[6, 1].Value = "Описание:";
                worksheet.Cells[6, 2].Value = o.Description;

                excelPackage.SaveAs(newFile);
                MessageBox.Show("Данные успешно экспортированы в файл " + fileName, "Экспорт завершен", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


    }
}
