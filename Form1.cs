using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace join
{
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection();
            conn.ConnectionString = "server=localhost;uid=root;pwd=;database=infosys;";

            try { conn.Open(); }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { conn.Close(); }

            string q = "SELECT * FROM student";

            adapter = new MySqlDataAdapter(q, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "idno";
            comboBox1.DataSource = dt;

            dataGridView1.DataSource = dt;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string q = $"SELECT DISTINCT idno, name, section, course, grade FROM student INNER JOIN class ON student.idno = class.studentno INNER JOIN student_grade ON student_grade.studentno = class.studentno WHERE idno = {comboBox1.SelectedValue}";
            adapter = new MySqlDataAdapter(q, conn);
            DataTable data = new DataTable();
            adapter.Fill(data);

            if (data.Rows.Count > 0)
            {
                textBox1.Text = data.Rows[0][0].ToString();
                textBox2.Text = data.Rows[0][1].ToString();
                textBox3.Text = data.Rows[0][2].ToString();
                textBox4.Text = data.Rows[0][3].ToString();
            }
            else
            {
                MessageBox.Show("Walang data para sa kanya Kaibigan!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            dataGridView1.DataSource = data;
        }
    }
}
