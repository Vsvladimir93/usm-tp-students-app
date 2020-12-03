using StudentsApp.model;
using System;
using System.Linq;

namespace StudentsApp
{
	sealed class ConsoleMenu
	{

		private GradeBookService bookService;

		public ConsoleMenu(GradeBookService bookService)
		{
			this.bookService = bookService;
		}

		public void ShowMenu()
		{
			PrintMainMenu();
			ProcessMainMenuInput();
		}

		private void PrintMainMenu()
		{
			Console.Clear();
			Console.Write(new string('_', Console.WindowWidth));
			Console.WriteLine("Меню");
			Console.Write(new string('_', Console.WindowWidth));
			Console.WriteLine();
			foreach (string option in bookService.Options)
			{
				Console.WriteLine(option);
			}
			Console.Write(new string('_', Console.WindowWidth));
			Console.WriteLine();
		}

		private void ProcessMainMenuInput()
		{
			int input = Util.GetNextInt(1, bookService.Options.Length);
			int subjectId = 0;

			if (bookService.SubjectRequired.Contains(input))
			{
				PrintSubjectMenu();
				subjectId = ProcessSubjectInput();
			}

			Console.Clear();
			Console.Write(new string('_', Console.WindowWidth));
			Console.WriteLine(bookService.Options[input - 1].Substring(3));
			Console.Write(new string('_', Console.WindowWidth));
			foreach (string result in bookService.ProcessInput(input, subjectId))
			{
				Console.WriteLine(result);
			}
			Console.Write(new string('_', Console.WindowWidth));
			PrintReturnMenu();
		}

		private void PrintSubjectMenu()
		{
			Console.Clear();
			Console.Write(new string('_', Console.WindowWidth));
			Console.WriteLine("Выберите предмет:");
			Console.Write(new string('_', Console.WindowWidth));
			Console.WriteLine();
			foreach (string subject in bookService.Subjects)
			{
				Console.WriteLine(subject);
			}
			Console.WriteLine();
		}

		private int ProcessSubjectInput()
		{
			return Util.GetNextInt(1, bookService.Subjects.Length) - 1;
		}

		private void PrintReturnMenu()
		{
			Console.WriteLine("Нажмите 1 чтобы вернутся в главное меню.");
			while (true)
			{
				string result = Console.ReadLine();
				if (result == "1")
				{
					PrintMainMenu();
					ProcessMainMenuInput();
					return;
				}
			}
		}






	}
}
