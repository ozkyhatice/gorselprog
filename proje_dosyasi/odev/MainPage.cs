using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace odev
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            AddBook add_book = new AddBook();
            add_book.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddStu add_stu = new AddStu();
            add_stu.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ViewStu view_stu = new ViewStu();
            view_stu.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            RemBook book_rem = new RemBook();
            book_rem.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewBook book_view = new ViewBook();
            book_view.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RemStu rem_stu = new RemStu();
            rem_stu.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RetBook ret_book = new RetBook();
            ret_book.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BorrowBook borr_book = new BorrowBook();
            borr_book.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
