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

namespace Club_Registration
{
    public partial class FrmUpdateMember : Form
    {
        private DB_Connection conn = new DB_Connection();
        
        private ClubRegistrationQuery clubRegistrationQuery;
        private string connectionString;
        private SqlConnection sqlConnect;

        FrmClubRegistration frm;

        public FrmUpdateMember(FrmClubRegistration clubRegistration)
        {
            InitializeComponent();
            connectionString = conn.MyConnection();
            clubRegistrationQuery = new ClubRegistrationQuery();
            sqlConnect = new SqlConnection(connectionString);
            frm = clubRegistration;
        }

        private void cbStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            clubRegistrationQuery.GetSelectedStudentID(cb_StudentID.Text);
            GetData();
        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            clubRegistrationQuery.View_StudentID(cb_StudentID);

            string[] ListOfProgram = new string[] { "BS Information Technology",
                                                    "BS Computer Science",
                                                    "BS Information Systems",
                                                    "BS in Accountancy",
                                                    "BS in Hospitality Management",
                                                    "BS in Tourism Management" };
            for (int i = 0; i < 6; i++)
            {
                cbProgram.Items.Add(ListOfProgram[i].ToString());
            }
        }

        public void GetData()
        {
            txtFirstName.Text = clubRegistrationQuery._FirstName;
            txtMiddleName.Text = clubRegistrationQuery._MiddleName;
            txtLastName.Text = clubRegistrationQuery._LastName;
            txtAge.Text = clubRegistrationQuery._Age.ToString();
            cbGender.Text = clubRegistrationQuery._Gender;
            cbProgram.Text = clubRegistrationQuery._Program;
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            if (cb_StudentID.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the student id.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(txtFirstName.Text))
            {
                MessageBox.Show("Please enter the firstname.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(txtMiddleName.Text))
            {
                MessageBox.Show("Please enter the middlename.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Please enter the lastname.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(txtAge.Text))
            {
                MessageBox.Show("Please enter the age", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a gender.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cbProgram.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a program.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                long stu_id = Convert.ToInt64(cb_StudentID.Text);
                string f_name = txtFirstName.Text;
                string m_name = txtMiddleName.Text;
                string l_name = txtLastName.Text;
                int stu_age = int.Parse(txtAge.Text);
                string stu_gender = cbGender.Text;
                string stu_program = cbProgram.Text;

                clubRegistrationQuery.UpdateStudent(stu_id, f_name, m_name, l_name, stu_age, stu_gender, stu_program);
                this.Dispose();
                frm.RefreshListOfClubMembers();
            }
        }
    }
}
