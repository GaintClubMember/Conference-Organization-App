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
    public partial class BlankPage : Page
    {
        public BlankPage()
        {
            InitializeComponent();
        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Classes.Manager.frameMaster.Navigate(new Pages.Pages_By_Role.OrganizatorPage(Classes.Manager.globalName, Classes.Manager.globalGender));
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
