using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    // ✅ إضافة المحافظة
    public string? Governorate { get; set; }

    public string? ProfilePictureUrl { get; set; }
    public string? Bio { get; set; }

    public bool IsDisabled { get; set; }

    // Navigation Properties
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
    
    // 🎣 الربط مع نظام الصيد (Fishing Platform)
    // ------------------------------------------
    
    // علاقة (1 إلى 1) مع ملف تعريف مالك القارب (إذا قدم طلباً)
    public virtual BoatOwnerProfile? BoatOwnerProfile { get; set; }

    // علاقة (1 إلى كثير) مع الحجوزات التي قام بها المستخدم
    public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();

    // علاقة (1 إلى كثير) مع التقييمات التي كتبها المستخدم
    public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();


    //-----------------------MarketPlace------------------
    public virtual SellerProfile? SellerProfile { get; set; }
    public virtual ICollection<MarketplaceCartItem> MarketplaceCartItems { get; set; } = new HashSet<MarketplaceCartItem>();
    public virtual ICollection<MarketplaceOrder> MarketplaceOrders { get; set; } = new HashSet<MarketplaceOrder>();
    public virtual ICollection<MarketplaceReview> MarketplaceReviews { get; set; } = new HashSet<MarketplaceReview>();
    public virtual ICollection<MarketplaceListingRequest> MarketplaceListingRequests { get; set; } = new HashSet<MarketplaceListingRequest>();

    // 🎣 ------------------Community Module------------------
    public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    public virtual ICollection<PostComment> PostComments { get; set; } = new HashSet<PostComment>();
    public virtual ICollection<PostLike> PostLikes { get; set; } = new HashSet<PostLike>();
    public virtual ICollection<SavedPost> SavedPosts { get; set; } = new HashSet<SavedPost>();
    public virtual ICollection<EventParticipant> EventParticipations { get; set; } = new HashSet<EventParticipant>();
    public virtual ICollection<ComplaintSupport> ComplaintSupports { get; set; } = new HashSet<ComplaintSupport>();
    public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();

    public virtual ICollection<UserFollow> Followers { get; set; } = new HashSet<UserFollow>();
    public virtual ICollection<UserFollow> Followings { get; set; } = new HashSet<UserFollow>();
}
