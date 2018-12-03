using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementShopDB
{
    public partial class MainForm : Form
    {
        private const string connectionStr = @"Data Source=DESKTOP-CHJ1K79\SQLEXPRESS;Initial Catalog=ShopDB;Integrated Security=True";
        private const int SHIFT_OF_CONTROLS = 50;

        public MainForm()
        {
            InitializeComponent();

            CustomizeComboBoxAccessoryTypes();
            CustomizeComboBoxAccessoryProducer();
            CustomizeGridViews();
        }

        private void CustomizeGridViews()
        {
            dataGridViewOfAccessory.RowHeadersVisible = false;
            dataGridViewOfClients.RowHeadersVisible = false;
            dataGridViewOfOrders.RowHeadersVisible = false;
            dataGridViewOfStockStores.RowHeadersVisible = false;
            dataGridViewOfAccessoriesOnInsertOrderPage.RowHeadersVisible = false;
            dataGridViewOfClientsOnInsertClientPage.RowHeadersVisible = false;
            dataGridViewOfAccessoriesOnInsertAccessoryPage.RowHeadersVisible = false;
        }

        private void CustomizeComboBoxAccessoryTypes()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(SqlScripts.SELECT_ACCESSORY_TYPE, sqlConnection);
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlCommand.ExecuteReader());
                foreach (DataRow row in dataTable.Rows)
                {
                    comboBoxOfAccessoryType.Items.Add(row[0]);
                }
                comboBoxOfAccessoryType.SelectedItem = comboBoxOfAccessoryType.Items[0];
            }
        }

        private void CustomizeComboBoxAccessoryProducer()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(SqlScripts.SELECT_PRODUCERS, sqlConnection);
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlCommand.ExecuteReader());
                foreach (DataRow row in dataTable.Rows)
                {
                    string fullRecord = "";
                    foreach (object record in row.ItemArray)
                    {
                        fullRecord += record.ToString() + " ";
                    }
                    comboBoxOfAccessProducer.Items.Add(fullRecord);
                }
                comboBoxOfAccessProducer.SelectedItem = comboBoxOfAccessProducer.Items[0];
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "shopDBDataSet.AccessoryType". При необходимости она может быть перемещена или удалена.
            this.accessoryTypeTableAdapter.Fill(this.shopDBDataSet.AccessoryType);
            //init started data of gridview on selected page
            tabControl_OperationOnDb_SelectedIndexChanged(new object(), new EventArgs());
        }

        public static void ClearGridView(DataGridView dataGridView)
        {
            if (dataGridView.RowCount > 1)
            {
                for (int i = dataGridView.RowCount - 2; i >= 0; i--)
                {
                    dataGridView.Rows.RemoveAt(i);
                }
                dataGridView.Refresh();
            }
        }

        private void FillGridViewOfTableFromDb(SqlConnection sqlConnection, SqlCommand sqlCommand, DataGridView dataGridView)
        {
            sqlCommand.Connection = sqlConnection;
            dataGridView.ReadOnly = true;
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlCommand.ExecuteReader());
            dataGridView.DataSource = dataTable;
        }

        private void tabControl_OperationOnDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl_OperationOnDb.SelectedTab.Name)
            {
                case "tabPageOfSelect":
                    FillDataGridViewsOfSelectData();
                    break;
                case "tabPageOfInsert":
                    tabControlOfInsertingDb_SelectedIndexChanged(new object(), new EventArgs());
                    break;
            }

        }

        private void FillDataGridViewsOfSelectData()
        {
            ClearGridView(dataGridViewOfClients);
            ClearGridView(dataGridViewOfAccessory);
            ClearGridView(dataGridViewOfOrders);
            ClearGridView(dataGridViewOfStockStores);
            using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
            {
                sqlConnection.Open();

                //fill clients
                FillGridViewOfTableFromDb(sqlConnection, new SqlCommand(SqlScripts.SELECT_CLIENTS), dataGridViewOfClients);
                AutoResizeGridView(dataGridViewOfClients);
                //fill accessories                   
                FillGridViewOfTableFromDb(sqlConnection, new SqlCommand(SqlScripts.SELECT_ACCESSORIES), dataGridViewOfAccessory);
                AutoResizeGridView(dataGridViewOfAccessory);
                //fill orders          
                FillGridViewOfTableFromDb(sqlConnection, new SqlCommand(SqlScripts.SELECT_ORDERS), dataGridViewOfOrders);
                AutoResizeGridView(dataGridViewOfOrders);
                ////fill stockstores
                FillGridViewOfTableFromDb(sqlConnection, new SqlCommand(SqlScripts.SELECT_STOCK_STORES), dataGridViewOfStockStores);
                AutoResizeGridView(dataGridViewOfStockStores);
                sqlConnection.Close();
            }
        }

        private void AutoResizeGridView(DataGridView dataGrid)
        {
            var width = dataGrid.Columns.GetColumnsWidth(DataGridViewElementStates.None);
            var height = dataGrid.Rows.GetRowsHeight(DataGridViewElementStates.None);
            height += dataGrid.ColumnHeadersHeight;
            dataGrid.ClientSize = new Size(width, height);
        }

        private void btnAddClientToDB_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
            {
                sqlConnection.Open();
                insertClient(sqlConnection, textBoxOfFNameClientOnInsertClientPage.Text, textBoxOfLNameClientOnInsertClientPage.Text, textBoxOfPhoneClientOnInsertClientPage.Text);
            }
            MessageBox.Show("Клиент успешно добавлен.");
            //update table of data gridview clients
            tabControlOfInsertingDb_SelectedIndexChanged(new object(), new EventArgs());
        }

        private int insertClient(SqlConnection sqlConnection, string FnameClient, string LnameClient, string Phone)
        {
            SqlCommand sqlCommandInsertClient = new SqlCommand("InsertClient", sqlConnection);
            sqlCommandInsertClient.CommandType = CommandType.StoredProcedure;
            sqlCommandInsertClient.Parameters.Add(new SqlParameter("@FName", FnameClient));
            sqlCommandInsertClient.Parameters.Add(new SqlParameter("@LName", LnameClient));
            sqlCommandInsertClient.Parameters.Add(new SqlParameter("@Phone", Phone));
            var returnParameter = sqlCommandInsertClient.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            sqlCommandInsertClient.ExecuteNonQuery();
            return (int)returnParameter.Value;
        }

        private void insertGoods(SqlConnection sqlConnection, int idOrder, int idAccessory, int countAccessory)
        {
            SqlCommand sqlCommandInsertOrder = new SqlCommand("InsertGoods", sqlConnection);
            sqlCommandInsertOrder.CommandType = CommandType.StoredProcedure;
            sqlCommandInsertOrder.CommandType = CommandType.StoredProcedure;
            sqlCommandInsertOrder.Parameters.Add(new SqlParameter("@OrderId", idOrder));
            sqlCommandInsertOrder.Parameters.Add(new SqlParameter("@AccessoryId", idAccessory));
            sqlCommandInsertOrder.Parameters.Add(new SqlParameter("@CountAccessory", countAccessory));
            sqlCommandInsertOrder.ExecuteNonQuery();
        }

        private int insertOrder(SqlConnection sqlConnection, int ClientId, string City, string Street, string NumbOfStreet)
        {
            SqlCommand sqlCommandInsertOrder = new SqlCommand("InsertOrder", sqlConnection);
            sqlCommandInsertOrder.CommandType = CommandType.StoredProcedure;
            sqlCommandInsertOrder.Parameters.Add(new SqlParameter("@ClientId", ClientId));
            sqlCommandInsertOrder.Parameters.Add(new SqlParameter("@City", City));
            sqlCommandInsertOrder.Parameters.Add(new SqlParameter("@Street", Street));
            sqlCommandInsertOrder.Parameters.Add(new SqlParameter("@NumberOfStreet", NumbOfStreet));
            var returnParameter = sqlCommandInsertOrder.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            sqlCommandInsertOrder.ExecuteNonQuery();
            return (int)returnParameter.Value;
        }

        private void tabControlOfInsertingDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControlOfInsertingDb.SelectedTab.Name)
            {
                case "tabPageOfInsertOrder":
                    ClearGridView(dataGridViewOfAccessoriesOnInsertOrderPage);
                    using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                    {
                        sqlConnection.Open();
                        //fill accessories
                        SqlCommand sqlCommand = new SqlCommand(SqlScripts.SELECT_ACCESSORIES);
                        sqlCommand.Connection = sqlConnection;
                        dataGridViewOfAccessoriesOnInsertOrderPage.ReadOnly = false;
                        DataTable dataTable = new DataTable();
                        //add checkbox field for choose accessory
                        dataTable.Columns.Add(new DataColumn("Ваш выбор", typeof(bool)));
                        dataTable.Load(sqlCommand.ExecuteReader());
                        dataTable.Columns.Add(new DataColumn("Количество аксессуаров для покупки", typeof(int)));
                        //fill default value - 1 column of quant access
                        foreach (DataRow row in dataTable.Rows)
                        {
                            row.SetField(dataTable.Columns.Count - 1, 1);
                        }

                        dataGridViewOfAccessoriesOnInsertOrderPage.DataSource = dataTable;

                        AutoResizeGridView(dataGridViewOfAccessoriesOnInsertOrderPage);
                    }
                    break;
                case "tabPageOfInsertClient":
                    ClearGridView(dataGridViewOfClientsOnInsertClientPage);
                    using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                    {
                        sqlConnection.Open();
                        //fill clients
                        FillGridViewOfTableFromDb(sqlConnection, new SqlCommand(SqlScripts.SELECT_CLIENTS), dataGridViewOfClientsOnInsertClientPage);
                        AutoResizeGridView(dataGridViewOfClientsOnInsertClientPage);
                    }
                    break;
                case "tabPageInsertAccessory":
                    ClearGridView(dataGridViewOfAccessoriesOnInsertAccessoryPage);
                    using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                    {
                        sqlConnection.Open();
                        //fill accessories on accessories insert page
                        FillGridViewOfTableFromDb(sqlConnection, new SqlCommand(SqlScripts.SELECT_ACCESSORIES), dataGridViewOfAccessoriesOnInsertAccessoryPage);
                        AutoResizeGridView(dataGridViewOfAccessoriesOnInsertAccessoryPage);
                    }
                    break;
            }
        }

        private void btnInsertAccessory_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
            {
                sqlConnection.Open();

                SqlCommand sqlCommandInsertClient = new SqlCommand("InsertAccessory", sqlConnection);
                sqlCommandInsertClient.CommandType = CommandType.StoredProcedure;
                sqlCommandInsertClient.Parameters.Add(new SqlParameter("@AccessName", textBoxOfAccessName.Text));
                sqlCommandInsertClient.Parameters.Add(new SqlParameter("@Color", textBoxOfAccessColor.Text));
                sqlCommandInsertClient.Parameters.Add(new SqlParameter("@TypeOfLink", textBoxOfTypeLink.Text));
                sqlCommandInsertClient.Parameters.Add(new SqlParameter("@ProducerId", comboBoxOfAccessProducer.SelectedIndex + 1));
                sqlCommandInsertClient.Parameters.Add(new SqlParameter("@AccessTypeId", comboBoxOfAccessoryType.SelectedIndex + 1));
                sqlCommandInsertClient.ExecuteNonQuery();
            }
            MessageBox.Show("Аксессуар успешно добавлен.");
            //update table of data gridview clients
            tabControlOfInsertingDb_SelectedIndexChanged(new object(), new EventArgs());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для добавления аксессуаров в заказ - выберете их в таблице.");
        }

        private void btnDoOrder_Click(object sender, EventArgs e)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
            {
                sqlConnection.Open();
                //insert client
                int resultIdClient = insertClient(sqlConnection, textBoxOfFNameClientOnInsertOrderPage.Text,
                    textBoxOfLNameClientOnInsertOrderPage.Text, textBoxOfPhoneClientOnInsertOrderPage.Text);
                //insert order
                int resultIdOrder = insertOrder(sqlConnection, resultIdClient, textBoxOfCityOnInsertOrderPage.Text,
                    textBoxOfStreetOnInsertOrderPage.Text, textBoxOfNumbStreetOnInsertOrderPage.Text);
                //insert goods
                foreach (DataGridViewRow row in dataGridViewOfAccessoriesOnInsertOrderPage.Rows)
                {
                    try
                    {
                        bool isChosenAccessory = (bool)row.Cells[0].Value;
                        if (isChosenAccessory)
                        {
                            insertGoods(sqlConnection, resultIdOrder, (int)row.Cells[1].Value,
                            (int)row.Cells[row.Cells.Count - 1].Value);
                        }
                    }
                    catch (Exception) { }                  
                    
                }

            }

        }

        private void dataGridViewOnInsertPageOfAccessories_Resize(object sender, EventArgs e)
        {
            groupBoxOfDataClient.Top = dataGridViewOfAccessoriesOnInsertOrderPage.Bottom + SHIFT_OF_CONTROLS;
        }

        private void groupBoxOfDataClient_Move(object sender, EventArgs e)
        {
            groupBoxOfDataDelivery.Top = groupBoxOfDataClient.Bottom + SHIFT_OF_CONTROLS;
        }

        private void groupBoxOfDataDelivery_Move(object sender, EventArgs e)
        {
            btnDoOrder.Top = groupBoxOfDataDelivery.Bottom + SHIFT_OF_CONTROLS;
        }
    }
}
