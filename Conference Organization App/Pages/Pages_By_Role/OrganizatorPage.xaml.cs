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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Conference_Organization_App.Pages.Pages_By_Role
{
    /// <summary>
    /// Логика взаимодействия для OrganizatorPage.xaml
    /// </summary>
    public partial class OrganizatorPage : Page
    {
        public OrganizatorPage()
        {
            InitializeComponent();
        }

        private void regBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Classes.Manager.frameMaster.Navigate(new Pages.LoginPage());
            }
            catch(Exception ex)
            {

            }
        }

        private void myProfileBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void eventsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void participantsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void judgesBtn_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.frameMaster.Navigate(new Pages.RegistrationJudgeModerPage());
        }
    }
}
