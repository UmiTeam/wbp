using System.Collections.Generic;

namespace Umi.Wbp.Dialogs;

public static class DialogResultExtensions
{
    public const string FilePathsKey = nameof(FilePathsKey);
    public const string FolderPathsKey = nameof(FolderPathsKey);
    public const string CanceledKey = nameof(CanceledKey);

    public static IEnumerable<string> GetSelectedFilePaths(this IDialogResult dialogResult)
    {
        IEnumerable<string> result = new List<string>();
        dialogResult.Parameters.TryGetValue(FilePathsKey, out result);
        return result;
    }

    public static string GetSelectedFilePath(this IDialogResult dialogResult)
    {
        if (dialogResult.Parameters.TryGetValue(FilePathsKey, out string[] result)) return result[0];
        return null;
    }

    public static IEnumerable<string> GetSelectedFolderPaths(this IDialogResult dialogResult)
    {
        IEnumerable<string> result = new List<string>();
        dialogResult.Parameters.TryGetValue(FolderPathsKey, out result);
        return result;
    }

    public static string GetSelectedFolderPath(this IDialogResult dialogResult)
    {
        if (dialogResult.Parameters.TryGetValue(FolderPathsKey, out string[] result)) return result[0];
        return null;
    }

    public static bool? GetCanceled(this IDialogResult dialogResult)
    {
        if (dialogResult.Parameters.TryGetValue(CanceledKey, out bool canceled)) return canceled;
        return null;
    }
}