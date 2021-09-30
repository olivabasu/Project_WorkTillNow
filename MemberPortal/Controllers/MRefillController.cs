﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MemberPortal.MailOrderContext;
using MemberPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MemberPortal.Controllers
{
    public class MRefillController : Controller
    {
        //Uri baseAddress = new Uri("https://refillservice.azurewebsites.net/api");    //https://localhost:44322
        static string str = Convert.ToString("http://localhost:12791");
        Uri baseAddress = new Uri(str);
        HttpClient client;
        PharmacyContext context;
        public MRefillController(PharmacyContext _con)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            context = _con;

        }
        /// <summary>
        /// This Method us giving the View For Checking Refill details
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult View_RefillDetails(Refill refill)
        {
            //string Token = HttpContext.Request.Cookies["Token"];
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            HttpResponseMessage response = client.GetAsync("api/Refill/RefillStatus/" + refill.Subscription_ID).Result;
            if (response.IsSuccessStatusCode)
            {
                string scheduleData = response.Content.ReadAsStringAsync().Result;
                Refill r = JsonConvert.DeserializeObject<Refill>(scheduleData);
                //context.RefillDetails.Add(r);
                //context.SaveChanges();


                return View("RefillStatus", r);
            }
            return View();
        }


        [HttpPost]
        public IActionResult RefillStatus(Refill obj)
        {
            return View();
        }
        /// <summary>
        /// This Method us giving the View For Checking Refill Due For a month or Year
        /// </summary>
        /// <returns></returns>
        public IActionResult IndexDue()
        {
            return View();
        }

        public ActionResult View_RefillDues(Refill obj)
        {
            //string Token = HttpContext.Request.Cookies["Token"];
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            int Subid = obj.Subscription_ID;
            string sdate = obj.FromDate.ToString("yyyy-MM-dd");

            HttpResponseMessage response = client.GetAsync("api/Refill/RefillDueAsOfDate/" + Subid + "/" + sdate).Result;
            if (response.IsSuccessStatusCode)
            {
                string scheduleData = response.Content.ReadAsStringAsync().Result;
                IEnumerable<Refill> r = JsonConvert.DeserializeObject<List<Refill>>(scheduleData);


                return View("RefillDues", r);
            }
            return View();


        }
        [HttpPost]
        public IActionResult RefillDues(Drug obj)
        {
            return View();
        }
        /// <summary>
        /// This Method us giving the View For Checking Adhoc Refill details
        /// </summary>
        /// <returns></returns>
        public IActionResult IndexAdhoc()
        {
            return View();
        }
        public ActionResult AdhocDrug_Refill(RefillOrder ad)
        {
            //string Token = HttpContext.Request.Cookies["Token"];
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            string data = JsonConvert.SerializeObject(ad);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("api/Refill/requestAdhocRefill/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string scheduleData = response.Content.ReadAsStringAsync().Result;
                try
                {
                    Refill r = JsonConvert.DeserializeObject<Refill>(scheduleData);
                    return View("AdhocRefill", r);
                }
                catch(Exception e)
                {
                    ViewBag.Message = scheduleData;
                    return View("IndexAdhoc");
                }


                //return View("AdhocRefill", r);

            }
            return View();


        }
        [HttpPost]
        public IActionResult AdhocRefill(Refill obj)
        {
            return View();
        }

    }
}