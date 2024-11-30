using HideAndSeek;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test;


[TestClass]
public class LoactionTests
{
	private Location center;
	/// <summary>
	/// Initializes each unit test by setting creating a new the center location
	/// and adding a room in each direction before the test
	/// </summary>
	[TestInitialize]
	public void Initialize()
	{
		center = new Location("Center Room");
		Assert.AreEqual(0, center.Exits.Count);
		Assert.AreSame("Center Room", center.Name.ToString());

		//center.AddExit(Direction.North, new Location("North Room"));
		center.AddExit(Direction.Northeast, new Location("Northeast Room"));
		//center.AddExit(Direction.East, new Location("East Room"));
		center.AddExit(Direction.Southeast, new Location("Southeast Room"));
		center.AddExit(Direction.South, new Location("South Room"));
		center.AddExit(Direction.Southwest, new Location("East Room"));
		center.AddExit(Direction.West, new Location("West Room"));
		center.AddExit(Direction.Northwest, new Location("Northwest Room"));
		center.AddExit(Direction.Up, new Location("Upper Room"));
		center.AddExit(Direction.Down, new Location("Lower Room"));
		center.AddExit(Direction.In, new Location("Inside Room"));
		center.AddExit(Direction.Out, new Location("Outside Room"));

		Assert.AreEqual(10, center.ExitList.Count());

		// You'll use this to create a bunch of locations before each test
	}
	/// <summary>
	/// Make sure GetExit returns the location in a direction only if it exists
	/// </summary>
	[TestMethod]
	public void TestGetExit()
	{
		//Arrange, Act
		var soatheastroom = center.GetExit(Direction.Southeast);

		//Assert
		Assert.AreSame("Southeast Room", soatheastroom.Name);
		Assert.AreSame(center, soatheastroom.GetExit(Direction.Northwest));
		Assert.AreSame(soatheastroom, soatheastroom.GetExit(Direction.Up));

	}
	/// <summary>
	/// Validates that the exit lists are working
	/// </summary>
	[TestMethod]
	public void TestExitList()
	{
		List<string> expectedRooms = new() {
				   //"North Room",
				   "Northeast Room",
				  // "East Room",
				   "Southeast Room",
				   "South Room",
				   "East Room",
				   "West Room",
				   "Northwest Room",
				   "Upper Room",
				   "Lower Room",
				   "Inside Room",
				   "Outside Room",
			  };
		foreach (string exitroom in expectedRooms)
		{
			Assert.IsTrue(center.ExitList.ToList().Contains(exitroom));
		}
		Assert.AreEqual(expectedRooms.Count, center.ExitList.ToList().Count);
	}
	/// <summary>
	/// Validates that each room’s name and return exit is created correctly
	/// </summary>
	[TestMethod]
	public void TestReturnExits()
	{
		//Arrange
		var westroom = center.GetExit(Direction.West);

		//Act

		//Assert
		Assert.AreEqual("West Room", westroom.Name);
		Assert.AreSame(center, westroom.GetExit(Direction.East));
	}

	[TestMethod]
	public void TestAddExit()
	{
		//Arrange
		var northRoom = new Location("North Room");
		center.AddExit(Direction.North, northRoom);

		// Act
		bool isInExits = center.Exits.Any(kvp => kvp.Key == Direction.North && kvp.Value.Name == "North Room");
		bool isOppositeDirInExits = northRoom.Exits.Any(kvp => kvp.Key == Direction.South && kvp.Value.Name == center.Name);

		//Assert
		Assert.IsTrue(isInExits);
		Assert.IsTrue(isOppositeDirInExits);
	}
	/// <summary>
	/// Add a hall to one of the rooms and make sure the hall room’s names
	/// and return exits are created correctly
	/// </summary>
	[TestMethod]
	public void TestAddHall()
	{
		//Arrange
		var eastroom = new Location("East Room");
		center.AddExit(Direction.East, eastroom);

		var hall = new Location("Hall");
		eastroom.AddExit(Direction.East, hall);

		var eastfromHall = new Location("East from Hall");
		hall.AddExit(Direction.East, eastfromHall);

		//Assert
		Assert.AreEqual(eastroom.GetExit(Direction.East), hall);
		Assert.AreEqual(hall.GetExit(Direction.East), eastfromHall);

	}
}

