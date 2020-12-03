using System;

namespace StudentsApp.model
{
	class GradeBook
	{
		private Student[] students;

		private Subject[] subjects;

		private int[][][] grades;		

		public GradeBook(Student[] students, Subject[] subjects)
		{
			this.students = students;
			this.subjects = subjects;
			InitGrades();
		}

		// Generate random grades for each Subject / Student
		private void InitGrades()
		{
			int[][][] grades = new int[subjects.Length][][];

			foreach (Subject subj in subjects)
			{
				grades[subj.ID] = new int[students.Length][];
				foreach (Student stud in students)
				{
					int randNumOfGrades = new Random().Next(3, 10);
					grades[subj.ID][stud.ID] = new int[randNumOfGrades];
					for (int gradeIndex = 0; gradeIndex < randNumOfGrades; gradeIndex++)
					{
						grades[subj.ID][stud.ID][gradeIndex] = new Random().Next(2, 10);
					}
				}
			}

			this.grades = grades;
		}


		// Return int[][] where int[StudentId][grades<int>] for specific Subject
		public int[][] GetSubjectGrades(int subjectId)
		{
			return grades[subjectId];
		}

		// Return int[][] where int[grades<int>] for specific Subject and Student
		public int[] GetStudentGradeFor(int subjectId, int studentId)
		{
			return GetSubjectGrades(subjectId)[studentId];
		}

		public Subject[] Subjects { get => subjects; }

		public Student[] Students { get => students; }

	}
}
