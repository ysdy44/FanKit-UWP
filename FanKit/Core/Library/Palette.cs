﻿using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;

namespace FanKit.Frames.Library
{

    /// <summary>
    /// Palette color from pictures
    /// </summary>
    public static class Palette
    {
        //The default color
        public static readonly Color DefaultColor = Color.FromArgb(255, 177, 101, 105);

        //Size of the cache thumbnail
        private static readonly int width = 8;
        private static readonly int height = 8;
        private static readonly CanvasDevice device = new CanvasDevice();

        /// <summary>
        /// Palette color from Uri
        /// </summary>
        /// <param name="uri">web or file path</param>
        /// <returns></returns>
        public static async Task<Color> GetPaletteFormImage(Uri uri)
        {
            try
            {
                CanvasBitmap bimap = await CanvasBitmap.LoadAsync(device, uri);

                return GetPaletteFormBitmap(bimap);
            }
            catch (Exception)
            {
                return DefaultColor;
            }
        }

        /// <summary>
        /// Palette color from CanvasBitmap
        /// </summary>
        /// <param name="bimap">win 2D CanvasBitmap</param>
        /// <returns></returns>
        private static Color GetPaletteFormBitmap(CanvasBitmap bimap)
        {
            try
            {
                //scale 
                double scaleX = width / bimap.Size.Width;
                double scaleY = height / bimap.Size.Height;
                Vector2 v = new Vector2((float)scaleX, (float)scaleY);
                Matrix3x2 m = Matrix3x2.CreateScale(v);

                //draw
                using (CanvasRenderTarget target = new CanvasRenderTarget(device, width, height, bimap.Dpi))
                {
                    using (CanvasDrawingSession ds = target.CreateDrawingSession())
                    {
                        Transform2DEffect effect = new Transform2DEffect
                        {
                            Source = bimap,
                            TransformMatrix = m
                        };
                        ds.DrawImage(effect);
                    }

                    //Palette
                    Color[] colors = target.GetPixelColors();
                    return GetPaletteFromColors(colors);
                }
            }
            catch (Exception)
            {
                return DefaultColor;
            }
        }

        /// <summary>
        ///  Palette color from colors
        /// </summary>
        /// <param name="colors">color array</param>
        /// <returns></returns>
        public static Color GetPaletteFromColors(Color[] colors)
        {
            try
            {
                //saturation
                double sumS = 0;

                //lightness
                double sumV = 0;
                double sumHue = 0;

                double maxV = 0;
                double maxS = 0;
                double maxH = 0;
                double count = 0;
                foreach (var item in colors)
                {
                    HsvColor hsv = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToHsv(item);

                    if (hsv.H == 0)
                    {
                        continue;
                    }
                    maxS = hsv.S > maxS ? hsv.S : maxS;
                    maxV = hsv.V > maxV ? hsv.V : maxV;
                    maxH = hsv.H > maxH ? hsv.H : maxH;
                    sumHue += hsv.H;
                    sumS += hsv.S;
                    sumV += hsv.V;
                    count++;
                }


                double avgH = sumHue / count;
                double avgV = sumV / count;
                double avgS = sumS / count;
                double maxAvgV = maxV / 2;
                double maxAvgS = maxS / 2;
                double maxAvgH = maxH / 2;

                double h = Math.Max(maxAvgV, avgV);
                double s = Math.Min(maxAvgS, avgS);
                double hue = Math.Min(maxAvgH, avgH);

                double R = 0;
                double G = 0;
                double B = 0;
                count = 0;


                foreach (var item in colors)
                {
                    HsvColor hsv = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToHsv(item);

                    if (hsv.H == 0)
                    {
                        continue;
                    }

                    if (hsv.H >= hue + 10 && hsv.V >= h && hsv.S >= s)
                    {
                        R += item.R;
                        G += item.G;
                        B += item.B;
                        count++;
                    }
                }

                double r = R / count;
                double g = G / count;
                double b = B / count;

             //   if (r + g + b > 96 && r + g + b < 672)
                    return Color.FromArgb(255, (byte)r, (byte)g, (byte)b);
              //  else
                  //  return DefaultColor;

            }
            catch (Exception)
            {
                return DefaultColor;
            }
        }

        /// <summary>
        /// get CanvasBitmap form uri
        /// </summary>
        /// <param name="uri">web or file path</param>
        /// <returns></returns>
        public static async Task<CanvasBitmap> GetBitmapFormImage(Uri uri)
        {
            try
            {
                //实例化资源
                return await CanvasBitmap.LoadAsync(device, uri);
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// Save the file via a Uri
        /// </summary>
        /// <param name="url">web or file path</param>
        /// <param name="foldername">folder name in PicturesLibrary</param>
        /// <returns></returns>
        public static async Task<bool> Save(string url, string foldername = "buka")
        {
            try
            {
                //得到路径与名称
                string[] Splits = url.Split('/');
                string name = Splits.Last();

                //得到图片与格式
                CanvasBitmap bitmap = await GetBitmapFormImage(new Uri(url));
                CanvasBitmapFileFormat format = GetFormat(name);

                //得到文件夹与格式
                StorageFolder folder = await KnownFolders.PicturesLibrary.CreateFolderAsync(foldername, CreationCollisionOption.OpenIfExists);
                StorageFile file = await folder.CreateFileAsync(name);

                //保存
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    await bitmap.SaveAsync(fileStream, format);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// CanvasBitmap FileFormat
        /// </summary>
        /// <param name="name">"jpg"?</param>
        /// <returns></returns>
        public static CanvasBitmapFileFormat GetFormat(string name)
        {
            string[] Splits = name.Split('.');
            string format = Splits.Last();
            string Upper = format.ToUpper();

            switch (Upper)
            {
                case "BMP": return CanvasBitmapFileFormat.Bmp;
                case "GIF": return CanvasBitmapFileFormat.Gif;
                case "JPEG": return CanvasBitmapFileFormat.Jpeg;
                case "JPEGXR": return CanvasBitmapFileFormat.JpegXR;
                case "PNG": return CanvasBitmapFileFormat.Png;
                case "TIFF": return CanvasBitmapFileFormat.Tiff;
                default: return CanvasBitmapFileFormat.Jpeg;
            }
        }
    }
}
