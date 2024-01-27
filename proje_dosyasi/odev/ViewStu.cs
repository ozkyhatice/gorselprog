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
using Microsoft.SqlServer.Server;

namespace odev
{
    public partial class ViewStu : Form
    {
        SqlConnection sqlconnect = new SqlConnection("Data Source=.;Initial Catalog=GorselProgramlama;Integrated Security=False; User ID=sa; Password=123456");
        public ViewStu()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlconnect.Open();
            SqlCommand sqlcom = new SqlCommand("view_stu", sqlconnect);
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.Add("@Identification", SqlDbType.NVarChar).Value = textBox1.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcom);
            DataTable data_table = new DataTable();
            adapter.Fill(data_table);
            dataGridView1.DataSource = data_table;
            sqlconnect.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            int scrollValue = vScrollBar1.Value;
            int rowCount = dataGridView1.Rows.Count;
            if (scrollValue < rowCount)
                dataGridView1.FirstDisplayedScrollingRowIndex = scrollValue;
            else
                dataGridView1.FirstDisplayedScrollingRowIndex = rowCount - 1;
        }

        private void ViewStu_Load(object sender, EventArgs e)
        {

        }
    }
}
