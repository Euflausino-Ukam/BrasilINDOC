namespace Integração_BrasilInDoc.BrasilInDoc.Domain.Entities.Attachments
{
    public class FilesArchives
    {
        public string Type { get; set; }
        public string UrlFile { get; set; }

        public FilesArchives(string type, string urlFile)
        {
            Type = type;
            UrlFile = urlFile;
        }
    }
}
