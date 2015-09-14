using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;


namespace ExcelReportCreater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
                InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string MyConString = "Server=localhost;" + "Database=cpp_data;" + "Uid=admin;" + "Pwd=admin;";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand cmd;
            connection.Open();

            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `cpp_data`.`dbm`;";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
                MessageBox.Show("Didn't Connect");
            }
            finally
            {

                if ( connection.State == ConnectionState.Open )
                {
                    connection.Close();
                    
                }
            }
        }
   
        public void ExportToExcel_17(DataGridView grid)
        {
 
            Excel.Application Exl = new Excel.Application();
            Excel.Workbook wb;

            Excel.XlReferenceStyle RefStyle = Exl.ReferenceStyle;
            Exl.Visible = true;
            
            String TemplatePath = System.Windows.Forms.Application.StartupPath + @"\Экспорт данных_17.xltx";
            try
            {
                wb = Exl.Workbooks.Add(TemplatePath); // !!! 
            }
            catch (System.Exception ex)
            {
                throw new Exception("Не удалось загрузить шаблон для экспорта " + TemplatePath + "\n" + ex.Message);
            }
            Excel.Worksheet ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;
            int startCells = 13;
            for (int j = 0; j < grid.Columns.Count; ++j)
            {
                (ws.Cells[startCells, j + 1] as Excel.Range).Value2 = grid.Columns[j].HeaderText;
                for (int i = startCells; i < grid.Rows.Count; ++i)
                {
                    object Val = grid.Rows[i-startCells].Cells[j].Value;
                    if (Val != null)
                        (ws.Cells[i + 2, j + 1] as Excel.Range).Value2 = Val.ToString();
                }
            }
            ws.Columns.EntireColumn.AutoFit();
            Exl.ReferenceStyle = RefStyle;
            releaseObject(Exl as Object);

                MessageBox.Show("File created !");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string MyConString = "Server=localhost;" + "Database=cpp_data;" + "Uid=admin;" + "Pwd=admin;";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand cmd;
            connection.Open();

            try
            {
                cmd = connection.CreateCommand();
                String date_begin = "2015-07-02";
                String date_end = "2015-07-03";
                date_begin = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                date_end = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                String command_str=  "SELECT * FROM `cpp_data`.`dbm`";
                       command_str += "where DATE_ADD(`dbm`.`DateTime`, INTERVAL 0 SECOND) > DATE_ADD(\"" + date_begin;
                       command_str += "\", INTERVAL 0 SECOND)";
                       command_str += "and DATE_ADD(`dbm`.`DateTime`, INTERVAL 0 SECOND) < DATE_ADD(\"" + date_end;
                       command_str += "\", INTERVAL 86400 SECOND);";
                cmd.CommandText = command_str;
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                ExportToExcel_17(dataGridView1);
            }
            catch (Exception)
            {
                MessageBox.Show("Didn't Connect");
            }
            finally
            {

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();

                }
            }
        } 
    }
}
