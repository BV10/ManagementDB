namespace ManagementShopDB
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl_OperationOnDb = new System.Windows.Forms.TabControl();
            this.tabPageOfSelect = new System.Windows.Forms.TabPage();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageOfClients = new System.Windows.Forms.TabPage();
            this.clientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.shopDBDataSet = new ManagementShopDB.ShopDBDataSet();
            this.tabPageOfAccessory = new System.Windows.Forms.TabPage();
            this.dataGridViewOfAccessory = new System.Windows.Forms.DataGridView();
            this.tabPageOfOrders = new System.Windows.Forms.TabPage();
            this.dataGridViewOfOrders = new System.Windows.Forms.DataGridView();
            this.tabPageOfStockStore = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.clientTableAdapter = new ManagementShopDB.ShopDBDataSetTableAdapters.ClientTableAdapter();
            this.accessoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.accessoryTableAdapter = new ManagementShopDB.ShopDBDataSetTableAdapters.AccessoryTableAdapter();
            this.dataGridViewOfClients = new System.Windows.Forms.DataGridView();
            this.dataGridViewOfStockStores = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.tabControl_OperationOnDb.SuspendLayout();
            this.tabPageOfSelect.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageOfClients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shopDBDataSet)).BeginInit();
            this.tabPageOfAccessory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOfAccessory)).BeginInit();
            this.tabPageOfOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOfOrders)).BeginInit();
            this.tabPageOfStockStore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accessoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOfClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOfStockStores)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // tabControl_OperationOnDb
            // 
            this.tabControl_OperationOnDb.Controls.Add(this.tabPageOfSelect);
            this.tabControl_OperationOnDb.Controls.Add(this.tabPage2);
            this.tabControl_OperationOnDb.Controls.Add(this.tabPage3);
            this.tabControl_OperationOnDb.Controls.Add(this.tabPage4);
            this.tabControl_OperationOnDb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_OperationOnDb.Location = new System.Drawing.Point(0, 24);
            this.tabControl_OperationOnDb.Name = "tabControl_OperationOnDb";
            this.tabControl_OperationOnDb.SelectedIndex = 0;
            this.tabControl_OperationOnDb.Size = new System.Drawing.Size(1184, 537);
            this.tabControl_OperationOnDb.TabIndex = 2;
            this.tabControl_OperationOnDb.SelectedIndexChanged += new System.EventHandler(this.tabControl_OperationOnDb_SelectedIndexChanged);
            // 
            // tabPageOfSelect
            // 
            this.tabPageOfSelect.Controls.Add(this.tabControl);
            this.tabPageOfSelect.Location = new System.Drawing.Point(4, 22);
            this.tabPageOfSelect.Name = "tabPageOfSelect";
            this.tabPageOfSelect.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOfSelect.Size = new System.Drawing.Size(1176, 511);
            this.tabPageOfSelect.TabIndex = 0;
            this.tabPageOfSelect.Text = "Выборка";
            this.tabPageOfSelect.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageOfClients);
            this.tabControl.Controls.Add(this.tabPageOfAccessory);
            this.tabControl.Controls.Add(this.tabPageOfOrders);
            this.tabControl.Controls.Add(this.tabPageOfStockStore);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(3, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1170, 505);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageOfClients
            // 
            this.tabPageOfClients.Controls.Add(this.dataGridViewOfClients);
            this.tabPageOfClients.Location = new System.Drawing.Point(4, 22);
            this.tabPageOfClients.Name = "tabPageOfClients";
            this.tabPageOfClients.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOfClients.Size = new System.Drawing.Size(1162, 479);
            this.tabPageOfClients.TabIndex = 0;
            this.tabPageOfClients.Text = "Клиенты";
            this.tabPageOfClients.UseVisualStyleBackColor = true;
            // 
            // clientBindingSource
            // 
            this.clientBindingSource.DataMember = "Client";
            this.clientBindingSource.DataSource = this.shopDBDataSet;
            // 
            // shopDBDataSet
            // 
            this.shopDBDataSet.DataSetName = "ShopDBDataSet";
            this.shopDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabPageOfAccessory
            // 
            this.tabPageOfAccessory.Controls.Add(this.dataGridViewOfAccessory);
            this.tabPageOfAccessory.Location = new System.Drawing.Point(4, 22);
            this.tabPageOfAccessory.Name = "tabPageOfAccessory";
            this.tabPageOfAccessory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOfAccessory.Size = new System.Drawing.Size(1162, 479);
            this.tabPageOfAccessory.TabIndex = 1;
            this.tabPageOfAccessory.Text = "Аксесуары";
            this.tabPageOfAccessory.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Accessory
            // 
            this.dataGridViewOfAccessory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewOfAccessory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewOfAccessory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOfAccessory.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewOfAccessory.Name = "dataGridView_Accessory";
            this.dataGridViewOfAccessory.Size = new System.Drawing.Size(120, 30);
            this.dataGridViewOfAccessory.TabIndex = 0;
            // 
            // tabPageOfOrders
            // 
            this.tabPageOfOrders.Controls.Add(this.dataGridViewOfOrders);
            this.tabPageOfOrders.Location = new System.Drawing.Point(4, 22);
            this.tabPageOfOrders.Name = "tabPageOfOrders";
            this.tabPageOfOrders.Size = new System.Drawing.Size(1162, 479);
            this.tabPageOfOrders.TabIndex = 2;
            this.tabPageOfOrders.Text = "Заказы";
            this.tabPageOfOrders.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Orders
            // 
            this.dataGridViewOfOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOfOrders.Location = new System.Drawing.Point(6, 5);
            this.dataGridViewOfOrders.Name = "dataGridView_Orders";
            this.dataGridViewOfOrders.Size = new System.Drawing.Size(120, 30);
            this.dataGridViewOfOrders.TabIndex = 0;
            // 
            // tabPageOfStockStore
            // 
            this.tabPageOfStockStore.Controls.Add(this.dataGridViewOfStockStores);
            this.tabPageOfStockStore.Location = new System.Drawing.Point(4, 22);
            this.tabPageOfStockStore.Name = "tabPageOfStockStore";
            this.tabPageOfStockStore.Size = new System.Drawing.Size(1162, 479);
            this.tabPageOfStockStore.TabIndex = 3;
            this.tabPageOfStockStore.Text = "Склады";
            this.tabPageOfStockStore.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1176, 511);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Вставка";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1176, 511);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Обновление";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1176, 511);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Удаление";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // clientTableAdapter
            // 
            this.clientTableAdapter.ClearBeforeFill = true;
            // 
            // accessoryBindingSource
            // 
            this.accessoryBindingSource.DataMember = "Accessory";
            this.accessoryBindingSource.DataSource = this.shopDBDataSet;
            // 
            // accessoryTableAdapter
            // 
            this.accessoryTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridView_Clients
            // 
            this.dataGridViewOfClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOfClients.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewOfClients.Name = "dataGridView_Clients";
            this.dataGridViewOfClients.Size = new System.Drawing.Size(120, 30);
            this.dataGridViewOfClients.TabIndex = 0;
            // 
            // dataGridViewOfStockStores
            // 
            this.dataGridViewOfStockStores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOfStockStores.Location = new System.Drawing.Point(5, 6);
            this.dataGridViewOfStockStores.Name = "dataGridViewOfStockStores";
            this.dataGridViewOfStockStores.Size = new System.Drawing.Size(120, 30);
            this.dataGridViewOfStockStores.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.tabControl_OperationOnDb);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Управление интернет магазином BV-market";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl_OperationOnDb.ResumeLayout(false);
            this.tabPageOfSelect.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageOfClients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shopDBDataSet)).EndInit();
            this.tabPageOfAccessory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOfAccessory)).EndInit();
            this.tabPageOfOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOfOrders)).EndInit();
            this.tabPageOfStockStore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accessoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOfClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOfStockStores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl_OperationOnDb;
        private System.Windows.Forms.TabPage tabPageOfSelect;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageOfClients;
        private System.Windows.Forms.TabPage tabPageOfAccessory;
        private System.Windows.Forms.TabPage tabPageOfOrders;
        private ShopDBDataSet shopDBDataSet;
        private System.Windows.Forms.BindingSource clientBindingSource;
        private ShopDBDataSetTableAdapters.ClientTableAdapter clientTableAdapter;
        private System.Windows.Forms.DataGridView dataGridViewOfAccessory;
        private System.Windows.Forms.BindingSource accessoryBindingSource;
        private ShopDBDataSetTableAdapters.AccessoryTableAdapter accessoryTableAdapter;
        private System.Windows.Forms.DataGridView dataGridViewOfOrders;
        private System.Windows.Forms.TabPage tabPageOfStockStore;
        private System.Windows.Forms.DataGridView dataGridViewOfClients;
        private System.Windows.Forms.DataGridView dataGridViewOfStockStores;
    }
}

