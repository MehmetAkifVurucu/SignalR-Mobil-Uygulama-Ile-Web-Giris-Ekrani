using SignalRLogin.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc.Ajax;

namespace SignalRLogin.Models
{
    public class LoginRepository
    {
        readonly string cnn = ConfigurationManager.ConnectionStrings["loginConnection"].ConnectionString;

        string genelKullaniciId;
        public int GirisDurum(string kullaniciId)
        {
            genelKullaniciId = kullaniciId;
            using (SqlConnection con = new SqlConnection(cnn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT [Durum] FROM [dbo].[Kullanici] where UyeNo=@UyeNo", con))
                {
                    cmd.Parameters.AddWithValue("@UyeNo", kullaniciId);
                    cmd.Notification = null;
                    SqlDependency dependency = new SqlDependency(cmd);
                    dependency.OnChange += new OnChangeEventHandler(DependencyOnChange);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private void DependencyOnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                LoginOperationHub.SayfaDurumDegistir(genelKullaniciId);
            }
        }

        public bool GirisYap(string kullaniciId, string connectionId)
        {
            Random r = new Random();
            using (SqlConnection con = new SqlConnection(cnn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("insert into Kullanici(UyeNo,ConnectionId,Kod,Durum) values(@UyeNo,@ConnectionId,@Kod,@Durum)", con))
                {
                    cmd.Parameters.AddWithValue("@UyeNo", kullaniciId);
                    cmd.Parameters.AddWithValue("@ConnectionId", connectionId);
                    cmd.Parameters.AddWithValue("@Kod", r.Next(1000, 10000).ToString());
                    cmd.Parameters.AddWithValue("@Durum", Kullanici.KullaniciDurum.Dogrulanmadi);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }


    }
}