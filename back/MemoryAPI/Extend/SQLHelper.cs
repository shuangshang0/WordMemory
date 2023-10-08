using System.Data;
using System.Data.SqlClient;

namespace Extend
{
    /// <summary>
    /// SQL助手
    /// 封装增删改查
    /// </summary>
    public class SQLHelper
    {
        //连接的数据库
        //Pooling=False; 不使用连接池    Max Pool Size =255;  最大连接池数量
        //private static readonly string connStr = ConfigurationManager.ConnectionStrings["myword"].ConnectionString; 
        private static readonly string connStr =
            "Data Source=LAPTOP-LNE28PAC;Initial Catalog=WordMemory;Persist Security Info=True;User ID=sa;Password=123456;Pooling=False";
        #region 格式化SQL语句

        #region 增、删、改
        /// <summary>
        /// 增、删、改 (insert/delete/update) 
        /// </summary>
        /// <param name="sql"></param>传入sql语句
        /// <returns></returns>
        public static int GetSingleResult(string sql)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                //检测
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                //异常处理，显示异常
                Console.WriteLine("执行GetSingLeResult异常" + ex.Message);
                throw new Exception("执行GetSingLeResult异常" + ex.Message);
            }
            finally
            {
                //需要处理的问题
                conn.Close();
            }
        }

        #endregion

        #region 读取单一结果  
        /// <summary>
        ///  读取单一结果   查
        /// </summary>
        /// <param name="sql"></param>传入sql语句
        /// <returns></returns>
        public static object GetSingleObjiect(string sql)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                //返回第一行第一列
                object obj = cmd.ExecuteScalar();
                return obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine("执行GetSingleObjiect异常" + ex.Message);
                throw new Exception("执行GetSingleObjiect异常" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region 读取多个对象
        /// <summary>
        /// 读取多个对象 查
        /// </summary>
        /// <param name="sql"></param>传入sql语句
        /// <returns></returns>
        public static SqlDataReader GetDataReader(string sql)
        {
            //链接ADO.NET做数据链接
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                //检测并自动关闭conn数据库连接
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine("执行GetDataReader异常" + ex.Message);
                throw new Exception("执行GetDataReader异常" + ex.Message);
            }
        }
        #endregion

        #region 事务
        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="sqlList"></param>sql语句List
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int UpdateByTransaction(List<string> sqlList)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.Transaction = conn.BeginTransaction();
                int result = 0;
                foreach (string sql in sqlList)//循环提交SQL语句
                {
                    cmd.CommandText = sql;
                    result += cmd.ExecuteNonQuery();
                }
                cmd.Transaction.Commit();//提交数据库事务
                return result;
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();//回滚事务
                }

                throw new Exception("执行UpdateByTransaction异常" + ex.Message);
            }
            finally
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction = null;//清除事务
                }
                conn.Close();
            }
        }

        #endregion

        #endregion

        #region 带参数的SQL语句

        #region 增、删、改
        /// <summary>
        /// 增、删、改 (insert/delete/update) 
        /// </summary>
        /// <param name="sql"></param>传入sql语句
        /// <returns></returns>
        public static int GetSingleResult(string sql, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                //检测
                conn.Open();
                cmd.Parameters.AddRange(param);//AddRange 添加数组 Add添加一个
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                //异常处理，显示异常
                Console.WriteLine("执行GetSingLeResult异常" + ex.Message);
                throw new Exception("执行GetSingLeResult异常" + ex.Message);
            }
            finally
            {
                //需要处理的问题
                conn.Close();
            }
        }

        #endregion

        #region 读取单一结果  
        /// <summary>
        ///  读取单一结果   查
        /// </summary>
        /// <param name="sql"></param>传入sql语句
        /// <returns></returns>
        public static object GetSingleObjiect(string sql, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                //返回第一行第一列
                cmd.Parameters.AddRange(param);
                object obj = cmd.ExecuteScalar();
                return obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine("执行GetSingleObjiect异常" + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region 读取多个对象
        /// <summary>
        /// 读取多个对象 查
        /// </summary>
        /// <param name="sql">传入sql语句</param>
        /// <returns></returns>
        public static SqlDataReader GetDataReader(string sql, SqlParameter[] param)
        {
            //链接ADO.NET做数据链接
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddRange(param);
                //检测并自动关闭conn数据库连接
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine("执行GetDataReader异常" + ex.Message);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #endregion

        #region 无参的存储过程调用

        #region 存储过程增删改
        /// <summary>
        /// 存储过程增删改
        /// </summary>
        /// <param name="SPName">存储过程名</param>
        /// <returns></returns>
        public static int SPInsertDeleteUpdata(string SPName)
        {
            // 执行存储过程语句
            //创建连接 connStr内是连接的数据库
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                conn.Open();//打开数据库连接
                SqlCommand cmd = new SqlCommand(); //执行sql语句实例
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure; // 数据库执行存储过程
                cmd.CommandText = SPName;  // 存储过程名
                cmd.CommandTimeout = 1800;
                int result = cmd.ExecuteNonQuery();//返回修改成功的行数的值
                cmd.Parameters.Clear();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }
        #endregion

        #region 存储过程单个对象查询
        /// <summary>
        /// 存储过程单个对象查询
        /// </summary>
        /// <param name="SPName">存储过程名</param>
        /// <returns></returns>
        public static object SPQueryOnly1(string SPName)
        {
            // 执行存储过程语句
            //创建连接 connStr内是连接的数据库
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                conn.Open();//打开数据库连接
                SqlCommand cmd = new SqlCommand(); //执行sql语句实例
                cmd.Connection = conn;
                cmd.CommandTimeout = 1800;
                cmd.CommandType = CommandType.StoredProcedure; // 数据库执行存储过程
                cmd.CommandText = SPName;  // 存储过程名
                object obj = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }
        #endregion

        #region 存储过程多个对象查询
        /// <summary>
        /// 存储过程多个对象查询
        /// </summary>
        /// <param name="SPName">存储过程名</param>
        /// <returns></returns>
        public static SqlDataReader SPQueryMultiple(string SPName)
        {
            // 执行存储过程语句
            //创建连接 connStr内是连接的数据库
            SqlConnection conn = new SqlConnection(connStr);
         
                try
                {
                    conn.Open();//打开数据库连接
                    SqlCommand cmd = new SqlCommand(); //执行sql语句实例
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 1800;
                    cmd.CommandType = CommandType.StoredProcedure; // 数据库执行存储过程
                    cmd.CommandText = SPName;  // 存储过程名
                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();
                return reader;
                }
                catch (Exception ex)
                {
                conn.Close();
                throw new Exception(ex.Message);
                }
            
        }
        #endregion


        #endregion

        #region 存在输入参数的存储过程调用

        #region 存储过程增删改
        /// <summary>
        /// 存储过程增删改
        /// </summary>
        /// <param name="SPName">存储过程名</param>
        /// <param name="param">参数</param>
        /// <returns>返回修改成功的行数的值</returns>
        public static int SPInsertDeleteUpdata(string SPName, SqlParameter[] param)
        {
            // 执行存储过程语句
            //创建连接 connStr内是连接的数据库
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                conn.Open();//打开数据库连接
                SqlCommand cmd = new SqlCommand(); //执行sql语句实例
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure; // 数据库执行存储过程
                cmd.CommandText = SPName;  // 存储过程名
                cmd.CommandTimeout = 1800;
                cmd.Parameters.AddRange(param);//将输入参数加载给cmd
                int result = cmd.ExecuteNonQuery();//返回修改成功的行数的值
                cmd.Parameters.Clear();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }
        #endregion

        #region 存储过程单个对象查询
        /// <summary>
        /// 存储过程单个对象查询
        /// </summary>
        /// <param name="SPName">存储过程名</param>
        /// <param name="param">参数</param>
        /// <returns>返回查询结果的第一行第一列</returns>
        public static object SPQueryOnly1(string SPName, SqlParameter[] param)
        {
            // 执行存储过程语句
            //创建连接 connStr内是连接的数据库
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                conn.Open();//打开数据库连接
                SqlCommand cmd = new SqlCommand(); //执行sql语句实例
                cmd.Connection = conn;
                cmd.CommandTimeout = 1800;
                cmd.CommandType = CommandType.StoredProcedure; // 数据库执行存储过程
                cmd.CommandText = SPName;  // 存储过程名
                cmd.Parameters.AddRange(param);//将输入参数加载给cmd
                object obj = cmd.ExecuteScalar();//返回查询结果的第一行第一列
                cmd.Parameters.Clear();
                return obj;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }
        #endregion

        #region 存储过程多个对象查询
        /// <summary>
        /// 存储过程多个对象查询
        /// </summary>
        /// <param name="SPName">存储过程名</param>
        /// <param name="param">参数</param>
        /// <returns>返回查询的表值，通过实体类创建List来存放</returns>
        public static SqlDataReader SPQueryMultiple(string SPName, SqlParameter[] param)
        {
            // 执行存储过程语句
            //创建连接 connStr内是连接的数据库
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                conn.Open();//打开数据库连接
                SqlCommand cmd = new SqlCommand(); //执行sql语句实例
                cmd.Connection = conn;
                cmd.CommandTimeout = 1800;
                cmd.CommandType = CommandType.StoredProcedure; // 数据库执行存储过程
                cmd.CommandText = SPName;  // 存储过程名
                cmd.Parameters.AddRange(param);//将输入参数加载给cmd
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return reader;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #endregion
    }
}