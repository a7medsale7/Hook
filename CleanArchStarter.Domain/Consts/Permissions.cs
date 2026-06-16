using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Consts;
public static class Permissions
{
    public const string Type = "Permissions";

    // Users
    public const string Users_ViewProfile = "Permissions.Users.ViewProfile";
    public const string Users_UpdateProfile = "Permissions.Users.UpdateProfile";
    public const string Users_ChangePassword = "Permissions.Users.ChangePassword";
    public const string Users_ViewAll = "Permissions.Users.ViewAll";
    public const string Users_ManageRoles = "Permissions.Users.ManageRoles";

    // Roles
    public const string Roles_View = "Permissions.Roles.View";
    public const string Roles_Create = "Permissions.Roles.Create";
    public const string Roles_Update = "Permissions.Roles.Update";
    public const string Roles_ToggleActive = "Permissions.Roles.ToggleActive";

    // BoatOwner
    public const string BoatOwner_Apply = "Permissions.BoatOwner.Apply";
    public const string BoatOwner_ViewProfile = "Permissions.BoatOwner.ViewProfile";
    public const string BoatOwner_ViewAll = "Permissions.BoatOwner.ViewAll";
    public const string BoatOwner_UpdateStatus = "Permissions.BoatOwner.UpdateStatus";
    public const string BoatOwner_Delete = "Permissions.BoatOwner.Delete";
    public const string BoatOwner_Restore = "Permissions.BoatOwner.Restore";

    // Boats
    public const string Boats_View = "Permissions.Boats.View";
    public const string Boats_Create = "Permissions.Boats.Create";
    public const string Boats_Update = "Permissions.Boats.Update";
    public const string Boats_Delete = "Permissions.Boats.Delete";
    public const string Boats_Restore = "Permissions.Boats.Restore";

    // Trips
    public const string Trips_View = "Permissions.Trips.View";
    public const string Trips_Create = "Permissions.Trips.Create";
    public const string Trips_Update = "Permissions.Trips.Update";
    public const string Trips_Delete = "Permissions.Trips.Delete";
    public const string Trips_Restore = "Permissions.Trips.Restore";

    // Bookings
    public const string Bookings_View = "Permissions.Bookings.View";
    public const string Bookings_Create = "Permissions.Bookings.Create";
    public const string Bookings_UpdateStatus = "Permissions.Bookings.UpdateStatus";
    public const string Bookings_Cancel = "Permissions.Bookings.Cancel";
    public const string Bookings_ViewAll = "Permissions.Bookings.ViewAll";
    public const string Bookings_Delete = "Permissions.Bookings.Delete";

    // Payments
    public const string Payments_View = "Permissions.Payments.View";
    public const string Payments_UploadReceipt = "Permissions.Payments.UploadReceipt";
    public const string Payments_Verify = "Permissions.Payments.Verify";
    public const string Payments_ViewAll = "Permissions.Payments.ViewAll";
    public const string Payments_Stats = "Permissions.Payments.Stats";

    // Reviews
    public const string Reviews_View = "Permissions.Reviews.View";
    public const string Reviews_Create = "Permissions.Reviews.Create";
    public const string Reviews_Update = "Permissions.Reviews.Update";
    public const string Reviews_Delete = "Permissions.Reviews.Delete";

    //------------------------------Market------------------------------------------
    //seller
    public const string Seller_Apply = "Permissions.Seller.Apply";
    public const string Seller_ViewProfile = "Permissions.Seller.ViewProfile";
    public const string Seller_ViewAll = "Permissions.Seller.ViewAll";
    public const string Seller_UpdateStatus = "Permissions.Seller.UpdateStatus";
    public const string Seller_Delete = "Permissions.Seller.Delete";
    public const string Seller_Restore = "Permissions.Seller.Restore";

    //products
    public const string MarketplaceProducts_View = "Permissions.Marketplace.Products.View";
    public const string MarketplaceProducts_Create = "Permissions.Marketplace.Products.Create";
    public const string MarketplaceProducts_Update = "Permissions.Marketplace.Products.Update";
    public const string MarketplaceProducts_Delete = "Permissions.Marketplace.Products.Delete";

    //Orders
    public const string MarketplaceOrders_Create = "Permissions.Marketplace.Orders.Create";
    public const string MarketplaceOrders_UpdateStatus = "Permissions.Marketplace.Orders.UpdateStatus";
    public const string MarketplaceOrders_View = "Permissions.Marketplace.Orders.View";
    public const string MarketplaceOrders_Cancel = "Permissions.Marketplace.Orders.Cancel";
    public const string MarketplaceOrders_Stats = "Permissions.Marketplace.Orders.Stats";

    //Cart
    public const string MarketplaceCart_View = "Permissions.Marketplace.Cart.View";
    public const string MarketplaceCart_Update = "Permissions.Marketplace.Cart.Update";

    //Reviews (only after purchase)
    public const string MarketplaceReviews_Create = "Permissions.Marketplace.Reviews.Create";
    public const string MarketplaceReviews_View = "Permissions.Marketplace.Reviews.View";

    //Admin Management
    public const string MarketplaceAdmin_ViewSellers = "Permissions.Marketplace.Admin.ViewSellers";
    public const string MarketplaceAdmin_DeleteSeller = "Permissions.Marketplace.Admin.DeleteSeller";
    public const string MarketplaceAdmin_ViewProducts = "Permissions.Marketplace.Admin.ViewProducts";
    public const string MarketplaceAdmin_DeleteProduct = "Permissions.Marketplace.Admin.DeleteProduct";

    //Admin Approvals
    public const string MarketplaceApprovals_View = "Permissions.Marketplace.Approvals.View";
    public const string MarketplaceApprovals_Update = "Permissions.Marketplace.Approvals.Update";
    
    //------------------------------Community------------------------------------------
    public const string Community_Posts_Create = "Permissions.Community.Posts.Create";
    public const string Community_Posts_Update = "Permissions.Community.Posts.Update";
    public const string Community_Posts_Delete = "Permissions.Community.Posts.Delete";
    public const string Community_Posts_Like = "Permissions.Community.Posts.Like";
    public const string Community_Posts_Save = "Permissions.Community.Posts.Save";
    public const string Community_Posts_Report = "Permissions.Community.Posts.Report";
    public const string Community_Comments_Add = "Permissions.Community.Comments.Add";
    public const string Community_Comments_Delete = "Permissions.Community.Comments.Delete";
    public const string Community_User_Follow = "Permissions.Community.User.Follow";
    public const string Community_Events_Join = "Permissions.Community.Events.Join";
    public const string Community_Events_Participants_View = "Permissions.Community.Events.Participants.View";
    public const string Community_Feed_View = "Permissions.Community.Feed.View";
    public const string Community_Notifications_View = "Permissions.Community.Notifications.View";
    public const string Community_Complaints_Support = "Permissions.Community.Complaints.Support";
    
    public const string CommunityAdmin_Complaints_View = "Permissions.Community.Admin.Complaints.View";
    public const string CommunityAdmin_Complaints_Resolve = "Permissions.Community.Admin.Complaints.Resolve";

    //------------------------------FishGuard AI---------------------------------------
    public const string FishGuardAdmin_Manage = "Permissions.FishGuard.Admin.Manage";

    //-------------------------------------------------------------------------------------------------------
    public static IList<string?> GetAllPermissions() =>
        typeof(Permissions).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy)
        .Where(f => f.IsLiteral && !f.IsInitOnly)
        .Select(f => f.GetValue(null) as string).ToList();
}
