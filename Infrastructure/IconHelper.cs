using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class IconHelper
    {
        public static byte[] LoadSvgAsPng(string path, int width = 64, int height = 64)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"El archivo SVG no se encuentra en la ruta {path}");

            var svg = new SKSvg();
            using var stream = File.OpenRead(path);

            svg.Load(stream);

            var picture = svg.Picture;
            if (picture == null)
                throw new Exception("No se pudo cargar la imagen SVG.");

            using var bitmap = new SKBitmap(width, height);
            using var canvas = new SKCanvas(bitmap);

            canvas.Clear(SKColors.Transparent);
            float scaleX = width / picture.CullRect.Width;
            float scaleY = height / picture.CullRect.Height;
            canvas.Scale(scaleX, scaleY);

            canvas.DrawPicture(picture);
            canvas.Flush();

            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);

            return data.ToArray();
        }
    }
}
