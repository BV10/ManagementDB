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
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();          

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string connection = @"Data Source=DESKTOP-CHJ1K79\SQLEXPRESS;Initial Catalog=ShopDB;Integrated Security=True";
            this.sqlConnection = new SqlConnection(connection);

            FillGridViewOfClients();
            AutosizeGridView(dataGridView_Clients);
        }

        public static void AutosizeGridView(DataGridView dataGridView)
        {
            var height = 40;
            foreach (DataGridViewRow dr in dataGridView.Rows)
            {
                height += dr.Height;
            }
            dataGridView.Height = height;
        }

        private void FillGridViewOfClients()
        {
            var select = "SELECT * FROM Shop.Client";
            var dataAdapter = new SqlDataAdapter(select, this.sqlConnection);
            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView_Clients.ReadOnly = true;
            dataGridView_Clients.DataSource = ds.Tables[0];
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.clientTableAdapter.FillBy(this.shopDBDataSet.Client);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
