namespace StudentsApp.model
{
	class Subject
	{

		private int id;
		private string title;

		public Subject(int id, string title)
		{
			this.id = id;
			this.title = title;
		}

		public int ID { get => id; }

		public string Title { get => title; }

		public override string ToString()
		{
			return title;
		}

	}
}
