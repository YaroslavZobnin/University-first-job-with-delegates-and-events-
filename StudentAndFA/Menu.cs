namespace StudentAndFaculty
{
    internal static class Menu
    {
        public static void MainMenu()
        {
            try
            {
                string[] parts = StudentProcessing.TakingStringAboutStudentsFromFile("Student.txt");
                StudentProcessing.SplittingStudentInformationIntoFields(parts, out string[] surnames, out string[] nameOfFaculty, out double[] grant, out uint[][] marks);

                var studentCollection = new ClassCollection<Student>();
                studentCollection.AddNewElementsInClassCollection(StudentProcessing.CreatingStudents(surnames, nameOfFaculty, grant, marks));

                parts = FacultyProcessing.TakingStringAboutFacultyFromFile("Faculty.txt");
                FacultyProcessing.DividingFacultyInformationIntoFields(parts, out string[] dean, out string[] facultyName, out string[] phoneNumber);

                var facultyCollection = new ClassCollection<Faculty>();
                facultyCollection.AddNewElementsInClassCollection(FacultyProcessing.CreatingFaculties(dean, facultyName, phoneNumber, studentCollection));

                RegisterStudentsOnTheirFaculties(facultyCollection, studentCollection);

                InfoForUser();
                
                var key = Console.ReadKey(true);
                while(key.Key != ConsoleKey.Escape)
                {
                    ClearAllScreen();
                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            ConsoleOutputInformation.MainTable(studentCollection, facultyCollection);
                            PressKeyToContinue();
                            break;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            ChangeNameFaculty(facultyCollection);
                            PressKeyToContinue();
                            break;
                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            AwardingStudent(facultyCollection);
                            PressKeyToContinue();
                            break;
                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4:
                            ReferenceInformation();
                            PressKeyToContinue();
                            break;
                    }
                    ClearAllScreen();
                    InfoForUser();
                    key = Console.ReadKey(true); 
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private static void RegisterStudentsOnTheirFaculties(ClassCollection<Faculty> faculties, ClassCollection<Student> students)
        {
            foreach(var faculty in faculties)
            {
                int index = 0;
                foreach(var student in students)
                {
                    if(faculty.NameOfFaculty == student.NameFaculty)
                        faculty.RegisterStudent(student, ref index);
                }
            }
        }
        private static void ChangeNameFaculty(ClassCollection<Faculty> faculties)
        {
            OutputFaculties(faculties, "Выберите какому факультету вы хотите изменить название: ");
            byte number = InputNumberForFaculties(faculties);
            Console.Write("Введите новое название для этого факультета: ");
            string? newNameFaculty = Console.ReadLine();
            faculties[number - 1].NameOfFaculty = string.IsNullOrEmpty(newNameFaculty) ? "Неизвестно" : newNameFaculty;
            Console.WriteLine("НАЗВАНИЕ ФАКУЛЬТЕТА УСПЕШНО ПЕРЕИМЕНОВАННО.");
        }
        private static void AwardingStudent(ClassCollection<Faculty> faculties)
        {

            OutputFaculties(faculties, "Выберите факультет, на котором деканат будет премировать студентов: ");
            byte number = InputNumberForFaculties(faculties);
            bool haveExcelentPupil = false;
            Student[] students = faculties[number - 1].Students;
            for (int i = 0; i < students.Length - 1; i++)
            {
                if (students[i].IsExcellentPupil)
                {
                    haveExcelentPupil = true;
                    break;
                }
            }
            if(!haveExcelentPupil)
            {
                Console.WriteLine("На данном факультете нет отличников");
                return;
            }
            Console.Write("Введите размер премии для студентов-отличников: ");
            double grant = InputGrantForStudents();
            faculties[number - 1].Bonuses(grant);
            Console.WriteLine("ПРЕМИЯ УСПЕШНО ОТПРАВЛЕНА ОТЛИЧНИКАМ");
        }
        private static void PressKeyToContinue()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Нажмите любую клавишу для продолжения..");
            Console.ReadKey(true);
            Console.ResetColor();
        }
        private static void ClearAllScreen()
        {
            Console.Write("\x1b[3J");
            Console.Clear();
        }
        private static byte InputNumberForFaculties(ClassCollection<Faculty> faculties)
        {
            byte.TryParse(Console.ReadLine(), out byte number);
            while (number <= 0 || number > faculties.CountElements)
            {
                Console.WriteLine("Вы выбрали цифру не в диапазоне заданного.\nПовторите ввод ниже.");
                byte.TryParse(Console.ReadLine(), out number);
            }
            return number;
        }
        private static double InputGrantForStudents()
        {
            double number;
            while (!double.TryParse(Console.ReadLine(), out number))
                Console.WriteLine("Вы выбрали цифру не в диапазоне заданного.\nПовторите ввод ниже.");
            return number;
        }
        private static void OutputFaculties(ClassCollection<Faculty> faculties, string? comment = null)
        {
            int count = 1;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            if (comment != null)
                Console.WriteLine(comment);
            foreach (var faculty in faculties)
                Console.WriteLine(count++ + ") " + faculty.ToString());
            Console.ResetColor();
            Console.WriteLine();
        }
        private static void InfoForUser()
        {
            Console.WriteLine("Меню");
            Console.WriteLine("1.Вывести информацию о факультетах");
            Console.WriteLine("2.Изменение названия факультета(название факультета также будет редактированно и у студентов)");
            Console.WriteLine("3.Премирование студентов(касается только отличников)");
            Console.WriteLine("4.Справочная информация");
        }
        private static void ReferenceInformation()
        {
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("---|Правила использования меню|---");
            Console.WriteLine("Выбирайте необходимый вам пункт, они не требуют ввода цифры и подтверждения с помощью Enter. Достаточно выбора цифры");
            Console.WriteLine("Меню не предусматривает ошибочного ввода, так как прямо привязано к клавишам.");
            Console.WriteLine("Для выхода нужно нажать клавишу Escape (Esc).");
            Console.WriteLine("|------------------------------------------------------------------------------------------------------------------|");
            Console.WriteLine("---|Что делает программа?|---");
            Console.WriteLine("-Считывает информацию из первого файла в массив объектов класса Студент.");
            Console.WriteLine("-Считывает информацию из второго файла в массив объектов класса Факультет.");
            Console.WriteLine("-Выводит информацию о студентах в виде таблицы + информации о факультете");
            Console.Write("-При изменении  названия факультета автоматически изменяет название факультета для всех студентов этого факультета. ");
            Console.WriteLine("Для этого каждый студент должен быть «зарегистрирован» на своем факультете. Факультет, название которого меняется, выбирается пользователем.");
            Console.Write("-При решении премировать отличников и установлении размера премии деканатом факультета к стипендии отличников добавляется размер премии. ");
            Console.WriteLine("Для этого отличники должны быть «зарегистрированы» на своем факультете.");
            Console.WriteLine("---|Для разработчиков|---");
            Console.WriteLine("При добавлении студента стоит учитывать, что оценок должно быть строго 5.");
            Console.WriteLine("В случае, если оценок меньше заданного, то \"недостающие\" оценки будут считаться за ноль.");
            Console.WriteLine("В случае, если оценок больше заданного, то программа выдаст исключение и завершит свою работу.");
            Console.WriteLine("Сделано это по следующей причине: в университете проводится пять экзаменов, следовательно и оценок должно быть пять.");
            Console.ResetColor();
        }
    }
}
