using System.Data;
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
using MySql.Data.MySqlClient;
using static RealEstateGUI.MainWindow;

namespace RealEstateGUI
{
    public partial class MainWindow : Window
    {
        List<Ad> adList = new List<Ad>();
        List<int> propertyCount = new List<int>();

        public MainWindow()
        {
            InitializeComponent();

            string dbConnectionString = "server=localhost;user=root;password=;database=realestate";

            using (MySqlConnection dbConnection = new MySqlConnection(dbConnectionString))
            {
                dbConnection.Open();

                string query =
                    "SELECT sellers.id, sellers.name, sellers.phone, COUNT(realestates.sellerId) AS properties FROM sellers LEFT JOIN realestates ON realestates.sellerId = sellers.id GROUP BY sellers.id, sellers.name, sellers.phone;";
                using (MySqlCommand dbCommand = new MySqlCommand(query, dbConnection))
                using (MySqlDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int adId = dataReader.GetInt32(0);
                        int rooms = dataReader.GetInt32(1);
                        string latlong = dataReader.GetString(2);
                        int floors = dataReader.GetInt32(3);
                        int area = dataReader.GetInt32(4);
                        string description = dataReader.GetString(5);
                        bool freeOfCharge = dataReader.GetBoolean(6);
                        string imageUrl = dataReader.GetString(7);
                        DateTime createdAt = dataReader.GetDateTime(8);

                        int sellerId = dataReader.GetInt32(9);
                        string sellerName = dataReader.GetString(10);
                        string sellerPhone = dataReader.GetString(11);

                        int categoryId = dataReader.GetInt32(12);
                        string categoryName = dataReader.GetString(13);

                        Seller seller = new Seller(sellerId, sellerName, sellerPhone);
                        Category category = new Category(categoryId, categoryName);

                        Ad ad = new Ad(adId, rooms, latlong, floors, area, description, freeOfCharge, imageUrl, createdAt, seller, category);
                        int propertiesCount = dataReader.GetInt32(4);

                        adList.Add(ad);
                        propertyCount.Add(propertiesCount);
                    }
                }
                dbConnection.Close();
            }

            var adNames = adList.Select(ad => ad.Seller.SellerName).ToList();
            foreach (var name in adNames)
            {
                listingBox.Items.Add(name);
            }
            listingBox.SelectedIndex = 0;
        }

        private void listingBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sellerNameLabel.Content = listingBox.SelectedItem;

            var phoneNumber = adList.Where(ad => ad.Seller.SellerName == listingBox.SelectedItem).Select(ad => ad.Seller.SellerPhone).First();
            sellerPhoneLabel.Content = $"{phoneNumber}";
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedadIndex = adList.FindIndex(ad => ad.Seller.SellerName == listingBox.SelectedItem);

            int adPropertiesCount = propertyCount[selectedadIndex];

            listingsCountLabel.Content = $"{adPropertiesCount}";
        }
    }
}