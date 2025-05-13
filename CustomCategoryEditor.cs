using System.Text.Json;

namespace FoxOrganizer
{
    public partial class CustomCategoryEditor : Form
    {
        private readonly string customCategoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FoxOrganizer", "custom-categories.json");
        private Dictionary<string, string> customCategories = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public CustomCategoryEditor()
        {
            InitializeComponent();
            // اگر در Designer تعریف نکردی، دستی ستون‌ها رو اضافه کن:
            dgvCategories.AllowUserToAddRows = true;
            dgvCategories.AllowUserToDeleteRows = true;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.Columns.Clear();

            dgvCategories.Columns.Add("Extension", "Extension (e.g. .psd)");
            dgvCategories.Columns.Add("Folder", "Destination Folder");

            LoadCustomCategories();
        }
        private void LoadCustomCategories()
        {
            if (File.Exists(customCategoryPath))
            {
                string json = File.ReadAllText(customCategoryPath);
                customCategories = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }
            else
            {
                customCategories = new Dictionary<string, string>();
            }

            dgvCategories.Rows.Clear();
            foreach (var pair in customCategories)
            {
                dgvCategories.Rows.Add(pair.Key, pair.Value);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            customCategories.Clear();
            foreach (DataGridViewRow row in dgvCategories.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    string ext = row.Cells[0].Value.ToString().Trim().ToLower();
                    string folder = row.Cells[1].Value.ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(ext) && !string.IsNullOrWhiteSpace(folder))
                    {
                        customCategories[ext] = folder;
                    }
                }
            }

            Directory.CreateDirectory(Path.GetDirectoryName(customCategoryPath));
            File.WriteAllText(customCategoryPath, JsonSerializer.Serialize(customCategories, new JsonSerializerOptions { WriteIndented = true }));
            MessageBox.Show("Changes saved successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvCategories.SelectedRows)
            {
                if (!row.IsNewRow)
                    dgvCategories.Rows.Remove(row);
            }
        }
    }
}
