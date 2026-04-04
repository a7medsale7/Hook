using Hook.Application.Abstractions.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Errors;
public static class BoatOwnerErrors
{
    public static readonly Error ProfileNotFound =
        new("BoatOwner.NotFound", "The boat owner profile was not found.");
    public static readonly Error AlreadyApplied =
        new("BoatOwner.AlreadyApplied", "You have already submitted an application.");
    public static readonly Error NotApproved =
        new("BoatOwner.NotApproved", "Your boat owner account is not yet approved or has been rejected.");
    public static readonly Error RejectionReasonRequired =
        new("BoatOwner.RejectionReasonRequired", "A rejection reason must be provided by the admin.");
    public static readonly Error InvalidDocuments =
        new("BoatOwner.InvalidDocuments", "The provided documents are invalid or expired.");
}