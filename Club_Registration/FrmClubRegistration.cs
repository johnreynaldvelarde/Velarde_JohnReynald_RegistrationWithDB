using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Club_Registration
{
    public partial class FrmClubRegistration : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;

        private int Age;
        private string FirstName, MiddleName, LastName, Gender, Program;
        private long StudentId;

        public FrmClubRegistration()
        {
            InitializeComponent();
            clubRegistrationQuery = new ClubRegistrationQuery();
            RefreshListOfClubMembers();
        }

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            string[] ListOfProgram = new string[] { "BS Information Technology",
                                                    "BS Computer Science",
                                                    "BS Information Systems",
                                                    "BS in Accountancy",
                                                    "BS in Hospitality Management",
                                                    "BS in Tourism Management" };
            for (int i = 0; i < 6; i++)
            {
                cb_Program.Items.Add(ListOfProgram[i].ToString());
            }
        }

        public void Clear()
        {
            txt_StudentID.Clear();
            txt_LastName.Clear();
            txt_MiddleName.Clear();
            txt_FirstName.Clear();
            txt_Age.Clear();
            cb_Gender.SelectedIndex = -1;
            cb_Program.SelectedIndex = -1;
            txt_StudentID.Focus();
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_StudentID.Text))
            {
                MessageBox.Show("Please enter the student id.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(txt_FirstName.Text))
            {
                MessageBox.Show("Please enter the firstname.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(txt_MiddleName.Text))
            {
                MessageBox.Show("Please enter the middlename.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(txt_LastName.Text))
            {
                MessageBox.Show("Please enter the lastname.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(txt_Age.Text))
            {
                MessageBox.Show("Please enter the age", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cb_Gender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a gender.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cb_Program.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a program.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                StudentId = int.Parse(txt_StudentID.Text);
                FirstName = txt_FirstName.Text;
                MiddleName = txt_MiddleName.Text;
                LastName = txt_LastName.Text;
                Age = int.Parse(txt_Age.Text);
                Gender = cb_Gender.Text;
                Program = cb_Program.Text;

                clubRegistrationQuery.RegisterStudent(StudentId, FirstName, MiddleName, LastName, Age, Gender, Program);
                Clear();
                RefreshListOfClubMembers();
            }
        }

        public void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList(grid_club_members);
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            FrmUpdateMember frm = new FrmUpdateMember(this);
            frm.ShowDialog();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }

        private void grid_club_members_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in grid_club_members.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void txt_StudentID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_Age_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
