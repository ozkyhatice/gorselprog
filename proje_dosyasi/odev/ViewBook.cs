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
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace odev
{
    public partial class ViewBook : Form
    {
        SqlConnection sqlconnect = new SqlConnection("Data Source=.;Initial Catalog=GorselProgramlama;Integrated Security=False; User ID=sa; Password=123456");

        public ViewBook()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlconnect.Open();
            SqlCommand sqlcom = new SqlCommand("viewBooks", sqlconnect);
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.Add("@BookName", SqlDbType.NVarChar).Value = textBox1.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcom);
            DataTable data_table = new DataTable();
            adapter.Fill(data_table);
            dataGridView1.DataSource = data_table;
            sqlconnect.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void ViewBook_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

