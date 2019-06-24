using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Mall_System
{
    class main
    {
        SqlConnection cn = new SqlConnection("Data Source= . ; Initial Catalog = Mall ; Integrated Security = true");
        SqlCommand cmd;
        SqlDataReader dr;
        public void renter(ListView ls)
        {
            ls.Items.Clear();
            cmd = new SqlCommand("Select * From Renter_db", cn);
            cn.Open();
            dr = cmd.ExecuteReader();
          
            while (dr.Read())
            {
                ListViewItem lst = new ListViewItem();
                lst.Text = dr[1].ToString();
                lst.SubItems.Add(dr[2].ToString());
                lst.SubItems.Add(dr[3].ToString());
                lst.SubItems.Add(dr[4].ToString());
                lst.SubItems.Add(dr[5].ToString());
                ls.Items.Add(lst);
            }
            dr.Close();
            cn.Close();
        }
        public void dept(int i,ListView ls)
        {
            ls.Items.Clear();
            cmd = new SqlCommand("Select * From Dept_db where dstate="+i , cn);
            cn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem lst = new ListViewItem();
                lst.Text = dr[1].ToString();
                lst.SubItems.Add(dr[2].ToString());
                lst.SubItems.Add(dr[3].ToString());
                ls.Items.Add(lst);
            }
            cn.Close();
            dr.Close();
        }
        public void renter_det(ListView ls)
        {
            
            ls.Items.Clear();
            cmd = new SqlCommand("Select * From Renter_db where state ="+1, cn);
            cn.Open();
            dr = cmd.ExecuteReader();
           
            while (dr.Read())
            {
                ListViewItem lst = new ListViewItem();
                lst.Text = dr[1].ToString();
                lst.SubItems.Add(dr[2].ToString());
                lst.SubItems.Add(dr[6].ToString());
                lst.SubItems.Add(dr[5].ToString());
                ls.Items.Add(lst);
            }
            
            cn.Close();   
        }
    }
}
