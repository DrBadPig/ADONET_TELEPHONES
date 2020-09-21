using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADONET_TELEPHONES
{
    public partial class Form1 : Form
    {
        DataSet ds;
        SqlDataAdapter adapterPh;
        SqlDataAdapter adapterInd;
        SqlConnection cn;

        List<String> inds = new List<string>();

        public Form1()
        {
            InitializeComponent();

            ds = new DataSet();
            cn = new SqlConnection();

            string connection = ConfigurationManager.ConnectionStrings["ADONET_TELEPHONES.Properties.Settings.DBConnection"].ConnectionString;

            cn.ConnectionString = connection;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;

            inds.Clear();

            string commandPh = "select * from phones";
            string commandInd = "select * from industries";

            SqlCommand cmd = new SqlCommand(commandPh, cn);
            SqlCommand cmd2 = new SqlCommand(commandInd, cn);

            adapterPh = new SqlDataAdapter(cmd);
            adapterInd = new SqlDataAdapter(cmd2);

            ds.Clear();
          
            adapterPh.Fill(ds, "phones");
            adapterInd.Fill(ds, "industries");

            if (ds.Tables["phones"].Constraints.Count != 0)
            {
                ds.Tables["phones"].Constraints.Clear();
            }


            dataGridView1.DataSource = ds.Tables["phones"];
            dataGridView2.DataSource = ds.Tables["industries"];

            foreach (DataRow item in ds.Tables[1].Rows)
            {
                inds.Add(item[0].ToString());
            }

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView2.Columns[0].ReadOnly = true;

         //   DataRelation drel = new DataRelation("relat", ds.Tables["industries"].Columns["ind_id"], ds.Tables["phones"].Columns["ind_id"]);



            ForeignKeyConstraint constr1 = new ForeignKeyConstraint("constr1", ds.Tables["industries"].Columns["ind_id"], ds.Tables["phones"].Columns["ind_id"]);
            constr1.UpdateRule = Rule.None;
            constr1.DeleteRule = Rule.Cascade;
            constr1.AcceptRejectRule = AcceptRejectRule.None;
            ds.Tables["phones"].Constraints.Add(constr1);

            //ds.Relations.Add(drel);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch(comboBox.SelectedIndex)
            {
                case 0:
                    Phone_rec recPH = new Phone_rec();

                    AddPhone addPH = new AddPhone(recPH, inds);
                    DialogResult res1 = addPH.ShowDialog();

                    if (res1 == DialogResult.OK)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr["t_id"] = recPH.Id;
                        dr["model"] = recPH.Model;
                        dr["ind_id"] = recPH.Ind_id;
                        dr["year"] = recPH.Year;
                        dr["price"] = recPH.Price;
                        ds.Tables[0].Rows.Add(dr);
                    }

                    break;
                case 1:
                    Industry_rec recIND = new Industry_rec();

                    AddIndustry addIND = new AddIndustry(recIND);
                    DialogResult res2 = addIND.ShowDialog();

                    if (res2 == DialogResult.OK)
                    {
                        DataRow dr = ds.Tables[1].NewRow();
                        dr["ind_id"] = recIND.Id;
                        dr["name"] = recIND.Name;
                        dr["country"] = recIND.Country;
                        dr["type"] = recIND.Type;
                        dr["website"] = recIND.Website;
                        ds.Tables[1].Rows.Add(dr);
                    }

                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///phone

            //insert
            string com = "insert into phones(t_id, model, ind_id, year, price) values (@p1, @p2, @p3, @p4, @p5)";
            SqlCommand command = new SqlCommand(com, cn);
            command.Parameters.Add("@p1", SqlDbType.Int, 4, "t_id");
            command.Parameters.Add("@p2", SqlDbType.NVarChar, 50, "model");
            command.Parameters.Add("@p3", SqlDbType.NVarChar, 4, "ind_id");
            command.Parameters.Add("@p4", SqlDbType.Int, 4, "year");
            command.Parameters.Add("@p5", SqlDbType.Money, 30, "price");
            adapterPh.InsertCommand = command;

            //delete
            com = "delete from phones where t_id=@p1";
            command = new SqlCommand(com, cn);
            command.Parameters.Add("@p1", SqlDbType.Int, 4, "t_id");
            adapterPh.DeleteCommand = command;

            //update
            com = "update phones set model=@p2, ind_id=@p3, year=@p4, price=@p5 where t_id=@p1";
            command = new SqlCommand(com, cn);
            command.Parameters.Add("@p1", SqlDbType.Int, 4, "t_id");
            command.Parameters.Add("@p2", SqlDbType.NVarChar, 50, "model");
            command.Parameters.Add("@p3", SqlDbType.NVarChar, 4, "ind_id");
            command.Parameters.Add("@p4", SqlDbType.Int, 4, "year");
            command.Parameters.Add("@p5", SqlDbType.Money, 30, "price");
            adapterPh.UpdateCommand = command;

            ///industries

            //insert
            string com2 = "insert into industries(ind_id, name, country, type, website) values (@p1, @p2, @p3, @p4, @p5)";
            SqlCommand command2 = new SqlCommand(com2, cn);
            command2.Parameters.Add("@p1", SqlDbType.NVarChar, 4, "ind_id");
            command2.Parameters.Add("@p2", SqlDbType.NVarChar, 30, "name");
            command2.Parameters.Add("@p3", SqlDbType.NVarChar, 20, "country");
            command2.Parameters.Add("@p4", SqlDbType.NVarChar, 20, "type");
            command2.Parameters.Add("@p5", SqlDbType.NVarChar, 30, "website");
            adapterInd.InsertCommand = command2;

            //delete
            com2 = "delete from industries where ind_id=@p1";
            command2 = new SqlCommand(com2, cn);
            command2.Parameters.Add("@p1", SqlDbType.NVarChar, 4, "ind_id");
            adapterInd.DeleteCommand = command2;

            //update
            com2 = "update industries set name=@p2, country=@p3, type=@p4, website=@p5 where ind_id=@p1";
            command2 = new SqlCommand(com2, cn);
            command2.Parameters.Add("@p1", SqlDbType.NVarChar, 4, "ind_id");
            command2.Parameters.Add("@p2", SqlDbType.NVarChar, 30, "name");
            command2.Parameters.Add("@p3", SqlDbType.NVarChar, 40, "country");
            command2.Parameters.Add("@p4", SqlDbType.NVarChar, 20, "type");
            command2.Parameters.Add("@p5", SqlDbType.NVarChar, 30, "website");
            adapterInd.UpdateCommand = command2;


            adapterPh.Update(ds.Tables[0]);
            ds.Tables[0].Clear();

            adapterInd.Update(ds.Tables[1]);
            ds.Tables[1].Clear();

            adapterInd.Fill(ds.Tables[1]);
            adapterPh.Fill(ds.Tables[0]);
        }
    }
}
