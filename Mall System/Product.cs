using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallSystem
{
    class Product 
    {
        SqlConnection cn = new SqlConnection("Data Source= . ; Initial Catalog = Mall ; Integrated Security = true");
        SqlCommand cmd;

        string type;
        int price;
        int count;
        int renterid;
        int prodid;

        public Product(string Type, int Price, int Count, int Renterid,int Prodid)
        {
            this.type = Type;
            this.price = Price;
            this.count = Count;
            this.renterid = Renterid;
            this.prodid = Prodid;
        }

        public Product(int Prodid)
        {
            this.prodid = Prodid;
        }
        
        // Add Method
        public void Add()
        {
            cmd = new SqlCommand("Insert into Prod_db (type,price,count,renter_id) values('" + type + "','" + price + "','" + count + "','" + renterid + "')", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        //Delete Method
        public void Delete()
        {
            cmd = new SqlCommand("Delete from Prod_db where prod_id='" + prodid + "'", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            
        }
        //Update Method
        public void Update()
        {
            cmd = new SqlCommand("Update Prod_db  set type= '" + type + "', price ='" + price + "',count= '" + count + "', renter_id ='" + renterid + "' where prod_id='" + prodid + "'", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}
