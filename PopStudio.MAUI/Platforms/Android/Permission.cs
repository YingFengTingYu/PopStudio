using Android.Content.Res;
using Microsoft.Maui.Platform;
using PopStudio.Language.Languages;

namespace PopStudio.Platform
{
    internal static partial class Permission
    {
        //static async void TransformSystemBar()
        //{
        //    if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.R)
        //    {
        //        Android.App.Activity activity = await Platform.WaitForActivityAsync();
        //        Android.Views.Window window = activity.Window;
        //        window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
        //        window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
        //        window.SetStatusBarColor(Android.Graphics.Color.Transparent);
        //        AndroidX.Core.View.WindowInsetsControllerCompat controller = AndroidX.Core.View.ViewCompat.GetWindowInsetsController(window.DecorView);
        //        controller!.AppearanceLightStatusBars = true;
        //    }
        //}

        public static partial void PlatformInit()
        {
            #region Switch
            Microsoft.Maui.Handlers.SwitchHandler.Mapper.AppendToMapping("BlueTrack", (h, v) =>
            {
                h.PlatformView.TrackTintList = ColorStateList.ValueOf(Color.FromUint(0xFFB9B9B9).ToPlatform());
            });
            #endregion
            #region Underline
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("BlueUnderline", (h, v) =>
            {
                h.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Colors.CornflowerBlue.ToPlatform());
            });
            Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping("BlueUnderline", (h, v) =>
            {
                h.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Colors.CornflowerBlue.ToPlatform());
            });
            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("BlueUnderline", (h, v) =>
            {
                h.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Colors.CornflowerBlue.ToPlatform());
            });
            #endregion
        }

        public static partial bool HiddenPermission() => false;

        public static partial bool HiddenFlyout() => true;

        static readonly string androidpath = Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath + "/setting.xml";
        public static partial string GetSettingPath() => androidpath;

        public static partial async Task<bool> CheckPermissionAsync()
        {
            ReadWriteStoragePermission readwritepermission = new ReadWriteStoragePermission();
            PermissionStatus status = await readwritepermission.CheckStatusAsync();
            if (status != PermissionStatus.Granted) return false;
#pragma warning disable CA1416
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.R && !Android.OS.Environment.IsExternalStorageManager) return false;
#pragma warning restore CA1416
            return true;
        }

        public static partial async Task<bool> CheckAndRequestPermissionAsync(this ContentPage page)
        {
            ReadWriteStoragePermission readwritepermission = new ReadWriteStoragePermission();
            PermissionStatus status = await readwritepermission.CheckStatusAsync();
            bool HavePermission = true;
            if (status != PermissionStatus.Granted)
            {
                HavePermission = (await readwritepermission.RequestAsync()) == PermissionStatus.Granted;
            }
            if (!HavePermission) return false;
#pragma warning disable CA1416
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.R && !Android.OS.Environment.IsExternalStorageManager)
            {
                if (await page.DisplayAlert(MAUIStr.Obj.Permission_Title, MAUIStr.Obj.Permission_Request2, MAUIStr.Obj.Permission_GoTo, MAUIStr.Obj.Permission_Cancel))
                {
                    var bb = new Android.Content.Intent(Android.Provider.Settings.ActionManageAllFilesAccessPermission);
                    bb.SetFlags(Android.Content.ActivityFlags.NewTask);
                    Android.App.Application.Context.StartActivity(bb);
                }
            }
#pragma warning restore CA1416
            return true;
        }

        private class ReadWriteStoragePermission : Permissions.BasePlatformPermission
        {
            public override (string androidPermission, bool isRuntime)[] RequiredPermissions => new (string androidPermission, bool isRuntime)[2]
            {
                (Android.Manifest.Permission.ReadExternalStorage, true),
                (Android.Manifest.Permission.WriteExternalStorage, true)
            };
        }
    }
}
