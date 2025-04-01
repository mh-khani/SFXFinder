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
            ListViewItem listViewItem1 = new ListViewItem("");
            ListViewItem listViewItem2 = new ListViewItem("");
            ListViewItem listViewItem3 = new ListViewItem("");
            ListViewItem listViewItem4 = new ListViewItem("");
            ListViewItem listViewItem5 = new ListViewItem("");
            ListViewItem listViewItem6 = new ListViewItem("");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            searchBox = new TextBox();
            label1 = new Label();
            sfxlist = new ListView();
            browse = new Button();
            MusicPath = new FolderBrowserDialog();
            stop = new Button();
            play = new Button();
            filterList = new CheckedListBox();
            Name_beautification = new CheckBox();
            label3 = new Label();
            progressBar1 = new ProgressBar();
            SuspendLayout();
            // 
            // searchBox
            // 
            searchBox.Location = new Point(68, 17);
            searchBox.MinimumSize = new Size(208, 23);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(265, 23);
            searchBox.TabIndex = 0;
            searchBox.TextChanged += searchBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(52, 19);
            label1.TabIndex = 1;
            label1.Text = "Search:";
            // 
            // sfxlist
            // 
            sfxlist.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            sfxlist.BorderStyle = BorderStyle.None;
            sfxlist.ForeColor = SystemColors.Desktop;
            sfxlist.FullRowSelect = true;
            sfxlist.GridLines = true;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.StateImageIndex = 0;
            listViewItem6.StateImageIndex = 0;
            sfxlist.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2, listViewItem3, listViewItem4, listViewItem5, listViewItem6 });
            sfxlist.Location = new Point(0, 103);
            sfxlist.Name = "sfxlist";
            sfxlist.Size = new Size(622, 438);
            sfxlist.TabIndex = 3;
            sfxlist.UseCompatibleStateImageBehavior = false;
            sfxlist.View = View.Details;
            sfxlist.MouseClick += sfxlist_MouseClick;
            sfxlist.MouseDoubleClick += sfxlist_MouseDoubleClick;
            // 
            // browse
            // 
            browse.BackColor = Color.FloralWhite;
            browse.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            browse.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            browse.ForeColor = Color.FromArgb(0, 0, 64);
            browse.Location = new Point(355, 12);
            browse.Name = "browse";
            browse.Size = new Size(167, 31);
            browse.TabIndex = 4;
            browse.Text = "Open folder";
            browse.UseVisualStyleBackColor = false;
            browse.Click += browse_Click;
            // 
            // MusicPath
            // 
            MusicPath.Description = "Open Your Music Library Path";
            // 
            // stop
            // 
            stop.BackColor = SystemColors.ButtonHighlight;
            stop.ForeColor = Color.Maroon;
            stop.Location = new Point(68, 58);
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
            play.Location = new Point(12, 58);
            play.Name = "play";
            play.Size = new Size(50, 23);
            play.TabIndex = 10;
            play.Text = "Play";
            play.UseVisualStyleBackColor = true;
            play.Click += play_Click;
            // 
            // filterList
            // 
            filterList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            filterList.BackColor = SystemColors.Control;
            filterList.BorderStyle = BorderStyle.None;
            filterList.FormattingEnabled = true;
            filterList.Items.AddRange(new object[] { "mp3", "wav", "ogg" });
            filterList.Location = new Point(542, 27);
            filterList.Name = "filterList";
            filterList.Size = new Size(58, 54);
            filterList.TabIndex = 15;
            filterList.ItemCheck += filterList_ItemCheck;
            // 
            // Name_beautification
            // 
            Name_beautification.AutoSize = true;
            Name_beautification.Checked = true;
            Name_beautification.CheckState = CheckState.Checked;
            Name_beautification.Location = new Point(146, 62);
            Name_beautification.Name = "Name_beautification";
            Name_beautification.Size = new Size(137, 19);
            Name_beautification.TabIndex = 17;
            Name_beautification.Text = "Name beautification ";
            Name_beautification.UseVisualStyleBackColor = true;
            Name_beautification.CheckedChanged += Name_beautification_CheckedChanged;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(542, 9);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 18;
            label3.Text = "Filter";
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.ForeColor = SystemColors.ControlDarkDark;
            progressBar1.Location = new Point(0, 100);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(622, 5);
            progressBar1.Step = 20;
            progressBar1.TabIndex = 20;
            progressBar1.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 541);
            Controls.Add(progressBar1);
            Controls.Add(label3);
            Controls.Add(Name_beautification);
            Controls.Add(filterList);
            Controls.Add(play);
            Controls.Add(stop);
            Controls.Add(browse);
            Controls.Add(sfxlist);
            Controls.Add(label1);
            Controls.Add(searchBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(630, 580);
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
        private FolderBrowserDialog MusicPath;
        private Button stop;
        private Button play;
        private CheckedListBox filterList;
        private CheckBox Name_beautification;
        private Label label3;
        private ProgressBar progressBar1;
    }
}