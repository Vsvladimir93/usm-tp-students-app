using StudentsApp.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsApp
{
	class GradeBookService
	{

		private GradeBook gradeBook;

		private string[] options;

		private int[] subjectRequired;

		public GradeBookService(GradeBook gradeBook)
		{
			this.gradeBook = gradeBook;

			options = new string[]
			{
				"1. Студент с лучшей средней оценкой.",
				"2. Студент с лучшей средней оценкой по предмету.",
				"3. Студент с худшей средней оценкой.",
				"4. Студент с худшей средней оценкой по предмету.",
				"5. Отстающие студенты. Средний балл меньше 5.",
				"6. Список всех студентов.",
				"7. Список всех предметов.",
				"8. Посмотреть Журнал по предмету.",
				"9. Выход.",
			};

			subjectRequired = new int[] { 2, 4, 8 };

		}

		public string[] Options { get => options; }

		public int[] SubjectRequired { get => subjectRequired; }

		public string[] Subjects { get => AllSubjects(); }

		public string[] ProcessInput(int selectedOption, int subjectId = 0)
		{
			switch (selectedOption)
			{
				case 1:
					return GetAverageFromAll();
				case 2:
					return GetAverageFor(subjectId);
				case 3:
					return GetAverageFromAll(false);
				case 4:
					return GetAverageFor(subjectId, false);
				case 5:
					return GetStudentsWithAvgLessThanFive();
				case 6:
					return AllStudents();
				case 7:
					return AllSubjects();
				case 8:
					return CollectGradeBookMatrix(subjectId);
				case 9:
					Environment.Exit(0);
					return new string[] { };
				default:
					return new string[] { };
			}
		}

		private double[] AverageGradesAll()
		{
			double[] studentsAvgAll = new double[gradeBook.Students.Length];

			for (int studIndex = 0; studIndex < gradeBook.Students.Length; studIndex++)
			{
				double[] averageForAllSubjects = new double[gradeBook.Subjects.Length];

				for (int subjIndex = 0; subjIndex < gradeBook.Subjects.Length; subjIndex++)
				{
					averageForAllSubjects[subjIndex] = gradeBook.GetStudentGradeFor(subjIndex, studIndex).Average();
				}

				studentsAvgAll[studIndex] = averageForAllSubjects.Average();
			}
			return studentsAvgAll;
		}

		private string[] GetStudentsWithAvgLessThanFive()
		{
			return AverageGradesAll()
					.Where(a => a < 5.0)
					.Select((grade, index) => StudentGradeOutputFormat(index + 1, gradeBook.Students[index].ToString(), grade)).ToArray();
		}

		private string[] GetAverageFromAll(bool getBest = true)
		{
			double[] studentsAvgAll = AverageGradesAll();

			double searchedAvg = getBest ? studentsAvgAll.Max() : studentsAvgAll.Min();

			KeyValuePair<Student, double> studAvg = studentsAvgAll
									.Select((avg, index) => KeyValuePair.Create(gradeBook.Students[index], avg))
									.Where(kv => kv.Value == searchedAvg).First();
			return new string[] { StudentGradeOutputFormat(studAvg.Key.ID + 1, studAvg.Key.ToString(), studAvg.Value) };
		}

		private KeyValuePair<Student, double> AverageFor(int subjectId, bool getBest = true)
		{
			IEnumerable<KeyValuePair<int, double>> studAvg = gradeBook
					.GetSubjectGrades(subjectId)
					.Select((studGrades, index) => KeyValuePair.Create(index, studGrades.Average()));
			if (getBest)
			{
				double maxAvg = studAvg.Max(s => s.Value);
				return studAvg.Where(stud => stud.Value == maxAvg).Select(s => KeyValuePair.Create(gradeBook.Students[s.Key], maxAvg)).First();
			}
			else
			{
				double minAvg = studAvg.Min(s => s.Value);
				return studAvg.Where(stud => stud.Value == minAvg).Select(s => KeyValuePair.Create(gradeBook.Students[s.Key], minAvg)).First();
			}
		}

		private string[] GetAverageFor(int subjectId, bool getBest = true)
		{
			KeyValuePair<Student, double> best = AverageFor(subjectId, getBest);
			return new string[] { StudentGradeOutputFormat(best.Key.ID + 1, best.Key.ToString(), best.Value) };
		}

		private string[] AllSubjects()
		{
			return gradeBook.Subjects.Select(s => string.Format("{0} - {1}", s.ID + 1, s.ToString())).ToArray();
		}

		private string[] AllStudents()
		{
			return gradeBook.Students.Select(s => string.Format("{0} - {1}", s.ID + 1, s.ToString())).ToArray();
		}
		private string[] CollectGradeBookMatrix(int subjectId)
		{
			string[] resultMatrix = new string[gradeBook.Students.Length + 1];
			resultMatrix[0] = string.Format("Предмет - {0}\n", gradeBook.Subjects[subjectId].ToString());
			for (int index = 0; index < gradeBook.Students.Length; index++)
			{
				int[] grades = gradeBook.GetSubjectGrades(subjectId)[index];

				resultMatrix[index + 1] = string.Format("{0} - {1,-15} \t {2}{3} Средняя: {4:0.0}",
						index + 1,
						gradeBook.Students[index],
						string.Join("\t", grades),
						new string('\t', 10 - grades.Length),
						gradeBook.GetSubjectGrades(subjectId)[index].Average()
					);
			}

			return resultMatrix;
		}

		private string StudentGradeOutputFormat(int index, string name, double grade)
		{
			return string.Format("Студент: {0} {1,-15} Оценка: {2:0.0}", index, name, grade);
		}
	}
}