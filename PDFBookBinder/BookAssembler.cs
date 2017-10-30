using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDFBookBinder;

namespace PDFBookBinderiText
{
    using iText.IO.Image;
    using iText.Kernel.Geom;
    using iText.Kernel.Pdf;
    using iText.Kernel.Pdf.Canvas;
    using iText.Kernel.Pdf.Navigation;
    using iText.Kernel.Pdf.Xobject;
    using iText.Layout;
    using iText.Layout.Element;
    using System.IO;
    using System.Windows.Media.Imaging;

    class BookAssembler
    {
        internal BookAssembler()
        {
        }

        internal Boolean iTextAssemble(Book CurrentBook, String Filename, Boolean AllowResize)
        {
            try
            {
                String Bufferpath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Buffer.jpg");
                using (PdfWriter pdfWriter = new PdfWriter(Filename, new WriterProperties().SetFullCompressionMode(true)))
                using (PdfDocument pdfDocument = new PdfDocument(pdfWriter))
                using (Document document = new Document(pdfDocument))
                {
                    PdfOutline pdfOutline = pdfDocument.GetOutlines(false);
                    for (Int32 i = 0; i < CurrentBook.Pages.Count; i++)
                    {
                        var bookPage = CurrentBook.Pages[i];
                        BitmapFrame bitmapFrame = BitmapFrame.Create(new Uri(bookPage.Filename));
                        PageSize pageSize;
                        Boolean Resize = false;
                        if (bitmapFrame.Width > bitmapFrame.Height)
                        {
                            //Landscape
                            pageSize = new PageSize(PageSize.Default);
                            if (bitmapFrame.PixelWidth > 1500 && AllowResize)
                            {
                                pageSize.SetWidth(1500);
                                pageSize.SetHeight((float)bitmapFrame.PixelHeight / (float)bitmapFrame.PixelWidth * pageSize.GetWidth());
                                Resize = true;
                            }
                            else
                            {
                                pageSize.SetWidth((float)bitmapFrame.PixelWidth);
                                pageSize.SetHeight((float)bitmapFrame.PixelHeight);
                            }
                        }
                        else
                        {
                            //Portrait
                            pageSize = new PageSize(PageSize.Default).Rotate();
                            if (bitmapFrame.Height > 1500 && AllowResize)
                            {
                                pageSize.SetHeight(1500);
                                pageSize.SetWidth((float)bitmapFrame.PixelWidth / (float)bitmapFrame.PixelHeight * pageSize.GetHeight());
                                Resize = true;
                            }
                            else
                            {
                                pageSize.SetHeight((float)bitmapFrame.PixelHeight);
                                pageSize.SetWidth((float)bitmapFrame.PixelWidth);
                            }
                        }
                        PdfCanvas canvas = new PdfCanvas(pdfDocument.AddNewPage(pageSize));
                        ImageData PagePicture = null;
                        if (Resize)
                        {
                            JpegBitmapEncoder jpgEncoder = new JpegBitmapEncoder();
                            jpgEncoder.Frames.Add(ImageHelper.Resize(bitmapFrame, (int)pageSize.GetWidth(), (int)pageSize.GetHeight(), System.Windows.Media.BitmapScalingMode.Fant));
                            MemoryStream imageStream = new MemoryStream();
                            //jpgEncoder.Save(imageStream);
                            FileStream imageFileStream = new FileStream(Bufferpath, FileMode.Create);
                            jpgEncoder.Save(imageFileStream);
                            imageFileStream.Close();
                            PagePicture = ImageDataFactory.Create(Bufferpath);
                        }
                        else
                        {
                            PagePicture = ImageDataFactory.Create(bookPage.Filename);
                        }

                        canvas.AddImage(PagePicture, pageSize, false);
                        //Bookmark
                        if (bookPage.Bookmark != null)
                            if (bookPage.Bookmark.Active)
                            {
                                try
                                {
                                    //pdfOutline.AddOutline(bookPage.Bookmark.Text);
                                    //pdfOutline.AddDestination(PdfDestination.MakeDestination();
                                }
                                catch (Exception exception)
                                {

                                }
                            }
                    }
                    pdfDocument.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
    }
}
namespace PDFBookBinderPdfSharp
{
    using PdfSharp.Pdf;
    using PdfSharp.Drawing;
    using PdfSharp;

    class BookAssembler
    {
        internal BookAssembler()
        {
        }

        internal Boolean PdfSharpAssemble(Book CurrentBook, String Filename)
        {
            try
            {
                //First, create the PDF file
                PdfDocument PDFBook = new PdfDocument();
                //And some standard page sizes
                PageSize[] pageSizes = (PageSize[])Enum.GetValues(typeof(PageSize));
                //Add some auxiliary info
                PDFBook.Info.Title = CurrentBook.Title;
                PDFBook.Info.Author = "Zero Serenity, LLC";
                PdfOutline Outline = null;
                for (Int32 i = 0; i < CurrentBook.Pages.Count; i++)
                {
                    var bookPage = CurrentBook.Pages[i];
                    //Next, add pages in order
                    PdfPage AssembledPage = PDFBook.AddPage();
                    XGraphics GFX = XGraphics.FromPdfPage(AssembledPage);
                    //Downscale image to 2,000px max, to save space
                    XImage PagePicture = XImage.FromFile(bookPage.Filename);
                    //Set the page size
                    if (PagePicture.PointWidth > PagePicture.PointHeight)
                    {
                        //Landscape
                        AssembledPage.Orientation = PageOrientation.Landscape;
                        AssembledPage.Width = PagePicture.PointWidth > 1500 ? 1500 : PagePicture.PointWidth;
                        AssembledPage.Height = PagePicture.PointHeight / PagePicture.PointWidth * AssembledPage.Width;
                    }
                    else
                    {
                        //Portrait
                        AssembledPage.Orientation = PageOrientation.Portrait;
                        AssembledPage.Height = PagePicture.PointHeight > 1500 ? 1500 : PagePicture.PointHeight;
                        AssembledPage.Width = PagePicture.PointWidth / PagePicture.PointHeight * AssembledPage.Height;
                    }
                    //Add image to page
                    GFX.DrawImage(PagePicture, new XRect(0, 0, AssembledPage.Width, AssembledPage.Height));
                    //Check to see if this page needs a bookmark.
                    if (bookPage.Bookmark != null)
                    {
                        if (Outline == null)
                        {
                            PDFBook.Outlines.Add(bookPage.Bookmark.Text, AssembledPage);
                        }
                        else
                        {
                            Outline.Outlines.Add(bookPage.Bookmark.Text, AssembledPage);
                        }
                    }
                }
                //Seal it up, ship it out!
                PDFBook.Save(Filename);
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return false;
        }

    }
}
