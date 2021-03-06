﻿using System;

namespace Septa.PardakhtVaset.Client
{
    public class PaymentLink
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string FollowId { get; set; }

        public string ClusterId { get; set; }

        public string Description { get; set; }

        public int ExpireDays { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastCheckForUpdateDate { get; set; }
        
        public DateTime? ResultDate { get; set; }

        public string BankReferenceId { get; set; }

        public string Url { get; set; }

        public string Token { get; set; }

        public int PaymentStatus { get; set; }
    }
}
