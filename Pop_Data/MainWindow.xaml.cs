using System.Windows;

namespace Pop_Data
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly VM vm = VM.Instance;

        private readonly string[] SortByList = { "City", "Population" };
        private readonly string[] OrderByList = { "Ascending", "Descending" };

        const string SELECT_CITY_MSG = "Please select a city!";

        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            foreach (string option in SortByList)
            {
                SortBy.Items.Add(option);
            }

            foreach (string option in OrderByList)
            {
                OrderBy.Items.Add(option);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            EditWindow ew = new EditWindow(false) { Owner = this };
            ew.ShowDialog();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedCity != null)
            {
                EditWindow ew = new EditWindow(true) { Owner = this };
                ew.ShowDialog();
            }
            else MessageBox.Show(SELECT_CITY_MSG);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedCity != null)
            {
                string message = $"Are you sure you want to delete this city ({vm.SelectedCity.CityName})?";

                MessageBoxResult result = MessageBox.Show(message, "Delete City", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    vm.Delete();
                }
            }
            else MessageBox.Show(SELECT_CITY_MSG);
        }
    }
}

