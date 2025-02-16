namespace SFXFinder
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ListViewItem listViewItem1 = new ListViewItem("");
            ListViewItem listViewItem2 = new ListViewItem("");
            ListViewItem listViewItem3 = new ListViewItem("");
            ListViewItem listViewItem4 = new ListViewItem("");
            ListViewItem listViewItem5 = new ListViewItem("");
            ListViewItem listViewItem6 = new ListViewItem("");
            searchBox = new TextBox();
            label1 = new Label();
            sfxlist = new ListView();
            browse = new Button();
            FileBrowse = new TextBox();
            MusicPath = new FolderBrowserDialog();
            imageList1 = new ImageList(components);
            stop = new Button();
            play = new Button();
            filterList = new CheckedListBox();
            Name_beautification = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // searchBox
            // 
            searchBox.Location = new Point(63, 46);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(198, 23);
            searchBox.TabIndex = 0;
            searchBox.TextChanged += searchBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 49);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 1;
            label1.Text = "Search:";
            // 
            // sfxlist
            // 
            sfxlist.FullRowSelect = true;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.StateImageIndex = 0;
            listViewItem6.StateImageIndex = 0;
            sfxlist.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2, listViewItem3, listViewItem4, listViewItem5, listViewItem6 });
            sfxlist.Location = new Point(12, 114);
            sfxlist.Name = "sfxlist";
            sfxlist.Size = new Size(587, 405);
            sfxlist.TabIndex = 3;
            sfxlist.UseCompatibleStateImageBehavior = false;
            sfxlist.View = View.Details;
            sfxlist.MouseClick += sfxlist_MouseClick;
            // 
            // browse
            // 
            browse.BackColor = SystemColors.Control;
            browse.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            browse.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            browse.ForeColor = Color.FromArgb(0, 0, 64);
            browse.Location = new Point(306, 41);
            browse.Name = "browse";
            browse.Size = new Size(154, 31);
            browse.TabIndex = 4;
            browse.Text = "Open folder";
            browse.UseVisualStyleBackColor = false;
            browse.Click += browse_Click;
            // 
            // FileBrowse
            // 
            FileBrowse.BackColor = SystemColors.ButtonFace;
            FileBrowse.Location = new Point(52, 12);
            FileBrowse.Name = "FileBrowse";
            FileBrowse.Size = new Size(445, 23);
            FileBrowse.TabIndex = 5;
            FileBrowse.Text = "C:\\";
            FileBrowse.TextChanged += FileBrowse_TextChanged;
            // 
            // MusicPath
            // 
            MusicPath.Description = "Open Your Music Library Path";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // stop
            // 
            stop.BackColor = SystemColors.ButtonHighlight;
            stop.ForeColor = Color.Maroon;
            stop.Location = new Point(68, 85);
            stop.Name = "stop";
            stop.Size = new Size(63, 23);
            stop.TabIndex = 9;
            stop.Text = "Stop SFX";
            stop.UseVisualStyleBackColor = false;
            stop.Click += stop_Click;
            // 
            // play
            // 
            play.ForeColor = Color.Green;
            play.Location = new Point(12, 85);
            play.Name = "play";
            play.Size = new Size(50, 23);
            play.TabIndex = 10;
            play.Text = "Play";
            play.UseVisualStyleBackColor = true;
            play.Click += play_Click;
            // 
            // filterList
            // 
            filterList.BackColor = SystemColors.Control;
            filterList.BorderStyle = BorderStyle.None;
            filterList.FormattingEnabled = true;
            filterList.Items.AddRange(new object[] { "mp3", "wav", "ogg" });
            filterList.Location = new Point(519, 27);
            filterList.Name = "filterList";
            filterList.Size = new Size(58, 54);
            filterList.TabIndex = 15;
            filterList.SelectedIndexChanged += filterList_SelectedIndexChanged;
            // 
            // Name_beautification
            // 
            Name_beautification.AutoSize = true;
            Name_beautification.Checked = true;
            Name_beautification.CheckState = CheckState.Checked;
            Name_beautification.Location = new Point(148, 89);
            Name_beautification.Name = "Name_beautification";
            Name_beautification.Size = new Size(137, 19);
            Name_beautification.TabIndex = 17;
            Name_beautification.Text = "Name beautification ";
            Name_beautification.UseVisualStyleBackColor = true;
            Name_beautification.CheckedChanged += Name_beautification_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 15);
            label2.Name = "label2";
            label2.Size = new Size(34, 15);
            label2.TabIndex = 1;
            label2.Text = "Path:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(519, 9);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 18;
            label3.Text = "Filter";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(611, 531);
            Controls.Add(label3);
            Controls.Add(Name_beautification);
            Controls.Add(filterList);
            Controls.Add(play);
            Controls.Add(stop);
            Controls.Add(FileBrowse);
            Controls.Add(browse);
            Controls.Add(sfxlist);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(searchBox);
            Name = "Form1";
            Text = "SFX Finder";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox searchBox;
        private Label label1;
        private ListView sfxlist;
        private Button browse;
        private TextBox FileBrowse;
        private FolderBrowserDialog MusicPath;
        private ImageList imageList1;
        private Button stop;
        private Button play;
        private CheckedListBox filterList;
        private CheckBox Name_beautification;
        private Label label2;
        private Label label3;
    }
}