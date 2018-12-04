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
            CustomizeGridViewsOfDeleteData();
        }

        private void CustomizeGridViewsOfDeleteData()
        {
            DataGridViewCheckBoxColumn gridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            dataGridViewOfClientsOnDeleteClientsPage.Columns.Insert(0, gridViewCheckBoxColumn);
            gridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            dataGridViewOfAccessOnDeleteAccessPage.Columns.Insert(0, gridViewCheckBoxColumn);
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
                case "tabPageOfUpdate":
                    tabControlOfUpdate_SelectedIndexChanged(new object(), new EventArgs());
                    break;
                case "tabPageOfDelete":
                    tabControlOfDelete_SelectedIndexChanged(new object(), new EventArgs());
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
                try
                {
                    insertClient(sqlConnection, textBoxOfFNameClientOnInsertClientPage.Text, textBoxOfLNameClientOnInsertClientPage.Text, textBoxOfPhoneClientOnInsertClientPage.Text);
                }
                catch(Exception)
                {
                    MessageBox.Show("Клиент с таким номером уже существует.");
                    return;
                }
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

        private void updateClient(SqlConnection sqlConnection, int Id, string FnameClient, string LnameClient, string Phone)
        {
            SqlCommand sqlCommandInsertClient = new SqlCommand("UpdateClient", sqlConnection);
            sqlCommandInsertClient.CommandType = CommandType.StoredProcedure;
            sqlCommandInsertClient.Parameters.Add(new SqlParameter("@ClientId", Id));
            sqlCommandInsertClient.Parameters.Add(new SqlParameter("@FName", FnameClient));
            sqlCommandInsertClient.Parameters.Add(new SqlParameter("@LName", LnameClient));
            sqlCommandInsertClient.Parameters.Add(new SqlParameter("@Phone", Phone));            
            sqlCommandInsertClient.ExecuteNonQuery();            
        }

        private void updateAccessory(SqlConnection sqlConnection, int Id, string AccessName, string Color, string TypeOfLink)
        {
            SqlCommand sqlCommandInsertClient = new SqlCommand("UpdateAccessory", sqlConnection);
            sqlCommandInsertClient.CommandType = CommandType.StoredProcedure;
            sqlCommandInsertClient.Parameters.Add(new SqlParameter("@AccessId", Id));
            sqlCommandInsertClient.Parameters.Add(new SqlParameter("@AccessName", AccessName));
            sqlCommandInsertClient.Parameters.Add(new SqlParameter("@Color", Color));
            sqlCommandInsertClient.Parameters.Add(new SqlParameter("@TypeOfLink", TypeOfLink));
            sqlCommandInsertClient.ExecuteNonQuery();
        }

        private void deleteAccessory(SqlConnection sqlConnection, int Id)
        {
            SqlCommand sqlCommandInsertClient = new SqlCommand("DeleteAccessory", sqlConnection);
            sqlCommandInsertClient.CommandType = CommandType.StoredProcedure;
            sqlCommandInsertClient.Parameters.Add(new SqlParameter("@AccessId", Id));
            sqlCommandInsertClient.ExecuteNonQuery();
        }

        private void deleteClient(SqlConnection sqlConnection, int Id)
        {
            SqlCommand sqlCommandInsertClient = new SqlCommand("DeleteClient", sqlConnection);
            sqlCommandInsertClient.CommandType = CommandType.StoredProcedure;
            sqlCommandInsertClient.Parameters.Add(new SqlParameter("@ClientId", Id));
            sqlCommandInsertClient.ExecuteNonQuery();
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
                MessageBox.Show("Заказ успешно оформлен.");
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

        private void tabControlOfUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControlOfUpdate.SelectedTab.Name)
            {
                case "tabPageOfUpdateClient":
                    //fill clients
                    using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                    {
                        sqlConnection.Open();                        
                        SqlCommand sqlCommand = new SqlCommand(SqlScripts.SELECT_CLIENTS);
                        sqlCommand.Connection = sqlConnection;                        
                        DataTable dataTable = new DataTable();
                        //add checkbox field for update rows
                        dataTable.Columns.Add(new DataColumn("Обновить данную строку", typeof(bool)));                        
                        dataTable.Load(sqlCommand.ExecuteReader());
                        dataGridViewOfClientsOnUpdateClientPage.DataSource = dataTable;
                    }
                    dataGridViewOfClientsOnUpdateClientPage.ReadOnly = false;
                    foreach (DataGridViewColumn col in dataGridViewOfClientsOnUpdateClientPage.Columns)
                    {
                        col.ReadOnly = true;
                    }
                    dataGridViewOfClientsOnUpdateClientPage.RowHeadersVisible = false;
                    AutoResizeGridView(dataGridViewOfClientsOnUpdateClientPage);
                    break;
                case "tabPageOfUpdateAccessory":
                    //fill accessories
                    using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                    {
                        sqlConnection.Open();
                        SqlCommand sqlCommand = new SqlCommand(SqlScripts.SELECT_ACCESSORIES);
                        sqlCommand.Connection = sqlConnection;
                        DataTable dataTable = new DataTable();
                        //add checkbox field for update rows
                        dataTable.Columns.Add(new DataColumn("Обновить данную строку", typeof(bool)));
                        dataTable.Load(sqlCommand.ExecuteReader());
                        dataGridViewOfAccessoriesOnUpdateAccessoryPage.DataSource = dataTable;
                    }
                    dataGridViewOfAccessoriesOnUpdateAccessoryPage.ReadOnly = false;
                    foreach (DataGridViewColumn col in dataGridViewOfAccessoriesOnUpdateAccessoryPage.Columns)
                    {
                        col.ReadOnly = true;
                    }
                    dataGridViewOfAccessoriesOnUpdateAccessoryPage.RowHeadersVisible = false;
                    AutoResizeGridView(dataGridViewOfAccessoriesOnUpdateAccessoryPage);
                    break;
            }
        }

        private void dataGridViewOfClientsOnUpdateClientPage_Resize(object sender, EventArgs e)
        {
            btnUpdatesClients.Top = dataGridViewOfClientsOnUpdateClientPage.Bottom + 20;
        }

        private void dataGridViewOfClientsOnUpdateClientPage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //switch on check box of updating
            if(e.ColumnIndex == 0)
            {                
                dataGridViewOfClientsOnUpdateClientPage.Rows[e.RowIndex].Cells[0].Value = true;
                //cell of fname
                dataGridViewOfClientsOnUpdateClientPage.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                //cell of lname
                dataGridViewOfClientsOnUpdateClientPage.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                //cell of phone
                dataGridViewOfClientsOnUpdateClientPage.Rows[e.RowIndex].Cells[4].ReadOnly = false;
            }
        }

        private void btnUpdatesClients_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
            {
                sqlConnection.Open();
                foreach (DataGridViewRow row in dataGridViewOfClientsOnUpdateClientPage.Rows)
                {
                    try
                    {
                        bool isUpdatedRow = (bool)row.Cells[0].Value;
                        if (isUpdatedRow)
                        {
                            updateClient(sqlConnection, (int)row.Cells[1].Value,
                                (string)row.Cells[2].Value, (string)row.Cells[3].Value,
                                (string)row.Cells[4].Value);
                        }
                    } catch (Exception) { }
                }
                MessageBox.Show("Данные о пользователях успешно обновлены");
                tabControlOfUpdate_SelectedIndexChanged(new object(), new EventArgs());
            }
        }

        private void dataGridViewOfAccessoriesOnUpdateAccessoryPage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //switch on check box of updating
            if (e.ColumnIndex == 0)
            {
                dataGridViewOfAccessoriesOnUpdateAccessoryPage.Rows[e.RowIndex].Cells[0].Value = true;
                //cell of name
                dataGridViewOfAccessoriesOnUpdateAccessoryPage.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                //cell of color
                dataGridViewOfAccessoriesOnUpdateAccessoryPage.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                //cell of type of link
                dataGridViewOfAccessoriesOnUpdateAccessoryPage.Rows[e.RowIndex].Cells[5].ReadOnly = false;
            }
        }

        private void dataGridViewOfAccessoriesOnUpdateAccessoryPage_Resize(object sender, EventArgs e)
        {
            btnUpdateAccessories.Top = dataGridViewOfAccessoriesOnUpdateAccessoryPage.Bottom + 50;
        }

        private void btnUpdateAccessories_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
            {
                sqlConnection.Open();
                foreach (DataGridViewRow row in dataGridViewOfAccessoriesOnUpdateAccessoryPage.Rows)
                {
                    try
                    {
                        bool isUpdatedRow = (bool)row.Cells[0].Value;
                        if (isUpdatedRow)
                        {
                            updateAccessory(sqlConnection, (int)row.Cells[1].Value,
                                (string)row.Cells[3].Value, (string)row.Cells[4].Value,
                                (string)row.Cells[5].Value);
                        }
                    }
                    catch (Exception) { }
                }
                MessageBox.Show("Данные о аксессуарах успешно обновлены");
                tabControlOfUpdate_SelectedIndexChanged(new object(), new EventArgs());
            }
        }

        private void tabControlOfDelete_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControlOfDelete.SelectedTab.Name)
            {
                case "tabPageOfDeleteClients":
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                        {
                            sqlConnection.Open();
                            //fill clients
                            FillGridViewOfTableFromDb(sqlConnection, new SqlCommand(SqlScripts.SELECT_CLIENTS), dataGridViewOfClientsOnDeleteClientsPage);
                        }
                        dataGridViewOfClientsOnDeleteClientsPage.RowHeadersVisible = false;
                        dataGridViewOfClientsOnDeleteClientsPage.ReadOnly = false;
                        foreach (DataGridViewColumn column in dataGridViewOfClientsOnDeleteClientsPage.Columns)
                        {
                            column.ReadOnly = true;
                        }
                        dataGridViewOfClientsOnDeleteClientsPage.Columns[0].ReadOnly = false;                        
                        //(new DataColumn("Удалить данную строку", typeof(bool)));
                        AutoResizeGridView(dataGridViewOfClientsOnDeleteClientsPage);
                    }                   
                    break;
                case "tabPageOfDeleteAccessories":
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                        {
                            sqlConnection.Open();
                            //fill clients
                            FillGridViewOfTableFromDb(sqlConnection,
                                new SqlCommand(SqlScripts.SELECT_ACCESSORIES),
                                dataGridViewOfAccessOnDeleteAccessPage);
                        }
                        dataGridViewOfAccessOnDeleteAccessPage.RowHeadersVisible = false;
                        dataGridViewOfAccessOnDeleteAccessPage.ReadOnly = false;
                        foreach (DataGridViewColumn column in dataGridViewOfAccessOnDeleteAccessPage.Columns)
                        {
                            column.ReadOnly = true;
                        }
                        dataGridViewOfAccessOnDeleteAccessPage.Columns[0].ReadOnly = false;                        
                        //(new DataColumn("Удалить данную строку", typeof(bool)));
                        AutoResizeGridView(dataGridViewOfAccessOnDeleteAccessPage);
                    }                   
                    break;                   
            }
        }

        private void btnDeleteClients_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
            {
                sqlConnection.Open();
                bool isDeletedRow = false;
                foreach (DataGridViewRow row in dataGridViewOfClientsOnDeleteClientsPage.Rows)
                {
                    try
                    {
                        isDeletedRow = (bool)row.Cells[0].Value;                       
                    }
                    catch (Exception) { continue; }
                    if (isDeletedRow)
                    {
                        deleteClient(sqlConnection, (int)row.Cells[1].Value);
                    }
                }
                
                MessageBox.Show("Данные успешно удалены");
                tabControlOfDelete_SelectedIndexChanged(new object(), new EventArgs());
            }
        }

        private void btnDeleteAccessory_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
            {
                sqlConnection.Open();
                bool isDeletedRow = false;
                foreach (DataGridViewRow row in dataGridViewOfAccessOnDeleteAccessPage.Rows)
                {
                    try
                    {
                        isDeletedRow = (bool)row.Cells[0].Value;                        
                    }
                    catch (Exception) { continue; }
                    if (isDeletedRow)
                    {
                        deleteAccessory(sqlConnection, (int)row.Cells[1].Value);
                    }
                }
                MessageBox.Show("Данные успешно удалены");
                tabControlOfDelete_SelectedIndexChanged(new object(), new EventArgs());
            }
        }

        private void dataGridViewOfClientsOnDeleteClientsPage_Resize(object sender, EventArgs e)
        {
            btnDeleteClients.Top = dataGridViewOfClientsOnDeleteClientsPage.Bottom + 50;
        }

        private void dataGridViewOfAccessOnDeleteAccessPage_Resize(object sender, EventArgs e)
        {
            btnDeleteAccessory.Top = dataGridViewOfAccessOnDeleteAccessPage.Bottom + 50;
        }
    }
}
