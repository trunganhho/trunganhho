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
    public partial class ModifyRelatedWord : System.Web.UI.Page
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

        void PopulateGV()
        {
            DataTable dt = connection.GetTable("Select * from tbl_formsofword");
            if (dt.Rows.Count > 0)
            {
                gvRelatedWord.DataSource = dt;
                gvRelatedWord.DataBind();
            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvRelatedWord.DataSource = dt;
                gvRelatedWord.DataBind();
                gvRelatedWord.Rows[0].Cells.Clear();
                gvRelatedWord.Rows[0].Cells.Add(new TableCell());
                gvRelatedWord.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                gvRelatedWord.Rows[0].Cells[0].Text = "No data found....!";
                gvRelatedWord.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        protected void gvRelatedWord_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    string NOUN = (gvRelatedWord.FooterRow.FindControl("txtNounFooter") as TextBox).Text.Trim();
                    string VERB = (gvRelatedWord.FooterRow.FindControl("txtVerbFooter") as TextBox).Text.Trim();
                    string ADJ = (gvRelatedWord.FooterRow.FindControl("txtAdjFooter") as TextBox).Text.Trim();
                    string ADV = (gvRelatedWord.FooterRow.FindControl("txtAdvFooter") as TextBox).Text.Trim();
                    query = "Insert into tbl_formsofword(NOUN,VERB,ADJECTIVE,ADVERB) values('" + NOUN + "','" + VERB + "','" + ADJ + "','" + ADV + "')";
                    connection.NonQuery(query);
                    PopulateGV();
                    Response.Write(@"<script language='javascript'>alert('Thêm vào thành công')</script>");
                }
            }
            catch(Exception ex) {
                Response.Write(ex.Message);
            }
        }

        protected void gvRelatedWord_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvRelatedWord.EditIndex = e.NewEditIndex;
            PopulateGV();
        }

        protected void gvRelatedWord_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvRelatedWord.EditIndex = -1;
            PopulateGV();
        }

        protected void gvRelatedWord_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string NOUN = (gvRelatedWord.Rows[e.RowIndex].FindControl("txtNoun") as TextBox).Text.Trim();
                string VERB = (gvRelatedWord.Rows[e.RowIndex].FindControl("txtVerb") as TextBox).Text.Trim();
                string ADJ = (gvRelatedWord.Rows[e.RowIndex].FindControl("txtAdj") as TextBox).Text.Trim();
                string ADV = (gvRelatedWord.Rows[e.RowIndex].FindControl("txtAdv") as TextBox).Text.Trim();
                int id = Convert.ToInt32(gvRelatedWord.DataKeys[e.RowIndex].Value.ToString());

                query = @"Update tbl_formsofword 
                set NOUN = '" + NOUN + "', VERB = '" + VERB + "', ADJECTIVE = '" + ADJ + "', ADVERB = '" + ADV + "' where STT = " + id + "";
                
                connection.NonQuery(query);
                gvRelatedWord.EditIndex = -1;
                PopulateGV();
                Response.Write(@"<script language='javascript'>alert('Cập nhật thành công')</script>");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void gvRelatedWord_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvRelatedWord.DataKeys[e.RowIndex].Value.ToString());

                query = @"delete from tbl_formsofword where STT = " + id;

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