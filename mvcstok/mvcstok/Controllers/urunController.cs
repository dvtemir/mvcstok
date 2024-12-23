using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcstok.Models.Entity;

namespace mvcstok.Controllers
{
    public class urunController : Controller
    {
        mvcstokEntities2 ra = new mvcstokEntities2();

        public ActionResult Index()
        {
            var degerler = ra.urunler.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(urunler s1)
        {
            // Entity validation hatalarını kontrol et
            var validationErrors = ra.GetValidationErrors();
            foreach (var validationError in validationErrors)
            {
                foreach (var error in validationError.ValidationErrors)
                {
                    // Hata mesajlarını logla
                    Console.WriteLine($"Property: {error.PropertyName}, Error: {error.ErrorMessage}");
                }
            }

            // Model doğrulama başarılıysa veritabanına kaydet
            if (ModelState.IsValid)
            {
                ra.urunler.Add(s1);
                ra.SaveChanges();
                return RedirectToAction("Index");
            }

            // Hata varsa, aynı sayfayı tekrar göster
            return View(s1);
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
        public ActionResult SİL(int id)
        {
            var urun = ra.urunler.Find(id);
            ra.urunler.Remove(urun);
            ra.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = ra.urunler.Find(id);
            return View("UrunGetir", urun);
        } 
         public ActionResult Güncelle(urunler p1)
        {
            var urun = ra.urunler.Find(p1.URUNID);
            urun.URUNID = p1.URUNID;
            urun.URUNAD = p1.URUNAD;
            urun.URUNKATEGORİ = p1.URUNKATEGORİ;
            urun.FİYAT = p1.FİYAT;
            urun.STOK = p1.STOK;
            return RedirectToAction("Index");



        }

    }
}
