using System.Collections.Generic;
using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class DialogueTailTests
    {
        [Test]
        public void DropSpoken_KeepsTheLastLine()
        {
            var lines = new List<string> { "前一句", "最后一句" };
            DialogueTail.DropSpoken(lines);
            DialogueTail.DropSpoken(lines);
            Assert.AreEqual(1, lines.Count);
            Assert.AreEqual("最后一句", lines[0]);
        }
    }
}
