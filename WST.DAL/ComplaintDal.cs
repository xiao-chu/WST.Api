using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//引用
using WST.Model;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;

namespace WST.DAL
{
    public class ComplaintDal
    {
        //连接字符串
        private const string strConn = "Data Source=GENTLEMAN\\SQLEXPRESS;Initial Catalog=WSTDb;Integrated Security=True";

        //显示
        public List<WST_Complaint> showComp(string Cbyname)
        {
            using (IDbConnection conn=new SqlConnection(strConn))
            {
                string sql = "select * from WST_Complaint ";
                if (!string.IsNullOrEmpty(Cbyname))
                {
                    sql += $" where Cbyname like '%{Cbyname}%'";
                }
                return conn.Query<WST_Complaint>(sql).ToList();
            }
        }

        //删除
        public int delComp(int Cid)
        {
            using (IDbConnection conn=new SqlConnection(strConn))
            {
                string sql = $"delete from WST_Complaint where Cid=@Cid";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Cid",Cid,DbType.Int32);
                return conn.Execute(sql,paras);
            }
        }

        //获取ipv4地址
        public static string GetLocalIP()
        {
            //AddressList是一个IPAddress[] 类型，这里面存放了系统的所有IP地址，有IPv4的，有IPv6的，还有不同网卡的也会在这里面。
            //在Win7下默认启用了IPv6，上面这段代码返回的是IPv6格式的地址，而且根据系统情况不同，IPv4不能确定存放在数组的哪一个下标中，比如Tunnel的IP也会被找到，在我朋友的机器上，他的下标为1，而在我的机器上，下标为7才返回IPv4的IP。
            //下面提供一段代码，用于获取本机的IPv4地址：
            //IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress ipa = ipe.AddressList[0];
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception)
            {
                throw;
            }
        }

        //修改
        public int gaiComp(WST_Complaint c)
        {
            using (IDbConnection conn=new SqlConnection(strConn))
            {
                //生成编号
                string bianhao = DateTime.Now.ToString("yyyyMMdd");
                Random rad = new Random();//实例化随机数产生器rad
                int value = rad.Next(1000, 10000);//用rad生成大于等于1000，小于等于9999的随机数
                var sjshu = value.ToString(); //产生的四位随机数

                string sql = $"update WST_Complaint set Cnumber='{bianhao+sjshu}',Ctype=@Ctype,Cinformation=@Cinformation,Cbyname=@Cbyname,Cphone=@Cphone,Cweixin=@Cweixin,Ceamil=@Ceamil,Cip='{GetLocalIP()}',Cisfujian=1,Cstate=@Cstate,Ctime=getdate(),Cpeople='超级管理员',Cname='德玛',Cfujian='2131' where Cid=@Cid";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Ctype", c.Ctype, DbType.Int32);
                paras.Add("@Cinformation", c.Cinformation, DbType.String);
                paras.Add("@Cbyname", c.Cbyname, DbType.String);
                paras.Add("@Cphone", c.Cphone, DbType.String);
                paras.Add("@Cweixin", c.Cweixin, DbType.String);
                paras.Add("@Ceamil", c.Ceamil, DbType.String);
                paras.Add("@Cstate", c.Cstate, DbType.Int32);
                paras.Add("@Cid", c.Cid, DbType.Int32);
                return conn.Execute(sql,paras);
            }
        }

        //查看
        public WST_Complaint lookComp(int Cid)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"select * from WST_Complaint where Cid=@Cid";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Cid", Cid, DbType.Int32);
                return conn.Query<WST_Complaint>(sql,paras).SingleOrDefault();
            }
        }

        //实名举报添加
        public int addShiComp(WST_Complaint c)
        {
            //生成编号
            string bianhao = DateTime.Now.ToString("yyyyMMdd");
            Random rad = new Random();//实例化随机数产生器rad
            int value = rad.Next(1000, 10000);//用rad生成大于等于1000，小于等于9999的随机数
            var sjshu = value.ToString(); //产生的四位随机数
            if (!string.IsNullOrEmpty(c.Cfujian))
            {
                c.Cisfujian = 1;
            }
            else
            {
                c.Cisfujian = 0;
            }

            using (IDbConnection conn=new SqlConnection(strConn))
            {
                string sql = $"insert into WST_Complaint values('{bianhao+sjshu}',1,@Cinformation,@Cbyname,@Cphone,@Cweixin,@Ceamil,'{GetLocalIP()}',@Cisfujian,0,getdate(),'管理员','德玛西亚',@Cfujian)";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Cinformation", c.Cinformation, DbType.String);
                paras.Add("@Cbyname", c.Cbyname, DbType.String);
                paras.Add("@Cphone", c.Cphone, DbType.String);
                paras.Add("@Cweixin", c.Cweixin, DbType.String);
                paras.Add("@Ceamil", c.Ceamil, DbType.String);
                paras.Add("@Cisfujian", c.Cisfujian, DbType.Int32);
                paras.Add("@Cfujian", c.Cfujian, DbType.String);
                return conn.Execute(sql,paras);
            }
        }

        //匿名举报添加
        public int addNiComp(WST_Complaint c)
        {
            //生成编号
            string bianhao = DateTime.Now.ToString("yyyyMMdd");
            Random rad = new Random();//实例化随机数产生器rad
            int value = rad.Next(1000, 10000);//用rad生成大于等于1000，小于等于9999的随机数
            var sjshu = value.ToString(); //产生的四位随机数
            if (!string.IsNullOrEmpty(c.Cfujian))
            {
                c.Cisfujian = 1;
            }
            else
            {
                c.Cisfujian = 0;
            }

            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"insert into WST_Complaint values('{bianhao + sjshu}',0,@Cinformation,@Cbyname,@Cphone,@Cweixin,@Ceamil,'{GetLocalIP()}',@Cisfujian,0,getdate(),'管理员','德玛西亚',@Cfujian)";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Cinformation", c.Cinformation, DbType.String);
                paras.Add("@Cbyname", c.Cbyname, DbType.String);
                paras.Add("@Cphone", c.Cphone, DbType.String);
                paras.Add("@Cweixin", c.Cweixin, DbType.String);
                paras.Add("@Ceamil", c.Ceamil, DbType.String);
                paras.Add("@Cisfujian", c.Cisfujian, DbType.Int32);
                paras.Add("@Cfujian", c.Cfujian, DbType.String);
                return conn.Execute(sql, paras);
            }
        }

    }
}
