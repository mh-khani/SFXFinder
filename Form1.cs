using NAudio.Wave;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace SFXFinder
{
    /// <summary>
    ///  improve speed of load
    /// </summary>
    /// next features:
    ///  - Like List
    ///  - Tags
    public partial class Form1 : Form
    {
        private string path = null;
        private IWavePlayer wavePlayer;
        private AudioFileReader audioFileReader;
        private List<string> Formats = new List<string>();

        private bool BNameChecked = true;

        private System.Windows.Forms.Timer searchTimer;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sfxlist.View = View.Details;
            sfxlist.Columns.Add("SFX Name", 350);
            sfxlist.Columns.Add("Play", 150);
            sfxlist.Columns.Add("Size", 120);
            sfxlist.FullRowSelect = true;

            for (int i = 0; i < filterList.Items.Count; i++)
            {
                filterList.SetItemChecked(i, true);
            }

            // Initialize Timer
            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 400;
            searchTimer.Tick += SearchTimer_Tick;
        }

        #region ---- Event ----
        private void browse_Click(object sender, EventArgs e) // browse button
        {
            if (MusicPath.ShowDialog() == DialogResult.OK)
            {
                searchBox.Text = string.Empty;
                path = MusicPath.SelectedPath;
                SFXListInitialize();
            }
        }
        private void searchBox_TextChanged(object sender, EventArgs e) // Search TextBox
        {
            searchTimer.Stop();
            searchTimer.Start();
        }
        private void Name_beautification_CheckedChanged(object sender, EventArgs e)
        {
            searchBox.Text = string.Empty;
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
        private async void SFXListInitialize()
        {
            string selectedPath = path;
            sfxlist.Items.Clear();
            progressBar1.Value = 0;
            progressBar1.Visible = true;

            try
            {
                var allFiles = Directory.GetFiles(selectedPath, "*.*", SearchOption.AllDirectories);
                int totalFiles = allFiles.Length;
                int processedFiles = 0;

                var progress = new Progress<int>(value =>
                {
                    progressBar1.Value = value;
                });

                var soundFiles = new ConcurrentBag<string>();

                await Task.Run(() =>
                {
                    Parallel.ForEach(allFiles, file =>
                    {
                        if (Formats.Any(format => file.EndsWith(format, StringComparison.OrdinalIgnoreCase)))
                        {
                            soundFiles.Add(file);
                        }

                        int percentage = Interlocked.Increment(ref processedFiles) * 100 / totalFiles;
                        ((IProgress<int>)progress).Report(percentage);
                    });
                });

                if (soundFiles.IsEmpty)
                {
                    MessageBox.Show("Couldn't Find anything 🙁");
                }
                else
                {
                    sfxlist.BeginUpdate(); // بهینه‌سازی نمایش لیست
                    foreach (string file in soundFiles)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(file);
                        if (BNameChecked)
                        {
                            fileName = FormatFileName(fileName);
                        }
                        fileName += Path.GetExtension(file);

                        var fileSize = GetFormattedFileSize(file);
                        var item = new ListViewItem(Path.GetFileName(fileName))
                        {
                            Tag = file
                        };
                        item.SubItems.Add("🔊 Click To Play");
                        item.SubItems.Add(fileSize);
                        sfxlist.Items.Add(item);
                    }
                    sfxlist.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred: {ex.Message}");
            }
            finally
            {
                progressBar1.Visible = false;
            }
        }

        private async Task Search(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.Trim().ToLower();
            string selectedFormat = searchBox.SelectedText?.ToString() ?? "All";

            sfxlist.Items.Clear();
            progressBar1.Value = 0;
            progressBar1.Visible = true;

            try
            {
                var allFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                int totalFiles = allFiles.Length;
                int processedFiles = 0;
                var foundFiles = new ConcurrentBag<string>();
                var progress = new Progress<int>(value => progressBar1.Value = value);

                await Task.Run(() =>
                {
                    Parallel.ForEach(allFiles, file =>
                    {
                        if (Formats.Any(format => file.EndsWith(format, StringComparison.OrdinalIgnoreCase)) &&
                            FileMatchesSearch(file, searchText))
                        {
                            foundFiles.Add(file);
                        }

                        int progressValue = Interlocked.Increment(ref processedFiles);
                        (progress as IProgress<int>)?.Report((int)((progressValue / (double)totalFiles) * 100));
                    });
                });

                if (foundFiles.IsEmpty)
                {
                    MessageBox.Show("No matching files found", "Error In Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                sfxlist.BeginUpdate();
                foreach (string file in foundFiles)
                {
                    var fileName = BNameChecked
                        ? FormatFileName(Path.GetFileNameWithoutExtension(file)) + Path.GetExtension(file)
                        : Path.GetFileNameWithoutExtension(file) + Path.GetExtension(file);

                    var fileSize = $"{new FileInfo(file).Length / 1024} KB";
                    var item = new ListViewItem(fileName) { Tag = file };
                    item.SubItems.Add("🔊 Click To Play");
                    item.SubItems.Add(fileSize);
                    sfxlist.Items.Add(item);
                }
                sfxlist.EndUpdate();
            }
            catch (Exception ex)
            {
                //  MessageBox.Show($"An error has occurred: {ex.Message}");
                MessageBox.Show("Press Open Folder button First ",
                    "No Directory Found",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            finally
            {
                progressBar1.Visible = false;
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
 
        // when Timer is Done Run Search Process
        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            Search(sender, e);
        }
    }
}