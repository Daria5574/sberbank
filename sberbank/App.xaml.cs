using sberbank.Models;
using System.Configuration;
using System.Data;
using System.Windows;

namespace sberbank
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Client currentClient = null;
        public static int UserRole;
    }

}
