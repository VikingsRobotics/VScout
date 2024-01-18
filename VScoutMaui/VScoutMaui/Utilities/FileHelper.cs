using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;

namespace VScoutCentral.Utilities
{
    internal static class FileHelper
    {
        public static async Task<string> GetFileContentsFromUserSelectionAsync()
        {
            if (!(await PermissionHelper.CheckAndRequestPermissionAsync<Permissions.StorageRead>()))
            {
                throw new InvalidOperationException("User does not have permission to read from storage.");
            }

            FileResult result = await FilePicker.Default.PickAsync();
            using Stream stream = await result.OpenReadAsync();

            using TextReader reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        public static async Task SaveFileToUserSelectedFileAsync(string fileName, string fileContents)
        {
            if (!(await PermissionHelper.CheckAndRequestPermissionAsync<Permissions.StorageWrite>()))
            {
                throw new InvalidOperationException("User does not have permission to write to storage.");
            }

            try
            {
                using MemoryStream stream = new MemoryStream();
                using StreamWriter writer = new StreamWriter(stream);
                writer.Write(fileContents);
                writer.Flush();
                stream.Position = 0;
                string fileLocation = await FileSaver.Default.SaveAsync(fileName, stream, CancellationToken.None);

                await Toast.Make($"File is saved: {fileLocation}").Show();
            }
            catch
            {
                await Toast.Make("Done.").Show();
            }
        }
    }
}
