using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//引用
using WST.Model;
using WST.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace WSTWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        //实例化dal层
        ComplaintDal dal = new ComplaintDal();

        //显示
        [HttpGet]
        public ObjectResult showComp(string Cbyname)
        {
            List<WST_Complaint> list = dal.showComp(Cbyname);
            return Ok(new { data=list,code=0});
        }

        //删除
        [HttpDelete]
        public int delComp(int Cid)
        {
            return dal.delComp(Cid);
        }

        //修改
        [HttpPut]
        public int gaiComp(WST_Complaint c)
        {
            return dal.gaiComp(c);
        }

        //查看
        [HttpGet]
        [Route("getOne")]
        public ObjectResult lookComp(int Cid)
        {
            WST_Complaint data = dal.lookComp(Cid);
            return Ok(new { data=data,code=0});
        }
    }
}
