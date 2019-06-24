using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace MallSystem
{
    class Dept 
    {
        SqlConnection cn = new SqlConnection("Data Source= . ; Initial Catalog = Mall ; Integrated Security = true");
        SqlCommand cmd;

        string name;
        int rent;
        bool state;
        int deptid;
        public Dept(string Name, int Rent, bool State,int Deptid)
        {
            this.name = Name;
            this.rent = Rent;
            this.state = State;
            this.deptid = Deptid;
        }
        public Dept(int Deptid)
        {
            this.deptid = Deptid;
        }
        
        // Add Method
        public void Add()
        {
            cmd = new SqlCommand("Insert into Dept_db (name,rent,dstate) values('" + name + "','" + rent + "','" + state + "')", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        //Update Method
        public void Update()
        {
            cmd = new SqlCommand("Update Dept_db  set name= '" + name + "', rent ='" + rent + "',dstate= '" + state + "' where dept_id='" + deptid + "'", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

    }
}
