using System.Text.Json;

namespace VRChat_Photo_Directory_Changer;

internal partial class Main : Form
{
    private readonly Dictionary<string, dynamic> _configData = [];
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };
    private static readonly string Username = Environment.UserName;
    private static readonly string ConfigPath = "C:\\Users\\" + Username + "\\AppData\\Locallow\\VRChat\\VRChat\\config.json";
    private static readonly string[] CheckPathList =
    [
        "C:\\Users\\" + Username + "\\Pictures\\VRChat",
        "C:\\Users\\" + Username + "\\OneDrive\\Pictures\\VRChat"
    ];

    internal Main()
    {
        InitializeComponent();

        foreach (string checkPath in CheckPathList)
        {
            if (!Directory.Exists(checkPath)) continue;
            previousFolderLabel.Text = checkPath;
        }
        
        if (File.Exists(ConfigPath))
        {
            string json = File.ReadAllText(ConfigPath);
            var configData = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(json);
            if (configData == null)
            {
                MessageBox.Show("Configファイルの読み込みに失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (configData.TryGetValue("picture_output_folder", out dynamic? value))
            {
                previousFolderLabel.Text = value.ToString();
            }

            _configData = configData;
        }
    }

    private void ChangeButton_Click(object sender, EventArgs e)
    {
        if (Directory.Exists(newFolderTextbox.Text))
        {
            _configData["picture_output_folder"] = newFolderTextbox.Text;
            string json = JsonSerializer.Serialize(_configData, _jsonSerializerOptions);
            File.WriteAllText(ConfigPath, json);

            MessageBox.Show("写真の保存先フォルダを変更しました！\n写真を元のフォルダから移動することで完全な移行が可能です！", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show("新しい保存先フォルダが存在しなかったため変更できませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
