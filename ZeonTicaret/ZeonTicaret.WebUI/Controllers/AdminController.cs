using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeonTicaret.WebUI.App_Classes;
using ZeonTicaret.WebUI.Models;

namespace ZeonTicaret.WebUI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Urunler()
        {
            return View(Context.Baglanti.Uruns.ToList());
        }
        public ActionResult UrunEkle()
        {
            ViewBag.Kategoriler = Context.Baglanti.Kategoris.ToList();
            ViewBag.Markalar = Context.Baglanti.Markas.ToList();
            return View();
        }
        public ActionResult Markalar()
        {
            return View(Context.Baglanti.Markas.ToList());
        }
        public ActionResult MarkaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MarkaEkle(Marka mrk, HttpPostedFileBase fileUpload)
        {
            int resimId = -1;
            if (fileUpload!=null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                int width = Convert.ToInt32( ConfigurationManager.AppSettings["MarkaWidth"].ToString());
                int height = Convert.ToInt32(ConfigurationManager.AppSettings["MarkaHeight"].ToString());

                string name = "/Content/MarkaResim/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName); 
                Bitmap bm = new Bitmap(img, width, height);
                bm.Save(Server.MapPath(name));
                Resim rsm = new Resim();
                rsm.OrtaYol = name;
                Context.Baglanti.Resims.Add(rsm);
                Context.Baglanti.SaveChanges();
                if (rsm.Id != null)
                    resimId = rsm.Id;
            }
            if(resimId!= -1)
            mrk.ResimID = resimId;
            Context.Baglanti.Markas.Add(mrk);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Markalar");
        }
    }
}