using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CGPAWeb
{
    public class ComputeResult
    {
        //Convert the score in integer to char grade
        public static char ConvertScoreToGrade(int score)
        {
            char grade = '\0';
            if (score >= 70 && score <= 100)
            {
                grade = 'A';
                //return grade;
            }

            else if (score >= 60)
            {
                grade = 'B';
                //return grade;
            }

            else if (score >= 50)
            {
                grade = 'C';
                //return grade ;
            }

            else if (score >= 45)
            {
                grade = 'D';
                //return grade ;
            }

            else if (score >= 40)
            {
                grade = 'E';
                //return grade;
            }

            if (score >= 0 && score < 40)
            {
                grade = 'F';
                //return grade;
            }
            return grade;
        }

        public static int ConvertGradeToPoint(string grade)
        {
            int point = 0;
            switch (grade)
            {
                case "A":
                    point = 5;
                    break;
                case "B":
                    point = 4;
                    break;
                case "C":
                    point = 3;
                    break;
                case "D":
                    point = 2;
                    break;
                case "E":
                    point = 1;
                    break;
                case "F":
                    point = 0;
                    break;
            }
            return point;
        }
    }
}