﻿using SkiaSharp;

namespace PopStudio.Texture
{
    internal static class RGBA4444
    {
        public static SKBitmap Read(BinaryStream bs, int width, int height)
        {
            int S = width * height;
            SKColor[] pixels = new SKColor[S];
            ushort temp;
            int r, g, b, a;
            for (int i = 0; i < S; i++)
            {
                temp = bs.ReadUInt16();
                r = (temp & 0xF000) >> 12;
                g = (temp & 0xF00) >> 8;
                b = (temp & 0xF0) >> 4;
                a = temp & 0xF;
                pixels[i] = new SKColor((byte)((r << 4) | r), (byte)((g << 4) | g), (byte)((b << 4) | b), (byte)((a << 4) | a));
            }
            SKBitmap image = new SKBitmap(width, height);
            image.Pixels = pixels;
            return image;
        }

        public static int Write(BinaryStream bs, SKBitmap image)
        {
            SKColor[] pixels = image.Pixels;
            int S = pixels.Length;
            for (int i = 0; i < S; i++)
            {
                bs.WriteUInt16((ushort)((pixels[i].Alpha >> 4) | (pixels[i].Blue & 0xF0) | ((pixels[i].Green & 0xF0) << 4) | ((pixels[i].Red & 0xF0) << 8)));
            }
            return image.Width << 1;
        }
    }
}