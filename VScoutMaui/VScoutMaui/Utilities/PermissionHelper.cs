namespace VScoutCentral.Utilities
{
    internal static class PermissionHelper
    {
        public static async Task<bool> CheckAndRequestPermissionAsync<T>() where T : Permissions.BasePermission, new()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<T>();

            if (status == PermissionStatus.Granted) return true;

            status = await Permissions.RequestAsync<T>();

            return status == PermissionStatus.Granted;
        }
    }
}
