using System;

namespace Hook.Domain.Helpers;

public static class EmailTemplates
{
    private const string BaseStyle = @"
        <style>
            body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f4f7f6; color: #333; line-height: 1.6; margin: 0; padding: 0; }
            .container { max-width: 600px; margin: 30px auto; background: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 6px rgba(0,0,0,0.1); }
            .header { background-color: #0056b3; color: #ffffff; padding: 20px; text-align: center; }
            .content { padding: 30px; }
            .footer { background-color: #f1f1f1; color: #777; text-align: center; padding: 15px; font-size: 12px; }
            .btn { display: inline-block; padding: 10px 20px; color: #ffffff; background-color: #28a745; text-decoration: none; border-radius: 5px; margin-top: 20px; font-weight: bold; }
            .alert-box { background-color: #fff3cd; color: #856404; padding: 15px; border-left: 4px solid #ffeeba; margin: 20px 0; border-radius: 4px; }
            .details-table { width: 100%; border-collapse: collapse; margin-top: 15px; }
            .details-table th, .details-table td { padding: 10px; border-bottom: 1px solid #ddd; text-align: left; }
            .details-table th { background-color: #f8f9fa; width: 40%; }
            .h1 { margin: 0; font-size: 24px; }
            .text-center { text-align: center; }
        </style>
    ";

    public static string GetReceiptUploadedTemplate(string ownerName, string tripTitle, decimal amount, string actionUrl)
    {
        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header'>
                    <h1 class='h1'>New Payment Receipt</h1>
                </div>
                <div class='content'>
                    <p>Hello <strong>{ownerName}</strong>,</p>
                    <p>A customer has just uploaded a new payment receipt for one of your trips. Please review it at your earliest convenience.</p>
                    
                    <table class='details-table'>
                        <tr><th>Trip Name</th><td>{tripTitle}</td></tr>
                        <tr><th>Amount Paid</th><td>{amount} EGP</td></tr>
                    </table>

                    <div class='text-center' style='margin-top: 30px;'>
                        <a href='{actionUrl}' class='btn'>Review & Accept Payment</a>
                    </div>
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }

    public static string GetBookingConfirmedTemplate(string userName, string tripTitle, DateTime date, decimal amount)
    {
        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header' style='background-color: #28a745;'>
                    <h1 class='h1'>Booking Confirmed! 🎉</h1>
                </div>
                <div class='content'>
                    <p>Hello <strong>{userName}</strong>,</p>
                    <p>Great news! Your booking and payment have been verified and confirmed by the boat owner.</p>
                    
                    <p>Here are your trip details:</p>
                    <table class='details-table'>
                        <tr><th>Trip Name</th><td>{tripTitle}</td></tr>
                        <tr><th>Date</th><td>{date.ToString("f")}</td></tr>
                        <tr><th>Total Paid</th><td>{amount} EGP</td></tr>
                    </table>

                    <p class='text-center'>
                        We wish you a fantastic fishing experience! Get your gear ready. 🎣
                    </p>
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }

    public static string GetBookingRejectedTemplate(string userName, string tripTitle, string? adminNotes)
    {
        string notesHtml = string.IsNullOrWhiteSpace(adminNotes) 
            ? "" 
            : $"<p><strong>Note from Owner:</strong> {adminNotes}</p>";

        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header' style='background-color: #dc3545;'>
                    <h1 class='h1'>Booking Issue ⚠️</h1>
                </div>
                <div class='content'>
                    <p>Hello <strong>{userName}</strong>,</p>
                    <p>Unfortunately, your booking/payment for the trip <strong>{tripTitle}</strong> could not be confirmed.</p>
                    
                    <div class='alert-box' style='background-color: #f8d7da; color: #721c24; border-color: #f5c6cb;'>
                        The boat owner rejected the payment/booking.
                        {notesHtml}
                    </div>

                    <p>If you believe this is a mistake or if you need a refund, please contact support.</p>
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }

    public static string GetTripReviewRequestTemplate(string userName, string tripTitle, string reviewUrl)
    {
        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header' style='background-color: #17a2b8;'>
                    <h1 class='h1'>How was your trip? 🎣</h1>
                </div>
                <div class='content'>
                    <p>Hello <strong>{userName}</strong>,</p>
                    <p>We hope you had a fantastic time on your recent fishing trip: <strong>{tripTitle}</strong>.</p>
                    
                    <p>Your feedback is incredibly valuable to us and to the boat owner. It helps maintain the quality of our community.</p>

                    <p class='text-center'>Please take a moment to rate your experience and leave a comment.</p>

                    <div class='text-center' style='margin-top: 30px;'>
                        <a href='{reviewUrl}' class='btn' style='background-color: #ffc107; color: #333;'>Leave a Review ⭐</a>
                    </div>
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }

    // Marketplace Emails
    // =========================

    public static string GetSellerRequestSubmittedTemplate(string userName, string sellerName)
    {
        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header'>
                    <h1 class='h1'>Seller Request Submitted</h1>
                </div>
                <div class='content'>
                    <p>Hello <strong>{userName}</strong>,</p>
                    <p>Your request to become a seller (<strong>{sellerName}</strong>) has been submitted successfully and is under review.</p>
                    <div class='alert-box'>
                        You will receive another email once the admin approves or rejects your request.
                    </div>
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }

    public static string GetSellerApprovedTemplate(string userName, string sellerName)
    {
        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header' style='background-color: #28a745;'>
                    <h1 class='h1'>Congratulations! 🎉</h1>
                </div>
                <div class='content'>
                    <p>Hello <strong>{userName}</strong>,</p>
                    <p>Your seller request (<strong>{sellerName}</strong>) has been approved. You are now a seller and can add/manage your products immediately.</p>
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }

    public static string GetSellerRejectedTemplate(string userName, string sellerName, string? reason)
    {
        var reasonHtml = string.IsNullOrWhiteSpace(reason) ? "" : $"<p><strong>Reason:</strong> {reason}</p>";
        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header' style='background-color: #dc3545;'>
                    <h1 class='h1'>Request Rejected</h1>
                </div>
                <div class='content'>
                    <p>Hello <strong>{userName}</strong>,</p>
                    <p>Your seller request (<strong>{sellerName}</strong>) has been rejected by admin.</p>
                    {reasonHtml}
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }

    public static string GetMarketplaceOrderCreatedBuyerTemplate(Hook.Domain.Entities.MarketplaceOrder order)
    {
        var itemsHtml = string.Join("", order.Items.Select(i =>
        {
            var title = i.Product?.Title ?? "Item";
            return $"<tr><th>{title}</th><td>{i.Quantity} × {i.UnitPrice} EGP = <strong>{i.LineTotal} EGP</strong></td></tr>";
        }));

        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header' style='background-color: #28a745;'>
                    <h1 class='h1'>Order Created ✅</h1>
                </div>
                <div class='content'>
                    <p>Your marketplace order was created successfully.</p>
                    <table class='details-table'>
                        <tr><th>Order Id</th><td>{order.Id}</td></tr>
                        <tr><th>Status</th><td>{order.Status}</td></tr>
                        <tr><th>Payment</th><td>{order.PaymentMethod}</td></tr>
                        <tr><th>Total</th><td><strong>{order.Total} EGP</strong></td></tr>
                    </table>
                    <h3>Items</h3>
                    <table class='details-table'>
                        {itemsHtml}
                    </table>
                    <div class='alert-box'>
                        Delivery is handled directly by the seller (no shipping company on the platform).
                    </div>
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }

    public static string GetMarketplaceNewOrderSellerTemplate(Hook.Domain.Entities.MarketplaceOrder order)
    {
        var itemsHtml = string.Join("", order.Items.Select(i =>
        {
            var title = i.Product?.Title ?? "Item";
            return $"<tr><th>{title}</th><td>{i.Quantity} × {i.UnitPrice} EGP</td></tr>";
        }));

        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header'>
                    <h1 class='h1'>New Order 📦</h1>
                </div>
                <div class='content'>
                    <p>You have a new marketplace order.</p>
                    <table class='details-table'>
                        <tr><th>Order Id</th><td>{order.Id}</td></tr>
                        <tr><th>Buyer Name</th><td>{order.FirstName} {order.LastName}</td></tr>
                        <tr><th>Buyer Phone</th><td>{order.ContactPhone}</td></tr>
                        <tr><th>Buyer Email</th><td>{order.ContactEmail}</td></tr>
                        <tr><th>Delivery Address</th><td>{order.Governorate}, {order.City}, {order.Address}, {order.PostalCode}</td></tr>
                        <tr><th>Payment</th><td>{order.PaymentMethod}</td></tr>
                        <tr><th>Total</th><td><strong>{order.Total} EGP</strong></td></tr>
                    </table>
                    <h3>Items</h3>
                    <table class='details-table'>
                        {itemsHtml}
                    </table>
                    <div class='alert-box'>
                        You are responsible for delivery (no shipping company integrated).
                    </div>
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }

    public static string GetMarketplaceOrderOutForDeliveryBuyerTemplate(Hook.Domain.Entities.MarketplaceOrder order)
    {
        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header' style='background-color: #17a2b8;'>
                    <h1 class='h1'>Out for Delivery 🚚</h1>
                </div>
                <div class='content'>
                    <p>Your order <strong>{order.Id}</strong> is on its way.</p>
                    <div class='alert-box'>
                        Once you receive it, open your profile → purchases and click <strong>I received my order</strong>.
                    </div>
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }

    public static string GetMarketplaceOrderCancelledBuyerTemplate(Hook.Domain.Entities.MarketplaceOrder order, string? reason)
    {
        var reasonHtml = string.IsNullOrWhiteSpace(reason) ? "" : $"<p><strong>Reason:</strong> {reason}</p>";
        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header' style='background-color: #dc3545;'>
                    <h1 class='h1'>Order Cancelled ⚠️</h1>
                </div>
                <div class='content'>
                    <p>Your order <strong>{order.Id}</strong> was cancelled by the seller.</p>
                    {reasonHtml}
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }

    public static string GetMarketplaceOrderDeliveredBuyerTemplate(Hook.Domain.Entities.MarketplaceOrder order)
    {
        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header' style='background-color: #28a745;'>
                    <h1 class='h1'>Delivered ✅</h1>
                </div>
                <div class='content'>
                    <p>Thank you! Your order <strong>{order.Id}</strong> is marked as delivered.</p>
                    <div class='alert-box'>
                        You can now leave a review (stars + comment). Reviews are only available after delivery confirmation.
                    </div>
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }

    public static string GetMarketplaceOrderDeliveredSellerTemplate(Hook.Domain.Entities.MarketplaceOrder order)
    {
        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header' style='background-color: #28a745;'>
                    <h1 class='h1'>Order Delivered ✅</h1>
                </div>
                <div class='content'>
                    <p>Order <strong>{order.Id}</strong> was confirmed delivered by the buyer.</p>
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }

    public static string GetMarketplaceListingApprovedTemplate(string userName, string listingTitle)
    {
        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header' style='background-color: #28a745;'>
                    <h1 class='h1'>Listing Approved ✅</h1>
                </div>
                <div class='content'>
                    <p>Hello <strong>{userName}</strong>,</p>
                    <p>Your marketplace listing <strong>{listingTitle}</strong> was approved by admin.</p>
                    <div class='alert-box'>
                        You now have access to the seller dashboard and can manage orders and products.
                    </div>
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }

    public static string GetMarketplaceListingRejectedTemplate(string userName, string listingTitle, string? reason)
    {
        var reasonHtml = string.IsNullOrWhiteSpace(reason) ? "" : $"<p><strong>Reason:</strong> {reason}</p>";
        return $@"
        <html>
        <head>{BaseStyle}</head>
        <body>
            <div class='container'>
                <div class='header' style='background-color: #dc3545;'>
                    <h1 class='h1'>Listing Rejected ❌</h1>
                </div>
                <div class='content'>
                    <p>Hello <strong>{userName}</strong>,</p>
                    <p>Your marketplace listing <strong>{listingTitle}</strong> was rejected by admin.</p>
                    {reasonHtml}
                </div>
                <div class='footer'>
                    &copy; {DateTime.UtcNow.Year} Hook Fishing Platform. All rights reserved.
                </div>
            </div>
        </body>
        </html>";
    }


}
