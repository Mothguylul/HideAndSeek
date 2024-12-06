using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek
{
	public class GameController
	{
		/// <summary>
		/// List of all Opponents in the House
		/// </summary>
		public IEnumerable<Opponent> Opponents = new List<Opponent>()
		{
			new Opponent("Joe"),
			new Opponent("Bob"),
			new Opponent("Ana"),
			new Opponent("Owen"),
			new Opponent("Jimmy"),
		};

		/// <summary>
		/// Lists up all Opponents that have been found already
		/// </summary>
		private readonly List<Opponent> foundOpponents = new List<Opponent>();

		/// <summary>
		/// The number of moves the player has made
		/// </summary>
		public int MoveNumber { get; private set; } = 1;


		/// <summary>
		/// Returns true if the game is over
		/// <return>If the Opponents Count equals the foundOpponents count true, otherwise false</return>
		/// </summary>
		public bool GameOver => Opponents.Count() == foundOpponents.Count();

		/// <summary>
		/// The player's current location in the house
		/// </summary>
		public Location CurrentLocation { get; private set; }
		/// <summary>
		/// Returns the the current status to show to the player
		/// </summary>
		public string Status => GetStatus();
			
		public string GetStatus()
		{
			var found = $"You have found {foundOpponents.Count} of {Opponents.Count()} opponents: {string.Join(", ", foundOpponents.Select(o => o.Name))}";
			var hidingPlace = (CurrentLocation is LocationWithHidingPlace location) ? $"Someone could hide {location.HidingPlace}"
				: string.Empty;

			return $"You are in the {CurrentLocation}. You see the following exits:" + Environment.NewLine +
				   $" - {string.Join(Environment.NewLine + " - ", CurrentLocation.ExitList)}{Environment.NewLine}{hidingPlace}" +
				   $"{Environment.NewLine}{found}";
		}
		/// <summary>
		/// A prompt to display to the player
		/// </summary>
		public string Prompt => "Which direction do you want to go (or type 'check'): ";

		public GameController()
		{
			House.ClearHidingPlaces();

			foreach (var opponent in Opponents)
				opponent.Hide();

			CurrentLocation = House.Entry;
		}
		/// <summary>
		/// Move to the location in a direction
		/// </summary>
		/// <param name="direction">The direction to move</param>
		/// <returns>True if the player can move in that direction, false oterwise</returns>
		public bool Move(Direction direction)
		{
			var oldLocation = CurrentLocation;
			CurrentLocation = CurrentLocation.GetExit(direction);

			// if old and new location are the same then false because there wasnt a exit in this direction
			if (oldLocation == CurrentLocation)
				return false;
			else
				return true;
		}

		/// <summary>
		/// Parses input from the player and updates the status
		/// </summary>
		/// <param name="input">Input to parse</param>
		/// <returns>The results of parsing the input</returns>
		public string ParseInput(string input)
		{
			var results = "That's not a valid direction";

			if (Enum.TryParse(typeof(Direction), input, out object direction))
			{
				MoveNumber++;
				if (!Move((Direction)direction))
					results = "There's no exit in that direction";
				else
					results = $"Moving {direction}";
			}

			else if (input.ToLower() == "check")
			{
				MoveNumber++;

				if (CurrentLocation is LocationWithHidingPlace hidingPlace)
				{
					var foundOpponent = hidingPlace.CheckHidingPlace();

					if (foundOpponent.Count() != 0)
					{
						foundOpponents.AddRange(foundOpponent);
						var s = foundOpponent.Count() == 1 ? "" : "s";
						results = $"You found {foundOpponent.Count()} opponent{s} hiding {hidingPlace.HidingPlace}";
					}
					else if (foundOpponent.Count() == 0)
						results = $"Nobody was hiding {hidingPlace.HidingPlace}";
				}
				else if (CurrentLocation is Location noHidingPlaceLocation)
				{
					if (noHidingPlaceLocation.Name.ToLower() == "entry")
						results = "There is no hiding place in the Entry";
					else if (noHidingPlaceLocation.Name.ToLower() == "hallway")
						results = "There is no hiding place in the Hallway";
					else if (noHidingPlaceLocation.Name.ToLower() == "landing")
						results = "There is no hiding place in the Landing";
				}
			}
			return results;
		}
	}
}
