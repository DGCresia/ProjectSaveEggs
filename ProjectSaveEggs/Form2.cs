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

namespace ProjectSaveEggs
{
    public partial class Form2 : Form
    {
        private SqlConnection con;
        private string constr = "SERVER=192.168.219.1,1433;DATABASE=projectSaveEggs" + "UID=project1;PASSWORD=1234";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void insertID_TextChanged(object sender, EventArgs e)
        {

        }

        private void insertPW_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            /*SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from User_Info where ID='gami' and PW='1234'", con);
            DataTable table = new DataTable();

            sda.Fill(table);

            if (table.Rows[0][0].ToString() == "1")
            {
                var login = MessageBox.Show("로그인 성공",
                    "LogIn Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Close();
            }
            else
            {

            }*/

            try
            {
                con = new SqlConnection(constr);
                con.Open();

                MessageBox.Show("연결성공");
            }
            catch (Exception Ex)
            {
                MessageBox.Show("에러발생 " + Ex.ToString());
            }
        }
    }
}
