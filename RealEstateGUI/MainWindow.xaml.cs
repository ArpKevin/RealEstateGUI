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

using MySql.Data;
using MySql.Data.MySqlClient;

namespace RealEstateGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connStr = "server=localhost;user=root;database=ingatlan;password=";
        public MainWindow()
        {
            InitializeComponent();
            connectToMySql();

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var query = "SELECT * FROM `realestates` inner join categories on categories.id = realestates.categoryId inner join sellers on sellers.id = realestates.sellerId;";
                using (var comm = new MySqlCommand(query, conn))
                {
                    using (var reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reader.GetOrdinal()
                        }
                    }
                }
            }
        }

        private void connectToMySql()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //close connection if you want
            conn.Close();
            Console.WriteLine("Done.");
        }
    }
}