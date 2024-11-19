using Conference_Organization_App.Classes;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Conference_Organization_App.Pages
{
    public partial class LoginPage : Page
    {
        private readonly Random randomNumber = new Random();
        private static string validCaptcha;
        private DispatcherTimer blockTimer;

        public LoginPage()
        {
            InitializeComponent();

            loginBox.Text = "26";
            passwordBox.Text = "bBFR23s95s";
        }

        private void checkAuth()
        {
            if (Manager.failedAuthCount < 2)
            {
                string login = loginBox.Text;
                string password = passwordBox.Text;

                if (Data.DB_Entities.GetContext().Users.Any(d => d.id.ToString() == login && d.Password == password))
                {
                    Manager.currentOrSavedUser = Data.DB_Entities.GetContext().Users
                        .FirstOrDefault(d => d.id.ToString() == login && d.Password == password);

                    string role = Manager.currentOrSavedUser.Roles.name;

                    string name = Manager.currentOrSavedUser.Name.ToString();
                    string gender = Manager.currentOrSavedUser.Genders.name.ToString();

                    switch (role)
                    {
                        case "Организатор":
                            Manager.frameMaster.Navigate(new Pages_By_Role.OrganizatorPage(name, gender));
                            break;
                        case "Участник":
                            Manager.frameMaster.Navigate(new Pages_By_Role.BlankPage());
                            break;
                        case "Жюри":
                            Manager.frameMaster.Navigate(new Pages_By_Role.BlankPage());
                            break;
                        case "Модератор":
                            Manager.frameMaster.Navigate(new Pages_By_Role.BlankPage());
                            break;
                    }

                    MessageBox.Show("Успешная авторизация", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Manager.failedAuthCount++;
                }
            }

            if (Manager.failedAuthCount >= 2)
            {
                if (Manager.failedAuthCount >= 3)
                {
                    if (!verifyCaptcha(captchaInputBox.Text) == true)
                    {
                        disableBoxes();
                        MessageBox.Show("Неправильная каптча", "Блокировака", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                    else
                    {
                        string login = loginBox.Text;
                        string password = passwordBox.Text;

                        if (Data.DB_Entities.GetContext().Users.Any(d => d.Phone == login && d.Password == password))
                        {
                            Manager.currentOrSavedUser = Data.DB_Entities.GetContext().Users
                                .FirstOrDefault(d => d.Phone == login && d.Password == password);

                            string role = Manager.currentOrSavedUser.Roles.name;

                            string name = Manager.currentOrSavedUser.Name.ToString();
                            string gender = Manager.currentOrSavedUser.Genders.name.ToString();

                            switch (role)
                            {
                                case "Организатор":
                                    Manager.frameMaster.Navigate(new Pages_By_Role.OrganizatorPage(name, gender));
                                    break;
                                case "Участник":
                                    Manager.frameMaster.Navigate(new Pages_By_Role.BlankPage());
                                    break;
                                case "Жюри":
                                    Manager.frameMaster.Navigate(new Pages_By_Role.BlankPage());
                                    break;
                                case "Модератор":
                                    Manager.frameMaster.Navigate(new Pages_By_Role.BlankPage());
                                    break;
                            }
                            MessageBox.Show("Успешная авторизация", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Неправильный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            Manager.failedAuthCount++;
                            disableBoxes();
                        }
                    }
                }
                else
                {
                    Manager.failedAuthCount++;
                    ShowCaptcha();
                    generateCaptcha();
                }
            }
        }



        private void disableBoxes()
        {
            loginBox.IsEnabled = false;
            passwordBox.IsEnabled = false;
            Classes.Manager.StartBlockTimer(UnblockBoxes);
        }



        private void UnblockBoxes()
        {
            captchaInputBox.Text = $"Блокировка: {Classes.Manager.BlockDuration} сек";
            if (Classes.Manager.BlockDuration == 0)
            {
                loginBox.IsEnabled = true;
                passwordBox.IsEnabled = true;
                MessageBox.Show("Можете попробовать снова", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                captchaInputBox.Text = "";
                generateCaptcha();
            }
        }

        private void ShowCaptcha()
        {
            captchaStackPanel.Visibility = Visibility.Visible;
        }

        private static bool verifyCaptcha(string captchaText)
        {
            return validCaptcha == captchaText;
        }

        private void generateCaptcha()
        {
            int randomCaptcha = randomNumber.Next(0, 2);
            string imagePath;
            MessageBox.Show($"{randomCaptcha.ToString()}", "!!!", MessageBoxButton.OK, MessageBoxImage.Hand);
            if (randomCaptcha == 1)
            {
                imagePath = $"C:/Users/User/source/repos/Conference Organization App/Conference Organization App/Resources/Captcha/captcha_1.png";
                validCaptcha = "1acr";
            }
            else
            {
                imagePath = $"C:/Users/User/source/repos/Conference Organization App/Conference Organization App/Resources/Captcha/captcha_2.png";
                validCaptcha = "5ik6";
            }

            if (File.Exists(imagePath))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                WriteableBitmap writeableBitmap = new WriteableBitmap(bitmapImage);
                AddNoiseToCaptcha(writeableBitmap);
                captchaImage.Source = writeableBitmap;
            }
            else
            {
                MessageBox.Show("Каптча не найдена");
            }
        }

        private void AddNoiseToCaptcha(WriteableBitmap bitmap)
        {
            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                int x = random.Next(0, bitmap.PixelWidth);
                int y = random.Next(0, bitmap.PixelHeight);
                byte[] blackPixel = { 0, 0, 0, 255 };
                bitmap.Lock();
                bitmap.WritePixels(new System.Windows.Int32Rect(x, y, 1, 1), blackPixel, 10, 0);
                bitmap.Unlock();
            }
        }

        private void applyBtn_Click(object sender, RoutedEventArgs e)
        {
            checkAuth();
        }
    }
}
