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
    /// Логика взаимодействия для RegistrationJudgeModerPage.xaml
    /// </summary>
    public partial class RegistrationJudgeModerPage : Page
    {
        public RegistrationJudgeModerPage()
        {
            InitializeComponent();
            HideElements();
            LoadComboBoxes();

        }

        private void attachToEventCheck_Click(object sender, RoutedEventArgs e)
        {

        }

        private void attachToEventCheck_Checked(object sender, RoutedEventArgs e)
        {
            ShowElements();
        }

        private void attachToEventCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            HideElements();
        }
        private void HideElements()
        {
            eventBox.Visibility = Visibility.Hidden;
            eventLabel.Visibility = Visibility.Hidden;
        }
        private void ShowElements()
        {
            eventBox.Visibility = Visibility.Visible;
            eventLabel.Visibility = Visibility.Visible;
        }
        private void LoadComboBoxes()
        {
            genderComboBox.ItemsSource = Data.DB_Entities.GetContext().Genders.ToList();
            roleComboBox.ItemsSource = Data.DB_Entities.GetContext().Roles.ToList();
        }
    }
}
