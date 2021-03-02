using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WST.DAL;
using WST.Model;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;

namespace WSTWeb.API.Controllers
{


     
    [Route("api/[controller]")]
    [EnableCors("any")]
    [ApiController]
    public class QusetionnaireController : ControllerBase
    {
        //实例化DAL
        QuestionnaireDal dal = new QuestionnaireDal();
        
        [HttpGet]
        public ObjectResult Get(int page = 1, int limit = 3, string Qnumber = null)
        {
            int totalCount;

            List<WST_Questionnaire> data = dal.showQuest(page, limit, Qnumber, out totalCount);

            return Ok(new { data=data,count=totalCount,code=0});

        }

        //删除
        [HttpDelete]
        public int Delete(int Qid)
        {
            return dal.deleteQuest(Qid);
        }

        //添加
        [HttpPost]
        public int createQuest(WST_Questionnaire quest)
        {
            return dal.createQuest(quest);
        }

        //修改
        [HttpPut]
        public int Edit(WST_Questionnaire quest)
        {
            return dal.updateQuest(quest);
        }

    }
}
