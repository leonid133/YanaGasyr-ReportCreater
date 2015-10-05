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
            this.button_Report17 = new System.Windows.Forms.Button();
            this.button_Report18 = new System.Windows.Forms.Button();
            this.button_Report19 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.textBox_ConnectString = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Setting_button = new System.Windows.Forms.Button();
            this.button_rao = new System.Windows.Forms.Button();
            this.button_add_data = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // button_Report17
            // 
            this.button_Report17.Location = new System.Drawing.Point(12, 71);
            this.button_Report17.Name = "button_Report17";
            this.button_Report17.Size = new System.Drawing.Size(139, 40);
            this.button_Report17.TabIndex = 3;
            this.button_Report17.Text = "прил № 17";
            this.button_Report17.UseVisualStyleBackColor = true;
            this.button_Report17.Click += new System.EventHandler(this.button_Report17_Click);
            // 
            // button_Report18
            // 
            this.button_Report18.Location = new System.Drawing.Point(166, 71);
            this.button_Report18.Name = "button_Report18";
            this.button_Report18.Size = new System.Drawing.Size(139, 40);
            this.button_Report18.TabIndex = 4;
            this.button_Report18.Text = "прил №18";
            this.button_Report18.UseVisualStyleBackColor = true;
            this.button_Report18.Click += new System.EventHandler(this.button_Report18_Click);
            // 
            // button_Report19
            // 
            this.button_Report19.Location = new System.Drawing.Point(12, 127);
            this.button_Report19.Name = "button_Report19";
            this.button_Report19.Size = new System.Drawing.Size(139, 40);
            this.button_Report19.TabIndex = 5;
            this.button_Report19.Text = "прил №19";
            this.button_Report19.UseVisualStyleBackColor = true;
            this.button_Report19.Click += new System.EventHandler(this.button_Report19_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(343, 79);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(100, 20);
            this.dateTimePicker1.TabIndex = 6;
            this.dateTimePicker1.Tag = "Дата От";
            this.dateTimePicker1.Value = new System.DateTime(2015, 7, 2, 0, 0, 0, 0);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dateTimePicker2.Location = new System.Drawing.Point(343, 127);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(100, 20);
            this.dateTimePicker2.TabIndex = 8;
            this.dateTimePicker2.Tag = "Дата До";
            this.dateTimePicker2.Value = new System.DateTime(2015, 7, 3, 0, 0, 0, 0);
            // 
            // textBox_ConnectString
            // 
            this.textBox_ConnectString.Location = new System.Drawing.Point(73, 24);
            this.textBox_ConnectString.Name = "textBox_ConnectString";
            this.textBox_ConnectString.Size = new System.Drawing.Size(376, 20);
            this.textBox_ConnectString.TabIndex = 9;
            this.textBox_ConnectString.Text = "Server=localhost;Database=cpp_data;Uid=admin;Pwd=admin;charset=utf8;";
            this.textBox_ConnectString.Visible = false;
            this.textBox_ConnectString.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_ConnectString_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(343, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "От";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(346, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "До";
            // 
            // Setting_button
            // 
            this.Setting_button.BackgroundImage = global::ExcelReportCreater.Properties.Resources.settings;
            this.Setting_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Setting_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Setting_button.Location = new System.Drawing.Point(12, 12);
            this.Setting_button.Name = "Setting_button";
            this.Setting_button.Size = new System.Drawing.Size(48, 48);
            this.Setting_button.TabIndex = 12;
            this.Setting_button.UseVisualStyleBackColor = true;
            this.Setting_button.Click += new System.EventHandler(this.Setting_button_Click);
            // 
            // button_rao
            // 
            this.button_rao.Location = new System.Drawing.Point(12, 184);
            this.button_rao.Name = "button_rao";
            this.button_rao.Size = new System.Drawing.Size(139, 40);
            this.button_rao.TabIndex = 13;
            this.button_rao.Text = "РАО";
            this.button_rao.UseVisualStyleBackColor = true;
            this.button_rao.Click += new System.EventHandler(this.button_rao_Click);
            // 
            // button_add_data
            // 
            this.button_add_data.Location = new System.Drawing.Point(166, 127);
            this.button_add_data.Name = "button_add_data";
            this.button_add_data.Size = new System.Drawing.Size(139, 40);
            this.button_add_data.TabIndex = 14;
            this.button_add_data.Text = "Внесение данных";
            this.button_add_data.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Form_ReportCreater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 247);
            this.Controls.Add(this.button_add_data);
            this.Controls.Add(this.button_rao);
            this.Controls.Add(this.Setting_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_ConnectString);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button_Report19);
            this.Controls.Add(this.button_Report18);
            this.Controls.Add(this.button_Report17);
            this.Name = "Form_ReportCreater";
            this.Text = "Отчеты ТНВ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Report17;
        private System.Windows.Forms.Button button_Report18;
        private System.Windows.Forms.Button button_Report19;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.TextBox textBox_ConnectString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Setting_button;
        private System.Windows.Forms.Button button_rao;
        private System.Windows.Forms.Button button_add_data;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

