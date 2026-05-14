using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Seller
{
    public class SellerResponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string SellerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Governorate { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string NationalIdPhotoUrl { get; set; } = string.Empty;
        public string? StoreImageUrl { get; set; }

        public RequestStatus Status { get; set; }
        public string? AdminRejectionReason { get; set; }
        public DateTime CreatedOn { get; set; }
    }

}
