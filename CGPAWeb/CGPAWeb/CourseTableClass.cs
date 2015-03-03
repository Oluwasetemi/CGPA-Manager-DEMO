using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CGPAWeb
{
    public class CourseTableClass
    {
        public string CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int CourseUnit { get; set; }
        public int DeptId { get; set; }
        public int LevelId { get; set; }
        public int SemesterId { get; set; }
    }
}