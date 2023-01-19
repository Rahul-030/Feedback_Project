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

namespace FeedBack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            load();
            
        }


        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Feedback;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False ");


        SqlCommand cmd;
        SqlDataReader read;
        bool Mode = true;
        string sql;
        string id;



        public void load()
        {


            try
            {

                sql = "Select * from Feedback";

                cmd = new SqlCommand(sql, con);
                con.Open();



                read = cmd.ExecuteReader();

                //   drr = new SqlDataAdapter(sql,con);
                dataGridView1.Rows.Clear();

                while (read.Read())
                {

                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);

                }
                con.Close();
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }


        }

        public void getId(String id)
        {

            sql = "Select * from Feedback where  Employee_Id =  ' " + id + " '  ";
            con.Open();
            read = cmd.ExecuteReader();




            while (read.Read())
            {
                txtId.Text = read[0].ToString();
                txtName.Text = read[1].ToString();
                txtDepartment.Text = read[2].ToString();
                txtComment.Text = read[3].ToString();

            }
            con.Close();
        }















        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Employee_Id = txtId.Text;
            string EmployeeName = txtName.Text;
            string Department = txtDepartment.Text;
            string Comment = txtComment.Text;


            if (Mode == true)
            {

                sql = "insert into Feedback(Employee_Id,Employee_Name,Department,Comment) values(@Employee_Id,@Employee_Name,@Department,@Comment)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Employee_Id", Employee_Id);
                cmd.Parameters.AddWithValue("@Employee_Name", EmployeeName);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                MessageBox.Show("Feedback Added Sucessfully");
                cmd.ExecuteNonQuery();
                txtId.Clear();
                txtName.Clear();
                txtDepartment.Clear();
                txtComment.Clear();
                txtId.Focus();

            }

            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = " update Feedback set  Employee_Name = @Employee_Name, Department = @Department , Comment = @Comment where Employee_Id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Employee_Name", EmployeeName);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                cmd.Parameters.AddWithValue("@id", id);

                MessageBox.Show("Feedback Updated Sucessfully");

                cmd.ExecuteNonQuery();

                txtId.Clear();
                txtName.Clear();
                txtDepartment.Clear();
                txtComment.Clear();
                
                txtId.Focus();

                button1.Text = "Save";

                Mode = true;
            }

            con.Close();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {

                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getId(id);


                button1.Text = "Update";
            }


            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {

                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                sql = "Delete from Feedback where Employee_Id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Feedback Deleted Sucessfully");
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtName.Clear();
            txtDepartment.Clear();
            txtComment.Clear();
            txtName.Focus();
            button1.Text = "Save";
            Mode = true;
        }
    }
   
}
