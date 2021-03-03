using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//引用
using WST.Model;
using WST.DAL;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;
namespace WSTWeb.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("any")]
    [ApiController]
    public class BlackListController : ControllerBase
    {
        public readonly BlackListDal dal;
        public BlackListController(BlackListDal dal)
        {

            this.dal = dal;
        }

        ////实例化dal层
        //BlackListDal dal = new BlackListDal();

        //显示
        [HttpGet]
        public ObjectResult showBlack(int page = 1, int limit = 10, string Bunit = "")
        {
            int totalCount;
            List<WST_BlackList> data = dal.showBlack(page, limit, Bunit, out totalCount);

            return Ok(new { data=data,code=0,count=totalCount});
        }

        //添加
        [HttpPost]
        public int addBlack(WST_BlackList b)
        {
            return dal.addBlack(b);
        }
        //删除
        [HttpDelete]
        public int delBlack([FromBody] int Bid)
        {
            return dal.delBlack(Bid);
        }
        //修改
        [HttpPut]
        public int gaiBlack(WST_BlackList b)
        {
            return dal.gaiBlack(b);
        }

        //查看
        [HttpGet]
        [Route("getOne")]
        public ObjectResult lookBlack(int Bid)
        {
            WST_BlackList data = dal.lookBlack(Bid);
            return Ok(new { data=data,code=0});
        }
    }
}
