using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek
{
	public class LocationWithHidingPlace : Location
	{
		public string HidingPlace { get; set; } 

		private List<Opponent> hiddenOpponents = new List<Opponent>();

		public LocationWithHidingPlace(string name, string hidingPlace) : base(name) 
		{ 
			HidingPlace = hidingPlace;
		}

		/// <summary>
		/// Adds an Opponent to the hiddenopponents List
		/// </summary>
		/// <param name="opponent"></param>
		public void Hide(Opponent opponent) => hiddenOpponents.Add(opponent);

		/// <summary>
		/// Checks in the Hiding Place for Opponents and clears the hiddingOpponents if all Opponents are found
		/// </summary>
		/// <returns>Returns the found Opponents</returns>	
		public IEnumerable<Opponent> CheckHidingPlace()
		{
			var foundOpponents = new List<Opponent>(hiddenOpponents);
			hiddenOpponents.Clear();
			return foundOpponents;
		}
	}
}
