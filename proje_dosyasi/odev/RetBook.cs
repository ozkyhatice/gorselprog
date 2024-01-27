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

namespace odev
{
    public partial class RetBook : Form
    {
        SqlConnection sqlconnect = new SqlConnection("Data Source=.;Initial Catalog=GorselProgramlama;Integrated Security=False; User ID=sa; Password=123456");
        public RetBook()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Öğrenci kimliği TextBox'tan alınır
                string studentIdentification = textBox1.Text;

                // Eğer öğrenci kimliği boş ise uyarı verilir
                if (string.IsNullOrWhiteSpace(studentIdentification))
                {
                    MessageBox.Show("Please enter the student identification.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Bağlantı durumunu kontrol et
                if (sqlconnect.State == ConnectionState.Closed)
                {
                    sqlconnect.Open();
                }

                // Öğrencinin ödünç aldığı kitapları listeleyen sorgu
                string query = "SELECT bb.BookID, b.BookName, bb.BorrowDate, bb.ReturnDate " +
                                "FROM BorrowedBooks bb " +
                                "INNER JOIN Books b ON bb.BookID = b.BookID " +
                                "WHERE bb.Identification = @Identification";

                using (SqlCommand cmd = new SqlCommand(query, sqlconnect))
                {
                    cmd.Parameters.AddWithValue("@Identification", studentIdentification);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // DataGridView'e verileri bağla
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Bağlantıyı kapat
                if (sqlconnect.State == ConnectionState.Open)
                {
                    sqlconnect.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Seçilen satırdan BorrowedBookID değerini alın
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int columnIndex = dataGridView1.Columns["BookID"].Index;
                    int bookID = Convert.ToInt32(dataGridView1.SelectedCells[0].Value);

                    sqlconnect.Open();

                    // Ödünç alınan kitabı iade etmek için sorgu
                    string returnBookQuery = "UPDATE BorrowedBooks SET ReturnDate = GETDATE() WHERE BookID = @BookID AND ReturnDate IS NULL";

                    using (SqlCommand returnCmd = new SqlCommand(returnBookQuery, sqlconnect))
                    {
                        returnCmd.Parameters.AddWithValue("@BookID", bookID);
                        returnCmd.ExecuteNonQuery();

                        // Ödünç alınan kitabı BorrowedBooks tablosundan sil
                        string deleteBookQuery = "DELETE FROM BorrowedBooks WHERE BookID = @BookID";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteBookQuery, sqlconnect))
                        {
                            deleteCmd.Parameters.AddWithValue("@BookID", bookID);
                            deleteCmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Book returned and removed from BorrowedBooks successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // DataGridView'deki verileri güncelle
                        button1_Click(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a book to return.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
