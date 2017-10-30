namespace PDFBookBinder
{
    partial class BookBinderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BindBookButton = new System.Windows.Forms.Button();
            this.CurrentPagePicture = new System.Windows.Forms.PictureBox();
            this.PageList = new System.Windows.Forms.ListBox();
            this.AddPagesButton = new System.Windows.Forms.Button();
            this.SaveBookButton = new System.Windows.Forms.Button();
            this.LoadBookButton = new System.Windows.Forms.Button();
            this.RemovePagesButton = new System.Windows.Forms.Button();
            this.BookmarkSelection = new System.Windows.Forms.CheckBox();
            this.BookmarkName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPagePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // BindBookButton
            // 
            this.BindBookButton.Location = new System.Drawing.Point(12, 866);
            this.BindBookButton.Name = "BindBookButton";
            this.BindBookButton.Size = new System.Drawing.Size(93, 23);
            this.BindBookButton.TabIndex = 0;
            this.BindBookButton.Text = "Bind Book";
            this.BindBookButton.UseVisualStyleBackColor = true;
            this.BindBookButton.Click += new System.EventHandler(this.BindBookButton_Click);
            // 
            // CurrentPagePicture
            // 
            this.CurrentPagePicture.Location = new System.Drawing.Point(312, 12);
            this.CurrentPagePicture.Name = "CurrentPagePicture";
            this.CurrentPagePicture.Size = new System.Drawing.Size(1000, 1000);
            this.CurrentPagePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CurrentPagePicture.TabIndex = 1;
            this.CurrentPagePicture.TabStop = false;
            // 
            // PageList
            // 
            this.PageList.FormattingEnabled = true;
            this.PageList.Location = new System.Drawing.Point(13, 12);
            this.PageList.Name = "PageList";
            this.PageList.Size = new System.Drawing.Size(293, 732);
            this.PageList.TabIndex = 2;
            this.PageList.SelectedIndexChanged += new System.EventHandler(this.PageList_SelectedIndexChanged);
            // 
            // AddPagesButton
            // 
            this.AddPagesButton.Location = new System.Drawing.Point(12, 750);
            this.AddPagesButton.Name = "AddPagesButton";
            this.AddPagesButton.Size = new System.Drawing.Size(94, 23);
            this.AddPagesButton.TabIndex = 3;
            this.AddPagesButton.Text = "Add Pages...";
            this.AddPagesButton.UseVisualStyleBackColor = true;
            this.AddPagesButton.Click += new System.EventHandler(this.AddPagesButton_Click);
            // 
            // SaveBookButton
            // 
            this.SaveBookButton.Location = new System.Drawing.Point(13, 808);
            this.SaveBookButton.Name = "SaveBookButton";
            this.SaveBookButton.Size = new System.Drawing.Size(93, 23);
            this.SaveBookButton.TabIndex = 4;
            this.SaveBookButton.Text = "Save Book";
            this.SaveBookButton.UseVisualStyleBackColor = true;
            this.SaveBookButton.Click += new System.EventHandler(this.SaveBookButton_Click);
            // 
            // LoadBookButton
            // 
            this.LoadBookButton.Location = new System.Drawing.Point(12, 837);
            this.LoadBookButton.Name = "LoadBookButton";
            this.LoadBookButton.Size = new System.Drawing.Size(93, 23);
            this.LoadBookButton.TabIndex = 5;
            this.LoadBookButton.Text = "Load Book";
            this.LoadBookButton.UseVisualStyleBackColor = true;
            this.LoadBookButton.Click += new System.EventHandler(this.LoadBookButton_Click);
            // 
            // RemovePagesButton
            // 
            this.RemovePagesButton.Location = new System.Drawing.Point(13, 779);
            this.RemovePagesButton.Name = "RemovePagesButton";
            this.RemovePagesButton.Size = new System.Drawing.Size(93, 23);
            this.RemovePagesButton.TabIndex = 6;
            this.RemovePagesButton.Text = "Remove Pages";
            this.RemovePagesButton.UseVisualStyleBackColor = true;
            this.RemovePagesButton.Click += new System.EventHandler(this.RemovePagesButton_Click);
            // 
            // BookmarkSelection
            // 
            this.BookmarkSelection.AutoSize = true;
            this.BookmarkSelection.Location = new System.Drawing.Point(13, 896);
            this.BookmarkSelection.Name = "BookmarkSelection";
            this.BookmarkSelection.Size = new System.Drawing.Size(74, 17);
            this.BookmarkSelection.TabIndex = 7;
            this.BookmarkSelection.Text = "Bookmark";
            this.BookmarkSelection.UseVisualStyleBackColor = true;
            this.BookmarkSelection.CheckedChanged += new System.EventHandler(this.BookmarkSelection_CheckedChanged);
            // 
            // BookmarkName
            // 
            this.BookmarkName.Location = new System.Drawing.Point(13, 920);
            this.BookmarkName.Name = "BookmarkName";
            this.BookmarkName.Size = new System.Drawing.Size(293, 20);
            this.BookmarkName.TabIndex = 8;
            this.BookmarkName.TextChanged += new System.EventHandler(this.BookmarkName_TextChanged);
            // 
            // BookBinderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1411, 1022);
            this.Controls.Add(this.BookmarkName);
            this.Controls.Add(this.BookmarkSelection);
            this.Controls.Add(this.RemovePagesButton);
            this.Controls.Add(this.LoadBookButton);
            this.Controls.Add(this.SaveBookButton);
            this.Controls.Add(this.AddPagesButton);
            this.Controls.Add(this.PageList);
            this.Controls.Add(this.BindBookButton);
            this.Controls.Add(this.CurrentPagePicture);
            this.Name = "BookBinderForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPagePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BindBookButton;
        private System.Windows.Forms.PictureBox CurrentPagePicture;
        private System.Windows.Forms.ListBox PageList;
        private System.Windows.Forms.Button AddPagesButton;
        private System.Windows.Forms.Button SaveBookButton;
        private System.Windows.Forms.Button LoadBookButton;
        private System.Windows.Forms.Button RemovePagesButton;
        private System.Windows.Forms.CheckBox BookmarkSelection;
        private System.Windows.Forms.TextBox BookmarkName;
    }
}

