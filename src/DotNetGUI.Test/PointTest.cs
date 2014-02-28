using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetGUI.Test
{
    [TestClass]
    public class PointTest
    {
        [TestMethod]
        [Description("Tests the parameterless constructor for a point")]
        public void PointParameterlessConstructor()
        {
            var p1 = new Point();
            Assert.AreEqual(default(int), p1.X, "expected x-coordinate to be int default");
            Assert.AreEqual(default(int), p1.Y, "expected y-coordinate to be int default");
        }

        [TestMethod]
        [Description("Tests the explict (x,y) constructor for a point")]
        public void PointExplicitConstructor()
        {
            const int expectedXCoordinate = 1;
            const int expectedYCoordinate = 2;
            var p1 = new Point(expectedXCoordinate, expectedYCoordinate);
            Assert.AreEqual(expectedXCoordinate, p1.X, "unexpected x-coordinate");
            Assert.AreEqual(expectedYCoordinate, p1.Y, "unexpected y-coordinate");
        }


        [TestMethod]
        [Description("== and != operators for Point")]
        public void PointOperatorEquality()
        {
            var p1 = new Point(20, 10);
            var p2 = new Point(10, 20);
            var p3 = p1;

            Assert.AreNotSame(p1, p3);
            Assert.IsTrue(p1 == p3, "== operator");
            Assert.IsTrue(p1 != p2, "!= operator");
        }

        [TestMethod]
        [Description("point equality test")]
        public void PointEquals()
        {
            var p1 = new Point(20, 20);
            object p2 = new Point(20, 20);
            Assert.AreEqual(p1, p2, "expected points to be equal");

            var p3 = new Point(20, 10);
            object p4 = new Point(10, 20);
            Assert.IsFalse(p3.Equals(p4), "expected points to not be equal");

            Assert.IsFalse(p3.Equals(new object()), "expected points to not be equal");
            Assert.IsFalse(p3.Equals(null), "expected points to not be equal");
        }

        [TestMethod]
        [Description("point toString test")]
        public void PointToString()
        {
            const int expectedX = 20;
            const int expectedY = 21;
            var p1 = new Point(expectedX, expectedY);
            Assert.IsTrue(p1.ToString().Contains(expectedX.ToString(CultureInfo.InvariantCulture)));
            Assert.IsTrue(p1.ToString().Contains(expectedY.ToString(CultureInfo.InvariantCulture)));
        }

        [TestMethod]
        [Description("point hashcode test")]
        public void PointGetHashCode()
        {
            var p1 = new Point(20, 20);
            var p2 = new Point(20, 20);
            var p3 = new Point(21, 21);
            Assert.AreEqual(p1.GetHashCode(), p2.GetHashCode());
            Assert.AreNotEqual(p3.GetHashCode(), p2.GetHashCode());
        }

        [TestMethod]
        [Description("Tests the explict (x,y) constructor for a point in all quadrants")]
        public void PointQuadrantAndAxis()
        {
            var axisAndQuadrants = new [] { -1, 0, 1 };

            foreach (var x in axisAndQuadrants)
            {
                foreach (var y in axisAndQuadrants)
                {
                    var p1 = new Point(x, y);

                    Assert.AreEqual(x, p1.X, "unexpected x-coordinate");
                    Assert.AreEqual(y, p1.Y, "unexpected y-coordinate");
                }
            }
        }
    }
}
