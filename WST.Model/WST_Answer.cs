using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.Model
{
    public class WST_Answer
    {
        public int Aid { get; set; }         //--PK
        public int Qid { get; set; }         //--问卷主键
        public int Eid { get; set; }         //--题目主键
        public string Tid { get; set; }         //--选项主键
    }
}
