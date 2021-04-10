using System.Windows;

namespace Pop_Data
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {

        readonly VM vm = VM.Instance;
        readonly City city = new City();

        public EditWindow(bool isCityEdit)
        {
            InitializeComponent();
            DataContext = city;

            if (isCityEdit)
            {
                Title = "Edit City";
                city.CityName = vm.SelectedCity.CityName;
                city.Population = vm.SelectedCity.Population;
                city.IsNew = false;

                CityNameTbx.IsEnabled = false;
            }
            else Title = "Add City";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            vm.Save(city);
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
