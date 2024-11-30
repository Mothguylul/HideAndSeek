using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek
{
	public class Location
	{
		/// <summary>
		/// The name of this location
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// The exits out of this location
		/// </summary>
		public IDictionary<Direction, Location> Exits { get; private set; }
		= new Dictionary<Direction, Location>();
		/// <summary>
		/// The constructor sets the location name
		/// </summary>
		/// <param name="name">Name of the location</param>
		public Location(string name)
		{
			Name = name;
		}

		public override string ToString() => Name;

		private string DescribeDirection(Direction d) => d switch
		{
			Direction.Up => "Up",
			Direction.Down => "Down",
			Direction.In => "In",
			Direction.Out => "Out",
			_ => $"to the {d}",
		};

		/// <summary>
		/// Returns a sequence of descriptions of the exits, sorted by direction
		/// </summary>
		public IEnumerable<string> ExitList => Exits
	     .OrderBy(kvp => (int)kvp.Key) 
         .OrderBy(kvp => Math.Abs((int)kvp.Key)) 
         .Select(kvp => kvp.Value.Name); 
		/// <summary>
		/// Adds an exit to this location
		/// </summary>
		/// <param name="direction">Direction of the connecting location</param>
		/// <param name="connectingLocation">Connecting location to add</param>
		public void AddExit(Direction direction, Location connectingLocation)
		{
			Exits.Add(direction, connectingLocation);

			var converteddirection = (int)direction;

			connectingLocation.Exits.Add((Direction)(-converteddirection), this); // add exit to oppostie direction
		}
		/// <summary>
		/// Gets the exit location in a direction
		/// </summary>
		/// <param name="direction">Direciton of the exit location</param>
		/// <returns>The exit location, or this if there is no exit in that direction</returns>
		public Location GetExit(Direction direction) => Exits.ContainsKey(direction) ? Exits[direction] : this;
	}
}
