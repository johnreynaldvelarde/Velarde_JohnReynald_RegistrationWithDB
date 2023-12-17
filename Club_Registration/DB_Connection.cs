using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club_Registration
{
    public class DB_Connection
    {
        public string MyConnection ()
        {
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Guze\\Desktop\\07 Laboratory Exercise 1\\repos\\Club_Registration\\Club_Registration\\ClubDB.mdf\";Integrated Security=True";
            return con;
        }
    }
}
