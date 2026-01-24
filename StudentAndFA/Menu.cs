using StudentAndFA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student
{
    internal static class Menu
    {
        public static void MainMenu()
        {
            try
            {
                string[] parts = StudentProcessing.TakingStringFromFile("Student.txt");
                StudentProcessing.DivisionIntoFields(parts, out string[] surnames, out string[] nameFaculty, out double[] grant, out uint[][] marks);
                var person1 = new Student(surnames[0], nameFaculty[0], grant[0], marks[0]);
                Console.WriteLine(person1);
                Console.WriteLine("прайм");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
