using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.Model
{
    public class WST_Exam
    {
        public int Eid           { get; set; }          //PK
        public int Qid           { get; set; }          //问卷ID
        public int  Questiontypes { get; set; }         //类型
        public string  Etitle        { get; set; }      //题目
        public string  Ememo         { get; set; }      //备注
        public int Estatus       { get; set; }          //状态
        public DateTime Ecreatetime { get; set; }       //记录时间

    }
}
