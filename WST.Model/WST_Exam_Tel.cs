using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.Model
{
    public class WST_Exam_Tel
    {
        public int Tid         { get; set; }               //PK
        public int Eid         { get; set; }               //题目ID
        public string  Titem       { get; set; }           //选项名称
        public DateTime Tcreatetime { get; set; }          //创建时间

    }
}
