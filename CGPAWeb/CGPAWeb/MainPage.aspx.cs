using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CGPAWeb
{
    public partial class MainPage : System.Web.UI.Page
    {
        private readonly MyCGPALinqDataContext _db = new MyCGPALinqDataContext();
        public List<int> Mylist = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnCompute.Visible = false;
                ClearControl();
                btnSubmit.Visible = false;
                lblError.Visible = false;
                DepartmentClass.ReturnDept(ddlDept);
                DepartmentClass.ReturnSemester(ddlSemester);
                DepartmentClass.ReturnLevel(ddlLevel);
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (ddlDept.SelectedIndex != 0 && ddlLevel.SelectedIndex != 0 && ddlSemester.SelectedIndex != 0)
            {
                courseGridView.Visible = true;
                btnSubmit.Visible = true;
                lblError.Visible = false;
                int dId = DepartmentClass.ReturnDeptId(ddlDept.SelectedItem.ToString());
                int sId = DepartmentClass.ReturnSemesterId(ddlSemester.SelectedItem.ToString());
                int lId = DepartmentClass.ReturnLevelId(Convert.ToInt32(ddlLevel.SelectedItem.ToString()));
                DepartmentClass.BindToCourseGridView(courseGridView, dId, sId, lId);
                btnCompute.Visible = true;
            }
            else
            {
                courseGridView.Visible = false;
                lblError.Visible = true;
                lblError.Text = "Please select missing value!";
            }
        }

        protected void btnCompute_OnClick(object sender, EventArgs e)
        {
            if (ddlSemester.SelectedIndex == 1)
            {
                ScoreToGrade();
                GradeToPoint();
                lblTotalPoint.Text = TotalPoint().ToString();
                lblTotalUnit.Text = TotalCourseUnit().ToString();
                lblGPA.Text = ComputeGpa();
                lblCGPA.Text = lblGPA.Text;
                float cgpa = float.Parse(lblCGPA.Text);
                //lblCGPA.Text = ComputeFirstCGpa();
                lblRemark.Text = ReturnRemark(cgpa);
                btnSubmit.Visible = true;
            }
            else
            {
                ScoreToGrade();
                GradeToPoint();
                lblTotalPoint.Text = TotalPoint().ToString();
                lblTotalUnit.Text = TotalCourseUnit().ToString();
                lblGPA.Text = ComputeGpa();
                lblCGPA.Text = ComputeSecondCGpa();
                float cgpa = float.Parse(lblCGPA.Text);
                lblRemark.Text = ReturnRemark(cgpa);
                btnSubmit.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtMatric.Text == string.Empty)
            {
                lblError.Visible = true;
                lblError.Text = "Please supply your MatricNo!";
            }
            else
            {
                //AddToStudentDetails();
                if (ddlSemester.SelectedIndex == 1)
                {
                    AddToStudentDetails();
                    lblSuccess.Text = "Result Saved Successfully";
                    //AddFirstSemesterResult();
                }
                else
                {
                    int stdI = Convert.ToInt32(txtMatric.Text);
                    int lId = DepartmentClass.ReturnLevelId(Convert.ToInt32(ddlLevel.SelectedItem.ToString()));
                    int dId = DepartmentClass.ReturnDeptId(ddlDept.SelectedItem.ToString());
                    int sId = DepartmentClass.ReturnSemesterId(ddlSemester.SelectedItem.ToString());
                    bool res = DepartmentClass.IsExist(stdI, lId, dId, sId);
                    if (res == true)
                    {
                        courseGridView.Visible = false;
                        lblError.Visible = true;
                        lblError.Text = "Sorry! Please kindly register for first semester.";
                    }
                    else
                    {
                        lblError.Visible = false;
                        AddSecondSemesterResult();
                        lblSuccess.Text = "Result Saved Successfully";
                    }
                }

            }
        }

        //Save to Student details table
        public void AddToStudentDetails()
        {
            int stdI = Convert.ToInt32(txtMatric.Text);
            int lId = DepartmentClass.ReturnLevelId(Convert.ToInt32(ddlLevel.SelectedItem.ToString()));
            int dId = DepartmentClass.ReturnDeptId(ddlDept.SelectedItem.ToString());
            int sId = DepartmentClass.ReturnSemesterId(ddlSemester.SelectedItem.ToString());
            bool res = DepartmentClass.IsExist(stdI, lId, dId, sId);
            if (res.Equals(false))
            {
                lblError.Visible = false;
                var dt = new StudentDetail
                {
                    StudentId = stdI,
                    LevelId = lId,
                    DeptId = dId,
                    SemesterId = sId
                };
                _db.StudentDetails.InsertOnSubmit(dt);
                AddFirstSemesterResult();
                _db.SubmitChanges();
            }
            else
            {
                courseGridView.Visible = false;
                lblError.Visible = true;
                lblError.Text = "Sorry! Result for this MatricNo already exist.";
                ClearControl();
            }
        }

        //Save first semester result to database
        public void AddFirstSemesterResult()
        {
            int studentid = Convert.ToInt32(txtMatric.Text);
            int firsttotalpoint = Convert.ToInt32(lblTotalPoint.Text);
            int firsttotalunit = Convert.ToInt32(lblTotalUnit.Text);
            string gpa = lblGPA.Text;
            //string remark = ReturnRemark(gpa);
            bool check = IsResultExist(studentid, gpa);
            if (check)
            {
                lblError.Text = "Sorry! Result already exist.";
            }
            else
            {
                var res = new _100LResultTable
                {
                    StudentId = studentid,
                    FirstSemesterTPU = firsttotalpoint,
                    FirstSemesterTCU = firsttotalunit,
                    FirstSemesterGPA = gpa
                };
                _db._100LResultTables.InsertOnSubmit(res);
                //_db.SubmitChanges();
            }
        }

        //Save second semester result to database
        public void AddSecondSemesterResult()
        {
            int studentid = Convert.ToInt32(txtMatric.Text);
            //second sem totalpoint
            int Secondtotalpoint = Convert.ToInt32(lblTotalPoint.Text);
            int Secondtotalunit = Convert.ToInt32(lblTotalUnit.Text);
            
            //second sem gpa
            string gpa = lblGPA.Text;
            int ftp = 0;
            int ftu = 0;
            var spoint = _db._100LResultTables.SingleOrDefault(p => p.StudentId == studentid);
            if (spoint != null)
            {
                ftp = Convert.ToInt32(spoint.FirstSemesterTPU);
                ftu = Convert.ToInt32(spoint.FirstSemesterTCU);
            }
            float pointAdd = ftp + Secondtotalpoint;
            float unitAdd = ftu + Secondtotalunit;
            float cgpa = pointAdd/unitAdd;
            //float c = float.Parse(cgpa);
            //remark
            string remark = ReturnRemark(cgpa);
            bool check = IsResultExist(studentid, gpa);
            if (check)
            {
                lblError.Text = "Sorry! Result already exist.";
            }
            else
            {
                var d = _db._100LResultTables.FirstOrDefault(p => p.StudentId==studentid);
                if (d != null)
                {
                    d.SecondSemesterTPU = Secondtotalpoint;
                    d.SecondSemesterTCU = Secondtotalunit;
                    d.SecondSemesterGPA = gpa;
                    d.CGPA = cgpa.ToString("F");
                    d.Remark = remark;
                }
                _db.SubmitChanges();
            }
        }

        //check whether student result already exist.
        public bool IsResultExist(int id, string fgpa)
        {
            var d = _db._100LResultTables.Where(p => p.StudentId == id && p.FirstSemesterGPA == fgpa);
            if (d.Count() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Determine remark
        public string ReturnRemark(float cgpa)
        {
            string rem;
            if (cgpa >= 4.50 && cgpa <= 5.00)
            {
                rem = "FIRST CLASS";
            }
            else if (cgpa >= 3.50 && cgpa <= 4.49)
            {
                rem = "SECOND CLAS UPPER";
            }
            else if (cgpa >= 2.50 && cgpa <= 3.49)
            {
                rem = "SECOND CLAS LOWER";
            }
            else if (cgpa >= 1.50 && cgpa <= 2.49)
            {
                rem = "THIRD CLASS";
            }
            else if (cgpa >= 1.00 && cgpa <= 1.49)
            {
                rem = "PASS";
            }
            else
            {
                rem = "YOU ARE ON PROBATION";
            }
            return rem;
        }

        //CLEAR CONTROL
        public void ClearControl()
        {
            lblTotalPoint.Text = "";
            lblTotalUnit.Text = "";
            lblGPA.Text = "";
            lblCGPA.Text = "";
        }

        //SCORE TO GRADE
        public void ScoreToGrade()
        {
            foreach (GridViewRow row in courseGridView.Rows)
            {
                var txtS = ((TextBox)row.FindControl("txtScore"));
                var lblG = ((Label)row.FindControl("lblGrade"));
                int score = int.Parse(txtS.Text);
                char grade = ComputeResult.ConvertScoreToGrade(score);
                lblG.Text = grade.ToString();
            }
        }

        //GRADE TO POINT
        public void GradeToPoint()
        {
            foreach (GridViewRow row in courseGridView.Rows)
            {
                var lblG = ((Label)row.FindControl("lblGrade"));
                var lblCunit = ((Label)row.FindControl("lblCourseUnit"));
                int point = ComputeResult.ConvertGradeToPoint(lblG.Text);
                int courseunit = Convert.ToInt32(lblCunit.Text);
                int res = courseunit * point;
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
            float gpa = totalpoint / totalunit;
            return gpa.ToString("F");
        }

        //COMPUTE First CGPA
        public string ComputeFirstCGpa()
        {
            //1st Semester TotalPoint & TotalUnit
            int firsttotalpoint = TotalPoint();
            int firsttotalunit = TotalCourseUnit();

            //2nd Semester TotalPoint & TotalUnit
            const int secondtotalpoint = 0;
            const int secondtotalunit = 0;

            //1st + 2nd Total Point
            int fstp = firsttotalpoint + secondtotalpoint;

            //1st + 2nd Total Unit
            int fstu = firsttotalunit + secondtotalunit;

            //cgpa
            string cgpa = (fstp / fstu).ToString("F");
            return cgpa;
        }

        //COMPUTE Second CGPA
        public string ComputeSecondCGpa()
        {
            int stdI = Convert.ToInt32(txtMatric.Text);
            int ftp = 0;
            int ftu = 0;
            var spoint = _db._100LResultTables.SingleOrDefault(p => p.StudentId == stdI);
            if (spoint != null)
            {
                ftp = Convert.ToInt32(spoint.FirstSemesterTPU);
                ftu = Convert.ToInt32(spoint.FirstSemesterTCU);
            }
            //1st Semester TotalPoint & TotalUnit
            int firsttotalpoint = ftp;
            int firsttotalunit = ftu;

            //2nd Semester TotalPoint & TotalUnit
            int secondtotalpoint = TotalPoint();
            int secondtotalunit = TotalCourseUnit();

            //1st + 2nd Total Point
            float fstp = firsttotalpoint + secondtotalpoint;

            //1st + 2nd Total Unit
            float fstu = firsttotalunit + secondtotalunit;

            //cgpa
            float cgpa = fstp/fstu;
            return cgpa.ToString("F");
        }

        protected void ddlSemester_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            courseGridView.Visible = false;
            ClearControl();
            btnCompute.Visible = false;
            btnSubmit.Visible = false;
        }
    }
}