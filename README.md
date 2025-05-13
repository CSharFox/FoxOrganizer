# 🦊 FoxTidy - Smart File Organizer for Windows

FoxTidy is a simple yet powerful desktop tool built with C# and Windows Forms that helps you automatically organize files in any folder based on type, date, name, or custom categories.

No more messy Downloads or Desktop folders — FoxTidy brings order with a single click.

---

## 🚀 Features

* ✅ Organize files by **File Type**, **Date**, **Name Prefix**, or **Smart Category**
* 🔁 **Undo** the last operation with one click
* 🛠 **Custom format mappings** — define your own rules like `.psd` → `Designs`, `.cs` → `Code`
* 🧠 Automatically skips empty files
* 📂 Maintains folder structure and creates destination folders if needed
* 📄 **Log view** to see all moved/restored files
* 💾 Settings and rules stored in local JSON files

---

## 🧰 Requirements

* .NET Framework 4.7+ or .NET 6.0+ (WinForms)
* Windows OS (tested on Windows 10/11)

---

## ⚙️ How to Use

1. Clone or download the repo
2. Open in Visual Studio
3. Build and run the project
4. Select a folder, choose a sort method, and click **Organize Files**
5. You can edit custom categories via the **Edit Categories** button
6. Use **Undo** to reverse the last move

---

## 📁 File Structure

```
FoxTidy/
├── Main.cs                   # Main form with sorting logic
├── CustomCategoryEditor.cs   # UI to edit user-defined mappings
├── last-action-log.json      # Stores last move for Undo
├── custom-categories.json    # Stores user-defined file type rules
└── ...
```

---

## 📜 License

MIT License. Free to use, modify and distribute.

---

## 🙌 Credits

Created with 💙 by \[Your Name].
Icon generated with AI and tweaked in Photoshop.

---

## 📬 Feedback

Pull requests and feature suggestions welcome!
