namespace StudentsApp.model
{
	class Student
	{

		private int id;
		private string name;

		public Student(int id, string name)
		{
			this.id = id;
			this.name = name;
		}

		public int ID { get => id; }
		public string Name { get => name; }
		public override string ToString()
		{
			return name;
		}

	}
}
