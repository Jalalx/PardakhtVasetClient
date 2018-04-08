using PardakhtVasetServices;
using System;

namespace Septa.PardakhtVaset.Client
{
    public delegate void PaymentLinkChangedEventHandler(object sender, PaymentLinkChangedEventArgs e);

    public class PaymentLinkChangedEventArgs : EventArgs
    {
        public PaymentLinkChangedEventArgs()
        {
        }

        public string Message { get; set; }

        public bool Success { get; set; }

        public bool Handled { get; set; }

        public string FollowId { get; set; }

        public string Token { get; set; }

        public DateTime? ResultDate { get; set; }

        public RequestStatus Status { get; set; }
    }
}
