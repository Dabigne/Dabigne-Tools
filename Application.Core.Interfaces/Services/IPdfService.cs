namespace Application.Core.Interfaces.Services;

public interface IPdfService
{
    bool CreatePdfFromImagesInFolder(string folderName, string pdfName);

    bool MergePdfsIntoOne(List<string> pdfPaths, string pdfName);
}