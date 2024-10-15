using Conference_Organization_App.Classes;
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

namespace Conference_Organization_App.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();


            // delete 2 lines below
            loginBox.Text = "9204927370";
            passwordBox.Text = "bBFR23s95s";
            
        }

        private void checkAuth()
        {
            if (Classes.Manager.failedAuthCount <= 2)
            {
                string login = loginBox.Text;
                string password = passwordBox.Text;

                var isUserExists = Data.DB_Entities.GetContext().Users.FirstOrDefault(d => d.Phone == login); // phone needs to be changed
                if(isUserExists == null)
                {
                    MessageBox.Show("Пользователя не существует", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    if (Data.DB_Entities.GetContext().Users.Any(d => d.Phone == login && d.Password == password)) // phone needs to be changed
                    {
                        Classes.Manager.currentOrSavedUser = Data.DB_Entities.GetContext().Users.FirstOrDefault(d => d.Phone == login && d.Password == password); // adding data to Saved User to retrive role  // phone needs to be changed
                        string role = Classes.Manager.currentOrSavedUser.Roles.name; // retriving name of role from Saved user

                        switch(role)
                        {
                            case "Организатор":
                                Classes.Manager.frameMaster.Navigate(new Pages.Pages_By_Role.OrganizatorPage());
                                break;
                            case "Участник":
                                Classes.Manager.frameMaster.Navigate(new Pages.Pages_By_Role.OrganizatorPage()); // add ParticipantPage
                                break;
                            case "Жюри":
                                Classes.Manager.frameMaster.Navigate(new Pages.Pages_By_Role.OrganizatorPage()); // add JudgePage
                                break;
                            case "Модератор":
                                Classes.Manager.frameMaster.Navigate(new Pages.Pages_By_Role.OrganizatorPage()); // add ModeratorPage
                                break;
                        }
                    }
                    else
                    {
                        Classes.Manager.failedAuthCount++;
                        MessageBox.Show("Неверный логин или пароль", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                        return;
                    }
                }
            }
            else
            {
                // generate capthca and more
            }
        }

        private void applyBtn_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.frameMaster.Navigate(new Pages.Pages_By_Role.OrganizatorPage());
        }
    }
}
