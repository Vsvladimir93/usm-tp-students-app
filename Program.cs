using StudentsApp.model;
using System;
using System.Collections.Generic;

namespace StudentsApp
{
	class Program
	{
		static void Main(string[] args)
		{
			KeyValuePair<Student[], Subject[]> data = Init();

			GradeBook gBook = new GradeBook(data.Key, data.Value);

			GradeBookService gradeBookService = new GradeBookService(gBook);

			ConsoleMenu menu = new ConsoleMenu(gradeBookService);

			menu.ShowMenu();
		}

		private static KeyValuePair<Student[], Subject[]> Init()
		{
			Console.WriteLine("Пожалуйста введите число студентов от 1 до 30:");
			int studentsNumber = Util.GetNextInt(1, 30);
			Student[] students = GetStudentsData(studentsNumber);

			Console.Clear();
			Console.WriteLine("Пожалуйста введите число предметов от 1 до 20");
			int subjectsNumber = Util.GetNextInt(1, 20);
			Subject[] subjects = GetSubjectData(subjectsNumber);

			return KeyValuePair.Create(students, subjects);

		}

		private static Student[] GetStudentsData(int numberOfStudents)
		{
			Student[] array = new Student[numberOfStudents];

			for (int index = 0; index < numberOfStudents; index++)
			{
				Console.Clear();
				Console.WriteLine("Пожалуйста введите имя и фамилию студента:");
				string name = Util.GetNextString(15);
				array[index] = new Student(index, name);
			}

			return array;
		}

		private static Subject[] GetSubjectData(int numberOfSubjects)
		{
			Subject[] array = new Subject[numberOfSubjects];

			for (int index = 0; index < numberOfSubjects; index++)
			{
				Console.Clear();
				Console.WriteLine("Пожалуйста введите название предмета:");
				string title = Util.GetNextString(15);
				array[index] = new Subject(index, title);
			}

			return array;
		}




	}
}
