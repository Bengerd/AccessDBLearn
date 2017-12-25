using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.ComponentModel;
using System.Data.OleDb;
//using System.Windows.Forms;


namespace AccessDBLearn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"E:\\ProgrammingSoftware\\Microsoft Visual Studio\\Projects\\AccessDBLearn\\AccessDBLearn\\Baza_Dannykh2.mdb\"";
        private Baza_Dannykh2DataSet baza_Dannykh2DataSet;
        private Dictionary<string, CollectionViewSource> dbViewsCollection = new Dictionary<string, CollectionViewSource>();
        private Dictionary<string, DataGridColumn> dbTableColumn = new Dictionary<string, DataGridColumn>();
        Baza_Dannykh2DataSetTableAdapters.АбонементTableAdapter adapter;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string viewSource = tablesListBox.SelectedValue.ToString();

            try
            {
                dataGrid.ItemsSource = dbViewsCollection[viewSource].View;

                FieldsComboBox.Items.Clear();
                dbTableColumn.Clear();

                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    dbTableColumn.Add(dataGrid.Columns[i].Header.ToString(), dataGrid.Columns[i]);
                    FieldsComboBox.Items.Add(dataGrid.Columns[i].Header.ToString());
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            baza_Dannykh2DataSet = ((AccessDBLearn.Baza_Dannykh2DataSet)(this.FindResource("baza_Dannykh2DataSet")));

            // Load data into the table Абонемент. You can modify this code as needed.
            AccessDBLearn.Baza_Dannykh2DataSetTableAdapters.АбонементTableAdapter baza_Dannykh2DataSetАбонементTableAdapter = new AccessDBLearn.Baza_Dannykh2DataSetTableAdapters.АбонементTableAdapter();
            baza_Dannykh2DataSetАбонементTableAdapter.Fill(baza_Dannykh2DataSet.Абонемент);
            adapter = baza_Dannykh2DataSetАбонементTableAdapter;
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
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        #region Selected row and cell visualisation

        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        private void SetCurrentRow(int rowIndex)
        {
            dataGrid.SelectionUnit = DataGridSelectionUnit.FullRow;

            dataGrid.SelectedItems.Clear();
            /* set the SelectedItem property */
            object item = dataGrid.Items[rowIndex]; // = Product X
            dataGrid.SelectedItem = item;

            DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            if (row == null)
            {
                /* bring the data item (Product object) into view
                 * in case it has been virtualized away */
                dataGrid.ScrollIntoView(item);
                row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            }
            else
            {
                DataGridCell cell = GetCell(dataGrid, row, 0);
                if (cell != null)
                    cell.Focus();
            }
        }
        private DataGridCell GetCell(DataGrid dataGrid, DataGridRow rowContainer, int column)
        {
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);
                if (presenter == null)
                {
                    /* if the row has been virtualized away, call its ApplyTemplate() method
                     * to build its visual tree in order for the DataGridCellsPresenter
                     * and the DataGridCells to be created */
                    rowContainer.ApplyTemplate();
                    presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);
                }
                if (presenter != null)
                {
                    DataGridCell cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                    if (cell == null)
                    {
                        /* bring the column into view
                         * in case it has been virtualized away */
                        dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[column]);
                        cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                    }
                    return cell;
                }
            }
            return null;
        }

        private void SelectCellByIndex(int rowIndex, int columnIndex)
        {
            dataGrid.SelectionUnit = DataGridSelectionUnit.Cell;
            dataGrid.SelectedCells.Clear();

            object item = dataGrid.Items[rowIndex]; //=Product X
            DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            if (row == null)
            {
                dataGrid.ScrollIntoView(item);
                row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            }
            if (row != null)
            {
                DataGridCell cell = GetCell(dataGrid, row, columnIndex);
                if (cell != null)
                {
                    DataGridCellInfo dataGridCellInfo = new DataGridCellInfo(cell);
                    dataGrid.SelectedCells.Add(dataGridCellInfo);
                    cell.Focus();
                }
            }
        }

        #endregion


        private void firstRowButton_Click(object sender, RoutedEventArgs e)
        {
            SetCurrentRow(0);
            // dataGrid.SelectedIndex = 0;

        }

        private void lastRowButton_Click(object sender, RoutedEventArgs e)
        {
            SetCurrentRow(dataGrid.Items.Count - 2);
            //dataGrid.SelectedIndex = dataGrid.Items.Count - 2;
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

            SetCurrentRow(nextSelectItem);
            //dataGrid.SelectedIndex = nextSelectItem;
        }

        private void FieldsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);

            view.SortDescriptions.Clear();
            //create a new sort order for the sorting that is done lastly
            view.SortDescriptions.Add(new SortDescription(FieldsComboBox.SelectedValue.ToString(), ListSortDirection.Ascending));
            //refresh the view which in turn refresh the grid
            view.Refresh();
        }

        private void exitButton1_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void sqlButton_Click(object sender, RoutedEventArgs e)
        {
            QueryEditor queryEditor = new QueryEditor();
            queryEditor.ShowDialog();
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dataGrid.Items.CurrentItem;

            int itemID = Convert.ToInt32(row[0]);
            SubscriptionsAdd subscriptionsWindow = new SubscriptionsAdd(false, row);
            subscriptionsWindow.ShowDialog();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dataGrid.Items.CurrentItem;

            int itemID = Convert.ToInt32(row[0]);
            string sql = string.Format(@"
delete from Абонемент 
where
    [код Абонемента] = {0}
", row[0].ToString()
);
            OleDbConnection connection = new OleDbConnection(connectionString);

            using (OleDbCommand com = new OleDbCommand(sql, connection))
            {
                connection.Open();

                com.ExecuteNonQuery();
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionsAdd subscriptionsWindow = new SubscriptionsAdd(true);
            subscriptionsWindow.ShowDialog();
        }

        private void findTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string columnName = FieldsComboBox.SelectedValue.ToString();
            string findItem = findTextBox.Text;
            int rowIndex = -1;

            for (int i = 0 ; i < dataGrid.Items.Count; i++)
            {
                DataRowView row = (DataRowView)dataGrid.Items[i];

                if (row[columnName].ToString() == findItem)
                {
                    rowIndex = i;
                    break;
                }
            }
            
            //DataRowView row = (DataRowView)dataGrid.Items[0];
            if (rowIndex == -1)
            {
                MessageBox.Show("Совпадений не найдено");
            }
            else
                SetCurrentRow(rowIndex);

        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
                
        }
    }
}
