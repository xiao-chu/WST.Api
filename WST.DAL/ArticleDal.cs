using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//引入Dapper
using Dapper;
using WST.Model;
using System.Data;
using System.Data.SqlClient;

namespace WST.DAL
{
    //文章CDUS
    public class ArticleDal
    {
        private string strConn = "Data Source=.;Initial Catalog=WSTDb;Integrated Security=True";//ConfigurationManager.ConnectionStrings["Conn"].ToString();
        //文章显示
        public List<WST_Article_management> GetArticle(int pageIndex,int pageSize,out int totalCount,string Dtitle, string Aname,int Audit_status)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@pageIndex", pageIndex, dbType: DbType.Int32);
                dynamic.Add("@pageSize", pageSize, dbType: DbType.Int32);
                dynamic.Add("@totalCount", direction: ParameterDirection.Output, dbType: DbType.Int32);
                dynamic.Add("@Dtitle", Dtitle, dbType: DbType.String);
                dynamic.Add("@Aname", Aname, dbType: DbType.String);
                dynamic.Add("@Audit_status", Audit_status, dbType: DbType.Int32);

                
                //获取结果集
                List<WST_Article_management> result = conn.Query<WST_Article_management>("p_ArtList", dynamic, commandType: CommandType.StoredProcedure).ToList();
                //输出参数
                totalCount = dynamic.Get<int>("@totalCount");
                //返回结果
                return result;
            }
        }

        //绑定下拉框（栏目）
        public List<WST_Detial_Pro> GetDetial()
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string strSql = $"select * from WST_Detial_Pro";
                return conn.Query<WST_Detial_Pro>(strSql).ToList();
            }
        }

        //新增文章
        public int AddArticle(WST_Article_management w)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string strSql = $"insert into WST_Article_management values('{w.ATId}','{w.Aname}',{w.Ranks},{w.Astatus},'{w.Tops}',{w.Recommend},{w.Audit_status},'{w.Release_time}','{w.Promulgator}',{w.Comment},'{w.Begin_time}','{w.End_time}','{w.AType}','{w.Jump_address}','{w.Picture}','{w.Accessory}')";
                return conn.Execute(strSql);
            }
        }

        //删除文章
        public int DeleteArticle(int id)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string strSql = $"delete from WST_Article_management where Aid = {id}";
                return conn.Execute(strSql);
            }
        }

        //反填
        //public WST_Article_management GetArticleById(int id)
        //{
        //    using (IDbConnection conn = new SqlConnection(strConn))
        //    {
        //        string strSql = $"select * from WST_Article_management where Aid={id}";
        //        return conn.Query<WST_Article_management>(strSql).SingleOrDefault();
        //    }
        //}
        //修改
        public int UpdateArticle(WST_Article_management w)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string strSql = $"update WST_Article_management set ATId='{w.ATId}',Aname='{w.Aname}',Ranks='{w.Ranks}',Astatus='{w.Astatus}',Tops='{w.Tops}',Recommend='{w.Recommend}'," +
                    $"Audit_status='{w.Audit_status}',Release_time='{w.Release_time}',Promulgator='{w.Promulgator}',Comment='{w.Comment}'," +
                    $"Begin_time='{w.Begin_time}',End_time='{w.End_time}',AType='{w.AType}',Jump_address='{w.Jump_address}',Picture='{w.Picture}',Accessory='{w.Accessory}' where Aid={w.Aid}";
                return conn.Execute(strSql);
            }
        }
    }
}
 