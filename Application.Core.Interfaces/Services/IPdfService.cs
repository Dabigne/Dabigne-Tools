namespace Application.Core.Interfaces.Services;

public interface IPdfService
{
    int GetPageNumberFromFiles(IList<string> pdfPaths);

    bool CreatePdfFromImagesInFolder(string folderName, string pdfName, int? maxPageHeight = null);

    bool MergePdfsIntoOne(List<string> pdfPaths, string pdfName);
}