using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();

        // GET: Musteri
        public ActionResult Index(string p)
        {
            //    var degerler = db.TBL_MUSTERILER.ToList();
            //    return View(degerler);

            var degerler = from d in db.TBL_MUSTERILER select d;

            // eğer p değeri boş ya da null değilse
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));
            }

            return View(degerler.ToList());
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TBL_MUSTERILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }

            db.TBL_MUSTERILER.Add(p1);
            db.SaveChanges();
            return View();
        }

        public ActionResult MusteriSil (int id)
        {
            var musteri = db.TBL_MUSTERILER.Find(id);

            db.TBL_MUSTERILER.Remove(musteri);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.TBL_MUSTERILER.Find(id);

            return View("MusteriGetir", musteri);
        }

        public ActionResult MusteriGuncelle(TBL_MUSTERILER p1)
        {
            var musteri = db.TBL_MUSTERILER.Find(p1.MUSTERIID);

            musteri.MUSTERIAD = p1.MUSTERIAD;
            musteri.MUSTERISOYAD = p1.MUSTERISOYAD;

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}