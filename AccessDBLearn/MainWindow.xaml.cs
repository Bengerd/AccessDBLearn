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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace AccessDBLearn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Baza_Dannykh2DataSet baza_Dannykh2DataSet;
        private Dictionary<string, CollectionViewSource> dbViewsCollection = new Dictionary<string, CollectionViewSource>();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string viewSource = tablesListBox.SelectedValue.ToString();

            dataGrid.ItemsSource = dbViewsCollection[viewSource].View;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            baza_Dannykh2DataSet = ((AccessDBLearn.Baza_Dannykh2DataSet)(this.FindResource("baza_Dannykh2DataSet")));

            // Load data into the table Абонемент. You can modify this code as needed.
            AccessDBLearn.Baza_Dannykh2DataSetTableAdapters.АбонементTableAdapter baza_Dannykh2DataSetАбонементTableAdapter = new AccessDBLearn.Baza_Dannykh2DataSetTableAdapters.АбонементTableAdapter();
            baza_Dannykh2DataSetАбонементTableAdapter.Fill(baza_Dannykh2DataSet.Абонемент);
            System.Windows.Data.CollectionViewSource абонементViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("абонементViewSource"));
            абонементViewSource.View.MoveCurrentToFirst();
            dbViewsCollection.Add("Абонемент", абонементViewSource);

            // Load data into the table Данные_спортклуба. You can modify this code as needed.
            AccessDBLearn.Baza_Dannykh2DataSetTableAdapters.Данные_спортклубаTableAdapter baza_Dannykh2DataSetДанные_спортклубаTableAdapter = new AccessDBLearn.Baza_Dannykh2DataSetTableAdapters.Данные_спортклубаTableAdapter();
            baza_Dannykh2DataSetДанные_спортклубаTableAdapter.Fill(baza_Dannykh2DataSet.Данные_спортклуба);
            System.Windows.Data.CollectionViewSource данные_спортклубаViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("данные_спортклубаViewSource")));
            данные_спортклубаViewSource.View.MoveCurrentToFirst();
            dbViewsCollection.Add("Данные спортклуба", данные_спортклубаViewSource);

            // Load data into the table Список_спортклубов. You can modify this code as needed.
            AccessDBLearn.Baza_Dannykh2DataSetTableAdapters.Список_спортклубовTableAdapter baza_Dannykh2DataSetСписок_спортклубовTableAdapter = new AccessDBLearn.Baza_Dannykh2DataSetTableAdapters.Список_спортклубовTableAdapter();
            baza_Dannykh2DataSetСписок_спортклубовTableAdapter.Fill(baza_Dannykh2DataSet.Список_спортклубов);
            System.Windows.Data.CollectionViewSource список_спортклубовViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("список_спортклубовViewSource")));
            список_спортклубовViewSource.View.MoveCurrentToFirst();
            dbViewsCollection.Add("Список спортклубов", список_спортклубовViewSource);

            // Load data into the table Тренерский_состав. You can modify this code as needed.
            AccessDBLearn.Baza_Dannykh2DataSetTableAdapters.Тренерский_составTableAdapter baza_Dannykh2DataSetТренерский_составTableAdapter = new AccessDBLearn.Baza_Dannykh2DataSetTableAdapters.Тренерский_составTableAdapter();
            baza_Dannykh2DataSetТренерский_составTableAdapter.Fill(baza_Dannykh2DataSet.Тренерский_состав);
            System.Windows.Data.CollectionViewSource тренерский_составViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("тренерский_составViewSource")));
            тренерский_составViewSource.View.MoveCurrentToFirst();
            dbViewsCollection.Add("Тренерский состав", тренерский_составViewSource);

            try
            {
                foreach (DataTable item in baza_Dannykh2DataSet.Tables)
                {
                    tablesListBox.Items.Add(item.TableName.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void firstRowButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.SelectedIndex = 0;
        }

        private void lastRowButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.SelectedIndex = dataGrid.Items.Count - 2;
        }

        private void customRowButton_Click(object sender, RoutedEventArgs e)
        {
            int selectItemModifer = Convert.ToInt32(selectItemModifierTextBox.Text);
            int tableRowCount = dataGrid.Items.Count - 2;
            int nextSelectItem = 0;

            if ((dataGrid.SelectedIndex + selectItemModifer) < 0)
                nextSelectItem = 0;
            else if ((dataGrid.SelectedIndex + selectItemModifer) > tableRowCount)
                nextSelectItem = tableRowCount;
            else
                nextSelectItem = dataGrid.SelectedIndex + selectItemModifer;

            dataGrid.SelectedIndex = nextSelectItem;
        }
    }
}
