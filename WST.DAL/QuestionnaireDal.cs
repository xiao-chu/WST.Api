using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using WST.Model;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Web;
using Newtonsoft.Json;


namespace WST.DAL
{
    public class QuestionnaireDal
    {
        //链接配置
        private string strConn = "Data Source=.;Initial Catalog = WSTDb;Integrated Security = True";
        //问卷表存储过程分页显示条件查询
        public List<WST_Questionnaire> showQuest(int pageIndex,int pageSize,string Qnumber,out int totalCount)
        {
            //使用dapper
            using(IDbConnection conn = new SqlConnection(strConn))
            {
                //参数化对象
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pageIndex",pageIndex,DbType.Int32);
                parameters.Add("@pageSize",pageSize,DbType.Int32);
                parameters.Add("@Qnumber",Qnumber,DbType.String);
                parameters.Add("@totalCount",direction: ParameterDirection.Output,dbType:DbType.Int32);

                List<WST_Questionnaire> list = conn.Query<WST_Questionnaire>("p_Questionnaire", parameters, commandType: CommandType.StoredProcedure).ToList();

                totalCount = parameters.Get<int>("@totalCount");

                return list;

            }
        }
        //问卷表添加
        public int createQuest(WST_Questionnaire quest)
        {
            var qnumber = DateTime.Now.ToString("yyyyMMddHHmmss");
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"insert into WST_Questionnaire values(@Qnumber,@Qtitle,@Qstate,@Qnum,getdate(),'大鹏',@Qstratdesc,@Qenddesc)";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Qtitle", quest.Qtitle, DbType.String);
                paras.Add("@Qnumber", qnumber, DbType.String);
                paras.Add("@Qstate", quest.Qstate, DbType.Int32);
                paras.Add("@Qnum", quest.Qnum, DbType.Int32);
                paras.Add("@Qstratdesc", quest.Qstratdesc, DbType.String);
                paras.Add("@Qenddesc", quest.Qenddesc, DbType.String);
                return conn.Execute(sql,paras);
            }
        }
        //问卷删除
        public int deleteQuest(int Qid)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"delete * from WST_Questionnaire where Qid=@Qid";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Qid", Qid, DbType.Int16);
                return conn.Execute(sql);
            }
        }
        //问卷数据修改
        public int updateQuest(WST_Questionnaire quest)
        {
            using(IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"update WST_Questionnaire set Qtitle=@Qtitle,Qstate=@Qstate,Qstratdesc=@Qstratdesc,Qenddesc=@Qenddesc where Qid=@Qid";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Qtitle", quest.Qtitle, DbType.String);
                paras.Add("@Qstate", quest.Qstate, DbType.Int32);
                paras.Add("@Qstratdesc", quest.Qstratdesc, DbType.String);
                paras.Add("@Qenddesc", quest.Qenddesc, DbType.String);
                paras.Add("@Qid", quest.Qid, DbType.Int32);
                return conn.Execute(sql,paras);
            }
        }

        //提目表的添加
        public int Addthetitle(WST_Exam e)
        {
           using(IDbConnection conn = new SqlConnection(strConn))
           {              
                string sql = $"insert into WST_Exam values(@Qid,@Questiontypes,@Etitle,@Ememo,'{DateTime.Now}')";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Qid", e.Qid, DbType.Int32);
                paras.Add("@Questiontypes", e.Questiontypes, DbType.Int32);
                paras.Add("@Etitle", e.Etitle, DbType.String);
                paras.Add("@Ememo", e.Ememo, DbType.String);             
                return conn.Execute(sql,paras);
           }
        }

        //题目选项表增加
        public int Addaoption(WST_Exam_Tel tel)
        {
            using(IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"insert into WST_Exam_Tel values(@Eid,@Titem,'{DateTime.Now}')";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Eid", tel.Eid, DbType.Int32);
                paras.Add("@Titem", tel.Titem, DbType.String);              
                return conn.Execute(sql,paras);
            } 
        }
        //根据问卷主键查询题目
        public List<WST_Exam> GetWST_Exams(int qid)
        {
            using(IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"select * from WST_Exam where Qid=@qid";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@qid", qid, DbType.Int32);
                List<WST_Exam> list = conn.Query<WST_Exam>(sql,paras).ToList();
                return list;
            }
        }
        //题目查询
        public List<WST_Exam> Get_Exams()
        {
            using(IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"select * from WST_Exam";
                List<WST_Exam> list = conn.Query<WST_Exam>(sql).ToList();
                return list;
            }
        }

        //问卷显示
        public List<WST_Questionnaire> GetQuestionnaires()
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"select * from WST_Questionnaire t1 join WST_Exam t2 on t1.Qid=t2.Qid join WST_Exam_Tel t3 on t2.Eid=t3.Eid where Qstate=1";
                List<WST_Questionnaire> list = conn.Query<WST_Questionnaire>(sql).ToList();
                return list;
            }
        }
        //邮箱推送
        public bool SendEmail(int qid)
        {
            MailMessage msg = new MailMessage();

            msg.To.Add("3169190453@qq.com");//收件人地址 
            msg.CC.Add("3501357909@qq.com");//抄送人地址 

            msg.From = new MailAddress("3169190453@qq.com", "小鬼");//发件人邮箱，名称 

            msg.Subject = "邮箱测试";//邮件标题 
            msg.SubjectEncoding = Encoding.UTF8;//标题格式为UTF8 

            IDbConnection conn = new SqlConnection(strConn);           
            string sql = $"select * from WST_Questionnaire where Qid=@qid";
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@qid", qid, DbType.Int32);
            List<WST_Questionnaire> list = conn.Query<WST_Questionnaire>(sql,paras).ToList();
            //转换格式
            //string data = JsonConvert.SerializeObject(list);

            msg.Body = list.ToString(); //邮件内容 
            msg.BodyEncoding = Encoding.UTF8;//内容格式为UTF8 
          
          
            SmtpClient client = new SmtpClient();

            client.Host = "smtp.qq.com";//SMTP服务器地址 
            client.Port = 587;//SMTP端口，QQ邮箱填写587 

            client.EnableSsl = true;//启用SSL加密 
                                    //发件人邮箱账号，授权码(注意此处，是授权码你需要到qq邮箱里点设置开启Smtp服务，然后会提示你第三方登录时密码处填写授权码)
            client.Credentials = new System.Net.NetworkCredential("3169190453@qq.com", "igcayjhzakhsdfgf");

            try
            {
                client.Send(msg);//发送邮件
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        //添加问题选项统计
        public int Addanswer(WST_Answer a)
        {
            using(IDbConnection conn = new SqlConnection(strConn))
            {              
               string sql = $"insert into WST_Answer values(@Qid,@Eid,@Tid)";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Qid", a.Qid, DbType.Int32);
                paras.Add("@Eid", a.Eid, DbType.Int32);
                paras.Add("@Tid", a.Tid, DbType.String);
                return conn.Execute(sql,paras);
            }
        }

        //修改问卷表的回收数量
        public int UpdateNum(WST_Questionnaire q)
        {
            using(IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"update WST_Questionnaire set Qnum = Qnum+1 where Qid = @Qid";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Qid", q.Qid, DbType.Int32);
                return conn.Execute(sql, paras);
            }
        }

    }
}
