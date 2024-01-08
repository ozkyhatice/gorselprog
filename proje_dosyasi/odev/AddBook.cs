using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace odev
{
    public partial class AddBook : Form
    {

        SqlConnection sqlconnect = new SqlConnection("Data Source=.;Initial Catalog=GorselProgramlama;Integrated Security=False; User ID=sa; Password=123456");

        public AddBook()
        {
            InitializeComponent();
        }

        private void AddBook_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
               string.IsNullOrWhiteSpace(textBox2.Text) ||
               string.IsNullOrWhiteSpace(textBox3.Text) ||
               string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Lütfen tüm gerekli alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            sqlconnect.Open();
            SqlCommand sqlcom = new SqlCommand("add_books", sqlconnect);
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.Add("@BookName", SqlDbType.NVarChar).Value = textBox1.Text;
            sqlcom.Parameters.Add("@AuthorName", SqlDbType.NVarChar).Value = textBox2.Text;
            sqlcom.Parameters.Add("@Publisher", SqlDbType.NVarChar).Value = textBox4.Text;
            sqlcom.Parameters.Add("@Isbn", SqlDbType.NVarChar).Value = textBox3.Text;
            sqlcom.Parameters.Add("@PublicationYear", SqlDbType.Date).Value = dateTimePicker1.Value;
            sqlcom.Parameters.Add("@NumOfPage", SqlDbType.Int).Value = numericUpDown1.Value;
            sqlcom.Parameters.Add("@ShelfNum", SqlDbType.NVarChar).Value = textBox7.Text;
            sqlcom.ExecuteNonQuery();
            sqlconnect.Close();
            MessageBox.Show("Book Added!");
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
