﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntellectWorkout
{
    class GlobalVars
    {
        private static string dummyString = "";

        public static string pubDummyString
        {
            get { return dummyString; }
            set { dummyString = value; }
        }

        private static string brshClr = "#000000";

        public static string brushClr
        {
            get { return brshClr; }
            set { brshClr = value; }
        }

        private static int brshConfigOpn = 0;

        public static int brushConfigOpen
        {
            get { return brshConfigOpn; }
            set { brshConfigOpn = value; }
        }

        private static string PthToAppFolder = System.AppDomain.CurrentDomain.BaseDirectory;

        public static string PathToAppFolder
        {
            get { return PthToAppFolder; }
            set { PthToAppFolder = value; }
        }
    }
}
