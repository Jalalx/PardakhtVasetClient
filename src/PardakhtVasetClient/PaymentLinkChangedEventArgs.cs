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

        public string FollowId { get; set; }

        public string Token { get; set; }

        public RequestStatus Status { get; set; }
    }
}
