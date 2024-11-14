using Conference_Organization_App.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        Data.Users newUser;

        public RegistrationJudgeModerPage()
        {
            InitializeComponent();
            HideElements();
            LoadComboBoxes();
            idBox.Text = Data.DB_Entities.GetContext().Users.Max(d => d.id)+1.ToString();
        }


        // EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS 
        // EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS 
        // EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS 

        private void attachToEventCheck_Checked(object sender, RoutedEventArgs e)
        {
            eventBox.Visibility = Visibility.Visible;
            eventLabel.Visibility = Visibility.Visible;
            //attachToEvent = true;
            //ShowElements();
        }

        private void attachToEventCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            eventBox.Visibility = Visibility.Hidden;
            eventLabel.Visibility = Visibility.Hidden;
            //attachToEvent = false;
            //HideElements();
        }
        
        //string selectedName;
        private void directionsSearchMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = directionSearchMenu.SelectedItem;
            if (selectedItem != null)
            {
                var selectedEvent = (UserEventDirections)selectedItem; //////////////////////////////////////////////////////////////////////////////////
                directionBox.Text = $"{selectedEvent.name.ToString()}";
            }
            else
            {
                return;
            }
        }


        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errorsString = new StringBuilder();
            
            // Event Attach Check
            if (attachToEvent == true)
            {
                // EventNames Name Check
                if (String.IsNullOrEmpty(eventBox.Text))
                {
                    //if (Data.DB_Entities.GetContext().EventNames.Any(d => d.Name == eventBox.Text) == false)
                    //{
                    //    errorsString.AppendLine("event name doesnt exist");
                    //}
                    //else
                    //{
                    //    // add to DB?
                    //    errorsString.AppendLine("event name already exist");
                    //}
                    errorsString.AppendLine("event name is null");

                }
                else
                {

                }
            }
            else
            {
                // nothing
            }

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
                    // add to DB
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
            // Roles id name Check
            if (roleComboBox.SelectedValue == null)
            {
                errorsString.AppendLine("role is null");
            }
            // Users email Check
            if (String.IsNullOrEmpty(emailBox.Text))
            {
                errorsString.AppendLine("email is null");
                // add to DB
            }

            //Users image Check
            // not required


            // Users password Check
            if(isPasswordVisible == true)
            {
                if (!String.IsNullOrEmpty(passwordTextBox.Text) && !String.IsNullOrEmpty(passwordTextBox2.Text))
                {
                    if (passwordBox.Password.Length < 6 || passwordBox2.Password.Length < 6)
                    {
                        errorsString.AppendLine($"password length must be more than 5:{passwordBox.Password.Length} 2:{passwordBox2.Password.Length}");
                    }
                    // password visible
                    if (passwordTextBox.Text == passwordTextBox2.Text)
                    {
                        errorsString.AppendLine($"visible passwords match 1:{passwordTextBox.Text} 2:{passwordTextBox2.Text}");
                        // add to DB
                    }
                    if (passwordTextBox.Text != passwordTextBox2.Text)
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
                    if(passwordBox.Password.Length < 6 || passwordBox2.Password.Length < 6)
                    {
                        errorsString.AppendLine($"password length must be more than 5:{passwordBox.Password.Length} 2:{passwordBox2.Password.Length}");
                    }
                    // password visible
                    if (passwordBox.Password == passwordBox2.Password)
                    {
                        errorsString.AppendLine($"invisible passwords match 1:{passwordBox.Password} 2:{passwordBox2.Password}");
                        // add to DB
                    }
                    if (passwordBox.Password != passwordBox2.Password)
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

        private void phoneBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Get the raw text
            string input = phoneBox.Text;

            // Clean input by removing unwanted characters
            input = input.Replace("+7", "").Replace("-", "").Replace(" ", "").Trim();

            // Only format if there's any input
            if (input.Length > 0)
            {
                // Start with country code
                string formattedText = "+7 ";

                // Adding formatting
                if (input.Length > 0) formattedText += input.Substring(0, Math.Min(3, input.Length)) + "-"; // First 3 digits
                if (input.Length > 3) formattedText += input.Substring(3, Math.Min(3, input.Length - 3)) + "-"; // Next 3 digits
                if (input.Length > 6) formattedText += input.Substring(6, Math.Min(2, input.Length - 6)) + "-"; // Next 2 digits
                if (input.Length > 8) formattedText += input.Substring(8, Math.Min(2, input.Length - 8)); // Last 2 digits

                // Temporarily remove the event handler to avoid recursion
                phoneBox.TextChanged -= phoneBox_TextChanged;
                phoneBox.Text = formattedText.TrimEnd('-'); // Remove trailing hyphen if present
                phoneBox.CaretIndex = phoneBox.Text.Length; // Move the caret to the end
                phoneBox.TextChanged += phoneBox_TextChanged; // Reattach event handler
            }
        }
        private void phoneBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Check if the input is a digit
            e.Handled = !IsTextAllowed(e.Text);
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

        private void directionBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            directionSearchMenu.ItemsSource = (from item in Data.DB_Entities.GetContext().UserEventDirections
                                               where item.name.ToLower().Contains(directionBox.Text) ||
                                               item.id.ToString().Contains(directionBox.Text)
                                               select item).ToList();
        }

        private void directionBox_GotFocus(object sender, RoutedEventArgs e)
        {
            attachToEvent = true;
            ShowElements();
        }

        private void directionBox_LostFocus(object sender, RoutedEventArgs e)
        {
            attachToEvent = false;
            HideElements();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
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

        // METHODS  METHODS  METHODS  METHODS  METHODS  METHODS  METHODS  METHODS 
        // METHODS  METHODS  METHODS  METHODS  METHODS  METHODS  METHODS  METHODS 
        // METHODS  METHODS  METHODS  METHODS  METHODS  METHODS  METHODS  METHODS 

        private int errorsCount()
        {
            StringBuilder errorsString = new StringBuilder;

            if (isPasswordVisible == true)
            {
                if (!String.IsNullOrEmpty(passwordTextBox.Text) && !String.IsNullOrEmpty(passwordTextBox2.Text))
                {
                    if (passwordBox.Password.Length < 6 || passwordBox2.Password.Length < 6)
                    {
                        errorsString.AppendLine($"password length must be more than 5:{passwordBox.Password.Length} 2:{passwordBox2.Password.Length}");
                    }
                    // password visible
                    if (passwordTextBox.Text == passwordTextBox2.Text)
                    {
                        errorsString.AppendLine($"visible passwords match 1:{passwordTextBox.Text} 2:{passwordTextBox2.Text}");
                        // add to DB
                    }
                    if (passwordTextBox.Text != passwordTextBox2.Text)
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
                    if (passwordBox.Password.Length < 6 || passwordBox2.Password.Length < 6)
                    {
                        errorsString.AppendLine($"password length must be more than 5:{passwordBox.Password.Length} 2:{passwordBox2.Password.Length}");
                    }
                    // password visible
                    if (passwordBox.Password == passwordBox2.Password)
                    {
                        errorsString.AppendLine($"invisible passwords match 1:{passwordBox.Password} 2:{passwordBox2.Password}");
                        // add to DB
                    }
                    if (passwordBox.Password != passwordBox2.Password)
                    {
                        errorsString.AppendLine("invisible passwords doesnt match");
                    }
                }
                else
                {
                    errorsString.AppendLine("invisible passwords is empty");
                }
            }

            return errorsString.Length;
        }

        private void HideElements()
        {
            eventBox.Visibility = Visibility.Hidden;
            eventLabel.Visibility = Visibility.Hidden;
            directionSearchMenu.Visibility = Visibility.Collapsed;
        }

        private void ShowElements()
        {
            eventBox.Visibility = Visibility.Visible;
            eventLabel.Visibility = Visibility.Visible;
            directionSearchMenu.Visibility = Visibility.Visible;

        }

        private void LoadComboBoxes()
        {
            eventBox.ItemsSource = Data.DB_Entities.GetContext().EventNames.ToList();
            directionSearchMenu.ItemsSource = Data.DB_Entities.GetContext().UserEventDirections.ToList();
            genderComboBox.ItemsSource = Data.DB_Entities.GetContext().Genders.ToList();
            roleComboBox.ItemsSource = Data.DB_Entities.GetContext().Roles.ToList();
            //if (String.IsNullOrEmpty(eventBox.Text))
            //{
            //    genderComboBox.ItemsSource = Data.DB_Entities.GetContext().Genders.ToList();
            //    roleComboBox.ItemsSource = Data.DB_Entities.GetContext().Roles.ToList();
            //}
            //else
            //{
            //    directionSearchMenu.ItemsSource = Data.DB_Entities.GetContext().UserEventDirections.ToList();
            //}
        }

        private bool imageProcessor()
        {
            // image upload to db
            return true;
        }

        private static bool IsTextAllowed(string text)
        {
            return Regex.IsMatch(text, @"^[0-9]+$"); // Allows only digits
        }
    }
}
