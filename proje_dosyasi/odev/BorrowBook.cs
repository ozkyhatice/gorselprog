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
    public partial class BorrowBook : Form
    {
        SqlConnection sqlconnect = new SqlConnection("Data Source=.;Initial Catalog=GorselProgramlama;Integrated Security=False; User ID=sa; Password=123456");

        public BorrowBook()
        {
            InitializeComponent();
        }

        private void BorrowBook_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlconnect.Open();
            SqlCommand sqlcom = new SqlCommand("viewBooks", sqlconnect);
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.Add("@BookName", SqlDbType.NVarChar).Value = textBox2.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcom);
            DataTable data_table = new DataTable();
            adapter.Fill(data_table);
            dataGridView1.DataSource = data_table;
            sqlconnect.Close();
        }


        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            int scrollValue = vScrollBar1.Value;
            int rowCount = dataGridView1.Rows.Count;
            // scrollValue değerini kontrol ediyoruz
            if (scrollValue < rowCount)
                dataGridView1.FirstDisplayedScrollingRowIndex = scrollValue;
            else
                // scrollValue, satır sayısından büyükse
                dataGridView1.FirstDisplayedScrollingRowIndex = rowCount - 1;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Eğer hiçbir satır seçilmediyse uyarı verir
                if (dataGridView1.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Please select a book to remove.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Seçilen satırdan kitap ID'sinin bulunduğu sütunun indeksini alır
                int columnIndex = dataGridView1.Columns["BookID"].Index;

                // İlk seçilen hücreyi->kitapId alır
                DataGridViewCell selectedCell = dataGridView1.SelectedCells[0];

                // Seçilen hücredeki kitap ID'sini alır
                int selectedBookID = Convert.ToInt32(selectedCell.Value);
                Borrow_Book(selectedBookID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Borrow_Book(int selectedBookID)
        {
            try
            {
                sqlconnect.Open();
                if (selectedBookID <= 0)
                {
                    MessageBox.Show("Please select a valid book.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string studentIdentification = textBox1.Text;
                if (string.IsNullOrWhiteSpace(studentIdentification))
                {
                    MessageBox.Show("Please enter the student identification.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DateTime borrowDate = dateTimePicker1.Value;
                string insertQuery = "INSERT INTO BorrowedBooks (Identification, BookID, BorrowDate, ReturnDate) VALUES (@Identification, @BookID, @BorrowDate, NULL)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, sqlconnect))
                {
                    cmd.Parameters.AddWithValue("@Identification", studentIdentification);
                    cmd.Parameters.AddWithValue("@BookID", selectedBookID);
                    cmd.Parameters.AddWithValue("@BorrowDate", borrowDate);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Book borrowed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlconnect.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
