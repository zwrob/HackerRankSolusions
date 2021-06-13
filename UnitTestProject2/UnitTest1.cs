using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperReducedStringLib;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
         //   Assert.AreEqual("abd",Solution.superReducedString("aaabccddd"));
            Assert.AreEqual("Empty String", Solution.superReducedString("baab"));
        }
    }
}
