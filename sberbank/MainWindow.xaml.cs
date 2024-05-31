using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using sberbank.Models;
using sberbank.View;

namespace sberbank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Client authClient = null;

        private SberContext db;
        public MainWindow()
        {
            InitializeComponent();

            db = new SberContext();

            //Account account = new Account
            //{
            //    IdClient = 2,
            //    IdDeposit = 1,
            //    DepositAmount = 100000,
            //    OpeningDate = DateOnly.Parse("31.03.2024"),
            //    ClosingDate = DateOnly.Parse("31.03.2025"),
            //    Balance = 100000,
            //    Status = 1,
            //};
            //db.Accounts.Add(account);
            //db.SaveChanges();
        }

        private void buttonСontinue_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();

            if (login.Length < 5)
            {
                textBoxLogin.ToolTip = "Это поле введено некорректно.";
                textBoxLogin.Background = Brushes.LightCoral;
            }
            else if (pass.Length < 6 || !pass.Any(char.IsUpper) || !pass.Any(char.IsDigit) || !pass.Any(c => "!@#$%^.".Contains(c)))
            {
                passBox.ToolTip = "Это поле введено некорректно.";
                passBox.Background = Brushes.LightCoral;
            }
            else
            {
                textBoxLogin.ToolTip = "";
                textBoxLogin.Background = Brushes.Transparent;
                passBox.ToolTip = "";
                passBox.Background = Brushes.Transparent;

                if (login == "adminka" && pass == "Admin5574!")
                {
                    App.UserRole = 2;

                    ClientsWindow wClients = new ClientsWindow();
                    wClients.Show();
                    Close();
                }
                else if (char.IsLetter(login[0]))
                {
                    authClient = db.Clients.Where(c => c.Login == login && c.Password == pass).FirstOrDefault();

                    if (authClient != null)
                    {
                        if (authClient.IsActive == 1)
                        {
                            App.UserRole = 1;
                            App.currentClient = authClient;

                            MainClientWindow wMain = new MainClientWindow(App.currentClient);
                            wMain.Show();
                            Close();
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Ваш профиль деактивирован. Для активации позвоните на горячую линию по номеру *** или обратитесь в отделение банка.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            textBoxLogin.Text = "";
                            passBox.Password = "";
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Пользователь не найден. Проверьте правильность введённых данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }
        }

    }
}
