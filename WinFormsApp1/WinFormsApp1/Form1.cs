using System.Data.SqlClient;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();
        }

        /* SqlConnection con = new SqlConnection("Data Source=DESKTOP-QPTJ53I\\SQLEXPRESS;Initial Catalog=gcbt;Integrated Security=True");
         SqlCommand cmd;
         SqlDataReader read;
         string id;
         bool Mode = true;
         string sql;
        */
        SqlDataReader read;
        SqlDataAdapter drr;
        string sql;
        string id;
        SqlCommand cmd,cmd1;
        bool Mode = true;



        public void Load()
        {
            try {
                sql = "select * from student";
                string connectString = ("Data Source=DESKTOP-QPTJ53I\\SQLEXPRESS;Initial Catalog=gcbt;Integrated Security=True");
                SqlConnection con = new SqlConnection(connectString);

                cmd = new SqlCommand(sql, con);
                con.Open();

                read= cmd.ExecuteReader();
                drr = new SqlDataAdapter(sql, con);
                dataGridView1.Rows.Clear();

                while (read.Read())
                {

                    dataGridView1.Rows.Add(read[0],read[1],read[2],read[3]);
                }
                con.Close();


            }
            catch(Exception ex) { 
               MessageBox.Show(ex.Message);
            }
        }






        public void getID(String id)
        {

            string connectString = ("Data Source=DESKTOP-QPTJ53I\\SQLEXPRESS;Initial Catalog=gcbt;Integrated Security=True");
            SqlConnection con = new SqlConnection(connectString);
            sql = "select * from student where id='" + id + "'";

            cmd = new SqlCommand(sql, con);
            con.Open();
            read= cmd.ExecuteReader();

            while(read.Read())
            {
                textBox1.Text = read[1].ToString();
                textBox2.Text= read[2].ToString();
                textBox3.Text= read[3].ToString();
            }
            con.Close();
        }





        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex > 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button2.Text = "Edit";

            }
            else if(e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex > 0)
            {
                string connectString = ("Data Source=DESKTOP-QPTJ53I\\SQLEXPRESS;Initial Catalog=gcbt;Integrated Security=True");
                SqlConnection con = new SqlConnection(connectString);
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from student where id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery(); 
                MessageBox.Show("Record Deleted");
                con.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Load();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Focus();
            button2.Text = "Save";
            Mode = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (Mode == true)
            {
                string connectString = ("Data Source=DESKTOP-QPTJ53I\\SQLEXPRESS;Initial Catalog=gcbt;Integrated Security=True");
                SqlConnection con = new SqlConnection(connectString);
                con.Open();

                string name = textBox1.Text;
                string course = textBox2.Text;
                string fee = textBox3.Text;

                string Query = "insert into student(stname,course,fee) values ('" + name + "','" + course + "','" + fee + "')";
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Record Added!!");


                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox1.Focus();
            }
            else {

                string name = textBox1.Text;
                string course = textBox2.Text;
                string fee = textBox3.Text;
                
                
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string connectString = ("Data Source=DESKTOP-QPTJ53I\\SQLEXPRESS;Initial Catalog=gcbt;Integrated Security=True");
                SqlConnection con = new SqlConnection(connectString);
                con.Open();
                sql = "update student set stname=@stname,course=@course,fee=@fee where id=@id";
                cmd=new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@stname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Record Updated!!");
                cmd.ExecuteNonQuery();


                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox1.Focus();
                button2.Text="Save";
                Mode = true;
            }
        }
    }
}