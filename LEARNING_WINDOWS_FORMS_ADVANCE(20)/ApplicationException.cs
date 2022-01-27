using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEARNING_WINDOWS_FORMS_ADVANCE_20_
{
	public class ApplicationException : System.ApplicationException
	{
		public int Number { get; set; }

		public ApplicationException(string message)
			: base(message)
		{
			Number = -1;
		}

		public ApplicationException(string message, int number)
			: base(message)
		{
			Number = number;
		}
	}
}
