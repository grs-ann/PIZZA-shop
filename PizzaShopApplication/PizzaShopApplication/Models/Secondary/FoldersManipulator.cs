using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Secondary
{
    /// <summary>
    /// This class helps to work with project folders and files.
    /// </summary>
    public static class FoldersManipulator
    {
        /// <summary>
        /// Checks if the file exists in the directory, 
        /// and if so, adds a unique postfix to the name. 
        /// </summary>
        /// <param name="filePath">File save path</param>
        /// <returns>Unique file save path</returns>
        public static string GetUniqueFilePath(string filePath)
        {
            if (File.Exists(filePath))
            {
                string folder = Path.GetDirectoryName(filePath);
                string filename = Path.GetFileNameWithoutExtension(filePath);
                string extension = Path.GetExtension(filePath);
                int number = 1;
                Match regex = Regex.Match(filePath, @"(.+) \((\d+)\)\.\w+");
                if (regex.Success)
                {
                    filename = regex.Groups[1].Value;
                    number = int.Parse(regex.Groups[2].Value);
                }
                do
                {
                    number++;
                    filePath = Path.Combine(folder, string.Format("{0} ({1}){2}", filename, number, extension));
                }
                while (File.Exists(filePath));
            }
            return filePath;
        }
    }
}
