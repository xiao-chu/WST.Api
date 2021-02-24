using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.Model
{
    //友情链接表
    public class WST_LinkList
    {
        public int LId { get; set; }         //序号
        public string LName { get; set; }    //名称
        public string URL { get; set; }      //URL地址
        public string LType { get; set; }    //类型
        public int Lsort { get; set; }       //int排序号
        public bool Show { get; set; }       //是否显示	
        public string Sites { get; set; }    //站点	
        public string Remark { get; set; }   //备注
    }
}
