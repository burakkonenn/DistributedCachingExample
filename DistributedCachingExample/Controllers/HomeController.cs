using DistributedCachingExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;

namespace DistributedCachingExample.Controllers
{
    public class HomeController : Controller
    {

        IDistributedCache _distributedCache;

        public HomeController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }


        public IActionResult SetStrings()
        {
            //Setstring, key-value tarzında veri depolamasını sağlayan metottur.

            _distributedCache.SetString("date", DateTime.Now.ToString());

            return View();
        }


        public IActionResult GetStrings()
        {

            //GetString, key değerine göre depolanan valueyu döndüren metottur.
            var cache = _distributedCache.GetString("date");

            return View();
        }


        public IActionResult CacheFiles()
        {

            //Set metodu ile dosya cacheleme
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resim.jpg");
            byte[] fileBytes =  System.IO.File.ReadAllBytes(path);

            _distributedCache.Set("files", fileBytes);
            return View();
        }


        public IActionResult Get()
        {

            //Get metodu ile cachelenmiş dosyayı okuma
            byte[] fileByte = _distributedCache.Get("file");

            return File(fileByte, "image/jpg");
        }
      
    }
}