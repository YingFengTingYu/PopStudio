﻿using PopStudio.Platform;

namespace PopStudio.WPF
{
    class Program
    {
        [STAThread]
        static void Main(params string[] args)
        {
            YFAPI.RegistPlatform<WPFAPI>();
            YFBitmap.RegistPlatform<GDIBitmap>();
            YFFileListStream.RegistPlatform(YFRes.CompiledImageList);
            try
            {
                string settingxml = Permission.GetSettingPath();
                if (File.Exists(settingxml))
                {
                    Setting.LoadFromXml(settingxml);
                }
            }
            catch (Exception)
            {
            }
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}