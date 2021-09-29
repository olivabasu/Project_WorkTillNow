using RefillAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefillAPI.Repository
{
    public interface IRefillRepository
    {
        public dynamic viewRefillStatus(int Subscription_ID);

        public dynamic PendingRefill(int Sub_id, DateTime date);
        public dynamic requestAdhocRefill(RefillOrderLine order);

    }
}