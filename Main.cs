using System.Text.Json;

namespace VRChat_Photo_Directory_Changer
{
    public partial class Main : Form
    {
        private readonly Dictionary<string, dynamic> _configData = new();

        public Main()
        {
            InitializeComponent();

            string username = Environment.UserName;

            string path = "C:\\Users\\" + username + "\\Pictures\\VRChat";
            if (Directory.Exists(path))
            {
                previousFolderLabel.Text = path;
            }

            string path2 = "C:\\Users\\" + username + "\\OneDrive\\Pictures\\VRChat";
            if (Directory.Exists(path2))
            {
                previousFolderLabel.Text = path2;
            }

            string configPath = "C:\\Users\\" + username + "\\AppData\\Locallow\\VRChat\\VRChat\\config.json";
            if (!File.Exists(configPath)) return;

            string json = File.ReadAllText(configPath);
            _configData = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(json) ??
                          new Dictionary<string, dynamic>();

            if (_configData.TryGetValue("picture_output_folder", out dynamic? value))
            {
                previousFolderLabel.Text = value.ToString();
            }
        }

        private async void ChangeButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(newFolderTextbox.Text))
            {
                _configData["picture_output_folder"] = newFolderTextbox.Text;
                string json = JsonSerializer.Serialize(_configData, new JsonSerializerOptions { WriteIndented = true });
                string username = Environment.UserName;
                string configPath = "C:\\Users\\" + username + "\\AppData\\Locallow\\VRChat\\VRChat\\config.json";
                await File.WriteAllTextAsync(configPath, json);

                MessageBox.Show("フォルダを変更しました", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                previousFolderLabel.Text = newFolderTextbox.Text;
                newFolderTextbox.Text = "";

                if (Directory.Exists(previousFolderLabel.Text))
                {
                    var result = MessageBox.Show("ファイルをコピーしますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result != DialogResult.Yes) return;
                    var progressForm = new ProgressForm();
                    progressForm.Show();

                    try
                    {
                        await progressForm.CopyFilesAsync(previousFolderLabel.Text, newFolderTextbox.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show("ファイルの移動が完了しました", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressForm.Close();
                }
                else
                {
                    MessageBox.Show("フォルダが存在しなかったため、ファイルの移動は無視されました", "エラー", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("フォルダが存在しません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openPreviousFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new()
            {
                UseDescriptionForTitle = true,
                Description = "変更前のフォルダを選択してください",
                ShowNewFolderButton = false
            };

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                previousFolderLabel.Text = fbd.SelectedPath;
            }
        }

        private void openNewFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new()
            {
                UseDescriptionForTitle = true,
                Description = "変更後のフォルダを選択してください",
                ShowNewFolderButton = false
            };

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                newFolderTextbox.Text = fbd.SelectedPath;
            }
        }

        public class ProgressForm : Form
        {
            public ProgressForm()
            {
                InitializeComponent();
            }

            private readonly ProgressBar _progressBar = new();
            private readonly TextBox _logTextBox = new();
            private readonly Button _cancelButton = new();

            private void InitializeComponent()
            {
                // ProgressBar
                _progressBar.Dock = DockStyle.Top;
                _progressBar.Minimum = 0;
                _progressBar.Maximum = 100;

                // Log TextBox
                _logTextBox.Dock = DockStyle.Fill;
                _logTextBox.Multiline = true;
                _logTextBox.ScrollBars = ScrollBars.Vertical;

                // Cancel Button
                _cancelButton.Dock = DockStyle.Bottom;
                _cancelButton.Text = "Cancel";
                _cancelButton.Click += (_, _) => Close();

                // Form
                Controls.Add(_logTextBox);
                Controls.Add(_progressBar);
                Controls.Add(_cancelButton);
                Text = "Copying Files";
                Size = new Size(600, 300);
            }

            public async Task CopyFilesAsync(string sourceFolder, string destinationFolder)
            {
                string[] files = Directory.GetFiles(sourceFolder, "*", SearchOption.AllDirectories);
                int totalFiles = files.Length;

                foreach (string file in files)
                {
                    string relativePath = Path.GetRelativePath(sourceFolder, file);
                    string destinationPath = Path.Combine(destinationFolder, relativePath);

                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath) ?? throw new InvalidOperationException());

                    await Task.Run(() => File.Copy(file, destinationPath, true));

                    var progress = (int)((Array.IndexOf(files, file) + 1) / (double)totalFiles * 100);

                    if (IsDisposed || !_progressBar.IsHandleCreated) return;

                    Invoke(() =>
                    {
                        _progressBar.Value = progress;
                        Text = $"ファイルのコピー中です... - {progress}%";
                        if (!_logTextBox.IsDisposed)
                        {
                            _logTextBox.AppendText(Environment.NewLine + "コピー: " + Path.GetFileName(file));
                        }
                    });
                }
            }
        }
    }
}
