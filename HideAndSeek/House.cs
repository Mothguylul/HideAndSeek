using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek
{
	public static class House
	{
		public static Location Entry { get; set; }

		public static Location Garage { get; set; }

		public static Location LivingRoom { get; set; }

		public static Location Hallway { get; set; }

		public static Location Bathroom { get; set; }

		public static Location Kitchen { get; set; }

		public static Location Landing { get; set; }

		public static Location Pantry { get; set; }

		public static Location KidsRoom { get; set; }

		public static Location Nursery { get; set; }

		public static Location SecondBathroom { get; set; }

		public static Location MasterBathroom { get; set; }

		public static Location MasterBedroom { get; set; }

		public static Location Attic { get; set; }

		private static List<Location> locations;

		public static Random randomField;

		public static Random Random = new Random();

		static House()
		{
			Entry = new Location("Entry");
			Hallway = new Location("Hallway");
			Landing = new Location("Landing");
			Garage = new LocationWithHidingPlace("Garage", "behind the car");
			LivingRoom = new LocationWithHidingPlace("Living Room", "behind the sofa" );
			Bathroom = new LocationWithHidingPlace("Bathroom", "behind the door");
			Kitchen = new LocationWithHidingPlace("Kitchen", "next to the stove");
			Pantry = new LocationWithHidingPlace("Pantry", "inside a cabinet");
			KidsRoom = new LocationWithHidingPlace("Kids Room", "under the bed");
			Nursery = new LocationWithHidingPlace("Nursery", "under the crib");
			SecondBathroom = new LocationWithHidingPlace("Second Bathroom", "in the shower");
			MasterBathroom = new LocationWithHidingPlace("Master Bath", "in the bathtub");
			MasterBedroom = new LocationWithHidingPlace("Master Bedroom", "in the closet");
			Attic = new LocationWithHidingPlace("Attic", "in the trunk");

			Entry.AddExit(Direction.Out, Garage);
			Entry.AddExit(Direction.East, Hallway);

			Hallway.AddExit(Direction.Northwest, Kitchen);
			Hallway.AddExit(Direction.North, Bathroom);
			Hallway.AddExit(Direction.South, LivingRoom);
			Hallway.AddExit(Direction.Up, Landing);

			Landing.AddExit(Direction.Northwest, MasterBedroom);
			Landing.AddExit(Direction.West, SecondBathroom);
			Landing.AddExit(Direction.Southwest, Nursery);
			Landing.AddExit(Direction.South, Pantry);
			Landing.AddExit(Direction.Southeast, KidsRoom);
			Landing.AddExit(Direction.Up, Attic);

			MasterBedroom.AddExit(Direction.East, MasterBathroom);

			// add locations to private location list
			locations = new List<Location>()
			{
				Entry,
				Garage,
				LivingRoom,
				Hallway,
				Bathroom,
				Kitchen,
				Landing,
				Pantry,
				KidsRoom,
				Nursery,
				SecondBathroom,
				MasterBathroom,
				MasterBedroom,
				Attic
			};
			//Garage.AddExit(Direction.In, Entry);
		}

		/// <summary>
		/// Gets a location by name
		/// </summary>
		/// <param name="name">The name of the location to find</param>
		/// <returns>The location, or Entry if no location by that name was found</returns>
		public static Location GetLocationByName(string locationName)
		{
			var findLocation = locations.FirstOrDefault(l => l.Name == locationName);

			return (findLocation == null? Entry : findLocation);			
		}

		/// <summary>
		/// Gets a random exit from the location
		/// </summary>
		/// <param name="location">Location to get the random exit from</param>
		/// <returns>One of the locatin's exits selected randomly</returns>
		public static Location RandomExit(Location location)
		{
			var exits = location.Exits.OrderBy(exit => exit.Value.Name).ToList();

			int randomIndex = Random.Next(exits.Count);

			return exits[randomIndex].Value;
		}

		public static void ClearHidingPlaces()
		{
			foreach(var location in locations)
			{
				if(location is LocationWithHidingPlace locationwithhidingplace)
				{
					locationwithhidingplace.CheckHidingPlace();
				}
			}
		}
	}
}
