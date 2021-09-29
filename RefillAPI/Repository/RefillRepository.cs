﻿using RefillAPI.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RefillAPI.Repository;

namespace RefillAPI.Repository
{
    public class RefillRepository : IRefillRepository
    {
        public static List<RefillDetails> ls = new List<RefillDetails>
        {
            new RefillDetails
            {
                RefillOrderId=1,
                Subscription_ID = 7,
                DrugID=1,
                RefillDate=new DateTime(2020,09,12),
                FromDate = new DateTime(2020, 05, 15),
                NextRefillDate=new DateTime(2020,10,08),
                Status="pending",
                Policy_ID = 001,
                Member_ID = 1,
                Location = "Delhi"
            },
            new RefillDetails
            {
                RefillOrderId=2,
                Subscription_ID = 8,
                DrugID=1,
                RefillDate=new DateTime(2020,09,12),
                FromDate = new DateTime(2020, 05, 15),
                NextRefillDate=new DateTime(2020,10,08),
                Status="clear",
                Policy_ID = 001,
                Member_ID = 02,
                Location = "Mumbai"
            }
        };
        /// <summary>
        ///  This method is responsible for returing the refill Details searched by Subscription ID
        /// </summary>
        /// <param name="Sub_Id"></param>
        /// <returns></returns>
        public virtual dynamic viewRefillStatus(int Sub_Id)
        {
            var item = ls.Where(x => x.Subscription_ID == Sub_Id).FirstOrDefault();
            if (item == null)
                return null;
            return item;
        }

        /// <summary>
        /// This method returns the next refill dates according to given date and refill Occurance 
        /// </summary>
        /// <param name="subscription_id"></param>
        /// <param name="date"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public virtual dynamic PendingRefill(int subscription_id, DateTime date)
        {
            List<RefillDetails> m = new List<RefillDetails>();

            //sublink--http://localhost:20430/swagger/index.html

            string data = JsonConvert.SerializeObject(subscription_id);

            Uri baseAddress = new Uri("http://localhost:20430/Subscription/ViewDetails_BySubID/" + subscription_id);
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            // HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            HttpResponseMessage response = client.GetAsync("api/Subscription/ViewDetails_BySubID/" + subscription_id).Result;
            string freq = "";
            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;

                Subsription s = JsonConvert.DeserializeObject<Subsription>(data);
                freq = s.RefillOccurrence;

            }
            List<RefillDetails> Pending_dues = new List<RefillDetails>();
            Pending_dues = Calculation_Dues(subscription_id, freq, date);
            return Pending_dues;
        }
        /// <summary>
        /// This method calculates the next refill date
        /// </summary>
        /// <param name="subscription_id"></param>
        /// <param name="freq"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public dynamic Calculation_Dues(int subscription_id, string freq, DateTime date)
        {
            List<RefillDetails> Pending = new List<RefillDetails>();
            if (string.Equals(freq, "Weekly"))
            {
                int month = date.Month;
                int nxtmonth = month + 1;

                while (month != nxtmonth)
                {


                    RefillDetails refill = new RefillDetails();
                    refill.Subscription_ID = subscription_id;

                    date = date.AddDays(7);
                    refill.RefillDate = date;
                    refill.NextRefillDate = date.AddDays(7);
                    Pending.Add(refill);
                    month = date.Month;

                }
            }
            else

            {
                int year = date.Year;
                int nxtyear = year + 1;

                while (year != nxtyear)
                {


                    RefillDetails refill = new RefillDetails();
                    refill.Subscription_ID = subscription_id;

                    date = date.AddMonths(1);
                    refill.RefillDate = date;
                    refill.NextRefillDate = date.AddMonths(1);
                    Pending.Add(refill);
                    year = date.Year;

                }
            }
            return Pending;
        }
        /// <summary>
        /// This method checks the adhoc refill details
        /// </summary>
        /// <param name="order"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public virtual dynamic requestAdhocRefill(RefillOrderLine order)
        {

            RefillDetails detail = ls.Where(x => x.Member_ID == order.Member_ID).FirstOrDefault();
            Uri baseAddress = new Uri("http://localhost:32595/");
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;

            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            // HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            int id = detail.DrugID;
            HttpResponseMessage response = client.GetAsync("api/Drugs/GetDrugDetails/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Drug s = JsonConvert.DeserializeObject<Drug>(data);
                if (s.drugLocation.Location == order.Location)
                {
                    return (detail);
                }
                return ("Unavailable");


            }
            return null;
        }

        /*public dynamic PendingRefill(int Sub_id, DateTime date)
        {
            throw new NotImplementedException();
        }

        public dynamic requestAdhocRefill(RefillOrderLine order)
        {
            throw new NotImplementedException();
        }*/
    }
}