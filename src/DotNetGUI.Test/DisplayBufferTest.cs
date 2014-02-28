using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetGUI.Test
{
    [TestClass]
    public class DisplayBufferTest
    {
        [TestMethod]
        [Description("Tests the display buffer constructor")]
        public void DisplayBufferConstructor()
        {
            const int expectedWidthAndHeight = 10;
            var displayBuffer = new DisplayBuffer(expectedWidthAndHeight, expectedWidthAndHeight);
            Assert.AreEqual(expectedWidthAndHeight, displayBuffer.Width, "width did not match");
            Assert.AreEqual(expectedWidthAndHeight, displayBuffer.Height, "height did not match");
        }

        [TestMethod]
        [Description("Tests the display buffer constructor")]
        public void DisplayBufferIndexer()
        {
            const int expectedWidthAndHeight = 10;
            var displayBuffer = new DisplayBuffer(expectedWidthAndHeight, expectedWidthAndHeight);
            Assert.AreEqual(displayBuffer[0,0].G, default(char), "expected G to be default");
            Assert.AreEqual(displayBuffer[0, 0].FG, default(ConsoleColor), "expected BG to be default");
            Assert.AreEqual(displayBuffer[0, 0].BG, default(ConsoleColor), "expected FG to be default");

            const char expectedG = '!';
            const ConsoleColor expectedFG = ConsoleColor.Blue;
            const ConsoleColor expectedBG = ConsoleColor.Cyan;
            displayBuffer[0,0] = new Glyph(expectedG, expectedFG, expectedBG);

            Assert.AreEqual(displayBuffer[0, 0].G, expectedG, "unexpected G");
            Assert.AreEqual(displayBuffer[0, 0].FG, expectedFG, "unexpected FG");
            Assert.AreEqual(displayBuffer[0, 0].BG, expectedBG, "unexpected BG"); 
        }
    }
}
