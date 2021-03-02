using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.Model
{
    //栏目内容表
    public class WST_Detial_Pro
    {
        public int Did { get; set; }          //  栏目内容表主键
        public string Dtitle { get; set; }    //  栏目内容名称
        public string Dptitle { get; set; }    //  父栏目
        public string UrlKey { get; set; }    //  UrlKey   
        public int Dsort { get; set; }        //  排序
        public int Dstate { get; set; }      //  内容状态   
        public string Dtype { get; set; }     //  内荣类型（）
        public string Daddress { get; set; }  //  地址
        public string Dexplain { get; set; }  //  说明

    }
}
