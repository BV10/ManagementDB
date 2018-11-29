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
        SqlConnection sqlConnection;     

        public MainForm()
        {
            InitializeComponent();
            dataGridViewOfAccessory.RowHeadersVisible = false;
            dataGridViewOfClients.RowHeadersVisible = false;
            dataGridViewOfOrders.RowHeadersVisible = false;            
        }        

        private void Form1_Load(object sender, EventArgs e)
        {
            string connection = @"Data Source=DESKTOP-CHJ1K79\SQLEXPRESS;Initial Catalog=ShopDB;Integrated Security=True";
            this.sqlConnection = new SqlConnection(connection);
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

        private void AutoSizeGridViewWidth(DataGridView dataGridView)
        {
            int totalWidth = 0;
            //Auto Resize the columns to fit the data
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                dataGridView.Columns[column.Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                int widthCol = dataGridView.Columns[column.Index].Width;
                dataGridView.Columns[column.Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView.Columns[column.Index].Width = widthCol;
                totalWidth = totalWidth + widthCol;
            }
            //the selector on the left of the DataGridView is about 45 in width
            dataGridView.Width = totalWidth + 3;
        }

        public static void AutosizeGridViewHeight(DataGridView dataGridView)
        {
            var height = 0;
            foreach (DataGridViewRow dr in dataGridView.Rows)
            {
                height += dr.Height;
            }
            dataGridView.Height += height - dataGridView.Rows[0].Height;
        }

        private void FillGridViewOfTable(SqlConnection sqlConnection, string script, DataGridView dataGridView)
        {            
            var dataAdapter = new SqlDataAdapter(script, this.sqlConnection);            
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView.ReadOnly = true;
            dataGridView.DataSource = ds.Tables[0];            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.sqlConnection.Close();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sqlConnection.Close();
            this.Close();
        }

        private void tabControl_OperationOnDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(tabControl_OperationOnDb.SelectedTab.Name)
            {
                case "tabPageOfSelect":
                    FillDataGridViewsOfSelectData();
                    break;
            }
           
        }

        private void FillDataGridViewsOfSelectData()
        {
            ClearGridView(dataGridViewOfClients);
            ClearGridView(dataGridViewOfAccessory);
            ClearGridView(dataGridViewOfOrders);
            ClearGridView(dataGridViewOfStockStores);
            //fill clients
            FillGridViewOfTable(sqlConnection, SqlScripts.SELECT_CLIENTS, dataGridViewOfClients);
            AutosizeGridViewHeight(dataGridViewOfClients);
            AutoSizeGridViewWidth(dataGridViewOfClients);
            //fill accessories                   
            FillGridViewOfTable(sqlConnection, SqlScripts.SELECT_ACCESSORIES, dataGridViewOfAccessory);
            AutosizeGridViewHeight(dataGridViewOfAccessory);
            AutoSizeGridViewWidth(dataGridViewOfAccessory);
            //fill orders          
            FillGridViewOfTable(sqlConnection, SqlScripts.SELECT_ORDERS, dataGridViewOfOrders);
            AutosizeGridViewHeight(dataGridViewOfOrders);
            AutoSizeGridViewWidth(dataGridViewOfOrders);
            //fill stockstores
            FillGridViewOfTable(sqlConnection, SqlScripts., dataGridViewOfStockStores);
            AutosizeGridViewHeight(dataGridViewOfStockStores);
            AutoSizeGridViewWidth(dataGridViewOfStockStores);
        }
    }
}
