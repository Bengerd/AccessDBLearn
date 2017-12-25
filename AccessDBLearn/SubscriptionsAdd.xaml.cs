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
using System.Data;
using System.Data.OleDb;

namespace AccessDBLearn
{
    /// <summary>
    /// Interaction logic for SubscriptionsAdd.xaml
    /// </summary>
    public partial class SubscriptionsAdd : Window
    {
        private string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"E:\\ProgrammingSoftware\\Microsoft Visual Studio\\Projects\\AccessDBLearn\\AccessDBLearn\\Baza_Dannykh2.mdb\"";
        private DataRowView row;
        private bool isAddNew = true;

        Baza_Dannykh2DataSet baza_Dannykh2DataSet;
        AccessDBLearn.Baza_Dannykh2DataSetTableAdapters.АбонементTableAdapter baza_Dannykh2DataSetАбонементTableAdapter;


        public SubscriptionsAdd(bool isAddNew, DataRowView row = null)
        {
            InitializeComponent();

            this.row = row;
            this.isAddNew = isAddNew;
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = "";

            if (isAddNew)
            {
                sql = string.Format(@"
insert into Абонемент
(
    [фамилия клиента]
    ,[возраст клиента]
    ,[рост клиента]
    ,[вес клиента]
    ,[тренер]
)
values
(
    '{0}'
    ,{1}
    ,{2}
    ,{3}
    ,'{4}'
)
", lastNameTextBox.Text
   ,ageTextBox.Text
   ,heightTextBox.Text
   ,weightTextBox.Text
   ,trainerTextBox.Text
);
            }
            else
            {
                sql = string.Format(@"
update 
    Абонемент 
set
    [фамилия клиента] = '{0}'
    ,[возраст клиента] = {1}
    ,[рост клиента] = {2}
    ,[вес клиента] = {3}
    ,[тренер] = '{4}'
where
    [код Абонемента] = {5}
"  , lastNameTextBox.Text
   , ageTextBox.Text
   , heightTextBox.Text
   , weightTextBox.Text
   , trainerTextBox.Text
   , row[0].ToString()
);
            }

            OleDbConnection connection = new OleDbConnection(connectionString);

            using (OleDbCommand com = new OleDbCommand(sql, connection))
            {
                connection.Open();

                com.ExecuteNonQuery();
            }

            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            baza_Dannykh2DataSet = ((AccessDBLearn.Baza_Dannykh2DataSet)(this.FindResource("baza_Dannykh2DataSet")));
            baza_Dannykh2DataSetАбонементTableAdapter = new AccessDBLearn.Baza_Dannykh2DataSetTableAdapters.АбонементTableAdapter();
            baza_Dannykh2DataSetАбонементTableAdapter.Fill(baza_Dannykh2DataSet.Абонемент);
            */
            if (!isAddNew)
            {
                codeTextBox.Text = row[0].ToString();
                lastNameTextBox.Text = row[1].ToString();
                ageTextBox.Text = row[2].ToString();
                heightTextBox.Text = row[3].ToString();
                weightTextBox.Text = row[4].ToString();
                trainerTextBox.Text = row[5].ToString();
            }
        }
    }
}
