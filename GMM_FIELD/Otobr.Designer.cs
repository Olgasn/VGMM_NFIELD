namespace GMM_FIELD
{
    partial class Otobr
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
            this.dataSet31 = new GMM_FIELD.DataSet3();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet31)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(537, 229);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Количество сфер: ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(117, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(43, 20);
            this.textBox1.TabIndex = 8;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(510, 141);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(448, 196);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Ок";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT        GMM.*\r\nFROM            GMM";
            this.oleDbSelectCommand1.Connection = this.oleDbConnection1;
            // 
            // oleDbDataAdapter1
            // 
            this.oleDbDataAdapter1.DeleteCommand = this.oleDbDeleteCommand1;
            this.oleDbDataAdapter1.SelectCommand = this.oleDbSelectCommand1;
            this.oleDbDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "GMM", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Код_сферы", "Код_сферы"),
                        new System.Data.Common.DataColumnMapping("Количество_сфер", "Количество_сфер"),
                        new System.Data.Common.DataColumnMapping("Описание_рассчета", "Описание_рассчета"),
                        new System.Data.Common.DataColumnMapping("Количество_ориентаций(nbeta)", "Количество_ориентаций(nbeta)"),
                        new System.Data.Common.DataColumnMapping("Количество_ориентаций(nthet)", "Количество_ориентаций(nthet)"),
                        new System.Data.Common.DataColumnMapping("Количество_ориентаций(nphai)", "Количество_ориентаций(nphai)"),
                        new System.Data.Common.DataColumnMapping("Ориентация(betami)", "Ориентация(betami)"),
                        new System.Data.Common.DataColumnMapping("Ориентация(betamx)", "Ориентация(betamx)"),
                        new System.Data.Common.DataColumnMapping("Ориентация(thetmi)", "Ориентация(thetmi)"),
                        new System.Data.Common.DataColumnMapping("Ориентация(thetmx)", "Ориентация(thetmx)"),
                        new System.Data.Common.DataColumnMapping("Ориентация(phaimi)", "Ориентация(phaimi)"),
                        new System.Data.Common.DataColumnMapping("Ориентация(phaimx)", "Ориентация(phaimx)"),
                        new System.Data.Common.DataColumnMapping("Итерация_по_схеме_X", "Итерация_по_схеме_X"),
                        new System.Data.Common.DataColumnMapping("Итерация_по_схеме_Y", "Итерация_по_схеме_Y"),
                        new System.Data.Common.DataColumnMapping("Максимальное_количество_итераций", "Максимальное_количество_итераций"),
                        new System.Data.Common.DataColumnMapping("Порядок_управления_рассеиванием", "Порядок_управления_рассеиванием"),
                        new System.Data.Common.DataColumnMapping("Погрешность_ошибки", "Погрешность_ошибки"),
                        new System.Data.Common.DataColumnMapping("Критерий_сходимости", "Критерий_сходимости"),
                        new System.Data.Common.DataColumnMapping("Индекс_взаимодействия", "Индекс_взаимодействия"),
                        new System.Data.Common.DataColumnMapping("Длина_волны", "Длина_волны"),
                        new System.Data.Common.DataColumnMapping("Дата_расчета", "Дата_расчета"),
                        new System.Data.Common.DataColumnMapping("Время расчета", "Время расчета")})});
            this.oleDbDataAdapter1.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // oleDbConnection1
            // 
            this.oleDbConnection1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\bin\\Debug\\GMM_FIELD" +
    "1.accdb";
            // 
            // dataSet31
            // 
            this.dataSet31.DataSetName = "DataSet3";
            this.dataSet31.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Otobr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GMM_FIELD.Properties.Resources.V_25uirhfcQ;
            this.ClientSize = new System.Drawing.Size(562, 255);
            this.Controls.Add(this.groupBox1);
            this.Name = "Otobr";
            this.Text = "Otobr";
            this.Load += new System.EventHandler(this.Otobr_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet31)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        private System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        private System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        private System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1;
        private System.Data.OleDb.OleDbConnection oleDbConnection1;
        private DataSet3 dataSet31;

    }
}