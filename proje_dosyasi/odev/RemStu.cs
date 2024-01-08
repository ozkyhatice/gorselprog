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

namespace odev
{
    public partial class RemStu : Form
    {
        SqlConnection sqlconnect = new SqlConnection("Data Source=.;Initial Catalog=GorselProgramlama;Integrated Security=False; User ID=sa; Password=123456");
        public RemStu()
        {
            InitializeComponent();
        }

        private void RemStu_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string identificationNumber = textBox1.Text;

            // Eğer kimlik numarası boşsa uyarı ver ve işlemi sonlandır
            if (string.IsNullOrWhiteSpace(identificationNumber))
            {
                MessageBox.Show("Please enter an identification number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Öğrenciyi silme işlemi
            RemoveStudent(identificationNumber);
        }
        private void RemoveStudent(string identificationNumber)
        {
            try
            {
                // Bağlantıyı aç
                sqlconnect.Open();

                // Öğrenciyi silme sorgusu
                SqlCommand removeStudentCommand = new SqlCommand("DELETE FROM students WHERE Identification = @Identification", sqlconnect);
                removeStudentCommand.Parameters.AddWithValue("@Identification", identificationNumber);

                int affectedRows = removeStudentCommand.ExecuteNonQuery();

                // Eğer en az bir satır etkilendiyse öğrenci silinmiştir
                if (affectedRows > 0)
                {
                    MessageBox.Show("Student removed successfully!");
                }
                else
                {
                    MessageBox.Show("Student not found or an error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Bağlantıyı kapat
                sqlconnect.Close();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
