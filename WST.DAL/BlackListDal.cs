using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//引用
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WST.Model;

namespace WST.DAL
{
    public class BlackListDal
    {
        //连接数据库
        private string strConn = "Data Source=.;Initial Catalog=WSTDb;Integrated Security=True";
        //显示黑名单
        public List<WST_BlackList> showBlack(int pageIndex, int pageSize, string Bunit, out int totalCount)
        {
            //使用dapper
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                //参数化对象
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@pageIndex", pageIndex, DbType.Int32);
                parameter.Add("@pageSize", pageSize, DbType.Int32);
                parameter.Add("@Bunit", Bunit, DbType.String);
                parameter.Add("@totalCount", direction: ParameterDirection.Output, dbType: DbType.Int32);

                //获取dal层
                List<WST_BlackList> list = conn.Query<WST_BlackList>("p_blfenye", parameter, commandType: CommandType.StoredProcedure).ToList();

                //获取输出参数
                totalCount = parameter.Get<int>("@totalCount");

                return list;
            }

        }

        //添加
        public int addBlack(WST_BlackList b)
        {
            using (IDbConnection conn=new SqlConnection(strConn))
            {
            }
        }

        //删除
        public int delBlack(int Bid)
        {
            using (IDbConnection conn=new SqlConnection(strConn))
            {
            }
        }

        //修改
        public int gaiBlack(WST_BlackList b)
        {
            using (IDbConnection conn=new SqlConnection(strConn))
            {
            }
        }

        //查看
        public WST_BlackList lookBlack(int Bid)
        {
            using (IDbConnection conn=new SqlConnection(strConn))
            {
                string sql = $"select * from WST_BlackList where Bid={Bid}";
                return conn.Query<WST_BlackList>(sql).SingleOrDefault();
            }
        }


        //查询
        public int chaHmd(string Bunit,string Bnumber)
        {
            using (IDbConnection conn=new SqlConnection(strConn))
            {
                string sql = "select count(*) from WST_BlackList where 1=1 ";
                if (Bunit != "")
                {
                    sql += $" and Bunit = @Bunit";
                }
                if (Bnumber != "")
                {
                    sql += $" and Bnumber = @Bnumber";
                }
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Bunit",Bunit,DbType.String);
                paras.Add("@Bnumber", Bnumber, DbType.String);

                return conn.ExecuteScalar<int>(sql,paras);
            }
        }
    }
}
