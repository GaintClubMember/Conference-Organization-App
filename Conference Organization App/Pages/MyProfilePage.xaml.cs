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
    /// <summary>
    /// Логика взаимодействия для MyProfilePage.xaml
    /// </summary>
    public partial class MyProfilePage : Page
    {
        public MyProfilePage()
        {
            InitializeComponent();

            try
            {
                LoadUserData();
            }
            catch(Exception ex)
            {
                return;
            }
        }

        private void LoadUserData()
        {
            try
            {
                userName.Content = Classes.Manager.currentOrSavedUser.Name.ToString();
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
