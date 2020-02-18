using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BeaconWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BeaconWeb.Controllers
{
    public class BeaconsController : Controller
    {        
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/api/beacons/");
                
                var response = client.GetAsync("get-all").Result;
                
                
                if (response.IsSuccessStatusCode)
                {
                    var reaconResponse = response.Content.ReadAsStringAsync().Result;
                    
                    var beacons = JsonConvert.DeserializeObject<List<BeaconViewModel>>(reaconResponse);
                }
                else 
                {
                    ModelState.AddModelError(string.Empty, "Server error.");
                }
            }
            return View();
        }
        
        public ActionResult Details(int id)
        {
            return View();
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {                

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {                

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Delete(int id)
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}