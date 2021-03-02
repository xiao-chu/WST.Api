using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WST.Model;
using WST.DAL;

namespace WSTWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecruitController : ControllerBase
    {
        //实例化dal层
        RecruitDal dal = new RecruitDal();

        //显示
        [HttpGet]
        public ObjectResult showRecr(int page=1, int limit=10,string Rposition="")
        {
            int totalCount;
            List<WST_Recruit> list = dal.showRecr(page,limit,Rposition,out totalCount);
            return Ok(new { data = list, code = 0 ,count=totalCount});
        }

        //删除
        [HttpDelete]
        public int delRecr(int Rid)
        {
            return dal.delRecr(Rid);
        }

        //修改
        [HttpPut]
        public int gaiRecr(WST_Recruit r)
        {
            return dal.gaiRecr(r);
        }

        //添加
        [HttpPost]
        public int addRecr(WST_Recruit r)
        {
            return dal.addRecr(r);
        }

        //查看
        [HttpGet]
        [Route("getOne")]
        public ObjectResult lookRecr(int Rid=0)
        {
            WST_Recruit data = dal.lookRecr(Rid);
            return Ok(new { data = data });
        }

        

    }
}
