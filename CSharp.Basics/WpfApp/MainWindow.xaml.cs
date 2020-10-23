using Models;
using Services.DbService.Services;
using Services.InMemeoryService;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ICrudAsyncService<Person> Service { get; } = new PeopleInMemoryAsyncService();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public IEnumerable<Person> People { get; set; }
        public Person SelectedPerson { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void Button_Click_Refresh(object sender = null, RoutedEventArgs e = null)
        {
            People = await Service.ReadAsync();
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(People)));
        }

        private async void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (SelectedPerson == null)
                return;
            await Service.DeleteAsync(SelectedPerson.Id);
            Button_Click_Refresh();
        }
        private async void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            if (SelectedPerson == null)
                return;
            var dialog = new PersonWindow(SelectedPerson);
            dialog.ShowDialog();
            
                await Service.UpdateAsync(SelectedPerson);
            
        }
    }
}
