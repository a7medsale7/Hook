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



    public static IList<string?> GetAllPermissions() =>
       typeof(Permissions).GetFields().Select(f => f.GetValue(f) as string).ToList();
}
