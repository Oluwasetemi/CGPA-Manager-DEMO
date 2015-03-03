using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CGPAWeb
{
    public class DepartmentClass
    {
        private static MyCGPALinqDataContext _db = new MyCGPALinqDataContext();

        public int StudentId;
        public string Department;

        public static void ReturnDept(DropDownList ddl)
        {
            var c = _db.DepartmentTables.ToList();
            ddl.DataSource = c;
            ddl.DataBind();
        }

        public static int ReturnDeptId(string deptname)
        {
            var c = _db.DepartmentTables.Where(i => i.DeptName == deptname).Select(p => p.DeptId).SingleOrDefault();
            return c;
        }

        public static void ReturnLevel(DropDownList ddl)
        {
            var c = _db.LevelTables.ToList();
            ddl.DataSource = c;
            ddl.DataTextField = "LevelName";
            ddl.DataValueField = "LevelId";
            ddl.Items.Insert(0, "--select--");
            ddl.AppendDataBoundItems = true;
            ddl.DataBind();
        }

        public static int ReturnLevelId(int lname)
        {
            var c = _db.LevelTables.Where(i => i.LevelName == lname).Select(p => p.LevelId).SingleOrDefault();
            return c;
        }

        public static void ReturnSemester(DropDownList ddl)
        {
            var c = _db.SemesterTables.ToList();
            ddl.DataValueField = "SemesterId";
            ddl.DataTextField = "SemesterName";
            ddl.Items.Insert(0, new ListItem("--select--", "0"));
            ddl.DataSource = c;
            ddl.DataBind();
        }

        public static int ReturnSemesterId(string sname)
        {
            var c = _db.SemesterTables.Where(i => i.SemesterName == sname).Select(p => p.SemesterId).SingleOrDefault();
            return c;
        }

        public static bool IsExist(int stdId, int lId, int dId, int sId)
        {
            //var c = _db.StudentDetails.Where(s => s.StudentId == stdId && s.LevelId==lId && s.DeptId==dId && s.SemesterId==sId).Select(p => p.StudentId).ToList();
            var c = _db.StudentDetails.Where(s => s.StudentId == stdId && s.LevelId==lId && s.DeptId==dId && s.SemesterId==sId);
            if (c.Count() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void BindToCourseGridView(GridView gridView, int dId, int sId, int lId)
        {
            var d = _db.CourseTables.Where(p => p.DeptId == dId && p.SemesterId == sId && p.LevelId == lId);
            //var d = from a in _db.GetTable<CourseTable>()
            //        where (a.DeptId == dId && a.SemesterId == sId && a.LevelId == lId)
            //        select a;
            gridView.DataSource = d;
            gridView.DataBind();
        }
    } 
}