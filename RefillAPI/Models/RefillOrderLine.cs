﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefillAPI.Models
{
    public class RefillOrderLine
    {
        public int Policy_ID { get; set; }
        public int Member_ID { get; set; }
        public int Subscription_ID { get; set; }
        public string Location { get; set; }
    }
}