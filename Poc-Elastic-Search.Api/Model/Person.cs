using System;

namespace Poc_Elastic_Search.Api.Model
{
    public class Person
	{

		public Person()
		{
			CreatedAt = DateTime.Now;
		}

		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public string Sex { get; set; }
		public string Message { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }

	}
}
