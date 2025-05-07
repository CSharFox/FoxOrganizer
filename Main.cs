namespace FoxOrganizer
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            cmbSortBy.Items.AddRange(new[] { "File Type", "Date", "Name Prefix" });
            cmbSortBy.SelectedIndex = 0;
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
            int movedCount = 0;
            lstLog.Items.Clear();

            foreach (var file in files)
            {
                string subfolder = "";
                FileInfo fi = new FileInfo(file);

                switch (sortOption)
                {
                    case "File Type":
                        subfolder = fi.Extension.Trim('.').ToUpper();
                        break;
                    case "Date":
                        subfolder = fi.CreationTime.ToString("yyyy-MM-dd");
                        break;
                    case "Name Prefix":
                        subfolder = fi.Name.Length >= 1 ? fi.Name.Substring(0, 1).ToUpper() : "Other";
                        break;
                }

                string targetDir = Path.Combine(folderPath, subfolder);
                Directory.CreateDirectory(targetDir);

                string targetPath = Path.Combine(targetDir, fi.Name);

                try
                {
                    File.Move(file, targetPath);
                    lstLog.Items.Add($"Moved: {fi.Name} ➤ {subfolder}");
                    movedCount++;
                }
                catch (Exception ex)
                {
                    lstLog.Items.Add($"Failed: {fi.Name} ({ex.Message})");
                }
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = folderDialog.SelectedPath;
                }
            }
        }
    }
}
