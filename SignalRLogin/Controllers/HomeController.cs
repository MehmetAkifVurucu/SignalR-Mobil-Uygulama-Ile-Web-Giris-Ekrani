using SignalRLogin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SignalRLogin.Controllers
{
    public class HomeController : Controller
    {

        string cnn = ConfigurationManager.ConnectionStrings["loginConnection"].ConnectionString;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public void DurumGuncelle(string kullaniciId, string kod)
        {
            if (KodKontrol(kullaniciId, kod))
            {
                using (SqlConnection con = new SqlConnection(cnn))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("update Kullanici set Durum=2 where UyeNo=@UyeNo", con))
                    {
                        cmd.Parameters.AddWithValue("@UyeNo", kullaniciId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                using (SqlConnection con = new SqlConnection(cnn))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("update Kullanici set Durum=3 where UyeNo=@UyeNo", con))
                    {
                        cmd.Parameters.AddWithValue("@UyeNo", kullaniciId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

        }


        public bool KodKontrol(string kullaniciId, string kod)
        {
            int durum = 0;
            using (SqlConnection con = new SqlConnection(cnn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("if(exists(select * from Kullanici where UyeNo=@UyeNo and Kod=@Kod)) begin select 1 end else begin select 0 end", con))
                {
                    cmd.Parameters.AddWithValue("@UyeNo", kullaniciId);
                    cmd.Parameters.AddWithValue("@Kod", kod);
                    durum = Convert.ToInt32(cmd.ExecuteScalar());
                    if (durum == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        [HttpPost]
        public int KullaniciDurum(string kullaniciId)
        {
            LoginRepository loginRepository = new LoginRepository();
            return loginRepository.GirisDurum(kullaniciId);
        }
    }
}