using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Pop_Data
{
    internal class VM : INotifyPropertyChanged
    {
        readonly DB db = DB.Instance;
        List<City> cities = new List<City>();

        #region Singleton
        private static VM vm;

        public static VM Instance
        {
            get
            {
                vm ??= new VM();
                return vm;
            }
        }

        private VM()
        {
            cities = db.Get();
            UpdateCities();
            SetMaxPop();
            CalcTotalPop();
        }
        #endregion

        #region Properties
        public BindingList<City> Cities { get; set; } = new BindingList<City>();

        private string sortBySelected = "City";
        public string SortBySelected
        {
            get { return sortBySelected; }
            set { sortBySelected = value; SortAndUpdateList(); }
        }

        private string orderBySelected = "Ascending";
        public string OrderBySelected
        {
            get { return orderBySelected; }
            set { orderBySelected = value; SortAndUpdateList(); }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set { selectedCity = value; propertyChanged(); }
        }

        private string maxPopCity;

        public string MaxPopCity
        {
            get { return maxPopCity; }
            set { maxPopCity = value; propertyChanged(); }
        }

        private double maxPop;

        public double MaxPop
        {
            get { return maxPop; }
            set { maxPop = value; propertyChanged(); }
        }

        private double totalPop;

        public double TotalPop
        {
            get { return totalPop; }
            set { totalPop = value; propertyChanged(); }
        }
        #endregion

        #region Methods
        public bool IsPresent(City city)
        {
            int cityIndex = cities.FindIndex(c => c.CityName == city.CityName);
            return cityIndex >= 0;
        }

        public void Save(City city)
        {
            bool isSuccess = false;

            if (!city.IsNew)
            {
                isSuccess = db.Update(city);
                if (isSuccess)
                {
                    cities.Remove(SelectedCity);
                    SelectedCity = null;
                }
            }
            else
            {
                if (IsPresent(city))
                {
                    MessageBox.Show($"No changes have been made as {city.CityName} already exists. Try editing the city instead!");
                    SelectedCity = cities[cities.FindIndex(c => c.CityName == city.CityName)];
                }
                else
                    isSuccess = db.Insert(city);
            }

            if (isSuccess)
            {
                cities.Add(city);
                SortAndUpdateList();
                SetMaxPop();
                CalcTotalPop();
            }
        }

        private void UpdateCities()
        {
            Cities.Clear();
            foreach (City city in cities)
            {
                Cities.Add(city);
            }
        }

        public void Delete()
        {
            db.Delete(SelectedCity);
            cities = db.Get();
            SortAndUpdateList();
            SetMaxPop();
            CalcTotalPop();
            SelectedCity = null;
        }

        private void CalcTotalPop()
        {
            TotalPop = Cities.Sum(city => city.Population);
        }

        private void SetMaxPop()
        {
            MaxPop = Cities.Max(city => city.Population);

            foreach (City c in cities)
            {
                if (c.Population == MaxPop)
                {
                    MaxPopCity = c.CityName;
                    break;
                }
            }
        }

        private void SortAndUpdateList()
        {
            if (SortBySelected == "City" && OrderBySelected == "Ascending")
                cities = cities.OrderBy(c => c.CityName).ToList();

            else if (SortBySelected == "City" && OrderBySelected == "Descending")
                cities = cities.OrderByDescending(c => c.CityName).ToList();

            else if (SortBySelected == "Population" && OrderBySelected == "Ascending")
                cities = cities.OrderBy(c => c.Population).ToList();

            else
                cities = cities.OrderByDescending(c => c.Population).ToList();

            UpdateCities();
        }
        #endregion

        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        private void propertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
