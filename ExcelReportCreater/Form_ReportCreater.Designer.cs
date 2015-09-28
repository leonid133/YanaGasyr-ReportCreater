namespace ExcelReportCreater
{
    partial class Form_ReportCreater
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_Report17 = new System.Windows.Forms.Button();
            this.button_Report18 = new System.Windows.Forms.Button();
            this.button_Report19 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.textBox_ConnectString = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 59);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 169);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(469, 225);
            this.dataGridView1.TabIndex = 2;
            // 
            // button_Report17
            // 
            this.button_Report17.Location = new System.Drawing.Point(114, 13);
            this.button_Report17.Name = "button_Report17";
            this.button_Report17.Size = new System.Drawing.Size(139, 40);
            this.button_Report17.TabIndex = 3;
            this.button_Report17.Text = "прил № 17";
            this.button_Report17.UseVisualStyleBackColor = true;
            this.button_Report17.Click += new System.EventHandler(this.button_Report17_Click);
            // 
            // button_Report18
            // 
            this.button_Report18.Location = new System.Drawing.Point(114, 59);
            this.button_Report18.Name = "button_Report18";
            this.button_Report18.Size = new System.Drawing.Size(139, 42);
            this.button_Report18.TabIndex = 4;
            this.button_Report18.Text = "прил №18";
            this.button_Report18.UseVisualStyleBackColor = true;
            // 
            // button_Report19
            // 
            this.button_Report19.Location = new System.Drawing.Point(114, 107);
            this.button_Report19.Name = "button_Report19";
            this.button_Report19.Size = new System.Drawing.Size(139, 38);
            this.button_Report19.TabIndex = 5;
            this.button_Report19.Text = "прил №19";
            this.button_Report19.UseVisualStyleBackColor = true;
            this.button_Report19.Click += new System.EventHandler(this.button_Report19_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(282, 33);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 6;
            this.dateTimePicker1.Tag = "Дата От";
            this.dateTimePicker1.Value = new System.DateTime(2015, 9, 14, 0, 0, 0, 0);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(282, 81);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 8;
            this.dateTimePicker2.Tag = "Дата До";
            // 
            // textBox_ConnectString
            // 
            this.textBox_ConnectString.Location = new System.Drawing.Point(24, 169);
            this.textBox_ConnectString.Name = "textBox_ConnectString";
            this.textBox_ConnectString.Size = new System.Drawing.Size(405, 20);
            this.textBox_ConnectString.TabIndex = 9;
            this.textBox_ConnectString.Text = "Server=localhost;Database=cpp_data;Uid=admin;Pwd=admin;charset=utf8;";
            this.textBox_ConnectString.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_ConnectString_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(282, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "От";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "До";
            // 
            // Form_ReportCreater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 413);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_ConnectString);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button_Report19);
            this.Controls.Add(this.button_Report18);
            this.Controls.Add(this.button_Report17);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form_ReportCreater";
            this.Text = "Отчеты ТНВ";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_Report17;
        private System.Windows.Forms.Button button_Report18;
        private System.Windows.Forms.Button button_Report19;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.TextBox textBox_ConnectString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

