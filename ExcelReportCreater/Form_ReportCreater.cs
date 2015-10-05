﻿using System;
using System.Collections;
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
    namespace MySQL
{
    public class Transcation : IDisposable
    {
        private string connectionString = null;
        private MySqlConnection mysqlConnection = null;
        private MySqlCommand mysqlCommand = null;
        private MySqlTransaction mysqlTransaction = null;

        public Transcation(string server = "localhost", string username = "admin", string password = "admin", uint port = 3306, bool isTransaction = true, uint connectionTimeout = 5000, uint defaultCommandTimeout = 5000)
        {
            connectionString = "Data Source=" + server + ";" + "Port=" + port + ";User ID=" + username + ";Password=" + password + ";Allow User Variables=True;Connect Timeout=" + connectionTimeout.ToString() + ";";
            connectionString += ";Default Command Timeout=" + defaultCommandTimeout.ToString() + ";Allow Zero Datetime=True";
            mysqlConnection = new MySqlConnection(connectionString);
            mysqlConnection.Open();
            if (isTransaction) mysqlTransaction = mysqlConnection.BeginTransaction();
            mysqlCommand = mysqlConnection.CreateCommand();
            mysqlCommand.Connection = mysqlConnection;
        }

        // Dispose
        public void Dispose()
        {
            if (mysqlTransaction != null) mysqlTransaction.Dispose();
            if (mysqlCommand != null) mysqlCommand.Dispose();
            if (mysqlConnection != null)
            {
                mysqlConnection.Close();
                mysqlConnection.Dispose();
            }
        }

        // Commit transaction
        public void Commit()
        {
            mysqlTransaction.Commit();
        }

        // Rollback transaction
        public void Rollback()
        {
            mysqlTransaction.Rollback();
        }

        // Add data to table
        public long AddRow(string database, string table, string[] columns, object[] values, string binary_column = null, byte[] binary_data = null, string updateWhere = null)
        {
            string valuetags = "";

            if (columns.Length != values.Length) throw new Exception("Columns and value count does not match");

            if (binary_column != null) valuetags += "@bin,";

            for (int i = 0; i < columns.Length; i++)
            {
                if (i != 0) valuetags += ",";
                valuetags += "@p" + i.ToString();
            }

            if (updateWhere == null)
            {
                mysqlCommand.CommandText = "insert into `" + database + "`.`" + table + "` " + (binary_column != null ? "(`" + binary_column + "`,`" : "(`") + string.Join("`,`", columns) + "`) values (" + valuetags + ")";

                if (binary_data != null)
                    mysqlCommand.Parameters.AddWithValue("@bin", binary_data);

                for (int i = 0; i < columns.Length; i++)
                    mysqlCommand.Parameters.AddWithValue("@p" + i.ToString(), values[i]);
            }
            else
            {
                mysqlCommand.CommandText = string.Empty;

                for (int i = 0; i < columns.Length; i++)
                {
                    mysqlCommand.CommandText += "update `" + database + "`.`" + table + "` SET `" + columns[i] + "`=@p" + i.ToString() + "x" + " WHERE " + updateWhere + " LIMIT 1;";
                    mysqlCommand.Parameters.AddWithValue("@p" + i.ToString() + "x", values[i]);
                }
            }

            mysqlCommand.ExecuteNonQuery();

            mysqlCommand.Parameters.Clear();

            return mysqlCommand.LastInsertedId;
        }

        // Add data using Column & Data class
        /*
        public long AddRow(string database, string table, List listColData, string updateWhere = null)
        {
            return AddRow(database, table, listColData.Select(n => n.columnName).ToArray(), listColData.Select(n => n.dataValue).ToArray(), updateWhere: updateWhere);

        }
        */
        // Sends a query to the database
        public void SendQuery(string query)
        {
            mysqlCommand.CommandText = query;
            mysqlCommand.ExecuteNonQuery();
        }

        // Returns object
        public object GetObject(string query)
        {
            mysqlCommand.CommandText = query;
            return mysqlCommand.ExecuteScalar();
        }

        // Returns signed integer
        public int GetInt(string query)
        {
            return int.Parse(GetObject(query).ToString());
        }

        // Returns unsigned integer
        public uint GetUint(string query)
        {
            return uint.Parse(GetObject(query).ToString());
        }

        // Returns string
        public string GetString(string query)
        {
            return GetObject(query).ToString();
        }

        // Returns datatable
        public DataTable GetTable(string query)
        {
            using (DataSet ds = new DataSet())
            {
                using (MySqlDataAdapter _adapter = new MySqlDataAdapter(query, mysqlConnection))
                    _adapter.Fill(ds, "map");

                return ds.Tables[0];
            }
        }

       // public void BulkSend(string database, string table, string column, List
        }
    }
    public partial class Form_ReportCreater : Form
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

        public Form_ReportCreater()
        {
            
                InitializeComponent();
                textBox_ConnectString.Text = ReadStringConnect();
            
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
        public void ExportToExcel_18(DataTable table)
        {
            Excel.Application Exl = new Excel.Application();
            Excel.Workbook wb;
            Exl.Visible = true;
            Excel.XlReferenceStyle RefStyle = Exl.ReferenceStyle;
            String TemplatePath = System.Windows.Forms.Application.StartupPath + @"\Экспорт данных_18.xltx";
            try
            {
                wb = Exl.Workbooks.Add(TemplatePath); // !!! 
            }
            catch (System.Exception ex)
            {
                throw new Exception("Не удалось загрузить шаблон для экспорта " + TemplatePath + "\n" + ex.Message);
            }
            Excel.Worksheet ws1 = wb.Worksheets.get_Item(1) as Excel.Worksheet;

            int rowExcel = 8; //начать со второй строки.

            for (int i = 0; i < table.Rows.Count; i++)
            {
                //заполняем строку
                ws1.Cells[rowExcel, "A"] = table.Rows[i][0].ToString();
                ws1.Cells[rowExcel, "E"] = table.Rows[i][1].ToString();
                ws1.Cells[rowExcel, "F"] = table.Rows[i][2].ToString();

                ++rowExcel;
            }
            
            //ws.Columns.EntireColumn.AutoFit();
            Exl.ReferenceStyle = RefStyle;
            releaseObject(Exl as Object);
            MessageBox.Show("Отчет 18 создан!");
           
        }
        public void ExportToExcel_19(DataGridView grid)
        {

            Excel.Application Exl = new Excel.Application();
            Excel.Workbook wb;

            Excel.XlReferenceStyle RefStyle = Exl.ReferenceStyle;
            Exl.Visible = true;

            String TemplatePath = System.Windows.Forms.Application.StartupPath + @"\Экспорт данных_19.xltx";
            try
            {
                wb = Exl.Workbooks.Add(TemplatePath); // !!! 
            }
            catch (System.Exception ex)
            {
                throw new Exception("Не удалось загрузить шаблон для экспорта " + TemplatePath + "\n" + ex.Message);
            }
            Excel.Worksheet ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;
            int startCells = 8;
            for (int j = 0; j < grid.Columns.Count; ++j)
            {
                if (j == 3)
                    j = 4;
                (ws.Cells[startCells, j + 1] as Excel.Range).Value2 = grid.Columns[j].HeaderText;
                for (int i = startCells; i < grid.Rows.Count; ++i)
                {
                    object Val = " " + grid.Rows[i - startCells].Cells[j].Value;
                    if (Val != null)
                        (ws.Cells[i + 1, j + 1] as Excel.Range).Value2 = Val.ToString();
                    if (j == 0)
                    {
                        //рамочки
                    }
                }

            }
            //ws.Columns.EntireColumn.AutoFit();
            Exl.ReferenceStyle = RefStyle;
            releaseObject(Exl as Object);

            MessageBox.Show("Отчет 19 создан!");

        }

        private void textBox_ConnectString_KeyUp(object sender, KeyEventArgs e)
        {
            SaveStringConnection(textBox_ConnectString.Text);
        }

        private void button_Report17_Click(object sender, EventArgs e)
        {
            //string MyConString = "Server=localhost;" + "Database=cpp_data;" + "Uid=admin;" + "Pwd=admin;";
            string MyConString = textBox_ConnectString.Text;
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
                dataGridView1.SelectAll();
                dataGridView1.ClearSelection();
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                ExportToExcel_17(dataGridView1);
            }
            catch (Exception err)
            {
                String message = "Что-то пошло не так \r\n";
                message += err.Source + "\r\n";
                message += err.Message;
                MessageBox.Show(message);
            }
            finally
            {

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

       
        private void button_Report18_Click(object sender, EventArgs e)
        {

            MySQL.Transcation tr= new MySQL.Transcation();
            
            try
            {
                String command_str = "SET @granularity:=60*60; ";
                tr.SendQuery(command_str);
                               
                
                String date_begin = "2015-07-02";
                String date_end = "2015-07-03";
                date_begin = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                date_end = dateTimePicker2.Value.ToString("yyyy-MM-dd");

                command_str = "SET @prepare_date:='" + date_begin + " 00:00:00'; ";
                tr.SendQuery(command_str);
               
                command_str = "select _uniqprogname as 'Название программы', SEC_TO_TIME( sum( _sum_timetrack ) ) as 'Хронометраж', count( _uniqprogname ) AS 'Выходы' \n";
                command_str += "from( select sum( _timetrack ) as _sum_timetrack, _progname as _uniqprogname from ( \n";
                command_str += "SELECT `g`.`group` as `group_`, time_to_sec(t2.time2) as _timetrack, SUBSTRING_INDEX(`g`.locate, '\\\\', 1) as _progname \n";
                command_str += "FROM `cpp_data`.`dbm` `g` JOIN `cpp_data`.`dbm` AS t2 ON (((t2.DateTime - `g`.DateTime) <= DATE_ADD(`g`.time2, INTERVAL 60 SECOND)) \n";
                command_str += "AND ((t2.DateTime - `g`.DateTime) > '00:00:00') AND '<' = `g`.n AND '>' = t2.n AND `g`.filename = t2.filename \n";
                command_str += "AND `g`.more0 = t2.more0) AND t2.DateTime >= @prepare_date AND t2.DateTime < DATE_ADD(@prepare_date, INTERVAL 1 DAY)  AND t2.type != 'ROTACIA'  AND t2.type != 'ATM' \n";
                command_str += "WHERE   `g`.DateTime >= @prepare_date AND `g`.DateTime < DATE_ADD(@prepare_date, INTERVAL 1 DAY)  AND `g`.type != 'ROTACIA'  AND `g`.type != 'ATM' \n";
                command_str += "GROUP BY  `g`.DateTime, SUBSTRING_INDEX(`g`.locate, '\\\\', 1) \n";
                command_str += ") as T group by T.`group_` ) as T_T group by _uniqprogname; ";

                DataTable dt = new DataTable();
                dt = tr.GetTable(command_str);
                tr.Commit();

                dataGridView1.SelectAll();
                dataGridView1.ClearSelection();
                dataGridView1.DataSource = dt.DefaultView;
                ExportToExcel_18(dt);
            }
            catch (Exception err)
            {
                String message = "Что-то пошло не так \r\n";
                message += err.Source + "\r\n";
                message += err.Message;
                MessageBox.Show(message);
            }
            finally
            {
                tr.Dispose();
            }
        }
        private void button_Report19_Click(object sender, EventArgs e)
        {
            //string MyConString = "Server=localhost;" + "Database=cpp_data;" + "Uid=admin;" + "Pwd=admin;";
            string MyConString = textBox_ConnectString.Text;
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
                String command_str = "SELECT 	t1.DateTime AS _begin, t2.DateTime AS _end, SUBSTRING_INDEX(t1.locate, '\\\\', 1) AS _progname, SEC_TO_TIME(t2.DateTime - t1.DateTime) AS _chrono \n";
                command_str += "FROM  `cpp_data`.`dbm` AS t1 \n";
                command_str += "JOIN  `cpp_data`.`dbm` AS t2 ON (((t2.DateTime - t1.DateTime) <= DATE_ADD(t1.time2, INTERVAL 60 SECOND)) \n";
                command_str += "AND ((t2.DateTime - t1.DateTime) > '00:00:00') AND '<' = t1.n AND '>' = t2.n AND t1.filename = t2.filename AND t1.more0 = t2.more0) \n";
                command_str += "WHERE   t1.DateTime >= '" + date_begin;
                command_str += "' \n";
                command_str += "AND t1.DateTime < DATE_ADD('" + date_begin;
                command_str += "', INTERVAL 1 DAY)  AND t1.type != 'ROTACIA'  AND t1.type != 'ATM' \n";
                command_str += "order by _begin;";

                cmd.CommandText = command_str;
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                dataGridView1.SelectAll();
                dataGridView1.ClearSelection();
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                ExportToExcel_19(dataGridView1);
            }
            catch (Exception err)
            {
                String message = "Что-то пошло не так \r\n";
                message += err.Source + "\r\n";
                message += err.Message;
                MessageBox.Show(message);
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
