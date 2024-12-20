﻿using HideAndSeek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
	[TestClass]
	public class OpponentTests
	{
		[TestMethod]
		public void TestOpponentHiding()
		{
			var opponent1 = new Opponent("opponent1");
			Assert.AreEqual("opponent1", opponent1.Name);
			House.Random = new MockRandomWithValueList(new int[] { 0, 1 });
			opponent1.Hide();
			var bathroom = House.GetLocationByName("Bathroom") as LocationWithHidingPlace;
			CollectionAssert.AreEqual(new[] { opponent1 }, bathroom.CheckHidingPlace().ToList());
			var opponent2 = new Opponent("opponent2");
			Assert.AreEqual("opponent2", opponent2.Name);
			House.Random = new MockRandomWithValueList(new int[] { 0, 1, 2, 3, 4 });
			opponent2.Hide();
			var kitchen = House.GetLocationByName("Kitchen") as LocationWithHidingPlace;
			CollectionAssert.AreEqual(new[] { opponent2 }, kitchen.CheckHidingPlace().ToList());
		}

		[TestMethod]
		public void TestName()
		{
			var opponent1 = new Opponent("opponent1");
			Assert.AreEqual("opponent1", opponent1.Name);
		}
	}
}
