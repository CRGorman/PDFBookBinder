using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PDFBookBinder
{
    public partial class BookBinderForm : Form
    {
        private Book _currentBook;
        private String _currentBookFilename;

        private Book CurrentBook { get => _currentBook; set => _currentBook = value; }
        private String CurrentBookFilename { get => _currentBookFilename; set => _currentBookFilename = value; }

        public BookBinderForm()
        {
            InitializeComponent();
        }

        private void BindBookButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                AddExtension = true,
                DefaultExt = ".pdf",
                InitialDirectory = Path.GetDirectoryName(CurrentBookFilename)
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                PDFBookBinderiText.BookAssembler Assembler = new PDFBookBinderiText.BookAssembler();
                Assembler.iTextAssemble(CurrentBook, saveFileDialog.FileName, false);

                //The big daddy, send the XML to the binder and tell it to create everything.
                //PDFBookBinderPdfSharp.BookAssembler Assembler = new PDFBookBinderPdfSharp.BookAssembler();
                //Assembler.PdfSharpAssemble(CurrentBook, @"C:\Users\Zero\Documents\Visual Studio 2017\Projects\PDFBookBinder\PDFBookBinder\TestOut.pdf");
            }
        }

        private void SaveBookButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(CurrentBookFilename))
            {
                SaveFileDialog sfdSaveFileDialog = new SaveFileDialog();
                //If it has a filename, why bother changing it?
                if (sfdSaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    CurrentBookFilename = sfdSaveFileDialog.FileName;
                }
            }
            try
            {
                XmlSerializer BBSerializer = new XmlSerializer(typeof(Book));
                FileStream fileStream = new FileStream(CurrentBookFilename, FileMode.Create);
                BBSerializer.Serialize(fileStream, CurrentBook);
                fileStream.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void LoadBookButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdOpenFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                DefaultExt = "xml"
            };
            if (ofdOpenFileDialog.ShowDialog() != DialogResult.OK) return;
            //Attempt to parse out. If it's invalid, throw it out.
            try
            {
                XmlSerializer bbSerializer = new XmlSerializer(typeof(Book));
                FileStream fileStream = new FileStream(ofdOpenFileDialog.FileName, FileMode.Open);
                Book bbBinder = (Book)bbSerializer.Deserialize(fileStream);
                CurrentBook = bbBinder;
                CurrentBookFilename = ofdOpenFileDialog.FileName;
                fileStream.Close();
                ReloadVisualList();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }

        private void AddPagesButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdOpenFileDialog = new OpenFileDialog() { Multiselect = true };
            if (ofdOpenFileDialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                //TODO: Add pages at the point where the selection is.
                for (Int32 i = 0; i < ofdOpenFileDialog.FileNames.Length; i++)
                {
                    CurrentBook.Pages.Add(new BookPage() { Filename = ofdOpenFileDialog.FileNames[i] });
                }
                ReloadVisualList();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void ReloadVisualList()
        {
            try
            {
                PageList.DataSource = null;
                PageList.DataSource = CurrentBook.Pages;
                PageList.DisplayMember = "FilenameWithoutDirectory";
                PageList.Refresh();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void RemovePagesButton_Click(object sender, EventArgs e)
        {
            try
            {
                //TODO: Remove pages selected in the menu
                foreach (var DeletedPage in PageList.SelectedItems)
                {
                    CurrentBook.Pages.Remove((BookPage)DeletedPage);
                }
                ReloadVisualList();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void PageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Change the image to the right.
                BookPage SelectedPage = (BookPage)PageList.SelectedItem;
                String FilenameForImage = SelectedPage.Filename;
                if (File.Exists(FilenameForImage))
                {
                    Image ReplacementImage = Image.FromFile(FilenameForImage);
                    CurrentPagePicture.Image = ReplacementImage;
                }
                if (SelectedPage.Bookmark != null)
                {
                    BookmarkSelection.Checked = SelectedPage.Bookmark.Active;
                    BookmarkName.Text = SelectedPage.Bookmark.Text;
                }
                else
                {
                    BookmarkSelection.Checked = false;
                    BookmarkName.Text = String.Empty;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //throw;
            }
        }

        private void BookmarkSelection_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBookmark();
        }

        private void BookmarkName_TextChanged(object sender, EventArgs e)
        {
            UpdateBookmark();
        }

        private void UpdateBookmark()
        {
            if (((BookPage)PageList.SelectedItem).Bookmark == null)
            {
                ((BookPage)PageList.SelectedItem).Bookmark = new BookPageBookmark();
            }

            ((BookPage)PageList.SelectedItem).Bookmark.Active = BookmarkSelection.Checked;
            ((BookPage)PageList.SelectedItem).Bookmark.Text = BookmarkName.Text;
        }
    }
}
