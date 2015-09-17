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
using System.IO;


namespace ExcelReportCreater
{
    
    public partial class Form1 : Form
    {
        void SaveStringConnection(string connectionString)
        {
            string path_connectionfile = @"connection.txt";
            try
            {
                // Create the file.
                using (FileStream fs = File.Create(path_connectionfile))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(connectionString);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        
        string ReadStringConnect()
        {
            string connectionString = "";
            string path_connectionfile = @"connection.txt";
            try
            {
                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(path_connectionfile))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                        connectionString = s;
                    }
                }
            }
            catch(Exception ex)
            {
                 Console.WriteLine(ex.ToString());
            }
            return connectionString;
        }

        public Form1()
        {
            
                InitializeComponent();
                textBox1.Text = ReadStringConnect();
            
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
            int startCells = 12;
            for (int j = 0; j < grid.Columns.Count; ++j)
            {
                (ws.Cells[startCells, j + 1] as Excel.Range).Value2 = grid.Columns[j].HeaderText;
                for (int i = startCells; i < grid.Rows.Count; ++i)
                {
                    object Val = " " + grid.Rows[i - startCells].Cells[j].Value;
                    if (Val != null)
                        (ws.Cells[i + 1, j + 1] as Excel.Range).Value2 = Val.ToString();
                    if (j == 0)
                    {
                        /*
                        string endcell = "a";
                        Excel.Range chartRange;
                        endcell += i;
                        chartRange = ws.get_Range("a12", endcell);
                        chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        endcell = "b" + i;
                        chartRange = ws.get_Range("a12", endcell);
                        chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        endcell = "c" + i;
                        chartRange = ws.get_Range("a12", endcell);
                        chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        endcell = "d" + i;
                        chartRange = ws.get_Range("a12", endcell);
                        chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        endcell = "e" + i;
                        chartRange = ws.get_Range("a12", endcell);
                        chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        endcell = "f" + i;
                        chartRange = ws.get_Range("a12", endcell);
                        chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                       */
                    }
                }
               
            }
            //ws.Columns.EntireColumn.AutoFit();
            Exl.ReferenceStyle = RefStyle;
            releaseObject(Exl as Object);

                MessageBox.Show("Отчет 17 создан!");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //string MyConString = "Server=localhost;" + "Database=cpp_data;" + "Uid=admin;" + "Pwd=admin;";
            string MyConString = textBox1.Text;
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
                String command_str = "SELECT DATE_FORMAT( t1.DateTime, '%H:%i') as 'Время выхода в эфир', TRIM(TRAILING '.' FROM TRIM(TRAILING SUBSTRING_INDEX(t1.filename, '.', -1) FROM t1.filename) ) as 'Наименование аудиоматериала (бренд )',";
                command_str += "alias.aliace as 'Категория а/мат ( рекл/ нерекл.)',";       
                command_str += "'' as 'Вид  заказных, промо, анонсных аудиоматериалов, наименование заказчика,№ и дата договора', ";
                command_str += "SEC_TO_TIME(t2.DateTime - t1.DateTime) as 'Хронометраж',";
                command_str += "'' as'Примечания' FROM `cpp_data`.`dbm` as t1 ";
                command_str += "join `cpp_data`.`dbm` as t2 on ";
                command_str += "( ( (t2.DateTime - t1.DateTime) <= DATE_ADD(t1.time2, INTERVAL 60 SECOND) ) and ( (t2.DateTime - t1.DateTime) > \"00:00:00\" ) and \"<\" = t1.n ";
                command_str += "and \">\" = t2.n  and t1.filename = t2.filename and t1.more0 = t2.more0 ) ";
                command_str += "join `cpp_data`.`aliases` as alias on alias.aliace = t1.type ";
                command_str += "where t1.DateTime >= \"" + date_begin;;
                command_str += "\" and t1.DateTime < DATE_ADD(\""  + date_end;
                command_str += "\", INTERVAL 1 DAY) AND t1.type != 'ROTACIA' AND t1.type != 'ATM'";
                command_str += "ORDER BY t1.filename, t1.DateTime";

                cmd.CommandText = command_str;
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                ExportToExcel_17(dataGridView1);
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так");
            }
            finally
            {

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();

                }
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            SaveStringConnection( textBox1.Text );
        } 
    }
}
