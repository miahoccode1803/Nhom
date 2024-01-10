
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Dangkitinchi
{
    internal class Database_store
    { 
        private DataTable tb;
        private SqlCommand cmd;
        private string connectionString;
        private SqlConnection con;  // Thêm biến thành viên để giữ kết nối

        public Database_store()
        {
            try
            {
                // Lấy chuỗi kết nối từ app.config
                connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                con = new SqlConnection(connectionString);

                // Kiểm tra nếu kết nối chưa mở trước khi mở
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi trong MessageBox
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Có thể thực hiện các xử lý khác tùy thuộc vào yêu cầu của bạn
            }
        }

        
        public DataTable SelectData(string sql, List<CustomParamater> lstPara)
                {
                     using (SqlConnection con = new SqlConnection(connectionString))
                     {
                            try
                            {
                                con.Open();
                                string sql_n =  sql ;
                                cmd = new SqlCommand(sql_n, con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                foreach (var p in lstPara)
                                {
                                    cmd.Parameters.AddWithValue(p.key, p.value);
                                }
                                tb = new DataTable();
                                tb.Load(cmd.ExecuteReader());
                                return tb;

                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi load data :" + ex.Message);
                                return null;
                            }
                            finally
                            {
                                con.Close();
                            }
                     }
                }
        public DataRow Select(string sql)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    tb = new DataTable();
                    tb.Load(cmd.ExecuteReader());
                    return tb.Rows[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load thông tin:" + ex.Message);
                    return null;
                }

                finally
                {
                    con.Close() ;
                }
            }

      
        }
        public int Execute(string sql,List<CustomParamater> lstPara)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach(var p in lstPara)
                    {
                        cmd.Parameters.AddWithValue(p.key, p.value);
                    }
                    var rs = cmd.ExecuteNonQuery();
                    return (int)rs;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi exec thông tin:" + ex.Message);
                    return -100;
                }

                finally
                {
                    con.Close();
                }
            }
        }
        public int DeleteData(string sql, List<CustomParamater> lstPara)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var p in lstPara)
                    {
                        cmd.Parameters.AddWithValue(p.key, p.value);
                    }
                    var rs = cmd.ExecuteNonQuery();
                    return (int)rs;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa dữ liệu:" + ex.Message);
                    return -100;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public DataTable SelectData_SQL(string sql)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load data :" + ex.Message);
                    return null;
                }
                finally
                {
                    con.Close();
                }
            }
        }



    }
}
