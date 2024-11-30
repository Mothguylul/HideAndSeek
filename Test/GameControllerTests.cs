using Microsoft.VisualStudio.TestTools.UnitTesting;
using HideAndSeek;
using System;

namespace Test;

[TestClass]
public class GameControllerTests
{
	GameController gameController;

	[TestInitialize]
	public void Initialize()
	{
		gameController = new GameController();
	}

	[TestMethod]
	public void TestMovement()
	{
		Assert.AreEqual("Entry", gameController.CurrentLocation.Name);
		Assert.IsFalse(gameController.Move(Direction.Up));
		Assert.AreEqual("Entry", gameController.CurrentLocation.Name);
		Assert.IsTrue(gameController.Move(Direction.East));
		Assert.AreEqual("Hallway", gameController.CurrentLocation.Name);
		Assert.IsTrue(gameController.Move(Direction.Up));
		Assert.AreEqual("Landing", gameController.CurrentLocation.Name);
		// Add more movement tests to the TestMovement test method
	}

	[TestMethod]
	public void TestMove()
	{
		Assert.IsFalse(gameController.Move(Direction.Up));
	}

	[TestMethod]
	public void TestCurrentLocation()
	{
		Assert.AreEqual("Entry", gameController.CurrentLocation.Name);
	}

	[TestMethod]
	public void TestStatus()
	{
		Assert.AreEqual("You are in the Entry. You see the following Exits: " +
		Environment.NewLine +
		Environment.NewLine + "- Hallway" +
        Environment.NewLine + "- Garage", gameController.Status);
	}

	[TestMethod]
	public void TestParseInput()
	{
		var initialStatus = gameController.Status;
		Assert.AreEqual("That's not a valid direction", gameController.ParseInput("xx"));
		Assert.AreEqual(initialStatus, gameController.Status);
		Assert.AreEqual("There's no exit in that direction", gameController.ParseInput("Up"));
		Assert.AreEqual(initialStatus,gameController.Status);
		Assert.AreEqual("Moving East", gameController.ParseInput("East"));
		Assert.AreEqual("You are in the Hallway. You see the following Exits: " +
		Environment.NewLine +
		Environment.NewLine + "- Bathroom" +
		Environment.NewLine + "- Living Room" +
		Environment.NewLine + "- Entry" +
		Environment.NewLine + "- Kitchen" +
		Environment.NewLine + "- Landing", gameController.Status);
		Assert.AreEqual("Moving South", gameController.ParseInput("South"));
		Assert.AreEqual("You are in the Living Room. You see the following Exits: " +
		Environment.NewLine +
		Environment.NewLine + "- Hallway", gameController.Status);

	}
}