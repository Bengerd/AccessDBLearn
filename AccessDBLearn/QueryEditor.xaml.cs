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
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Data;


namespace AccessDBLearn
{
    /// <summary>
    /// Interaction logic for QueryEditor.xaml
    /// </summary>
    public partial class QueryEditor : Window
    {
        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\Baza_Dannykh2.mdb;Persist Security Info = False;";
        public QueryEditor()
        {
            InitializeComponent();
        }

        private void executeButton_Click(object sender, RoutedEventArgs e)
        {
            string query = queryTextBox.Text;

            try
            {
                OleDbConnection connection = new OleDbConnection(connectionString);

                using (OleDbCommand com = new OleDbCommand(queryTextBox.Text, connection))
                {
                    connection.Open();

                    OleDbDataReader reader = com.ExecuteReader();

                    DataTable table = new DataTable();
                    table.Load(reader);
                    dataGrid.ItemsSource = table.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            queryTextBox.Clear();
        }
    }
}
