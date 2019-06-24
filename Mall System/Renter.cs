using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallSystem
{
    public class Renter
    {
        SqlConnection cn = new SqlConnection("Data Source= . ; Initial Catalog = Mall ; Integrated Security = true");
        SqlCommand cmd;

        string fname;
        string lname;
        DateTime start;
        DateTime finsh;
        int deptid;
        int manid;
        bool state;
        int renterid;
        public Renter(string Fname, string Lname, DateTime Start, DateTime Finsh, int Deptid, int Manid,bool State, int Renterid)
        {
            this.fname = Fname;
            this.lname = Lname;
            this.start = Start;
            this.finsh = Finsh;
            this.deptid = Deptid;
            this.manid = Manid;
            this.state = State;
            this.renterid = Renterid;
        }

        public Renter(int Renterid)
        {
            this.renterid = Renterid;
        }
        
        // Add Method
        public void Add()
        {
            cmd = new SqlCommand("Insert into Renter_db (fname,lname,start,finsh,dept_id,man_id,state) values('" + fname + "','" + lname + "','" + start + "','" + finsh + "','" + deptid + "','" + manid + "','" + state + "')", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            cmd = new SqlCommand("Update Dept_db  set dstate= '" + true + "' where dept_id='" + deptid + "'", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        //Delete Method
        public void Delete()
        {
            cmd = new SqlCommand("Delete From User_db where renter_id=" + renterid, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            cmd = new SqlCommand("Delete from Renter_db where renter_id=" + renterid , cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            cmd = new SqlCommand("Update Dept_db  set dstate= '" + false + "' where dept_id='" + deptid + "'", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        //Update Method
        public void Update()
        {
            cmd = new SqlCommand("Update User_db  set ufname= '" + fname + "', ulname= '" + lname + "' where renter_id='" + renterid + "'", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            cmd = new SqlCommand("Update Renter_db  set fname= '" + fname + "', lname ='" + lname + "', start ='" + start + "',finsh= '" + finsh + "', dept_id ='" + deptid + "', man_id ='" + manid + "',state='"+state+"' where renter_id='" +renterid+ "'", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}
