using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace PhoneBook2
{
   
    public partial class Form1 : Form
    { 
        //DataTable table;
        //int id = 0;
        public Form1()
        {
            InitializeComponent();
            UpdateTable();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PhoneBook;Trusted_Connection=True";
                try
                {
                    string insert = "INSERT INTO [Telephones] (Name,LastName,TelNumber)" + "VALUES (@Name, @LastName, @TelNumber);";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insert, conn);
                    cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@LastName", textBox3.Text);
                    cmd.Parameters.AddWithValue("@TelNumber", textBox2.Text);
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                        UpdateTable();
                    }
                }
            }
            else MessageBox.Show("Error");
        }
        private void UpdateTable()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PhoneBook;Trusted_Connection=True";
            try
            {
                conn.Open();
                string command = "SELECT * FROM [Telephones]";
                SqlDataAdapter adapt = new SqlDataAdapter(command, conn);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    table = new DataTable();
        //    table.Columns.Add("id");
        //    table.Columns.Add("Name");
        //    table.Columns.Add("LastName");
        //    table.Columns.Add("Number");
        //    using (SqlConnection conn = new SqlConnection(@"Server = (localdb)\MSSQLLocalDB;Database = PhoneBook;Trusted_Connection=True;"))
        //    {
        //        conn.Open();
        //        string command = "SELECT * FROM [Telephones];";
        //        SqlDataAdapter adapt = new SqlDataAdapter(command, conn);
        //        DataSet ds = new DataSet();
        //        adapt.Fill(ds);
        //        dataGridView1.DataSource = ds;
        //        id++;
        //        DataRow row = table.NewRow();
        //        row[0] = id;
        //        row[1] = textBox1.Text;
        //        row[2] = textBox2.Text;
        //        row[3] = textBox3.Text;
        //        table.Rows.Add(row);
        //        SqlCommandBuilder boilder = new SqlCommandBuilder(adapt);
        //        adapt.Update(ds);
        //        conn.Close();
        //    }
        //}
    }
}
