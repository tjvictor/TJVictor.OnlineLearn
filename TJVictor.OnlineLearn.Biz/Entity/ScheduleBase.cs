using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TJVictor.OnlineLearn.Biz.Entity
{
    public class ScheduleBase
    {
        public Guid C_ID { get; set; }
        public Guid T_ID { get; set; }
        public DateTime C_StartTime { get; set; }
        public DateTime C_EndTime { get; set; }
        public DateTime CreateTime { get; set; }
        public Int16 Status { get; set; }
        public string StatusStr
        {
            get
            {
                switch (Status)
                {
                    case 0: return "已删除";
                    case 1: return "可预定";
                    case 2: return "已预定";
                    case 3: return "已取消";
                    case 4: return "已成交";
                    case 5: return "已结束";
                    default: return "未知";
                }
            }
        }
        public Color StatusBackColor
        {
            get
            {
                switch (Status)
                {
                    case 0: return Color.Red;
                    case 1: return Color.Green;
                    case 2: return Color.Orange;
                    case 3: return Color.Black;
                    case 4: return Color.Blue;
                    case 5: return Color.Gray;
                    default: return Color.Yellow;
                }
            }
        }

        public DateTime UpdateTime { get; set; }
        public string TaobaoLink { get; set; }
    }
}
