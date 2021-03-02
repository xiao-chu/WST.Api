using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.Model
{
    public class WST_Recruit
    {
        public int Rid { get; set; }   //序号

        public string Rkind { get; set; } //种类

        public string Rposition { get; set; } //职位

        public string Rdescribe { get; set; }  //描述

        public string Raddress { get; set; } //详细地址

        public string Rcompany { get; set; } //公司

        public int Rstate { get; set; }    //状态

        public DateTime Rtime { get; set; } //发布时间

        public string Rpeople { get; set; } //发布人
    }
}
