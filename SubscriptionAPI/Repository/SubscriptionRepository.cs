using SubscriptionAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;


namespace SubscriptionAPI.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        static string str = Convert.ToString("http://localhost:32595");
        Uri baseAddress = new Uri(str);

        static string str1 = Convert.ToString("http://localhost:12791");
        Uri base_Address = new Uri(str1);

        public static List<SubscriptionDetails> ls = new List<SubscriptionDetails>
        {
             new SubscriptionDetails
            {
                Drug_ID=1,
                Sub_id=7,
                RefillOccurrence="Weekly",
                Member_Location="Pune",
                MemberID=1,
                PrescriptionID=1,


            },
            new SubscriptionDetails
            {
                Drug_ID=2,
                Sub_id=8,
                RefillOccurrence="Monthly",
                Member_Location="Pune"
            }
        };


        public dynamic ViewDetailsByID(int Sub_id)
        {
            var item = ls.Where(x => x.Sub_id == Sub_id).FirstOrDefault();
            if (item == null)
                return null;
            return item;
        }
        public string AddSubscription(SubscriptionDetails obj)
        {
            string data = JsonConvert.SerializeObject(obj);
            string drugname = obj.Drug_name;
            //Uri baseAddress = new Uri("http://20.193.144.232/api");
           
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            //HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Drugs/GetDrugDetailByName/" + drugname).Result;
            HttpResponseMessage response = client.GetAsync("api/Drugs/GetDrugDetailByName/" + drugname).Result;
            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                Drug drug = JsonConvert.DeserializeObject<Drug>(data);
                if (drug.Quantity > 0)
                {
                    ls.Add(obj);

                    return ("Your Subscription Added Successfully!");
                }
                return ("Sorry! Subscription Not Possible Due To Unavailable drug!!");

            }

            return ("Sorry! Subscription Not Possible Due To Unavailable drug.");
        }



        public dynamic RemoveSubscription(SubscriptionDetails obj)
        {
            string data = JsonConvert.SerializeObject(obj);

           // Uri baseAddress = new Uri("https://refillservicemfpe.azurewebsites.net/api");
            HttpClient client = new HttpClient();
            client.BaseAddress = base_Address;
            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            // HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Refill/RefillStatus/" + obj.Sub_id).Result;
            HttpResponseMessage response = client.GetAsync("api/Refill/RefillStatus/" + obj.Sub_id).Result;
            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                Refill refill = JsonConvert.DeserializeObject<Refill>(data);
                if (refill.Status == "clear")
                {
                    ls.Remove(obj);
                    return ("Unsubscription Done. Thank You!");
                }
                return ("Sorry!Clear The Dues Before Unsubscription");
            }
            return null;
        }

        /*public string AddSubscription(SubscriptionDetails obj)
        {
            throw new NotImplementedException();
        }*/
    }
}