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
}
