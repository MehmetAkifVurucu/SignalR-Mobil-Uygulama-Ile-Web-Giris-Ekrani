using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRLogin.Models
{
    public class Kullanici
    {
        public string UyeNo { get; set; }
        public string ConnectionId { get; set; }
        public string Kod { get; set; }
        public bool Durum { get; set; }

        public enum KullaniciDurum
        {
            Dogrulanmadi = 1,
            Dogrulandi = 2,
            HataliKod = 3
        }
    }
}