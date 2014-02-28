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
