using Conference_Organization_App.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
    public partial class RegistrationJudgeModerPage : Page
    {
        bool attachToEvent = false;
        bool isPasswordVisible = false;

        Data.Users newUser = new Data.Users();

        public RegistrationJudgeModerPage()
        {
            InitializeComponent();
            HideFindListView();
            LoadComboBoxes();
            idBox.Text = Data.DB_Entities.GetContext().Users.Max(d => d.id)+1.ToString();
        }





        // EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS 
        // EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS 
        // EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS  EVENTS LISTENERS 

        private void attachToEventCheck_Checked(object sender, RoutedEventArgs e)
        {
            ShowEventComboBox();
        }
        private void attachToEventCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            HideEventComboBox();
        }
        
        private void directionsSearchMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = directionSearchMenu.SelectedItem;
            var selectedEvent = (UserEventDirections)selectedItem;
            directionBox.Text = $"{selectedEvent.name.ToString()}";
        }


        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (errorsCount() == 0)
            {
                fillData();
                MessageBox.Show("Успешно добавленно", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                Classes.Manager.frameMaster.Navigate(new Pages.MyProfilePage());
            }
        }

        private void phoneBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = phoneBox.Text;

            input = input.Replace("+7", "").Replace("-", "").Replace(" ", "").Trim();

            if (input.Length > 0)
            {
                string formattedText = "+7 ";

                if (input.Length > 0) formattedText += input.Substring(0, Math.Min(3, input.Length)) + "-"; // First 3 digits
                if (input.Length > 3) formattedText += input.Substring(3, Math.Min(3, input.Length - 3)) + "-"; // Next 3 digits
                if (input.Length > 6) formattedText += input.Substring(6, Math.Min(2, input.Length - 6)) + "-"; // Next 2 digits
                if (input.Length > 8) formattedText += input.Substring(8, Math.Min(2, input.Length - 8)); // Last 2 digits

                phoneBox.TextChanged -= phoneBox_TextChanged;
                phoneBox.Text = formattedText.TrimEnd('-');
                phoneBox.CaretIndex = phoneBox.Text.Length;
                phoneBox.TextChanged += phoneBox_TextChanged;
            }
        }
        private void phoneBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
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
            ShowFindListView();
        }

        private void directionBox_LostFocus(object sender, RoutedEventArgs e)
        {
            attachToEvent = false;
            HideFindListView();
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
        private void fillData()
        {
            if (errorsCount() == 0)
            {
                string[] fioByParts = fioBox.Text.Split(' ');
                newUser.Lastname = fioByParts[0];
                newUser.Name = fioByParts[1];
                newUser.Patronymic = fioByParts[2];

                newUser.Gender_Id = genderComboBox.SelectedIndex + 1;
                newUser.Role_Id = roleComboBox.SelectedIndex + 1;

                newUser.Email = emailBox.Text;
                newUser.Phone = phoneBox.Text;

                var UserDirectionIsExist = Data.DB_Entities.GetContext().UsersDirections.Where(d => d.name == directionBox.Text).FirstOrDefault();
                if(UserDirectionIsExist != null)
                {
                    newUser.UserDirection_Id = UserDirectionIsExist.id;
                }
                else
                {
                    Data.UsersDirections _UserDirection = new Data.UsersDirections();
                    _UserDirection.name = directionBox.Text;
                    Data.DB_Entities.GetContext().UsersDirections.Add(_UserDirection);
                    Data.DB_Entities.GetContext().SaveChanges();

                    newUser.UserDirection_Id = _UserDirection.id;
                }

                if (isPasswordVisible == false)
                {
                    newUser.Password = passwordBox.Password;
                }
                if (isPasswordVisible == true)
                {
                    newUser.Password = passwordTextBox.Text;
                }

                if (attachToEvent == false)
                {
                    newUser.UserEventDirection_Id = eventBox.SelectedIndex + 1;
                }


                // Image

                newUser.Country_Id = 1; // delete later

                Data.DB_Entities.GetContext().Users.Add(newUser);
                Data.DB_Entities.GetContext().SaveChanges();
            }
            else
            {
                return;
            }

        }

        private int errorsCount()
        {
            StringBuilder errorsString = new StringBuilder();


            if (attachToEvent == true)
            {
                if (String.IsNullOrEmpty(eventBox.Text))
                {
                    errorsString.AppendLine("event name is null");
                }
            }

            if (!String.IsNullOrEmpty(fioBox.Text))
            {
                string fioBoxText = fioBox.Text.Trim();
                string[] Parts = fioBoxText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (Parts.Length == 3)
                {
                    //string lastname = Parts[0];
                    //string name = Parts[1];
                    //string patronymic = Parts[2];
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

            if (genderComboBox.SelectedValue == null)
            {
                errorsString.AppendLine("gender is null");
            }
            if (roleComboBox.SelectedValue == null)
            {
                errorsString.AppendLine("role is null");
            }
            if (String.IsNullOrEmpty(emailBox.Text))
            {
                errorsString.AppendLine("email is null");
            }
            if (String.IsNullOrEmpty(phoneBox.Text.ToString()))
            {
                errorsString.AppendLine("phone is null");
            }
            if (String.IsNullOrEmpty(directionBox.Text))
            {
                errorsString.AppendLine("direction is null");
            }

            if (isPasswordVisible == true)
            {
                if (!String.IsNullOrEmpty(passwordTextBox.Text) && !String.IsNullOrEmpty(passwordTextBox2.Text))
                {
                    if (passwordTextBox.Text.Length < 6 || passwordTextBox2.Text.Length < 6)
                    {
                        errorsString.AppendLine($"password length must be more than 5 1:{passwordTextBox.Text.Length} 2:{passwordTextBox2.Text.Length}");
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

            if (errorsString.Length > 0)
            {
                MessageBox.Show($"{errorsString.ToString()}", "Errors", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

            return errorsString.Length;
        }

        private void HideFindListView()
        {
            directionSearchMenu.Visibility = Visibility.Collapsed;
        }
        private void ShowFindListView()
        {
            directionSearchMenu.Visibility = Visibility.Visible;
        }

        private void HideEventComboBox()
        {
            eventLabel.Visibility = Visibility.Hidden;
            eventBox.Visibility = Visibility.Hidden;
        }
        private void ShowEventComboBox()
        {
            eventLabel.Visibility = Visibility.Visible;
            eventBox.Visibility = Visibility.Visible;
        }

        private void LoadComboBoxes()
        {
            eventBox.ItemsSource = Data.DB_Entities.GetContext().UserEventDirections.ToList();
            directionSearchMenu.ItemsSource = Data.DB_Entities.GetContext().UserEventDirections.ToList();
            genderComboBox.ItemsSource = Data.DB_Entities.GetContext().Genders.ToList();
            roleComboBox.ItemsSource = Data.DB_Entities.GetContext().Roles.ToList();
        }

        private bool imageProcessor()
        {
            // image upload to db
            return true;
        }

        private static bool IsTextAllowed(string text)
        {
            return Regex.IsMatch(text, @"^[0-9]+$");
        }

        private void imageHolder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string selectedFileName = openFileDialog.FileName;

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(selectedFileName);
                    bitmap.EndInit();

                    if (bitmap.PixelWidth != 300 || bitmap.PixelHeight != 200)
                    {
                        MessageBox.Show("Изображение должно быть размером 300x200 пикселей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    imageHolder.Source = bitmap;
                    byte[] imageBytes = GetImageBytes(selectedFileName);

                    if (imageBytes != null)
                    {
                        newUser.Photo_Image = imageBytes;
                        newUser.Photo_Name = selectedFileName.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private byte[] GetImageBytes(string imagePath)
        {
            try
            {
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                return imageBytes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при конвертации изображения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
