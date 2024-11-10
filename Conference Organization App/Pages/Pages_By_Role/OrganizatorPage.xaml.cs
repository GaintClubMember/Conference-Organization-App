using Conference_Organization_App.Data;
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
        string MrOrMrs;

        string _name;
        string _gender;
        public OrganizatorPage(string name, string gender)
        {
            InitializeComponent();

            Classes.Manager.globalName = name;
            Classes.Manager.globalGender = gender;

            try
            {
                _name = name;
                _gender = gender;

                SetWelcomeText();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void regBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Classes.Manager.frameMaster.Navigate(new Pages.LoginPage());
            }
            catch(Exception ex)
            {
                return;
            }
        }

        private void myProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Classes.Manager.frameMaster.Navigate(new Pages.MyProfilePage());
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void eventsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void participantsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void judgesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Classes.Manager.frameMaster.Navigate(new Pages.RegistrationJudgeModerPage());
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void setWelcomeCutIndex()
        {
            try
            {
                if(_gender == "Женский")
                {
                    MrOrMrs = "Mrs";
                }
                if(_gender == "Мужской")
                {
                    MrOrMrs = "Mr";
                }
                else
                {
                    MrOrMrs = "";
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void SetWelcomeText()
        {
            setWelcomeCutIndex();
            int hour = DateTime.Now.Hour;
            if (hour >= 9 && hour <= 11)
            {
                welcomeLabel.Content = $"Доброе утро!\n   {MrOrMrs} {_name}";
            }
            else if (hour >= 11 && hour < 18)
            {
                welcomeLabel.Content = $"Добрый день!\n   {MrOrMrs} {_name}";
            }
            else if (hour >= 18 && hour <= 24)
            {
                welcomeLabel.Content = $"Добрый вечер!\n   {MrOrMrs} {_name}";
            }
            else
            {
                welcomeLabel.Content = $"Добро пожаловать!\n{MrOrMrs} {_name}";
            }
        }
    }
}
