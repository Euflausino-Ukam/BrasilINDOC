namespace Tools
{
    public class Base64Convert
    {
        public static string FileToBase64(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Arquivo não encontrado.", filePath);

            byte[] fileBytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(fileBytes);
        }
    }
}
