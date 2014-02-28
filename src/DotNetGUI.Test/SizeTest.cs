using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetGUI.Test
{
    [TestClass]
    public class SizeTest
    {
        [TestMethod]
        [Description("Tests the parameterless constructor for size")]
        public void SizeParameterlessConstructor()
        {
            var s1 = new Size();
            Assert.AreEqual(default(int), s1.Width, "expected width to be defualt int");
            Assert.AreEqual(default(int), s1.Height, "expected height to be default int");
        }

        [TestMethod]
        [Description("Tests the parameterless constructor for size")]
        public void SizeExplicitConstructor()
        {
            const int expectedWidth = 1;
            const int expectedHeight = 2;
            var s1 = new Size(expectedWidth, expectedHeight);
            Assert.AreEqual(expectedWidth, s1.Width, "unexpected width");
            Assert.AreEqual(expectedHeight, s1.Height, "unexpected height");
        }

        [TestMethod]
        [Description("Tests Equals method")]
        public void SizeEquals()
        {
            var s1 = new Size(100, 100);
            var s2 = s1;
            var s3 = new Size(101, 101);

            Assert.IsTrue(s1.Equals((object)s2), "expected sizes to be equal");
            Assert.IsTrue(s1.Equals(s1), "expected sizes to be equal");

            var obj = new object();
            Assert.IsFalse(s1.Equals(obj));
            Assert.IsFalse(s1.Equals(null));
        }

        /// <summary>
        /// Compares two structures by reference
        /// </summary>
        /// <typeparam name="T">The type to compare</typeparam>
        /// <param name="left">the left struct</param>
        /// <param name="right">the right struct</param>
        /// <returns></returns>
        public static bool CompareByRef<T>(ref T left, ref T right) where T : struct
        {
            return left.Equals(right);
        }

        [TestMethod]
        [Description("Tests the get hash code method")]
        public void SizeHashCode()
        {
            const int expectedWidth = 1;
            const int expectedHeight = 2;
            var s1 = new Size(expectedWidth, expectedHeight);
            var s2 = new Size(expectedWidth, expectedHeight);
            Assert.AreEqual(s1.GetHashCode(), s2.GetHashCode());
        }

        [TestMethod]
        [Description("== and != operator test")]
        public void SizeEqualityOperators()
        {
            var s1 = new Size(10, 10);
            var s2 = new Size(11, 11);
            object s3 = s1;

            Assert.IsTrue(s1 == (Size)s3, "== operator");
            Assert.IsTrue(s1 != s2, "!= operator");
        }

        [TestMethod]
        [Description("Tests the toString method for size")]
        public void SizeToString()
        {
            const int expectedWidth = 123;
            const int expectedHeight = 321;
            var s1 = new Size(expectedWidth, expectedHeight);
            var s1String = s1.ToString();
            Assert.IsTrue(s1String.Contains(expectedWidth.ToString(CultureInfo.InvariantCulture)));
            Assert.IsTrue(s1String.Contains(expectedHeight.ToString(CultureInfo.InvariantCulture)));
        }

        [TestMethod]
        [Description("A size cannot have a negative width")]
        [ExpectedException(typeof(DotNetGUIException))]
        public void SizeWidthCannotBeNegative()
        {
            const int positive = 1;
            const int negative = -1;
            var s1 = new Size(negative, positive);
        }

        [TestMethod]
        [Description("A size cannot have a negative height")]
        [ExpectedException(typeof(DotNetGUIException))]
        public void SizeHeightCannotBeNegative()
        {
            const int positive = 1;
            const int negative = -1;
            var s1 = new Size(positive, negative);
        }

        [TestMethod]
        [Description("A size cannot have a negative height")]
        [ExpectedException(typeof(DotNetGUIException))]
        public void SizeWidthHeightCannotBeNegative()
        {
            const int negativeHeight = -1;
            const int negativeWidth = -1;
            var s1 = new Size(negativeWidth, negativeHeight);
        }
    }
}
