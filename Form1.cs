using NAudio.Wave;
namespace SFXFinder
{
    /// <summary>
    ///  1-درست کردن راست کلیک باز شدن دایرگتوری
    ///  فیلتر فرمت
    ///  بهینه سازی رابط کابری
    ///  make it english all th app
    ///  خروجی exe
    ///  
    /// </summary>
    public partial class Form1 : Form
    {
        public static string path = "I:\\Soft Saaz\\8_more\\Sound Effect\\#game\\sounds\\SuperMario";
        private IWavePlayer wavePlayer;
        private AudioFileReader audioFileReader;
        private List<string> Formats = new List<string>();

        public Form1()
        {
            InitializeComponent();
            Formats.Add("mp3");
            Formats.Add("wav");
            Formats.Add("obb");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sfxlist.View = View.Details;
            sfxlist.Columns.Add("SFX Name", 350);
            sfxlist.Columns.Add("Play", 150);
            sfxlist.Columns.Add("Size", 100);
            sfxlist.FullRowSelect = true;
        }

        #region ---- Event ----
        private void browse_Click(object sender, EventArgs e) // browse button
        {
            if (MusicPath.ShowDialog() == DialogResult.OK)
            {
                FileBrowse.Text = MusicPath.SelectedPath.ToString();
                path = MusicPath.SelectedPath;
                SFXListInitialize();
                FileBrowse.Text = path.ToString();
            }
        }
        private void searchBox_TextChanged(object sender, EventArgs e) // Search TextBox
        {
            Search(sender, e);
        }

        private void FileBrowse_TextChanged(object sender, EventArgs e) // Path TexBox
        {

        }
        private void Name_beautification_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void filterList_SelectedIndexChanged(object sender, EventArgs e) // change filter option
        {

        }
        #endregion
        #region ---- Sound ----
        private void PlaySound(string filePath)
        {
            try
            {
                // اگر صدای قبلی در حال پخش است آن را متوقف کن
                wavePlayer?.Stop();
                wavePlayer?.Dispose();
                audioFileReader?.Dispose();

                wavePlayer = new WaveOutEvent();
                audioFileReader = new AudioFileReader(filePath);
                wavePlayer.Init(audioFileReader);
                wavePlayer.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در پخش صدا: {ex.Message}");
            }
        } //main Function
        private void stop_Click(object sender, EventArgs e)
        {
            wavePlayer?.Stop();
        } //bt
        private void play_Click(object sender, EventArgs e)
        {
            try
            {
                PlaySound(sfxlist.SelectedItems[0].Tag.ToString());
            }
            catch
            {
                Console.WriteLine("Nothing");
            }
        } //bt
        private void sfxlist_MouseClick(object sender, MouseEventArgs e) // play sound effect when user clicked
        {
            if (sfxlist.SelectedItems.Count > 0)
            {
                var subItem = sfxlist.HitTest(e.Location).SubItem;
                if (subItem != null)
                {
                    string filePath = sfxlist.SelectedItems[0].Tag.ToString();

                    PlaySound(filePath);
                }
            }
        }

        #endregion
        private void SFXListInitialize()
        {
            string selectedPath = path;
            sfxlist.Items.Clear();

            // جستجوی فایل‌های صوتی
            string[] soundFiles = Directory.GetFiles(selectedPath, "*.*", SearchOption.AllDirectories)
                                            .Where(f => f.EndsWith(".wav") || f.EndsWith(".mp3"))
                                            .ToArray();

            if (soundFiles.Length == 0)
            {
                MessageBox.Show("هیچ ساند افکتی پیدا نشد.");
            }
            else
            {
                foreach (string file in soundFiles)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file);
                    fileName = FormatFileName(fileName); // پردازش نام فایل
                    fileName += Path.GetExtension(file); // الحاق پسوند فایل
                    var fileSize = GetFormattedFileSize(file);
                    var item = new ListViewItem(Path.GetFileName(fileName));
                    item.Tag = file;
                    item.SubItems.Add("🔊 Click To Play");
                    item.SubItems.Add(fileSize);
                    sfxlist.Items.Add(item);
                }
            }
        }
        private void Search(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.Trim().ToLower();
            string selectedFormat = searchBox.SelectedText?.ToString() ?? "All";

            sfxlist.Items.Clear();

            try
            {
                var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                                      .Where(file => (selectedFormat == "All" || file.ToLower().EndsWith(selectedFormat)) &&
                                                     FileMatchesSearch(file, searchText)).ToList();

                if (files.Count == 0)
                {
                    MessageBox.Show("No matching files found", "Error In Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                foreach (string file in files)
                {
                    var fileName = FormatFileName(Path.GetFileNameWithoutExtension(file)) + Path.GetExtension(file);
                    var fileSize = $"{new FileInfo(file).Length / 1024} KB";
                    var item = new ListViewItem(fileName);
                    item.Tag = file;
                    item.SubItems.Add("🔊 Click To Play");
                    item.SubItems.Add(fileSize);
                    sfxlist.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred: {ex.Message}");
            }
        }

        private string FormatFileName(string fileName)
        {
            fileName = fileName.Replace("_", " ");

            return string.Join(" ", fileName
                .Split(' ')
                .Where(word => !string.IsNullOrWhiteSpace(word))
                .Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
        }
        private string GetFormattedFileSize(string filePath)
        {
            long sizeInBytes = new FileInfo(filePath).Length;

            if (sizeInBytes >= 1024 * 1024)
                return $"{(sizeInBytes / (1024.0 * 1024.0)):0.00} MB";
            else if (sizeInBytes >= 1024)
                return $"{sizeInBytes / 1024} KB"; // حذف اعشار
            else
                return $"{sizeInBytes} Bytes";
        } // Beautifulize SFX Names
        private bool FileMatchesSearch(string filePath, string searchText)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath).ToLower();
            return string.IsNullOrEmpty(searchText) || fileName.Contains(searchText);
        }


    }
}