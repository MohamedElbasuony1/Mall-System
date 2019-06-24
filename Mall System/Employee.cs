using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallSystem
{
    public class Employee 
    {
        SqlConnection cn = new SqlConnection("Data Source= . ; Initial Catalog = Mall ; Integrated Security = true");
        SqlCommand cmd;
        string fname;
        string lname;
        DateTime birth;
        int phone;
        int salary;
        int manid;
        int empid;

        public Employee(string Fname, string Lname, DateTime Birth, int Phone, int Salary,int Manid,int Empid)
        {
            this.fname = Fname;
            this.lname = Lname;
            this.birth = Birth;
            this.phone = Phone;
            this.salary = Salary;
            this.manid = Manid;
            this.empid = Empid;
        }

        public Employee(int Empid)
        {
            this.empid = Empid;
        }
        
        // Add Method
        public void Add()
        {
            cmd = new SqlCommand("Insert into Emp_db (efname,elname,ebirth,phone,salary,man_id) values('" + fname + "','" + lname + "','" + birth + "','" + phone + "','" + salary + "','" + manid + "')", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        //Delete Method
        public void Delete()
        {
            cmd = new SqlCommand("Delete from Emp_db where emp_id='" + empid + "'", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        //Update Method
        public void Update()
        {
            cmd = new SqlCommand("Update Emp_db  set efname= '" + fname + "', elname ='" + lname + "',ebirth= '" + birth + "', phone ='" + phone + "',salary= '" + salary + "', man_id ='" + manid + "' where emp_id='" + empid + "'", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}
