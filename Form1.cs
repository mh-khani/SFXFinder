using NAudio.Wave;
using System.Diagnostics;
using System.Windows.Forms;

namespace SFXFinder
{
    /// <summary>
    ///  
    ///  بهینه سازی رابط کابری
    ///  make it english all th app
    ///  خروجی exe
    ///  
    /// </summary>
    public partial class Form1 : Form
    {
        private string path = null;
        private IWavePlayer wavePlayer;
        private AudioFileReader audioFileReader;
        private List<string> Formats = new List<string>();

        private bool BNameChecked = true;

        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sfxlist.View = View.Details;
            sfxlist.Columns.Add("SFX Name", 350);
            sfxlist.Columns.Add("Play", 150);
            sfxlist.Columns.Add("Size", 100);
            sfxlist.FullRowSelect = true;

            for (int i = 0; i < filterList.Items.Count; i++)
            {
                filterList.SetItemChecked(i, true);
            }
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
        private void Name_beautification_CheckedChanged(object sender, EventArgs e)
        {
            BNameChecked = !BNameChecked;
            if (path != null)
                SFXListInitialize();
        }
        private void sfxlist_MouseClick(object sender, MouseEventArgs e) // play sound effect when user clicked --- List View
        {
            if (sfxlist.SelectedItems.Count > 0 && path != null)
            {
                var subItem = sfxlist.HitTest(e.Location).SubItem;
                if (subItem != null)
                {
                    string filePath = sfxlist.SelectedItems[0].Tag.ToString();

                    PlaySound(filePath);
                }
            }
        }
        private void sfxlist_MouseDoubleClick(object sender, MouseEventArgs e) // List View - open Diecrtoey
        {
            if (sfxlist.SelectedItems.Count > 0 && path != null)
            {
                string filePath = sfxlist.SelectedItems[0].Tag.ToString();

                if (File.Exists(filePath))
                {
                    string folderPath = Path.GetDirectoryName(filePath);

                    try
                    {
                        Process.Start("explorer.exe", $"/select, \"{filePath}\"");
                    }
                    catch
                    {
                        MessageBox.Show("Error in opening File Explorer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("choosen File doesn't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Select Somthing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void filterList_ItemCheck(object sender, ItemCheckEventArgs e) // change Filter List
        {
            string selectedItem = filterList.Items[e.Index].ToString();

            if (e.NewValue == CheckState.Checked)
            {
                Formats.Add(selectedItem);
            }
            else
            {
                Formats.Remove(selectedItem);
            }
        }
        #endregion
        #region ---- Sound ----
        private void PlaySound(string filePath)
        {
            try
            {
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
                MessageBox.Show($"Error in playing Sound: {ex.Message}");
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
        #endregion
        private void SFXListInitialize()
        {
            string selectedPath = path;
            sfxlist.Items.Clear();

            // search sfx
            string[] soundFiles = Directory.GetFiles(selectedPath, "*.*", SearchOption.AllDirectories)
                                            .Where(file => Formats.Any(format => file.EndsWith(format, StringComparison.OrdinalIgnoreCase)))
                                            .ToArray();

            if (soundFiles.Length == 0)
            {
                MessageBox.Show("Couldn't Find anythink🙁");
            }
            else
            {
                foreach (string file in soundFiles)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file);
                    if (BNameChecked)
                    {
                        fileName = FormatFileName(fileName); // file name

                    }
                    fileName += Path.GetExtension(file);

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
                // search code ---
                var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                                       .Where(file => Formats.Any(format => file.EndsWith(format, StringComparison.OrdinalIgnoreCase))
                                       &&
                                        FileMatchesSearch(file, searchText))
                                       .ToList();

                if (files.Count == 0)
                {
                    MessageBox.Show("No matching files found", "Error In Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                foreach (string file in files)
                {
                    var fileName = "";
                    if (BNameChecked)
                    {
                        fileName = FormatFileName(Path.GetFileNameWithoutExtension(file)) + Path.GetExtension(file);
                    }
                    else
                    {
                        fileName = Path.GetFileNameWithoutExtension(file) + Path.GetExtension(file);
                    }
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
                return $"{sizeInBytes / 1024} KB";
            else
                return $"{sizeInBytes} Bytes";
        }
        private bool FileMatchesSearch(string filePath, string searchText)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath).ToLower();
            return string.IsNullOrEmpty(searchText) || fileName.Contains(searchText);
        }
    }
}