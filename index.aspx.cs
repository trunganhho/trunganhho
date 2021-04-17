using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using VietEngDict.Model;
using System.Data;

namespace VietEngDict
{
    public partial class index : System.Web.UI.Page
    {
        public Connection connection = new Connection();
        public string query;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            query = "select word from tbl_edict where word like = '%" + txtSearch.Text +"'";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lb1.Text = "";
            lb2.Text = "";
            lb3.Text = "";
            lb4.Text = "";
            
            RelatedWord rw = new RelatedWord();
            query = @"select * from tbl_formsofword where NOUN like '%" + txtSearch.Text + "%' or VERB like '%" + txtSearch.Text + "%' or ADJECTIVE like '%" + txtSearch.Text + "%' or ADVERB like '%" + txtSearch.Text + "%'";
            SqlDataReader dr = connection.GetRow(query);
            DataTable dt;

            if (dr.Read())
            {
                rw.noun = dr["NOUN"].ToString().Split(new char[] { '/', '\\' });
                rw.verb = dr["VERB"].ToString().Split(new char[] { '/', '\\' });
                rw.adj = dr["ADJECTIVE"].ToString().Split(new char[] { '/', '\\' });
                rw.adv = dr["ADVERB"].ToString().Split(new char[] { '/', '\\' });
            }
            else
            {
                string[] temp = new string[1];
                temp[0] = txtSearch.Text;
                rw.noun = temp;
            }
            connection.conn.Close();

            for(int i = 0; i < rw.noun?.Length; i++)
            {
                query = "select detail from tbl_edict where word = '" + rw.noun[i] + "'";
                dt = connection.GetTable(query);
                foreach(DataRow row in dt.Rows)
                {
                    lb1.Text += row["detail"]?.ToString();
                }
            }

            for (int i = 0; i < rw.verb?.Length; i++)
            {
                query = "select detail from tbl_edict where word = '" + rw.verb[i] + "'";
                dt = connection.GetTable(query);
                foreach (DataRow row in dt.Rows)
                {
                    lb2.Text += row["detail"]?.ToString();
                }
            }
            for (int i = 0; i < rw.adj?.Length; i++)
            {
                query = "select detail from tbl_edict where word = '" + rw.adj[i] + "'";
                dt = connection.GetTable(query);
                foreach (DataRow row in dt.Rows)
                {
                    lb3.Text += row["detail"]?.ToString();
                }
            }
            for (int i = 0; i < rw.adv?.Length; i++)
            {
                query = "select detail from tbl_edict where word = '" + rw.adv[i] + "'";
                dt = connection.GetTable(query);
                foreach (DataRow row in dt.Rows)
                {
                    lb4.Text += row["detail"]?.ToString();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            query = "select * from tbl_account where account = '" + txtAcc.Text + "' and password = '" + txtPass.Text + "'";
            SqlDataReader dr = connection.GetRow(query);
            if (dr.Read())
            {
                connection.conn.Close();
                Response.Redirect("AdminDashboard.aspx");
            }
            else
            {
                connection.conn.Close();
                Response.Write(@"<script language='javascript'>alert('Sai thông tin đăng nhập, vui lòng thử lại')</script>");
            }
        }
    }
}