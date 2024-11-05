using System;
using System.Collections.Generic;
using System.Data;
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
    }
}
