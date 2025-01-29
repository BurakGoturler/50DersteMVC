using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {

        // MvcDbStokEntities bizim modelimizi tutuyor. Modelde tablolar mevcut.
        // "db" bizim MvcDbStokEntities sınıfından türettiğimiz nesnemiz.
        // Bu yüzden tablolara erişmek için db nesnesize ihtiyacımız var.
        MvcDbStokEntities db = new MvcDbStokEntities();

        // GET: Kategori
        public ActionResult Index(int sayfa = 1)
        {
            // Listeleme
            // db nesnesi içerisinde TBL_KATEGORILER tablosu içeisindeki değerleri listele.
            // var degerler = db.TBL_KATEGORILER.ToList();

            // sayfalama işlemii kullanıyoruz.
            var degerler = db.TBL_KATEGORILER.ToList().ToPagedList(sayfa,4);

            // Bana geriye değerleri döndür. Çünkü listelenecek verileri degerler değişkenine aktardık yukarıda.
            return View(degerler);
        }

        // Eğer ki sayfada herhangi bir işlem yapılmazsa, butona tıklanılmazsa sadece sayfayı getir.
        // Bir nevi index sayfası gibi.
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        // HttpPost], sayfa yüklendiğinde, herhangi bir post işlemi veya form kullanımı olduğunda, bir butona basıldığı zaman buradaki işlemi gerçekleştir demeye yarıyor. Sayfa ilk yüklendiğinde ise [HttpGet]
        [HttpPost]
        public ActionResult YeniKategori(TBL_KATEGORILER p1)
        {
            if(!ModelState.IsValid)
            {
                return View("YeniKategori");
            }

            // Göndermemiz gereken tablo değerlerine p1 parametresi ile ulaşıyoruz.

            // p1'den gelecek olan değeri TBL_KATEGORILER tablosuna ekle.
            db.TBL_KATEGORILER.Add(p1);

            // Değişiklikleri kaydet ve sayfayı geri döndür.
            db.SaveChanges();
            return View();
        }

        public ActionResult KategoriSil(int id)
        {
            var kategori = db.TBL_KATEGORILER.Find(id);

            db.TBL_KATEGORILER.Remove(kategori);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var kategori = db.TBL_KATEGORILER.Find(id);

            return View("KategoriGetir", kategori);
        }

        public ActionResult KategoriGuncelle(TBL_KATEGORILER p1)
        {
            var kategori = db.TBL_KATEGORILER.Find(p1.KATEGORIID);

            kategori.KATEGORIAD = p1.KATEGORIAD;

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}