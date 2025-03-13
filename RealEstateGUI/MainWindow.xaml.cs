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
                   "SELECT * FROM realestates LEFT JOIN sellers ON realestates.sellerId = sellers.id LEFT JOIN categories on realestates.categoryId = categories.id;";
                using (MySqlCommand dbCommand = new MySqlCommand(query, dbConnection))
                using (MySqlDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int adId = dataReader.GetInt32(0);
                        int categoryId = dataReader.GetInt32(1);
                        int sellerId = dataReader.GetInt32(2);
                        string description = dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3);
                        DateTime createdAt = dataReader.GetDateTime(4);
                        bool freeOfCharge = dataReader.GetBoolean(5);
                        string imageUrl = dataReader.IsDBNull(6) ? string.Empty : dataReader.GetString(6);
                        int area = dataReader.GetInt32(7);
                        int rooms = dataReader.GetInt32(8);
                        int floors = dataReader.GetInt32(9);
                        string latlong = dataReader.GetString(10);
                        int sellerIdAgain = dataReader.GetInt32(11);
                        string sellerName = dataReader.GetString(12);
                        string sellerPhone = dataReader.GetString(13);
                        int categoryIdAgain = dataReader.GetInt32(14);
                        string categoryName = dataReader.GetString(15);

                        Seller seller = new Seller(sellerId, sellerName, sellerPhone);
                        Category category = new Category(categoryId, categoryName);
                        Ad ad = new Ad(adId, rooms, latlong, floors, area, description, freeOfCharge, imageUrl, createdAt, seller, category);

                        adList.Add(ad);
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
            listingsCountLabel.Content = adList.Count(ad => ad.Seller.SellerName == listingBox.SelectedItem);
        }
    }
}