using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Data;
using System.Windows.Forms;
using System.Runtime.Remoting.Contexts;
using System.Xml;

namespace Club_Registration
{
    public class ClubRegistrationQuery
    {
        private DB_Connection conn = new DB_Connection();

        private string connectionString;
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlReader;

        public DataTable dataTable;
        public BindingSource bindingSource;

        public string _FirstName, _MiddleName, _LastName, _Gender, _Program;
        public int _Age;
        public long _StudentID;

        public ClubRegistrationQuery()
        {
            connectionString = conn.MyConnection();

            sqlConnect = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand();
            sqlAdapter = new SqlDataAdapter();
        }

        public bool RegisterStudent(long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {

            sqlCommand = new SqlCommand("INSERT INTO ClubMembers VALUES(@StudentID, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)", sqlConnect);
            sqlCommand.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = StudentID;
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
            sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = MiddleName;
            sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
            sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
            sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender;
            sqlCommand.Parameters.Add("@Program", SqlDbType.VarChar).Value = Program;

            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            MessageBox.Show("Club information added successfully");
            return true;
        }

        public bool UpdateStudent(long student_id, string first_name, string middle_name, string last_name, int student_age, string student_gender, string student_program)
        {
            sqlCommand = new SqlCommand("UPDATE ClubMembers " +
                                                "SET FirstName = @FirstName, " +
                                                "MiddleName = @MiddleName, " +
                                                "LastName = @LastName,  " +
                                                "Age = @Age, " +
                                                "Gender = @Gender," +
                                                "Program = @Program WHERE StudentId = @StudentId", sqlConnect);
            
            sqlCommand.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = student_id;
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = first_name;
            sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = middle_name;
            sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = last_name;
            sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = student_age;
            sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = student_gender;
            sqlCommand.Parameters.Add("@Program", SqlDbType.VarChar).Value = student_program;

            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            MessageBox.Show("Student information update successfully");

            return true;
        }




        public bool DisplayList(DataGridView dataGridView)
        {
            try
            {
                dataGridView.Rows.Clear();

                string ViewClubMembers = "SELECT ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers";

                sqlConnect.Open();
                
                sqlCommand = new SqlCommand(ViewClubMembers, sqlConnect);
                sqlReader = sqlCommand.ExecuteReader();
                
                while (sqlReader.Read())
                {
                    dataGridView.Rows.Add(0,
                        sqlReader["ID"],
                        sqlReader["StudentId"],
                        sqlReader["FirstName"],
                        sqlReader["MiddleName"],
                        sqlReader["LastName"],
                        sqlReader["Age"],
                        sqlReader["Gender"],
                        sqlReader["Program"]
                    );
                }
                
                sqlConnect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        public bool View_StudentID(ComboBox cb_studentID)
        {
            try
            {
                string studentQuery = "SELECT StudentId FROM ClubMembers";

                sqlConnect.Open();

                sqlCommand = new SqlCommand(studentQuery, sqlConnect);
                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    _StudentID = sqlReader.GetInt64(0);
                    cb_studentID.Items.Add(_StudentID);
                }

                sqlConnect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return true;
        }

        public bool GetSelectedStudentID(string student_id)
        {
            try
            {
                string studentQuery = "SELECT * FROM ClubMembers WHERE StudentId = @Id";

                sqlConnect.Open();

                sqlCommand = new SqlCommand(studentQuery, sqlConnect);
                sqlCommand.Parameters.AddWithValue("@Id", student_id);
                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    _FirstName = sqlReader.GetString(2);
                    _MiddleName = sqlReader.GetString(3);
                    _LastName = sqlReader.GetString(4);
                    _Age = sqlReader.GetInt32(5);
                    _Gender = sqlReader.GetString(6);
                    _Program = sqlReader.GetString(7);
                }

                sqlConnect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return true;
        }
    }
}

