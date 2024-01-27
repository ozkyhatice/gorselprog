using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace odev
{
    public partial class AddStu : Form
    {
        SqlConnection sqlconnect = new SqlConnection("Data Source=.;Initial Catalog=GorselProgramlama;Integrated Security=False; User ID=sa; Password=123456");

        public AddStu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
               string.IsNullOrWhiteSpace(textBox2.Text) ||
               string.IsNullOrWhiteSpace(textBox3.Text) ||
               string.IsNullOrWhiteSpace(textBox4.Text) ||
               string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Please fill out all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (CheckIfIdentificationExists(Convert.ToInt32(textBox1.Text)))
            {
                MessageBox.Show("A student with the same Identification already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            sqlconnect.Open();
            SqlCommand sqlcom = new SqlCommand("add_stu", sqlconnect);
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.Add("@Identification", SqlDbType.Int).Value = textBox1.Text;
            sqlcom.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = textBox2.Text;
            sqlcom.Parameters.Add("@Surname", SqlDbType.NVarChar).Value = textBox3.Text;
            sqlcom.Parameters.Add("@EMail", SqlDbType.NVarChar).Value = textBox4.Text;
            sqlcom.Parameters.Add("@Telephone", SqlDbType.NVarChar).Value = textBox6.Text;
            sqlcom.ExecuteNonQuery();
            sqlconnect.Close();
            MessageBox.Show("Student Added!");
            this.Close();
        }
        private bool CheckIfIdentificationExists(int identification)
        {
            // Aynı kimlik numarasının var olup olmadığını kontrol et
            using (SqlCommand checkCmd = new SqlCommand("SELECT 1 FROM [dbo].[students] WHERE [Identification] = @Identification", sqlconnect))
            {
                checkCmd.Parameters.Add("@Identification", SqlDbType.Int).Value = identification;
                sqlconnect.Open();
                SqlDataReader reader = checkCmd.ExecuteReader();
                bool exists = reader.HasRows;
                sqlconnect.Close();
                return exists;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
