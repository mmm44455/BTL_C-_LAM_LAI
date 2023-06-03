using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class Class1
    {
        static string cnStr = "Data Source=duycao;Initial Catalog=thongtincanhan;Integrated Security=True;Encrypt = False";

        public static async Task<string> TimKiemAsync(string q)
        {
            string kq = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cnStr))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandText = "SP_Search_NV";
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.Add("@q", SqlDbType.NVarChar, 50).Value = q;

                        using (SqlDataReader dr = cm.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string hoten = dr["HT"].ToString();
                                string que = dr["QUE"].ToString();
                                kq += $"Họ tên: {hoten}\nQuê quán: {que}\n\n";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                kq += $"Error: {ex.Message}";
            }
            return kq;
        }
        public static async Task<string> xoaAsync(string q)
        {
            string kq = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cnStr))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandText = "delete_id";
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.Add("@q", SqlDbType.NVarChar, 50).Value = "%" + q.Replace(' ', '%') + "%"; ;
                        cm.ExecuteNonQuery();
                    } 
                }
            }
            catch (Exception ex)
            {
                kq += $"Error: {ex.Message}";
            }
            return kq;
        }
        public static async Task<string> them(string ht,string que,string MS)
        {
            string kq = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cnStr))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandText = "INSERT INTO CANHAN (MS,HT, QUE) VALUES (@MS,@HT, @QUE)";
                        cm.Parameters.Add("@HT", SqlDbType.NVarChar, 50).Value = ht;
                        cm.Parameters.Add("@QUE", SqlDbType.NVarChar, 50).Value = que;
                        cm.Parameters.Add("@MS", SqlDbType.NVarChar, 50).Value = MS;
                        cm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                kq += $"Error: {ex.Message}";
            }
            return kq;
        }
    }
    }
