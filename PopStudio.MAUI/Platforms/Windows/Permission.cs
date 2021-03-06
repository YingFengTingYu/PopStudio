namespace PopStudio.Platform
{
    internal static partial class Permission
    {
        public static partial void PlatformInit()
        {
        }
        public static partial bool HiddenPermission() => true;
        public static partial bool HiddenFlyout() => false;
        static readonly string windowspath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\setting.xml";

        public static partial string GetSettingPath() => windowspath;

        public static partial Task<bool> CheckPermissionAsync() => Task.FromResult(true);

        public static partial Task<bool> CheckAndRequestPermissionAsync(this ContentPage page) => Task.FromResult(true);
    }
}
