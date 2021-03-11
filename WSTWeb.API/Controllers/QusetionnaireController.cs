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

        //提目表的添加
        [Route("api/[controller]/[action]")]
        [HttpPost]
        public int Addthetitle(WST_Exam e)
        {
            return dal.Addthetitle(e);
        }

        //题目选项表增加
        [Route("[action]")]
        [HttpPost]
        public int Addaoption(WST_Exam_Tel tel)
        {
            return dal.Addaoption(tel);
        }

        //根据问卷主键查询题目
        [Route("[action]")]
        [HttpGet]
        public ObjectResult GetWST_Exams(int qid)
        {
             var data=dal.GetWST_Exams(qid);
            return Ok(new {data=data,code=0,count=data.Count() });
        }

        //邮箱推送
        [Route("[action]")]
        [HttpGet]
        public bool SendEmail(int qid)
        {
            return dal.SendEmail(qid);
        }

        //题目查询
        [HttpGet]
        [Route("[action]")]
        public List<WST_Exam> Get_Exams()
        {
            return dal.Get_Exams();
        }
        //问卷显示
        [HttpGet]
        [Route("[action]")]
        public List<WST_Questionnaire> GetQuestionnaires()
        {
            return dal.GetQuestionnaires();
        }

        //添加问题选项统计
        [HttpPost]
        [Route("[action]")]
        public int Addanswer(WST_Answer a)
        {
            return dal.Addanswer(a);
        }

        //修改问卷表的回收数量
        [HttpPut]
        [Route("[action]")]
        public int UpdateNum(WST_Questionnaire q)
        {
            return dal.UpdateNum(q);
        }
    }
}
