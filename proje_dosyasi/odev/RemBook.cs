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
using System.Net;

namespace odev
{
    public partial class RemBook : Form
    {
        SqlConnection sqlconnect = new SqlConnection("Data Source=.;Initial Catalog=GorselProgramlama;Integrated Security=False; User ID=sa; Password=123456");
        public RemBook()
        {
            InitializeComponent();
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

                // Kullanıcıdan onay alın
                DialogResult result = MessageBox.Show($"Are you sure you want to remove the book with ID '{selectedBookID}'?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Eğer kullanıcı onay verirse, kitabı sil
                if (result == DialogResult.Yes)
                {
                    RemoveBook(selectedBookID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RemoveBook(int selectedBookID)
        {
            try
            {
                sqlconnect.Open();

                // Ödünç alınan kitapları kontrol et
                SqlCommand checkBorrowedBooksCommand = new SqlCommand("SELECT COUNT(*) FROM BorrowedBooks WHERE BookID = @BookID AND ReturnDate IS NULL", sqlconnect);
                checkBorrowedBooksCommand.Parameters.AddWithValue("@BookID", selectedBookID);

                int borrowedBookCount = Convert.ToInt32(checkBorrowedBooksCommand.ExecuteScalar());

                if (borrowedBookCount > 0)
                {
                    // Eğer kitap öğrenci tarafından ödünç alınmışsa uyarı ver
                    MessageBox.Show("The book is currently borrowed by a student. It cannot be removed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Kitabı sil
                    SqlCommand removeBookCommand = new SqlCommand("DELETE FROM books WHERE BookID = @BookID", sqlconnect);
                    removeBookCommand.Parameters.AddWithValue("@BookID", selectedBookID);

                    int affectedRows = removeBookCommand.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Book removed successfully!");
                        listbook();
                    }
                    else
                    {
                        MessageBox.Show("Book not found or an error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        private void RemBook_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void listbook()
        {
            if (sqlconnect.State != ConnectionState.Open)
            {
                sqlconnect.Open();
            }
            SqlCommand sqlcom = new SqlCommand("viewBooks", sqlconnect);
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.Add("@BookName", SqlDbType.NVarChar).Value = textBox1.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcom);
            DataTable data_table = new DataTable();
            adapter.Fill(data_table);
            dataGridView1.DataSource = data_table;
            sqlconnect.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listbook();
        }
    }
}
