using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace RealEstateGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Ad> AdsList { get; set; } = new();
        private Ad _selectedAd;

        public Ad SelectedAd
        {
            get => _selectedAd;
            set
            {
                _selectedAd = value;
                OnPropertyChanged(nameof(SelectedAd));
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            List<Ad> adsList = new();

            foreach (var item in File.ReadAllLines(@"..\..\..\src\realestates.csv").Skip(1))
            {
                adsList.Add(new(item));
            }

            foreach (var ad in adsList)
            {
                AdsList.Add(ad);
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        private void LoadAds(object sender, RoutedEventArgs e)
        {
            AdsList.Clear();

            foreach (var item in File.ReadAllLines(@"..\..\..\src\realestates.csv").Skip(1))
            {
                AdsList.Add(new Ad(item));
            }

            if (AdsList.Count > 0)
            {
                SelectedAd = AdsList[0];
            }

            OnPropertyChanged(nameof(AdsList));
            OnPropertyChanged(nameof(SelectedAd));
        }
    }
}