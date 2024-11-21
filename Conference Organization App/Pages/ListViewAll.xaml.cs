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
    public partial class ListViewAll : Page
    {
        public ListViewAll()
        {
            InitializeComponent();

            try
            {
                loadListView();
                loadComboBox();
            }
            catch(Exception ex)
            {
                return;
            }
        }

        private void loadComboBox()
        {
            try
            {
                var combo = Data.DB_Entities.GetContext().UserEventDirections.ToList();
                combo.Insert(0, new Data.UserEventDirections { name = "Все" });
                findByComboBox.ItemsSource = combo;
                findByComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void loadListView()
        {
            try
            {
                listView.ItemsSource = Data.DB_Entities.GetContext().EventsMain.ToList();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void findByTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (findByTextBox.Text.Length > 0)
                {
                    listView.ItemsSource = (from item in Data.DB_Entities.GetContext().EventsMain
                                            where item.EventNames.Name.ToLower().Contains(findByTextBox.Text.ToLower()) ||
                                            item.CountOfDays.ToString().ToLower().Contains(findByTextBox.Text.ToLower()) ||
                                            item.Cities.Name.ToLower().ToString().Contains(findByTextBox.Text.ToLower()) ||
                                            item.UserEventDirections.name.ToLower().ToString().Contains(findByTextBox.Text.ToLower())
                                            select item).ToList();
                }
                else
                {
                    loadListView();
                }
            }
            catch(Exception ex)
            {
                return;
            }
        }

        private void findByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (findByComboBox.SelectedValue.ToString() != "0")
                {
                    listView.ItemsSource = Data.DB_Entities.GetContext().EventsMain
                        .Where(d => d.EventName_Id == findByComboBox.SelectedIndex+1).ToList();
                }
                else
                {
                    loadListView();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime? selectedDate = datePicker.SelectedDate;

                if (selectedDate.HasValue)
                {
                    string dateStr = selectedDate.Value.ToString("yyyy-MM-dd");

                    var selectedByDateTable = Data.DB_Entities.GetContext().EventsMain
                        .Where(d => d.Date.ToString() == dateStr.ToString()).ToList();

                    listView.ItemsSource = selectedByDateTable;
                }
                else
                {
                    loadListView();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void authBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Classes.Manager.frameMaster.Navigate(new Pages.LoginPage());
            }
            catch(Exception ex)
            {
                return;
            }
        }
    }
}
