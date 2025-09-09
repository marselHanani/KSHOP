using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Models
{
    public enum OrderStatusEnum
    {
        Pending=1,
        Approved=2,
        Shipped=3,
        Delivered=4,
        Cancelled=5,
    }
    public enum  PaymentMethodEnum
    {
        Visa=1,
        Cash=2,
    }
    public class Order
    {
        public int Id { get; set; }
        public OrderStatusEnum Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }

        public decimal TotalAmount { get; set; }

        public PaymentMethodEnum PaymentMethod { get; set; }

        public string? PaymentId { get; set; }
        public string? CarrierName { get; set; }
        public string? TrackingNumber { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
