using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace BattleBoats.Wpf.Models.Tests
{
    [TestClass()]
    public class CoordinateRangeTests
    {
        [TestMethod()]
        public void GetAllCoordinatesTest()
        {
            // Arrange
            CoordinateRange coordinateRange = new CoordinateRange(
                new Coordinate(0, 0),
                new Coordinate(0, 3));
            List<Coordinate> expected = new List<Coordinate>() 
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
                new Coordinate(0, 3),
            };

            // Act
            List<Coordinate> actual = coordinateRange.GetAllCoordinates();

            // Assert

            // Check Lists are same length
            Assert.AreEqual(expected.Count, actual.Count);
            // Check each item in lists
            for (var i = 0; i < expected.Count; i++)
            {
                // Get properties of each item
                PropertyInfo[] expectedProps = expected.GetType().GetProperties();
                PropertyInfo[] actualProps = actual.GetType().GetProperties();
                Assert.AreEqual(expectedProps.Length, actualProps.Length);
                // Compare properties of each item
                for (int j = 0; j < expectedProps.Length; j++)
                {
                    Assert.AreEqual(expectedProps[j], actualProps[j]);
                }
            }
        }
    }
}