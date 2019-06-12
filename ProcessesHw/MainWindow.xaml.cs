using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ProcessesHw
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
        List<Process> Processes { get; set; } = new List<Process>();
        
        public MainWindow()
        {
            InitializeComponent();
            LoadCurrency();
            itemsDataGrid.ItemsSource = Items;
        }

        public void LoadCurrency()
        {
            Thread.Sleep(500);

            Processes.Clear();
            Processes = Process.GetProcesses().ToList();
            var sortedProcesses = Processes.OrderBy(u => u.ProcessName);
            Items.Clear();

            foreach (var process in sortedProcesses)
            {
                Items.Add(new Item
                {
                    Id = process.Id,
                    ProcessName = process.ProcessName
                });
            }
        }
        
        private void CombleteButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Item item = itemsDataGrid.SelectedItem as Item;
                if (item != null)
                {
                    foreach (var process in Processes)
                    {
                        if(process.Id == item.Id)
                        {
                            process.Kill();
                            LoadCurrency();
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Процесс для завершения не выбран");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
                LoadCurrency();
            }
        }

        private void UpdateButtonClick(object sender, RoutedEventArgs e)
        {
            LoadCurrency();
        }
    }
}