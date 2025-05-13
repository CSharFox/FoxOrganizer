using System.Text.Json;

namespace FoxOrganizer
{
    public partial class Main : Form
    {
        private readonly Dictionary<string, string> categoryMap = new(StringComparer.OrdinalIgnoreCase)
        {
            { ".jpg", "Images" }, { ".jpeg", "Images" }, { ".png", "Images" }, { ".gif", "Images" }, { ".bmp", "Images" }, { ".webp", "Images" },
            { ".doc", "Docs" }, { ".docx", "Docs" }, { ".pdf", "Docs" }, { ".txt", "Docs" }, { ".xls", "Docs" }, { ".xlsx", "Docs" }, { ".ppt", "Docs" }, { ".pptx", "Docs" },
            { ".mp4", "Videos" }, { ".avi", "Videos" }, { ".mov", "Videos" }, { ".mkv", "Videos" }, { ".webm", "Videos" },
            { ".mp3", "Music" }, { ".wav", "Music" }, { ".ogg", "Music" }, { ".flac", "Music" },
            { ".exe", "Executables" }, { ".msi", "Executables" }, { ".bat", "Executables" }, { ".sh", "Executables" },
            { ".zip", "Archives" }, { ".rar", "Archives" }, { ".7z", "Archives" }, { ".tar", "Archives" }, { ".gz", "Archives" }
        };
        private Dictionary<string, string> customCategoryMap = new(StringComparer.OrdinalIgnoreCase);

        private readonly string undoLogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FoxOrganizer", "last-action-log.json");
        private readonly string customCategoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FoxOrganizer", "custom-categories.json");
        public Main()
        {
            InitializeComponent();
            cmbSortBy.Items.AddRange(["File Type", "Category", "Date", "Name Prefix"]);
            cmbSortBy.SelectedIndex = 0;
            LoadCustomCategories();
        }
        private void LoadCustomCategories()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(customCategoryPath));

            if (!File.Exists(customCategoryPath))
            {
                var example = new Dictionary<string, string>
                {
                    { ".psd", "Designs" },
                    { ".cs", "Code" }
                };
                File.WriteAllText(customCategoryPath, JsonSerializer.Serialize(example, new JsonSerializerOptions { WriteIndented = true }));
                customCategoryMap = example;
            }
            else
            {
                string json = File.ReadAllText(customCategoryPath);
                customCategoryMap = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            }
        }
        private void btnOrganize_Click(object sender, EventArgs e)
        {
            string folderPath = txtFolderPath.Text;
            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
            {
                MessageBox.Show("Please select a valid folder.");
                return;
            }

            string sortOption = cmbSortBy.SelectedItem.ToString();
            var files = Directory.GetFiles(folderPath);
            var previewList = new List<string>();
            var undoList = new List<FileMoveRecord>();

            foreach (var file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.Length == 0) continue;

                string subfolder = GetSubfolderName(fi, sortOption);
                previewList.Add($"{fi.Name} ➤ {subfolder}");
            }

            if (previewList.Count == 0)
            {
                MessageBox.Show("No files to organize (or all are empty).", "Info");
                return;
            }

            DialogResult result = MessageBox.Show($"{previewList.Count} files will be moved. Proceed?", "Preview", MessageBoxButtons.OKCancel);
            if (result != DialogResult.OK) return;

            int movedCount = 0;
            lstLog.Items.Clear();

            foreach (var file in files)
            {
                FileInfo fi = new(file);
                if (fi.Length == 0) continue;

                string subfolder = GetSubfolderName(fi, sortOption);
                string targetDir = Path.Combine(folderPath, subfolder);
                Directory.CreateDirectory(targetDir);

                string targetPath = Path.Combine(targetDir, fi.Name);

                try
                {
                    File.Move(file, targetPath);
                    undoList.Add(new FileMoveRecord { Source = file, Destination = targetPath });
                    lstLog.Items.Add($"Moved: {fi.Name} ➤ {subfolder}");
                    movedCount++;
                }
                catch (Exception ex)
                {
                    lstLog.Items.Add($"Failed: {fi.Name} ({ex.Message})");
                }
            }

            Directory.CreateDirectory(Path.GetDirectoryName(undoLogPath));
            File.WriteAllText(undoLogPath, JsonSerializer.Serialize(undoList, new JsonSerializerOptions { WriteIndented = true }));

            MessageBox.Show($"Done! Moved {movedCount} files.", "Complete");
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = folderDialog.SelectedPath;
                }
            }
        }
        private string GetSubfolderName(FileInfo fi, string sortOption)
        {
            string ext = fi.Extension?.Trim().ToLower();

            switch (sortOption)
            {
                case "File Type":
                    return fi.Extension.Trim('.').ToUpper();
                case "Date":
                    return fi.CreationTime.ToString("yyyy-MM-dd");
                case "Name Prefix":
                    return fi.Name.Length >= 1 ? fi.Name.Substring(0, 1).ToUpper() : "Other";
                case "Category":
                    if (customCategoryMap.TryGetValue(ext, out var custom)) return custom;
                    if (categoryMap.TryGetValue(ext, out var builtIn)) return builtIn;
                    return "Other";
                default:
                    return "Other";
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (!File.Exists(undoLogPath))
            {
                MessageBox.Show("No undo log found.", "Info");
                return;
            }

            var undoList = JsonSerializer.Deserialize<List<FileMoveRecord>>(File.ReadAllText(undoLogPath));
            int restored = 0;
            lstLog.Items.Clear();

            foreach (var item in undoList)
            {
                if (File.Exists(item.Destination))
                {
                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(item.Source));
                        File.Move(item.Destination, item.Source);
                        lstLog.Items.Add($"Restored: {Path.GetFileName(item.Source)}");
                        restored++;
                    }
                    catch (Exception ex)
                    {
                        lstLog.Items.Add($"Failed to restore: {Path.GetFileName(item.Source)} ({ex.Message})");
                    }
                }
            }

            File.Delete(undoLogPath);
            MessageBox.Show($"Undo complete! Restored {restored} files.", "Undo");
        }

        public class FileMoveRecord
        {
            public string Source { get; set; }
            public string Destination { get; set; }
        }

        private void btnEditCategories_Click(object sender, EventArgs e)
        {
            var editor = new CustomCategoryEditor();
            editor.ShowDialog();
            LoadCustomCategories(); // بعد از بستن فرم، دسته‌بندی‌ها رو دوباره بارگذاری کن
        }
    }
}
