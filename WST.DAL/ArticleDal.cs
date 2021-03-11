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
                string strSql = $"insert into WST_Article_management values(@ATId,@Aname,@Ranks,@Astatus,@Tops,@Recommend,@Audit_status,@Release_time,@Promulgator,@Comment,@Begin_time,@End_time,@AType,@Jump_address,@Picture,@Accessory,@Content)";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@ATId", w.ATId, DbType.Int32);
                dynamic.Add("@Aname", w.Aname, DbType.String);
                dynamic.Add("@Ranks", w.Ranks, DbType.Int32);
                dynamic.Add("@Astatus", w.Astatus, DbType.Int32);
                dynamic.Add("@Tops", w.Tops, DbType.String);
                dynamic.Add("@Recommend", w.Recommend, DbType.Int32);
                dynamic.Add("@Audit_status", w.Audit_status, DbType.Int32);
                dynamic.Add("@Release_time", w.Release_time, DbType.DateTime);
                dynamic.Add("@Promulgator", w.Promulgator, DbType.String);
                dynamic.Add("@Comment", w.Comment, DbType.Int32);
                dynamic.Add("@Begin_time", w.Begin_time, DbType.DateTime);
                dynamic.Add("@End_time", w.End_time, DbType.DateTime);
                dynamic.Add("@AType", w.AType, DbType.String);
                dynamic.Add("@Jump_address", w.Jump_address, DbType.String);
                dynamic.Add("@Picture", w.Picture, DbType.String);
                dynamic.Add("@Accessory", w.Accessory, DbType.String);
                dynamic.Add("@Content", w.Content, DbType.String);
                return conn.Execute(strSql,dynamic);
            }
        }

        //删除文章
        public int DeleteArticle(int id)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string strSql = $"delete from WST_Article_management where Aid = @id";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@id",id,DbType.Int32);
                return conn.Execute(strSql,dynamic);
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
                string strSql = $"update WST_Article_management set ATId=@ATId,Aname=@Aname,Ranks=@Ranks,Astatus=@Astatus,Tops=@Tops,Recommend=@Recommend,Audit_status=@Audit_status,Release_time=@Release_time,Promulgator=@Promulgator,Comment=@Comment,Begin_time=@Begin_time,End_time=@End_time,AType=@AType,Jump_address=@Jump_address,Picture=@Picture,Accessory=@Accessory,Content=@Content where Aid=@Aid";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@ATId", w.ATId, DbType.Int32);
                dynamic.Add("@Aname", w.Aname, DbType.String);
                dynamic.Add("@Ranks", w.Ranks, DbType.Int32);
                dynamic.Add("@Astatus", w.Astatus, DbType.Int32);
                dynamic.Add("@Tops", w.Tops, DbType.String);
                dynamic.Add("@Recommend", w.Recommend, DbType.Int32);
                dynamic.Add("@Audit_status", w.Audit_status, DbType.Int32);
                dynamic.Add("@Release_time", w.Release_time, DbType.DateTime);
                dynamic.Add("@Promulgator", w.Promulgator, DbType.String);
                dynamic.Add("@Comment", w.Comment, DbType.Int32);
                dynamic.Add("@Begin_time", w.Begin_time, DbType.DateTime);
                dynamic.Add("@End_time", w.End_time, DbType.DateTime);
                dynamic.Add("@AType", w.AType, DbType.String);
                dynamic.Add("@Jump_address", w.Jump_address, DbType.String);
                dynamic.Add("@Picture", w.Picture, DbType.String);
                dynamic.Add("@Accessory", w.Accessory, DbType.String);
                dynamic.Add("@Content", w.Content, DbType.String);
                dynamic.Add("@Aid", w.Aid, DbType.Int32);
                return conn.Execute(strSql,dynamic);
            }
        }
    }
}
 