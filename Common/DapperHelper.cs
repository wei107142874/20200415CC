using Common.Attributes;
using Dapper;
using MySql.Data.MySqlClient;
using Nest;
using Serilog.Parsing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common
{
    public class DapperHelper
    {
        /// <summary>
        /// 定义一个私有的静态变量用来存储类的对象
        /// </summary>
        private static DapperHelper _dapperHelper = null;
        /// <summary>
        /// 定义一个私有的只读字符串
        /// </summary>
        private static readonly string _connString = "Database=DataCollect;Data Source=localhost;User Id=root;Password=agilebuilder;CharSet=utf8;port=3306";//"data source=127.0.0.1;port=3306;database=DataCollect; uid=root;pwd=agilebuilder;";


        /// <summary>
        /// 定义公共的连接数据库实例化MySqlConnection对象
        /// </summary>
        /// <returns></returns>
        private static MySqlConnection GetMySqlConnection()
        {
            var conn = new MySqlConnection(_connString);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int Execute(string sql, object param)
        {
            using (MySqlConnection con = GetMySqlConnection())
            {
                return con.Execute(sql, param);
            }
        }

        private static string insertSql = "insert into {0}({1}) values({2})";
        private static string updateSql = "update {0} set {1} where {2}";

        /// <summary>
        /// 改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int Update<TEntity>(TEntity entity)
        {
            using (MySqlConnection con = GetMySqlConnection())
            {
                Type type = entity.GetType();
                PropertyInfo[] props = type.GetProperties();
                IEnumerable<PropertyInfo> propsAble = props.Where(x => x.Name.ToLower() != "id");
                string sql = string.Format(updateSql, type.Name,string.Join(",", propsAble.Select(s => $"{s.Name}=@{s.Name}")),$"id=@Id");
                return con.Execute(sql, entity);
            }
        }

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int Insert<TEntity>(TEntity entity)
        {
            using (MySqlConnection con = GetMySqlConnection())
            {
                Type type = entity.GetType();
                PropertyInfo[] props = type.GetProperties();
                IEnumerable<PropertyInfo> propsAble = props.Where(x => x.Name.ToLower() != "id");

                string sql = string.Format(insertSql, type.Name, string.Join(",", propsAble.Select(s => s.Name)), string.Join(",", propsAble.Select(s => "@" + s.Name)));
                return con.Execute(sql, entity);
            }
        }
    }
}
