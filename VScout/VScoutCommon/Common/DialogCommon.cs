using Microsoft.Win32;
using System.Windows.Forms;

namespace VScoutCommon.Common
{
    public static class DialogCommon
    {
        public static string GetSaveFilePathFromUser(FileType fileType)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            SetFileDialogFilter(fileType, saveFileDialog);

            bool? result = saveFileDialog.ShowDialog();
            if (result != true) return null;

            return saveFileDialog.FileName;
        }

        /// <summary>
        /// Gets a path to a file to open.
        /// </summary>
        public static string GetFilePathFromUser(FileType fileType)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            SetFileDialogFilter(fileType, openFileDialog);

            bool? result = openFileDialog.ShowDialog();
            if (result != true) return null;

            return openFileDialog.FileName;
        }

        private static void SetFileDialogFilter(FileType fileType, Microsoft.Win32.FileDialog openFileDialog)
        {
            switch (fileType)
            {
                case FileType.Json:
                    openFileDialog.Filter = "JSON Files (.json)|*.json";
                    break;
                case FileType.Jpg:
                    openFileDialog.Filter = "JPG Files (.jpg)|*.jpg";
                    break;
                case FileType.Csv:
                    openFileDialog.Filter = "CSV Files (.csv)|*.csv";
                    break;
            }
        }

        public static string GetFolderPathFromUser()
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();

            DialogResult result = openFolderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                return openFolderDialog.SelectedPath;
            }
            else
            {
                return null;
            }
        }
    }
}
