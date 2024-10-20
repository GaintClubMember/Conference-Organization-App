using Conference_Organization_App.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        bool attachToEvent = false;
        bool isPasswordVisible = false;
        public RegistrationJudgeModerPage()
        {
            InitializeComponent();
            HideElements();
            LoadComboBoxes();

        }

        private void attachToEventCheck_Checked(object sender, RoutedEventArgs e)
        {
            attachToEvent = true;
            ShowElements();
        }

        private void attachToEventCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            attachToEvent = false;
            HideElements();
        }
        private void HideElements()
        {
            eventBox.Visibility = Visibility.Hidden;
            eventLabel.Visibility = Visibility.Hidden;
            eventSearchMenu.Visibility = Visibility.Collapsed;
        }
        private void ShowElements()
        {
            eventBox.Visibility = Visibility.Visible;
            eventLabel.Visibility = Visibility.Visible;
            eventSearchMenu.Visibility = Visibility.Visible;

            eventSearchMenu.ItemsSource = Data.DB_Entities.GetContext().EventNames.ToList();
        }
        private void LoadComboBoxes()
        {
            if (String.IsNullOrEmpty(eventBox.Text))
            {
                genderComboBox.ItemsSource = Data.DB_Entities.GetContext().Genders.ToList();
                roleComboBox.ItemsSource = Data.DB_Entities.GetContext().Roles.ToList();
            }
            else
            {
                eventSearchMenu.ItemsSource = (Data.DB_Entities.GetContext().EventNames.Where(d => d.Name == eventBox.Text)).ToList();

            }
        }


        string selectedName;
        private void eventSearchMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = eventSearchMenu.SelectedItem;
            if (selectedItem != null)
            {
                var selectedEvent = (EventNames)selectedItem;
                eventBox.Text = $"{selectedEvent.Name.ToString()}";
            }
            else
            {
                return;
            }
        }

        private void eventBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            eventSearchMenu.ItemsSource = (from item in Data.DB_Entities.GetContext().EventNames
             where item.Name.ToLower().Contains(eventBox.Text) ||
             item.id.ToString().Contains(eventBox.Text)
             select item).ToList();
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errorsString = new StringBuilder();
            
            // Event Attach Check
            if (attachToEvent == true)
            {
                // EventNames Name Check
                if (!String.IsNullOrEmpty(eventBox.Text))
                {
                    if (Data.DB_Entities.GetContext().EventNames.Any(d => d.Name == eventBox.Text) == false)
                    {
                        errorsString.AppendLine("event name doesnt exist");
                        //MessageBox.Show("doesnt exist", "EventNames", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        errorsString.AppendLine("event name already exist");
                        //MessageBox.Show("already exist", "EventNames", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    errorsString.AppendLine("event name is null");
                }
            }
            else
            {
                // notrhing
            }

            // Users Lastname Name Patronymic Check
            if (!String.IsNullOrEmpty(fioBox.Text))
            {
                string fioBoxText = fioBox.Text.Trim();
                string[] Parts = fioBoxText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (Parts.Length >= 3)
                {
                    string lastname = Parts[0];
                    string name = Parts[1];
                    string patronymic = Parts[2];

                    errorsString.AppendLine($"{lastname} | {name} | {patronymic}");
                }
                else
                {
                    errorsString.AppendLine("fio need to be from 3 parts");
                }
            }
            else
            {
                errorsString.AppendLine("fio is null");
            }

            // Genders id name Check
            if(genderComboBox.SelectedValue == null)
            {
                errorsString.AppendLine("gender is null");
            }
            else
            {
                int genderId = (int)genderComboBox.SelectedValue;
                string genderName = genderComboBox.Text;
                errorsString.AppendLine($"gender: {genderName} id: {genderId}");
            }

            // Roles id name Check
            if (roleComboBox.SelectedValue == null)
            {
                errorsString.AppendLine("role is null");
            }
            else
            {
                int roleId = (int)roleComboBox.SelectedValue;
                string roleName = roleComboBox.Text;
                errorsString.AppendLine($"role {roleName} id {roleId}");
            }

            // Users email Check
            if (!String.IsNullOrEmpty(emailBox.Text))
            {
                errorsString.AppendLine($"email is {emailBox.Text}");
            }
            else
            {
                errorsString.AppendLine("email is null");
            }

            // Users phone Check
            // more complicated check needed (like FIO check but for phone form

            //Users image Check
            if (imageProcessor)


            // Users password Check
            if(isPasswordVisible == true)
            {
                if (!String.IsNullOrEmpty(passwordTextBox.Text) && !String.IsNullOrEmpty(passwordTextBox2.Text))
                {
                    // password visible
                    if (passwordTextBox.Text == passwordTextBox2.Text)
                    {
                        errorsString.AppendLine($"visible passwords match 1:{passwordTextBox.Text} 2:{passwordTextBox2.Text}");
                    }
                    else
                    {
                        errorsString.AppendLine("visible passwords doesnt match");
                    }
                }
                else
                {
                    errorsString.AppendLine("visible passwords is empty");
                }
            }
            else if (isPasswordVisible == false)
            {
                if (!String.IsNullOrEmpty(passwordBox.Password) && !String.IsNullOrEmpty(passwordBox2.Password))
                {
                    // password visible
                    if (passwordBox.Password == passwordBox2.Password)
                    {
                        errorsString.AppendLine($"invisible passwords match 1:{passwordBox.Password} 2:{passwordBox2.Password}");
                    }
                    else
                    {
                        errorsString.AppendLine("invisible passwords doesnt match");
                    }
                }
                else
                {
                    errorsString.AppendLine("invisible passwords is empty");
                }
            }
             

            // Show errors
            MessageBox.Show($"{errorsString.ToString()}", "Errors", MessageBoxButton.OK, MessageBoxImage.Stop);

        }

        private bool imageProcessor()
        {
            if ()
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void visiblePassword_Checked(object sender, RoutedEventArgs e)
        {
            isPasswordVisible = true;

            passwordTextBox.Visibility = Visibility.Visible;
            passwordBox.Visibility = Visibility.Collapsed;

            passwordTextBox2.Visibility = Visibility.Visible;
            passwordBox2.Visibility = Visibility.Collapsed;

            passwordTextBox.Text = passwordBox.Password;
            passwordTextBox2.Text = passwordBox2.Password;
        }

        private void visiblePassword_Unchecked(object sender, RoutedEventArgs e)
        {
            isPasswordVisible = false;

            passwordTextBox.Visibility = Visibility.Collapsed;
            passwordBox.Visibility = Visibility.Visible;

            passwordTextBox2.Visibility = Visibility.Collapsed;
            passwordBox2.Visibility = Visibility.Visible;

            passwordBox.Password = passwordTextBox.Text;
            passwordBox2.Password = passwordTextBox2.Text;
        }

        private void roleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
