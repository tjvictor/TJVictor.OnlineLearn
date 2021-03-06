﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TJVictor.OnlineLearn.Biz.Entity
{
    public class Schedule : ScheduleBase
    {
        public Guid ID { get; set; }

        public string C_Name { get; set; }
        public string C_Des { get; set; }
        public string T_FirstName { get; set; }
        public string T_LastName { get; set; }
        public string T_Name
        {
            get
            {
                return string.Format("{0} {1}", T_LastName, T_FirstName);
            }
        }
    }
}
