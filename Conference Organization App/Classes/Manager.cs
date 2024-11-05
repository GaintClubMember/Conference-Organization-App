using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Conference_Organization_App.Classes
{
    internal class Manager
    {
        public static Frame frameMaster { get; set; }

        public static Data.Users currentOrSavedUser { get; set; }

        public static int failedAuthCount = 0;

        public static bool isBlocked { get; set; }



        public static DispatcherTimer BlockTimer { get; private set; }
        public static int BlockDuration { get; private set; } = 10;
        public static Action OnBlockUpdate;
        static Manager()
        {
            BlockTimer = new DispatcherTimer();
            BlockTimer.Interval = TimeSpan.FromSeconds(1); // Интервал обновления таймера 1 секунда
            BlockTimer.Tick += BlockTimer_Tick;
        }

        // Метод запуска таймера блокировки
        public static void StartBlockTimer(Action updateUI)
        {
            BlockDuration = 10; // Сброс времени блокировки
            OnBlockUpdate = updateUI; // Устанавливаем действие для обновления UI
            BlockTimer.Start(); // Запуск таймера
        }

        // Метод обработки тиков таймера
        private static void BlockTimer_Tick(object sender, EventArgs e)
        {
            BlockDuration--;

            // Вызов делегата для обновления UI на странице
            OnBlockUpdate?.Invoke();

            // Остановка таймера, если время блокировки истекло
            if (BlockDuration == 0)
            {
                BlockTimer.Stop();
                ResetBlock();
            }
        }

        // Метод сброса блокировки (восстановление состояния)
        public static void ResetBlock()
        {
            BlockDuration = 10; // Сброс продолжительности блокировки
        }

        public static void GetImageDataForEvents()
        {
            try
            {
                var list = Data.DB_Entities.GetContext().EventsMain.ToList();
                foreach (var item in list)
                {
                    string path = Directory.GetCurrentDirectory() + @"\img\" + item.Photo_Name.ToString();
                    if (File.Exists(path))
                    {
                        item.Photo_Image = File.ReadAllBytes(path);
                    }
                }
                Data.DB_Entities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public static void GetImageDataForUsers()
        {
            try
            {
                var list = Data.DB_Entities.GetContext().Users.ToList();
                foreach (var item in list)
                {
                    string path = Directory.GetCurrentDirectory() + @"\img\" + item.Photo_Name;
                    if (File.Exists(path))
                    {
                        item.Photo_Image = File.ReadAllBytes(path);
                    }
                }
                Data.DB_Entities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
