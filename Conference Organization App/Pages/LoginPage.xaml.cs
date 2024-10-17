using Conference_Organization_App.Classes;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (Classes.Manager.failedAuthCount < 2)
            {
                string login = loginBox.Text;
                string password = passwordBox.Text;

                //var isUserExists = Data.DB_Entities.GetContext().Users.FirstOrDefault(d => d.Phone == login); // phone needs to be changed
                //if(isUserExists == null)
                //{
                //    MessageBox.Show("Пользователя не существует | " + Classes.Manager.failedAuthCount.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                //    return;
                //}
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
                        MessageBox.Show("Неверный логин или пароль | " + Classes.Manager.failedAuthCount.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                        return;
                    }
                
            }
            if (Classes.Manager.failedAuthCount == 2)
            {
                MessageBox.Show(">2 try", ">2 try", MessageBoxButton.OK, MessageBoxImage.Question);
                ShowElements();
                generateCaptcha();
                return;
                // generate capthca and more
            }
        }

        private void ShowElements()
        {
            // Устанавливаем видимость элементов, которые были Hidden
            loginBox.Visibility = Visibility.Visible;
            passwordBox.Visibility = Visibility.Visible;

            // Меняем StackPanel из Collapsed на Visible
            captchaStackPanel.Visibility = Visibility.Visible;
        }

        private void generateCaptcha()
        {
            try
            {
                string imagePath = "/Resources/Captcha/Captcha1.png";

                if (File.Exists(imagePath))
                {
                    // Load the image
                    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                    // Convert the image to a format we can modify (like a writable bitmap)
                    WriteableBitmap writeableBitmap = new WriteableBitmap(bitmapImage);

                    // Modify pixels (example: draw random dots for CAPTCHA noise)
                    AddNoiseToCaptcha(writeableBitmap);

                    // Set the modified image as the source for the captchaImage control
                    captchaImage.Source = writeableBitmap;
                }
                else
                {
                    MessageBox.Show("Captcha image not found.");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void AddNoiseToCaptcha(WriteableBitmap bitmap)
        {
            Random random = new Random();

            // Простой пример: добавляем 50 случайных черных точек на изображение
            for (int i = 0; i < 50; i++)
            {
                int x = random.Next(0, bitmap.PixelWidth);
                int y = random.Next(0, bitmap.PixelHeight);

                // Цвет в формате ARGB (черный цвет: 255, 0, 0, 0)
                byte[] blackPixel = { 0, 0, 0, 255 };  // A, R, G, B

                // Открываем доступ к пикселям изображения
                bitmap.Lock();

                // Устанавливаем пиксель чёрного цвета
                bitmap.WritePixels(new Int32Rect(x, y, 1, 1), blackPixel, 4, 0);

                // Завершаем изменение пикселей
                bitmap.Unlock();
            }
        }



        private void applyBtn_Click(object sender, RoutedEventArgs e)
        {
            checkAuth();        
        }
    }
}
