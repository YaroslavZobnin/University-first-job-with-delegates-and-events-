using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAndFA
{
    internal static class StudentProcessing
    {
        public static string[] TakingStringFromFile(string fileName)
        {
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                return lines;
            }
            catch(FileNotFoundException ex)
            {
                throw new Exception("Файл не найден.", ex);
            }
        }
        public static void DivisionIntoFields(string[] parts, out string[] surnames, out string[] nameFaculty, out double[] grant, out uint[][] marks)
        {
            surnames = new string[parts.Length];
            nameFaculty = new string[parts.Length];
            grant = new double[parts.Length];
            marks = new uint[parts.Length][];
            for(int i = 0; i < parts.Length; i++)
                marks[i] = new uint[5];
            for(int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                string[] partsOfPart = part.Split(';');
                if (partsOfPart.Length != 4)
                    throw new ArgumentOutOfRangeException($"Количество элементов в файле не соответствует количеству полей (в строке {i + 1}).");
                surnames[i] = partsOfPart[0];
                nameFaculty[i] = partsOfPart[1];
                double.TryParse(partsOfPart[3],out grant[i]);
                string[] stringMark = partsOfPart[2].Split(',');
                for (int j = 0; j < stringMark.Length; j++)
                    uint.TryParse(stringMark[j], out marks[i][j]);
            }
        }

    }
}
