using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student
{
    internal class Student
    {
        private string? surname, nameFaculty;
        private double? grant;
        private uint[]? marks = new uint[5];

        public bool isExcellentPupil
        {
           get
           {
                if(marks != null)
                {
                    uint res = 0;
                    foreach (var mark in marks)
                        res += mark;
                    return res / 5 > 8 ? true : false;
                }
                throw new NullReferenceException("Нет оценок.");
           }
        }
        public string? Surname => surname;
        public string? NameFaculty => nameFaculty;
        public double? Grant => grant;
        public uint[]? Marks => marks;
        public Student(string? surname, string? nameFaculty, double? grant, uint[]? marks)
        {
            this.surname = surname;
            this.nameFaculty = nameFaculty;
            this.grant = grant;
            this.marks = marks;
        }
        public Student()
        {
            surname = "Неизвестно";
            nameFaculty = "ФАИС";
            grant = 0;
            marks = [0,0,0,0,0];
        }
        public override string ToString()
        {
            if (marks == null)
                throw new NullReferenceException("Оценок нет.");
            string stringMark = marks[0].ToString();
            for (int i = 1; i < marks.Length; i++)
                stringMark += ' ' + marks[i].ToString();
            return $"{surname}, {nameFaculty}, {grant}, {stringMark}";
        }
    }
}
