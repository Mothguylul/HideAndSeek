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
			var currentLocation = House.Entry;

			var locationToMoveThrough = House.Random.Next(10, 20);

			for (int i = 0; i < locationToMoveThrough; i++)
			{
				currentLocation = House.RandomExit(currentLocation);
			}

			while (!(currentLocation is LocationWithHidingPlace))
			{
				currentLocation = House.RandomExit(currentLocation);
			}

			(currentLocation as LocationWithHidingPlace).Hide(this);

			System.Diagnostics.Debug.WriteLine($"{Name} is hiding " +
            $"{(currentLocation as LocationWithHidingPlace).HidingPlace} in the {currentLocation.Name}");

		}
	}
}
