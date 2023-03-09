namespace Arts.Services
{
    public class CopyThumbail
    {
        /*
         *  Ceci est une méthode qui vas permettre de copier un fichier 
         *  dans sa source le dossier ImageProjet vers une destination appelle UploadImage
         */
        public void CopyThumbnail(string filename, string sourcePath, string targetPath)
        {
            try
            {
                // Represente l' emplacemant du fichier source
                string filenameSource = Path.Combine(sourcePath, filename);
                // Rrepresente l' emplacement du fichier de destination
                string filenameTarget = Path.Combine(targetPath, filename);

                // Copie le fichier source pour la destination 
                File.Copy(filenameSource, filenameTarget, true);
                Console.WriteLine("Copie de l' image reussie");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
