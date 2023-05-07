using FFMediaToolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSub
{
    public static class AppService
    {
		public unsafe static Bitmap ToBitmap(this ImageData bitmap)
		{
			fixed (byte* value = bitmap.Data)
			{
				return new Bitmap(bitmap.ImageSize.Width, bitmap.ImageSize.Height, bitmap.Stride, PixelFormat.Format24bppRgb, new IntPtr(value));
			}
		}
	}
}
