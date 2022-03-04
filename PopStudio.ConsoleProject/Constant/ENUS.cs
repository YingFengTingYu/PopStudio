﻿namespace PopStudio.ConsoleProject.Constant
{
    internal class ENUS : ILanguage
    {
        static readonly string[] all = {
            "{0} {1}",
            "Author:{0}",
            "Hello! This is the program to convert files used in PopCap Games.",
            "enter 1 to run unpack mode",
            "enter 2 to run pack mode",
            "enter 3 to run image decode mode",
            "enter 4 to run image encode mode",
            "If you want to unpack \".dz\", please enter 1.",
            "If you want to unpack \".rsb\", please enter 2.",
            "If you want to unpack \".pak\", please enter 3.",
            "Please choose dz file",
            "Please choose rsb file",
            "Please choose pak file",
            "If you want to pack \".dz\", please enter 1.",
            "If you want to pack \".rsb\", please enter 2.",
            "If you want to pack \".pak\", please enter 3.",
            "Please choose dz source folder",
            "Please choose rsb source folder",
            "Please choose pak source folder",
            "If you want to decode \".ptx\"(in rsb), please enter 1.",
            "If you want to decode \".cdat\", please enter 2.",
            "If you want to decode \".tex\"(iOS version), please enter 3.",
            "If you want to decode \".txz\", please enter 4.",
            "If you want to decode \".tex\"(Android TV version), please enter 5.",
            "If you want to decode \".ptx\"(in PVZ Xbox360), please enter 6.",
            "If you want to decode \".ptx\"(in PVZ PS3), please enter 7.",
            "Please choose ptx file",
            "Please choose cdat file",
            "Please choose tex file",
            "Please choose txz file",
            "If you want to encode \".ptx\"(in rsb), please enter 1.",
            "If you want to encode \".cdat\", please enter 2.",
            "If you want to encode \".tex\"(iOS version), please enter 3.",
            "If you want to encode \".txz\", please enter 4.",
            "If you want to encode \".tex\"(Android TV version), please enter 5.",
            "If you want to encode \".ptx\"(in PVZ Xbox360), please enter 6.",
            "If you want to encode \".ptx\"(in PVZ PS3), please enter 7.",
            "Please choose png file",
            "Please select the mode",
            "enter 1 to use ARGB8888 texture(format 0)",
            "enter 2 to use ABGR8888 texture(format 0)",
            "enter 3 to use RGBA4444 texture(format 1)",
            "enter 4 to use RGB565 texture(format 2)",
            "enter 5 to use RGBA5551 texture(format 3)",
            "enter 6 to use RGBA4444_Block texture(format 21)",
            "enter 7 to use RGB565_Block texture(format 22)",
            "enter 8 to use RGBA5551_Block texture(format 23)",
            "enter 9 to use XRGB8888_A8 texture(format 149)",
            "enter 10 to use ARGB8888 big endian texture(format 0)",
            "enter 11 to use ARGB8888 big endian and padding texture(format 0)",
            "enter 12 to use DXT1_RGB texture(format 35)",
            "enter 13 to use DXT3_RGBA texture(format 36)",
            "enter 14 to use DXT5_RGBA texture(format 37)",
            "enter 15 to use DXT5 texture(format 5)",
            "enter 16 to use DXT5 big endian texture(format 5)",
            "enter 17 to use ETC1_RGB texture(format 32)",
            "enter 18 to use ETC1_RGB_A8 texture(format 147)",
            "enter 19 to use ETC1_RGB_A_Palette texture(format 147)",
            "enter 20 to use ETC1_RGB_A_Palette texture(format 150)",
            "enter 21 to use PVRTC_4BPP_RGBA texture(format 30)",
            "enter 22 to use PVRTC_4BPP_RGB_A8 texture(format 148)",
            "enter 23 to use PVRTC_2BPP_RGBA texture(format 31)",
            "enter 1 to use ABGR8888 texture(format 1)",
            "enter 2 to use RGBA4444 texture(format 2)",
            "enter 3 to use RGBA5551 texture(format 3)",
            "enter 4 to use RGB565 texture(format 4)",
            "enter 1 to use LUT8 texture(format 1)(Invalid)",
            "enter 2 to use ARGB8888 texture(format 2)",
            "enter 3 to use ARGB4444 texture(format 3)",
            "enter 4 to use ARGB1555 texture(format 4)",
            "enter 5 to use RGB565 texture(format 5)",
            "enter 6 to use ABGR8888 texture(format 6)",
            "enter 7 to use RGBA4444 texture(format 7)",
            "enter 8 to use RGBA5551 texture(format 8)",
            "enter 9 to use XRGB8888 texture(format 9)",
            "enter 10 to use LA88 texture(format 10)",
            "Invalid mode!",
            "enter 5 to run RTON decode mode",
            "enter 6 to run RTON encode mode",
            "If you want to decode simple \".RTON\", please enter 1.",
            "If you want to decode encrypt \".RTON\", please enter 2.(You must enter the key by yourself)",
            "If you want to encode simple \".RTON\", please enter 1.",
            "If you want to encode encrypt \".RTON\", please enter 2.(You must enter the key by yourself)",
            "Please choose RTON file",
            "Please choose json file",
            "Please enter the key",
            "enter 7 to run reanim decode mode(decode to json)",
            "enter 8 to run reanim encode mode(encode from json)",
            "If you want to decode PC version(Windows and MacOS), please enter 1.",
            "If you want to decode Phone32 version(Android, iOS, Bada and BlackBerry), please enter 2.",
            "If you want to decode Phone64 version(Android and iOS), please enter 3.",
            "If you want to decode WP version(WindowsPhone), please enter 4.",
            "If you want to decode GameConsole version(PS3, PSV and Xbox360), please enter 5.",
            "If you want to decode TV version(Android TV), please enter 6.",
            "If you want to encode PC version(Windows and MacOS), please enter 1.",
            "If you want to encode Phone32 version(Android, iOS and Bada), please enter 2.",
            "If you want to encode Phone64 version(Android and iOS), please enter 3.",
            "If you want to encode WP version(WindowsPhone), please enter 4.",
            "If you want to encode GameConsole version(PS3, PSV and Xbox360), please enter 5.",
            "If you want to encode TV version(Android TV), please enter 6.",
            "Please choose reanim.compiled file",
            "Please choose xnb file",
            "Please choose reanim file",
            "enter 9 to run reanim decode mode(decode to json)",
            "enter 10 to run reanim encode mode(encode from json)",
            "Please choose trail.compiled file",
            "Please choose xnb file",
            "Please choose trail file",
            "If you want to decode \".ptx\"(in PVZ PSV), please enter 8.",
            "If you want to encode \".ptx\"(in PVZ PSV), please enter 8.",
            "enter 11 to run particles decode mode(decode to json)",
            "enter 12 to run particles encode mode(encode from json)",
            "Please choose xml file",
            "Please choose xml.compiled file",
        };

        public string[] All => all;
    }
}