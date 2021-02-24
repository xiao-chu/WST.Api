using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.Model
{
    public class WST_Complaint
    {
        public int Cid { get; set; }                      //序号
        public string Cnumber { get; set; }               //投诉编号
        public bool Ctype { get; set; }                   //投诉类别  1：实名  0：匿名
        public string Cinformation { get; set; }          //身份信息
        public string Cbyname { get; set; }               //被投诉人姓名
        public string Cphone { get; set; }                //电话
        public string Cweixin { get; set; }               //微信
        public string Ceamil { get; set; }                //邮箱
        public string Cip { get; set; }                   //投诉人IP
        public bool Cisfujian { get; set; }               //是否有附件
        public bool Cstate { get; set; }                  //状态
        public DateTime Ctime { get; set; }               //投诉时间
        public string Cpeople { get; set; }               //处理人
        public string Cname { get; set; }                 //投诉人姓名
        public string Cfujian { get; set; }               //附件
        
    }
}
