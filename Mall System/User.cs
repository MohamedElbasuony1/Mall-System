using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallSystem
{
    public class User 
    {
        SqlConnection cn = new SqlConnection("Data Source= . ; Initial Catalog = Mall ; Integrated Security = true");
        SqlCommand cmd;
        SqlDataReader dr;
        int man_id;
        int renter_id;
        string fname, lname, email, gender,password;
        DateTime birth;
        bool flag,flag1,job = false;
        public string jo;
       public int Renter_id;
        public string name;
        public User(string Fname,string Lname,string Email,string Gender,bool Job,string Password,DateTime Birth)
        {
            this.fname = Fname;
            this.lname = Lname;
            this.email = Email;
            this.gender = Gender;
            this.job = Job;
            this.password = Password;
            this.birth = Birth;
        }

        public User(string Email, string Password)
        {
            this.email = Email;
            this.password = Password;
        }
        
        //Login Method
        public bool Login()
        {
            cmd = new SqlCommand("Select email,password,permission,renter_id,ufname from User_db", cn);
            cn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (email == dr[0].ToString() && password == dr[1].ToString())
                {
                    jo = dr[2].ToString();
                    if (dr[2].ToString() == "Renter")
                    {
                        Renter_id = int.Parse(dr[3].ToString());
                    }
                    name = dr[4].ToString();
                    flag = true;
                    break;
                }
            }
            dr.Close();
            cn.Close();
            return flag;
        }

        //Register Method
        public string Register()
        {
            cmd = new SqlCommand("Select email From User_db ", cn);
            cn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (email == dr[0].ToString())
                {
                    flag1 = true;
                    break;
                }
            }
            dr.Close();
            cn.Close();
            string s;
            
            if (flag1 == false)
            {
                if (job)
                {
                    s = "Manger";
                    if (job)
                    {
                        cmd = new SqlCommand("Select top 1 man_id From Man_db order by man_id desc", cn);
                        cn.Open();
                        dr = cmd.ExecuteReader();
                        dr.Read();
                        man_id = int.Parse((dr[0].ToString())) + 1;
                        dr.Close();
                        cn.Close();
                        cmd = new SqlCommand("Insert into Man_db (mfname,mlname,job) values('" + fname + "','" + lname + "','" + s + "')", cn);
                        cn.Open();
                        cmd.ExecuteNonQuery();
                        cn.Close();
                    }
                }
                else
                {
                    s = "Renter";
                    cmd = new SqlCommand("Select top 1 renter_id From Renter_db order by renter_id desc", cn);
                    cn.Open();
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    renter_id = int.Parse((dr[0].ToString())) + 1;
                    dr.Close();
                    cn.Close();
                }
                cmd = new SqlCommand("Insert into User_db (ufname,ulname,email,gender,permission,birth,password,man_id,renter_id) values('" + fname + "','" + lname + "','" +email + "','" + gender + "','" + s + "','" + birth + "','" + password + "','" + man_id + "','" + renter_id + "')", cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                
               return s+','+fname+','+lname;
            }
            else
            {
                flag1 = false;
                return "The Email Is alredy Exist";
            }
        }
    }
}
