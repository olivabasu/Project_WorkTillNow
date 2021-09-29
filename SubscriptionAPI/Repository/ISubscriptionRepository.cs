using SubscriptionAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionAPI.Repository
{
    public interface ISubscriptionRepository
    {
        public dynamic ViewDetailsByID(int Sub_id);
        public string AddSubscription(SubscriptionDetails obj);
        public dynamic RemoveSubscription(SubscriptionDetails obj);
    }
}
