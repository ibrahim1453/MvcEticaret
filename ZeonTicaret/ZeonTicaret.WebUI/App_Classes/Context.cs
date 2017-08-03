using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeonTicaret.WebUI.Models;

namespace ZeonTicaret.WebUI.App_Classes
{
    public class Context
    {
        private static ETicaretContext baglanti;

        public static ETicaretContext Baglanti
        {
            get {
                if (baglanti == null)
                    baglanti = new ETicaretContext();
                return baglanti; }
            set { baglanti = value; }
        }

    }
}