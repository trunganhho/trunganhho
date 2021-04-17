using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VietEngDict.Model;
using System.Data.SqlClient;
using System.Data;

namespace VietEngDict
{
    public partial class ModifyWord : System.Web.UI.Page
    {
        public string query;
        public Connection connection = new Connection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGV();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            query = "select * from tbl_edict where word = '" + txtWord.Text + "'";
            SqlDataReader dr = connection.GetRow(query);
            if (dr.Read())
            {
                Response.Write(@"<script language='javascript'>alert('Đã có từ này, vui lòng nhập lại!')</script>");
                connection.conn.Close();
                return;
            }
            else
            {
                connection.conn.Close();
                string detail = "";
                string[] temp = txtDetail.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach(var s in temp)
                {
                    detail += s + "<br /> ";
                }
                query = "insert into tbl_edict(word,detail) values('" + txtWord.Text + "','" + detail + "')";
                connection.NonQuery(query);
                Response.Write(@"<script language='javascript'>alert('Thêm từ thành công!')</script>");
                PopulateGV();
            }
        }

        void PopulateGV()
        {
            DataTable dt = connection.GetTable("select * from tbl_edict");
            if (dt.Rows.Count > 0)
            {
                gvWord.DataSource = dt;
                gvWord.DataBind();
            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvWord.DataSource = dt;
                gvWord.DataBind();
                gvWord.Rows[0].Cells.Clear();
                gvWord.Rows[0].Cells.Add(new TableCell());
                gvWord.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                gvWord.Rows[0].Cells[0].Text = "No data found....!";
                gvWord.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void gvWord_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Select")
            {
                txtDetail.Text = "";
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvWord.Rows[rowIndex];
                txtWord.Text = row.Cells[1].Text;
                SqlDataReader dr = connection.GetRow("Select detail from tbl_edict where word = '" + txtWord.Text + "'");
                if (dr.Read())
                {
                    string[] temp = dr["detail"].ToString().Split(new string[] { "<br />" }, StringSplitOptions.None);
                    foreach(var s in temp)
                    {
                        txtDetail.Text += @s + "\n";
                    }
                }
                connection.conn.Close();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string word = txtWord.Text;
            string detail = "";
            string[] temp = txtDetail.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var s in temp)
            {
                detail += s + "<br /> ";
            }
            query = "update tbl_edict set word = '" + txtWord.Text + "', detail = '" + detail + "' where word = '" + word + "'";
            connection.NonQuery(query);
            Response.Write(@"<script language='javascript'>alert('Sửa từ thành công!')</script>");
            PopulateGV();
        }

        protected void gvWord_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvWord.DataKeys[e.RowIndex].Value.ToString());

                query = @"delete from tbl_edict where idx = " + id;

                connection.NonQuery(query);
                PopulateGV();
                Response.Write(@"<script language='javascript'>alert('Xóa thành công')</script>");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}