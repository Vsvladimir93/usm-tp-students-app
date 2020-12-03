using System;

namespace StudentsApp
{
	sealed class Util
	{
		private Util() { }

		public static int GetNextInt(int min, int max)
		{
			try
			{
				int number = int.Parse(Console.ReadLine());
				if (number < min || number > max)
				{
					Console.WriteLine(" - Пожалуйста введите число от {0} до {1}", min, max);
					return GetNextInt(min, max);
				}
				else
				{
					return number;
				}
			}
			catch (Exception e)
			{
				if (e is FormatException)
				{
					Console.WriteLine(" - Ошибка формата!");
				}
				else if (e is OverflowException)
				{
					Console.WriteLine(" - Число должно быть в диапазоне от {0} до {1}", int.MinValue, int.MaxValue);
				}
				else
				{
					Console.WriteLine(" - Oops!");
				}

				return GetNextInt(min, max);
			}
		}

		public static string GetNextString(int maxLength)
		{
			try
			{
				string result = Console.ReadLine();
				if (result.Trim().Length == 0 || result.Trim().Length > maxLength)
				{
					Console.WriteLine("Пожалуйста введите строку длинной до {0} символа/ов.", maxLength);
					return GetNextString(maxLength);
				}
				else
				{
					return result;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Что то пошло не так! {0}", e.Message);
				return GetNextString(maxLength);
			}
		}

	}
}
