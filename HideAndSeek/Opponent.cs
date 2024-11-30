using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek
{
	public class Opponent
	{
		public string Name { get; set; }

		public Opponent(string name) => Name = name;
		
		public void Hide()
		{
			throw new NotImplementedException();
		}
	}
}
