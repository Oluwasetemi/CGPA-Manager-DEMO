using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CGPAWeb
{
    public class ConnectionString
    {
        public static string ConnString()
        {
            return
                (@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\PHARHEEZ\Documents\Visual Studio 2010\Projects\CGPAWeb\CGPAWeb\App_Data\CGPADatabase.mdf;Integrated Security=True;User Instance=True");
        }
    }
}