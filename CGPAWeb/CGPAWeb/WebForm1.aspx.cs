using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CGPAWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private readonly MyCGPALinqDataContext _db = new MyCGPALinqDataContext();
        public List<int> Mylist = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindToGridView();
            }
        }

        public void BindToGridView()
        {
            
            //int dId = DepartmentClass.ReturnDeptId(ddlDept.Text);
            //int sId = DepartmentClass.ReturnSemesterId(ddlSemester.Text);
            //int lId = DepartmentClass.ReturnLevelId(Convert.ToInt32(ddlLevel.Text));
            //var d = _db.CourseTables.Where(p => p.DeptId == dId && p.SemesterId == sId && p.LevelId == lId);
            int dId = 1;
            int sId = 1;
            int lId = 1;
            //var d = from a in _db.GetTable<CourseTable>()
            //        where (a.DeptId == 1 && a.SemesterId == sId && a.LevelId == lId)
            //        select a;
            var d = _db.CourseTables.Where(p => p.DeptId == 1 && p.SemesterId == sId && p.LevelId == lId);
            courseGridView.DataSource = d;
            courseGridView.DataBind();
        }

        protected void btnClick_OnClick(object sender, EventArgs e)
        {
            ScoreToGrade();
            GradeToPoint();
            txtTotalUnit.Text = TotalCourseUnit().ToString();
            txtTotalPoint.Text = TotalPoint().ToString();
            lblGPA.Text = ComputeGpa();
        }

        //SCORE TO GRADE
        public void ScoreToGrade()
        {
            foreach (GridViewRow row in courseGridView.Rows)
            {
                var txtS = ((TextBox)row.FindControl("txtScore"));
                var txtG = ((TextBox)row.FindControl("txtGrade"));
                int score = int.Parse(txtS.Text);
                char grade = ComputeResult.ConvertScoreToGrade(score);
                txtG.Text = grade.ToString();
            }
            //return grade.ToString();
        }
        
        //GRADE TO POINT
        public void GradeToPoint()
        {
            foreach (GridViewRow row in courseGridView.Rows)
            {
                var txtG = ((TextBox)row.FindControl("txtGrade"));
                var lblCunit = ((Label)row.FindControl("lblCourseUnit"));
                int point = ComputeResult.ConvertGradeToPoint(txtG.Text);
                int courseunit = Convert.ToInt32(lblCunit.Text);
                int res = courseunit*point;
                //add to list
                Mylist.Add(res);
            }
        }

        //TOTAL COURSE UNIT
        public int TotalCourseUnit()
        {
            //return (from GridViewRow row in courseGridView.Rows select ((Label)row.FindControl("lblCourseUnit")) into lblCunit select Convert.ToInt32(lblCunit.Text)).Sum();
            int unit = 0;
            foreach (GridViewRow row in courseGridView.Rows)
            {
                var lblCunit = ((Label)row.FindControl("lblCourseUnit"));
                int courseunit = Convert.ToInt32(lblCunit.Text);
                unit += courseunit;
            }
            return unit;
        }

        //TOTAL POINT
        public int TotalPoint()
        {
            int pt = 0;
            pt += Mylist.Sum();
            return pt;
        }

        //COMPUTE GPA
        public string ComputeGpa()
        {
            float totalpoint = TotalPoint();
            float totalunit = TotalCourseUnit();
            float gpa = totalpoint/totalunit;
            return gpa.ToString("F");
        }
    }
}









//protected void btnClick_OnClick(object sender, EventArgs e)
//        {
//            //string txt =((DropDownList)row.FindControl("ddlOne")).SelectedItem.Value;
//            //Response.Write(txt);
//            int unit = 0;
//            int point = 0;
//            foreach (GridViewRow row in courseGridView.Rows)
//            {
//                var txtS = ((TextBox)row.FindControl("txtScore"));
//                var txtG = ((TextBox) row.FindControl("txtGrade"));
//                var lblP = ((Label)row.FindControl("lblPoint"));
//                var lblPunit = ((Label)row.FindControl("lblPUnit"));
//                var lblCunit = ((Label) row.FindControl("lblCourseUnit"));
//                int score = int.Parse(txtS.Text);
                
//                //compute grade
//                char grade = ComputeResult.ConvertScoreToGrade(score);
//                txtS.Focus();
//                txtG.Text = grade.ToString();
                
//                //compute point
//                int p = ComputeResult.ConvertGradeToPoint(txtG.Text);
//                txtG.Focus();
//                lblP.Text = p.ToString();
                
//                //compute point*unit
//                lblCunit.Focus();
//                int cunit = Convert.ToInt32(lblCunit.Text);
//                int pt = Convert.ToInt32(lblP.Text);
//                int res = cunit*pt;
//                lblPunit.Text = res.ToString();
                
//                //total unit
//                unit += cunit;
//                txtTotalUnit.Text = unit.ToString();
                
//                //total point
//                int pu = Convert.ToInt32(lblPunit.Text);
//                point += pu;
//                txtTotalPoint.Text = point.ToString();
//            }
//            //compute GPA
//            float tp = float.Parse(txtTotalPoint.Text);
//            float tu = float.Parse(txtTotalUnit.Text);
//            float result = (tp/tu);
//            lblGPA.Text = result.ToString("F");
//        }