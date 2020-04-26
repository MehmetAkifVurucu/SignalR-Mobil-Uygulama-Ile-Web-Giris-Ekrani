using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalRLogin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SignalRLogin.Hubs
{
    public class LoginOperationHub : Hub
    {

        public void GirisKayit(string kullaniciId)
        {
            LoginRepository loginRepository = new LoginRepository();
            loginRepository.GirisYap(kullaniciId, ConnectionIdGetir());
        }

        [HubMethodName("sayfaDurumDegistir")]
        public static void SayfaDurumDegistir(string kullaniciId)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<LoginOperationHub>();
            List<string> kullaniciIds = new List<string>();
            kullaniciIds.Add(ConnectionIdGetirFromKullaniciId(kullaniciId));
            context.Clients.Clients(kullaniciIds).updateDurum();
        }
        private string ConnectionIdGetir()
        {
            return Context.ConnectionId;
        }

        private static string ConnectionIdGetirFromKullaniciId(string kullaniciId)
        {
            string cnn = ConfigurationManager.ConnectionStrings["loginConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cnn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select ConnectionId from Kullanici where UyeNo=@UyeNo", con))
                {
                    cmd.Parameters.AddWithValue("@UyeNo", kullaniciId);
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }
    }
}