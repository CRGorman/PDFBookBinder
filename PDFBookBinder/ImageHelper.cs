using iText.IO.Image;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PDFBookBinder
{
    class ImageHelper
    {
        public static ImageData Resize(String imageFilename, int width, int height)
        {
            try
            {
                BitmapFrame bitmapFrame = Resize(BitmapFrame.Create(new FileStream(imageFilename, FileMode.Open)), width, height, BitmapScalingMode.HighQuality);
                return ImageDataFactory.Create(Resize(bitmapFrame, width, height, BitmapScalingMode.Fant).BaseUri);
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        public static BitmapFrame Resize(BitmapFrame photo, int width, int height, BitmapScalingMode scalingMode)
        {
            try
            {
                //Double check the size.

                var group = new DrawingGroup();
                RenderOptions.SetBitmapScalingMode(group, scalingMode);
                group.Children.Add(new ImageDrawing(photo, new Rect(0, 0, width, height)));
                var targetVisual = new DrawingVisual();
                var targetContext = targetVisual.RenderOpen();
                targetContext.DrawDrawing(group);
                var target = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Default);
                targetContext.Close();
                target.Render(targetVisual);
                var targetFrame = BitmapFrame.Create(target);
                return targetFrame;
            }
            catch (Exception exception)
            {
                return photo;
            }
        }
    }
}
