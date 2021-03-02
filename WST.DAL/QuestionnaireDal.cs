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

namespace WST.DAL
{
    public class QuestionnaireDal
    {
        //链接配置
        private string strConn = "Data Source=.;Initial Catalog = WSTDb;Integrated Security = True";
        //存储过程分页显示条件查询
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
        //添加
        public int createQuest(WST_Questionnaire quest)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"insert into WST_Questionnaire values('{quest.Qnumber}','{quest.Qtitle}','{quest.Qstate}','{quest.Qnum}','{DateTime.Now}','大鹏','{quest.Qstratdesc}','{quest.Qenddesc}')";
                return conn.Execute(sql);
            }
        }
        //删除
        public int deleteQuest(int Qid)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"delete * from WST_Questionnaire where Qid='{Qid}'";
                return conn.Execute(sql);
            }
        }
        //数据修改
        public int updateQuest(WST_Questionnaire quest)
        {
            using(IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"update WST_Questionnaire set Qtitle='{quest.Qtitle}',Qstate='{quest.Qstate}',Qstratdesc='{quest.Qstratdesc}',Qenddesc='{quest.Qenddesc}' where Qid='{quest.Qid}'";
                return conn.Execute(sql);
            }
        }

    }
}
