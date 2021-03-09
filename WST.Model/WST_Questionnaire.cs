using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.Model
{
    //问卷管理表
    public class WST_Questionnaire
    {
        public int Qid		 { get; set; }               //--序号
        public string  Qnumber { get; set; }             //--问卷编号
        public string  Qtitle	 { get; set; }           //--问卷标题
        public int Qstate	 { get; set; }               //--问卷状态
        public int Qnum	 { get; set; }                  //--回收数量
        public DateTime Qtime	 { get; set; }           //--创建时间
        public string Qpeople { get; set; }              //--创建人
        public string  Qstratdesc { get; set; }         //开始描述
        public string  Qenddesc { get; set; }          //结尾描述


        public string Etitle { get; set; }      //题目
        public string Titem { get; set; }           //选项名称
        public int Questiontypes { get; set; }         //类型
        public int Tid { get; set; }               //PK
        public int Eid { get; set; }               //PK

    }
}
