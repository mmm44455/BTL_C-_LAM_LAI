using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        private SqlConnection c;
        public Form2()
        {
            InitializeComponent();
            c = new SqlConnection("Data Source = duycao; Initial Catalog = thongtincanhan; Integrated Security = True;encrypt = false");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string MS = textBox1.Text;
            string HT = textBox3.Text;
            string QUE = textBox2.Text;


            try
            {
                c.Open();
                SqlCommand cmd = new SqlCommand("delete_i", c);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QUE", QUE);

                cmd.ExecuteNonQuery();

                MessageBox.Show("xoa hoàn tất");
                xuat();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                c.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            xuat();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string MS = textBox1.Text;
            string HT = textBox3.Text;
            string QUE = textBox2.Text;


            try
            {
                c.Open();
                SqlCommand cmd = new SqlCommand("nhap_ID", c);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MS", MS);
                cmd.Parameters.AddWithValue("@HT", HT);
                cmd.Parameters.AddWithValue("@QUE", QUE);

                cmd.ExecuteNonQuery();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";

                MessageBox.Show("Nhập hoàn tất");
                xuat();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                c.Close();
            }

        }
        void xuat()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("xuat", c);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dt = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                dt.Fill(dt2);
                dataGridView1.DataSource = dt2;
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                c.Close();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            xuat();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
