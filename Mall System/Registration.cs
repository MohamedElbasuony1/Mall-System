using MallSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mall_System
{
    public partial class Registration : Form
    {
        SqlConnection cn = new SqlConnection("Data Source= . ; Initial Catalog = Mall ; Integrated Security = true");
        SqlCommand cmd;
        SqlDataReader dr;
        string premission;
        bool m;
        int id;
        int renid = 0;
        int empid = 0;
        int depid = 0;
        int proid = 0;
        
        public Registration(string name,int id2)
        {
            InitializeComponent();
            premission = name;
            id = id2;

        }

        //***********************************************************
        //load

        private void Registration_Load(object sender, EventArgs e)
        {
           
            switch (premission)
            {
                case "Admin":
                    ShowItem(6, "Renter_db", this.renterlst);
                    ShowItem(5, "Emp_db", this.emplst);
                    ShowItem(3, "Dept_db", this.deptlst);
                    tabControl1.TabPages.Remove(tbg_renter);
                    SelectItem(0, RDept);
                    break;
                case "Manger":
                    tabControl1.TabPages.Remove(tbg_renter);
                    tabControl1.TabPages.Remove(tbg_owner);
                    ShowItem(6, "Renter_db", this.renterlst);
                    SelectItem(0,RDept);
                    break;
                case "Renter":
                    tabControl1.TabPages.Remove(tbg_owner);
                    tabControl1.TabPages.Remove(tbg_manger);
                    tabControl1.TabPages.Remove(tbg_register);
                    ShowProd(3, this.prodlst);
                    PRenter.Text = id.ToString();
                    break;
            }
        }

        //***********************************************************
        //register

        private void btnregister_Click(object sender, EventArgs e)
        {
            #region validation
            if (txtfname.Text == "")
            {
                errorProvider1.SetError(txtfname, "please enter your first name");
            }
            else
            {
                errorProvider1.SetError(txtfname, null);
            }
            if (txtlname.Text == "")
            {
                errorProvider1.SetError(txtlname, "please enter your last name");
            }
            else
            {
                errorProvider1.SetError(txtlname, null);
            }
            if (txtmail.Text == "")
            {
                errorProvider1.SetError(txtmail, "please enter your email");
            }
            else
            {
                errorProvider1.SetError(txtmail, null);
            }
            if (txtpass.Text.Length<9)
            {
                errorProvider1.SetError(txtpass, "password must be larger than 9 letter");
            }
            else
            {
                errorProvider1.SetError(txtpass, null);
            }
            if (cmbgender.Text == "")
            {
                errorProvider1.SetError(cmbgender, "please enter your gender");
            }
            else
            {
                errorProvider1.SetError(cmbgender, null);
            }
            if (!radmanger.Checked && !radrenter.Checked)
            {
                errorProvider1.SetError(groupBox1, "please enter your job");
            }
            else
            {
                errorProvider1.SetError(groupBox1, null);
            } 
            #endregion

            if (txtfname.Text != "" && txtlname.Text != "" && txtmail.Text != "" && txtpass.Text.Length > 9 && cmbgender.Text != "" && errorProvider1.GetError(groupBox1) == "")
            {
                User U = new User(txtfname.Text, txtlname.Text, txtmail.Text, cmbgender.Text, radmanger.Checked, txtpass.Text, dtp_birth.Value);
                string d = U.Register();
                if (d == "The Email Is alredy Exist")
                {

                    notifyIcon1.ShowBalloonTip(2000, "Welcome", d, ToolTipIcon.Error);
                }
                else
                {
                    string[] arr = d.Split(',');
                    if (arr[0] == "Renter")
                    {
                        tabControl1.TabPages.Remove(tbg_register);
                        RAdd.Enabled = true;
                        RFname.Text = arr[1];
                        RLname.Text = arr[2];
                    }
                    else
                    {
                        notifyIcon1.ShowBalloonTip(2000, "Welcome", "Succesful Registration", ToolTipIcon.Info);
                    }
                }
            }
        }

        //***************************************************************
        //Select and Add and Delete And Update form Emp Table

        private void emplst_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = emplst.Items.Count - 1; i >= 0; i--)
            {
                if (emplst.Items[i].Selected)
                {
                    empid = int.Parse(emplst.Items[i].SubItems[0].Text);
                    EFname.Text = emplst.Items[i].SubItems[1].Text;
                    ELname.Text = emplst.Items[i].SubItems[2].Text;
                    EBirth.Text = emplst.Items[i].SubItems[3].Text;
                    EPhone.Text = emplst.Items[i].SubItems[4].Text;
                    ESalary.Text = emplst.Items[i].SubItems[5].Text;
                }
            }
        }

        private void EAdd_Click(object sender, EventArgs e)
        {
            Validation();
            if (ESalary.Text != "" && EFname.Text != "" && ELname.Text != "" && EPhone.Text != "")
            {
                Employee em = new Employee(EFname.Text, ELname.Text, EBirth.Value, int.Parse(EPhone.Text), int.Parse(ESalary.Text), int.Parse(EMan.Text),empid);
                em.Add();
                notifyIcon1.ShowBalloonTip(2000, "Welcome", "Emp Added", ToolTipIcon.Info);
                ShowItem(5, "Emp_db", this.emplst);
            }
        }

        private void btn_delete_emp_Click(object sender, EventArgs e)
        {
             Validation();
            if (ESalary.Text != "" && EFname.Text != "" && ELname.Text != "" && EPhone.Text != "")
            {
                Employee em = new Employee(empid);
                em.Delete();
                notifyIcon1.ShowBalloonTip(2000, "Welcome", "Emp Deleted", ToolTipIcon.Info);
                ShowItem(5, "Emp_db", this.emplst);
            }
        }

        private void EUpdate_Click(object sender, EventArgs e)
        {
             Validation();
            if (ESalary.Text != "" && EFname.Text != "" && ELname.Text != "" && EPhone.Text != "")
            {
                Employee em = new Employee(EFname.Text, ELname.Text, EBirth.Value, int.Parse(EPhone.Text), int.Parse(ESalary.Text), int.Parse(EMan.Text), empid);
                em.Update();
                notifyIcon1.ShowBalloonTip(2000, "Welcome", "Emp Updated", ToolTipIcon.Info);
                ShowItem(5, "Emp_db", this.emplst);
            }
        }

        //***************************************************************
        //Select and Add And Update form Dept Table

        private void deptlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = deptlst.Items.Count - 1; i >= 0; i--)
            {
                if (deptlst.Items[i].Selected)
                {
                    depid = int.Parse(deptlst.Items[i].SubItems[0].Text);
                    DName.Text = deptlst.Items[i].SubItems[1].Text;
                    DRent.Text = deptlst.Items[i].SubItems[2].Text;
                    if(deptlst.Items[i].SubItems[3].Text=="true")
                    {
                        DReserv.Checked = true;
                    }
                    else
                    {
                        DNReserv.Checked = true;
                    }
                }
            }
        }

        private void DAdd_Click(object sender, EventArgs e)
        {
            validation2();
            if (DName.Text != "" && DRent.Text != "" && errorProvider1.GetError(groupBox4) == "")
            {
                if (DReserv.Checked)
                {
                    m = true;
                }
                else
                {
                    m = false;
                }
                Dept d = new Dept(DName.Text, int.Parse(DRent.Text), m,depid);
                d.Add();
                notifyIcon1.ShowBalloonTip(2000, "Welcome", "Dept Added", ToolTipIcon.Info);
                ShowItem(3, "Dept_db", this.deptlst);
            }
        }
      
        private void DUpdate_Click(object sender, EventArgs e)
        {
            validation2();
            if (DName.Text != "" && DRent.Text != "" && errorProvider1.GetError(groupBox4) == "")
            {
                if (DReserv.Checked)
                {
                    m = true;
                }
                else
                {
                    m = false;
                }
                Dept d = new Dept(DName.Text, int.Parse(DRent.Text), m, depid);
                d.Update();
                notifyIcon1.ShowBalloonTip(2000, "Welcome", "Dept Updated", ToolTipIcon.Info);
                ShowItem(3, "Dept_db", this.deptlst);
            }
        }

        //****************************************************************
        //Select and Add and Delete And Update form Renter Table

        private void renterlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = renterlst.Items.Count - 1; i >= 0; i--)
            {
                if (renterlst.Items[i].Selected)
                {
                    renid= int.Parse(renterlst.Items[i].SubItems[0].Text);
                    RFname.Text = renterlst.Items[i].SubItems[1].Text;
                    RLname.Text = renterlst.Items[i].SubItems[2].Text;
                    RStart.Text = renterlst.Items[i].SubItems[3].Text;
                    RFinsh.Text = renterlst.Items[i].SubItems[4].Text;
                    RDept.Text = renterlst.Items[i].SubItems[5].Text;
                    if (renterlst.Items[i].SubItems[6].Text=="True")
                    {
                        radP.Checked = true;
                    }
                    else
                    {
                        radN.Checked = true;
                    }

                }
            }
        }

        private void RAdd_Click(object sender, EventArgs e)
        {
            validation3();
            if (RDept.Text!="" && RFname.Text!="" && RLname.Text!="" && errorProvider1.GetError(groupBox5)=="")
            {
                bool f;
                if (radP.Checked)
                {
                    f=true;
                }
                else
                {
                    f=false;
                }
	
                Renter r = new Renter(RFname.Text, RLname.Text, RStart.Value, RFinsh.Value, int.Parse(RDept.Text), int.Parse(RMan.Text),f,renid);
                r.Add();
                notifyIcon1.ShowBalloonTip(2000, "Welcome", "Renter Added", ToolTipIcon.Info);
                RAdd.Enabled = false;
                tabControl1.TabPages.Add(tbg_register);
                ShowItem(6, "Renter_db", this.renterlst);
            }
        }
      
        private void RDelete_Click(object sender, EventArgs e)
        {
            validation3();
            if (RDept.Text != "" && RFname.Text != "" && RLname.Text != "")
            {
                Renter r = new Renter(renid);
                r.Delete();
                notifyIcon1.ShowBalloonTip(2000, "Welcome", "Renter Deleted", ToolTipIcon.Info);
                ShowItem(6, "Renter_db", this.renterlst);
            }
        }

        private void RUpdate_Click(object sender, EventArgs e)
        {
            validation3();
             bool f;
                if (radP.Checked)
                {
                    f=true;
                }
                else
                {
                    f=false;
                }
	
            if (RDept.Text != "" && RFname.Text != "" && RLname.Text != "")
            {
                Renter r = new Renter(RFname.Text, RLname.Text, RStart.Value, RFinsh.Value, int.Parse(RDept.Text), int.Parse(RMan.Text),f,renid);
                r.Update();
                notifyIcon1.ShowBalloonTip(2000, "Welcome", "Renter Updated", ToolTipIcon.Info);
                ShowItem(6, "Renter_db", this.renterlst);
            }
        }

        //****************************************************************
        //Select and Add and Delete And Update form Product Table

        private void prodlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = prodlst.Items.Count - 1; i >= 0; i--)
            {
                if (prodlst.Items[i].Selected)
                {
                    proid = int.Parse(prodlst.Items[i].SubItems[0].Text);
                    PType.Text = prodlst.Items[i].SubItems[1].Text;
                    PPrice.Text = prodlst.Items[i].SubItems[2].Text;
                    PCount.Text = prodlst.Items[i].SubItems[3].Text;

                }
            }
        }

        private void PAdd_Click(object sender, EventArgs e)
        {
            validation4();
            if (PPrice.Text != "" && PType.Text != "" && PCount.Text != "")
            {
                Product p = new Product(PType.Text, int.Parse(PPrice.Text), int.Parse(PCount.Text), int.Parse(PRenter.Text),proid);
                p.Add();
                notifyIcon1.ShowBalloonTip(2000, "Welcome", "Product Added", ToolTipIcon.Info);
                ShowProd(3, this.prodlst);
            }
        }

        private void PDelete_Click(object sender, EventArgs e)
        {
            validation4();
            if (PPrice.Text != "" && PType.Text != "" && PCount.Text != "")
            {
                Product p = new Product(proid);
                p.Delete();
                notifyIcon1.ShowBalloonTip(2000, "Welcome", "Product Deleted", ToolTipIcon.Info);
                ShowProd(3, this.prodlst);
            }
        }

        private void PUpdate_Click(object sender, EventArgs e)
        {
             validation4();
             if (PPrice.Text != "" && PType.Text != "" && PCount.Text != "")
             {
                 Product p = new Product(PType.Text, int.Parse(PPrice.Text), int.Parse(PCount.Text), int.Parse(PRenter.Text),proid);
                 p.Update();
                notifyIcon1.ShowBalloonTip(2000, "Welcome", "Product Updated", ToolTipIcon.Info);
                ShowProd(3, this.prodlst);
             }
        }

        //***************************************************************
        //Close

        private void Registration_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form f = Application.OpenForms["Login"];
            f.Show();
        }

        //***************************************************************
        //ShowItems

        public void ShowItem(int i, string h,ListView ls)
        {
            ls.Items.Clear();
            cmd = new SqlCommand("Select * From "+ h , cn);
            cn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem lst = new ListViewItem();
                lst.Text = dr[0].ToString();
                for (int j = 1; j <=i; j++)
                {
                    lst.SubItems.Add(dr[j].ToString());
                    
                }
                ls.Items.Add(lst);
            }
            cn.Close();
            dr.Close();
        }

        //***********************************************************
        //show Products

        public void ShowProd(int i, ListView ls)
        {
            ls.Items.Clear();
            cmd = new SqlCommand("Select * From Prod_db where renter_id="+id, cn);
            cn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem lst = new ListViewItem();
                lst.Text = dr[0].ToString();
                for (int j = 1; j <= i; j++)
                {
                    lst.SubItems.Add(dr[j].ToString());

                }
                ls.Items.Add(lst);
            }
            cn.Close();
            dr.Close();
        }

        //***********************************************************
        //ShowNullDept
    
        public void SelectItem(int x,ComboBox co)
        {
            cmd = new SqlCommand("Select dept_id From Dept_db where dstate = "+x,cn);
            cn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                co.Items.Add(dr[0].ToString());
            }
            cn.Close();
            dr.Close();
        }

        //***********************************************************
        //Valdation Methods

        void Validation()
        {
            if (EFname.Text == "")
            {
                errorProvider1.SetError(EFname, "please enter your first name");
            }
            else
            {
                errorProvider1.SetError(EFname, null);
            }
            if (ELname.Text == "")
            {
                errorProvider1.SetError(ELname, "please enter your last name");
            }
            else
            {
                errorProvider1.SetError(ELname, null);
            }
            if (EPhone.Text == "")
            {
                errorProvider1.SetError(EPhone, "please enter your phone");
            }
            else
            {
                errorProvider1.SetError(EPhone, null);
            }
            if (ESalary.Text == "")
            {
                errorProvider1.SetError(ESalary, "please enter employee salary");
            }
            else
            {
                errorProvider1.SetError(ESalary, null);
            }
        }

        void validation2()
        {
            if (DName.Text == "")
            {
                errorProvider1.SetError(DName, "please enter Dept name");
            }
            else
            {
                errorProvider1.SetError(DName, null);
            }
            if (DRent.Text == "")
            {
                errorProvider1.SetError(DRent, "please enter Dept rent");
            }
            else
            {
                errorProvider1.SetError(DRent, null);
            }
            if (!DReserv.Checked && !DNReserv.Checked)
            {
                errorProvider1.SetError(groupBox4, "please enter Dept dstate");
            }
            else
            {
                errorProvider1.SetError(groupBox4, null);
            }
        }

        void validation3()
        {
            if (RFname.Text == "")
            {
                errorProvider1.SetError(RFname, "please enter first name");
            }
            else
            {
                errorProvider1.SetError(RFname, null);
            }
            if (RLname.Text == "")
            {
                errorProvider1.SetError(RLname, "please enter last name");
            }
            else
            {
                errorProvider1.SetError(RLname, null);
            }
            if (RDept.Text == "")
            {
                errorProvider1.SetError(RDept, "please enter dept_id");
            }
            else
            {
                errorProvider1.SetError(RDept, null);
            }
            if (!radP.Checked && !radN.Checked)
            {
                errorProvider1.SetError(groupBox5, "please enter renter state");
            }
            else
            {
                errorProvider1.SetError(groupBox5, null);
            }

        }

        void validation4()
        {
            if (PType.Text == "")
            {
                errorProvider1.SetError(PType, "please enter product type");
            }
            else
            {
                errorProvider1.SetError(PType, null);
            }
            if (PPrice.Text == "")
            {
                errorProvider1.SetError(PPrice, "please enter product price");
            }
            else
            {
                errorProvider1.SetError(PPrice, null);
            }
            if (PPrice.Text == "")
            {
                errorProvider1.SetError(PPrice, "please enter product price");
            }
            else
            {
                errorProvider1.SetError(PPrice, null);
            }
        }

        //***********************************************************
        //Report

        private void btn_report_Click(object sender, EventArgs e)
        {
            Report r=new Report();
            notifyIcon1.ShowBalloonTip(2000, "Welcome", "Report Send", ToolTipIcon.Info);
            r.ShowDialog();
        }

        //***********************************************************
        //Sell

        private void button3_Click(object sender, EventArgs e)
        {
            validation4();
            if (PPrice.Text != "" && PType.Text != "" && PCount.Text != "")
            {
                Product p = new Product(PType.Text, int.Parse(PPrice.Text), (int.Parse(PCount.Text)-1), int.Parse(PRenter.Text),proid);
                p.Update();
                notifyIcon1.ShowBalloonTip(2000, "Welcome", "Product Selled", ToolTipIcon.Info);
                ShowProd(3, this.prodlst);
            }
        }

        //***********************************************************
        //Show Report

        private void button4_Click(object sender, EventArgs e)
        {
            Report r = new Report();
            notifyIcon1.ShowBalloonTip(2000, "Welcome", "Show Report", ToolTipIcon.Info);
            r.ShowDialog();
        }

        //***********************************************************
        //Prevent

        void num(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar ==(char)Keys.Back)
            {
                e.Handled = false; 
            }
            else
            {
                e.Handled = true; 
            }
        }
        
        void str(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 'a' && e.KeyChar <= 'z' || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
