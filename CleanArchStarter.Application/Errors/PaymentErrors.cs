using Hook.Application.Abstractions.Result;

namespace Hook.Application.Errors;

public static class PaymentErrors
{
    public static readonly Error NotFound = new(
        "Payment.NotFound", "The payment with the specified identifier was not found.");

    public static readonly Error Unauthorized = new(
        "Payment.Unauthorized", "You are not authorized to access or verify this payment.");

    public static readonly Error AlreadyVerified = new(
        "Payment.AlreadyVerified", "This payment has already been verified.");

    public static readonly Error InvalidStatus = new(
        "Payment.InvalidStatus", "The payment is not in a states that allows this action.");

    public static readonly Error ReceiptRequired = new(
        "Payment.ReceiptRequired", "A receipt image URL is required for this payment method.");
}
