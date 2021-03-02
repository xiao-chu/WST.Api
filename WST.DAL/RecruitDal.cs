using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//引用
using WST.Model;
using WST.DAL;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace WST.DAL
{
    public class RecruitDal
    {
        //连接字符串
        private const string strConn = "Data Source=GENTLEMAN\\SQLEXPRESS;Initial Catalog=WSTDb;Integrated Security=True";

        //显示黑名单
        public List<WST_Recruit> showRecr(int pageIndex, int pageSize, string Rposition, out int totalCount)
        {
            //使用dapper
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                //参数化对象
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@pageIndex", pageIndex, DbType.Int32);
                parameter.Add("@pageSize", pageSize, DbType.Int32);
                parameter.Add("@Rposition", Rposition, DbType.String);
                parameter.Add("@totalCount", direction: ParameterDirection.Output, dbType: DbType.Int32);

                //获取dal层
                List<WST_Recruit> list = conn.Query<WST_Recruit>("p_refenye", parameter, commandType: CommandType.StoredProcedure).ToList();

                //获取输出参数
                totalCount = parameter.Get<int>("@totalCount");

                return list;
            }
        }

        //添加
        public int addRecr(WST_Recruit r)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"insert into WST_Recruit values('{r.Rkind}','{r.Rposition}','{r.Rdescribe}','{r.Raddress}','{r.Rcompany}',{r.Rstate},getdate(),'超级管理员')";
                return conn.Execute(sql);
            }
        }

        //删除
        public int delRecr(int Rid)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"delete from WST_Recruit where Rid={Rid}";
                return conn.Execute(sql);
            }
        }

        //修改
        public int gaiRecr(WST_Recruit r)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"update WST_Recruit set Rkind='{r.Rkind}',Rposition='{r.Rposition}',Rdescribe='{r.Rdescribe}',Raddress='{r.Raddress}',Rcompany='{r.Rcompany}',Rstate={r.Rstate},Rtime=getdate(),Rpeople='超级管理员' where Rid={r.Rid}";
                return conn.Execute(sql);
            }
        }

        //查看
        public WST_Recruit lookRecr(int Bid)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"select * from WST_Recruit where Rid={Bid}";
                return conn.Query<WST_Recruit>(sql).SingleOrDefault();
            }
        }
    }
}
