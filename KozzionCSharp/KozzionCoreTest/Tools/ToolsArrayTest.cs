using KozzionCore.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCoreTest.Tools
{
    [TestClass]
    public class ToolsCollectionTest
    {
        [TestMethod]
        public void ToolsCollectionCopyRBATest()
        {
            float[,,] source = new float[2, 2, 2];
            source[0, 0, 0] = 0;
            source[0, 0, 1] = 1;
            source[0, 1, 0] = 2;
            source[0, 1, 1] = 3;
            source[1, 0, 0] = 4;
            source[1, 0, 1] = 5;
            source[1, 1, 0] = 6;
            source[1, 1, 1] = 7;
            float[,,] target = new float[2, 2, 2];
            ToolsCollection.CopyRBA(source, target);
            Assert.Equals(target[0, 0, 0], source[0, 0, 0]);
            Assert.Equals(target[0, 0, 1], source[0, 0, 1]);
            Assert.Equals(target[0, 1, 0], source[0, 1, 0]);
            Assert.Equals(target[0, 1, 1], source[0, 1, 1]);
            Assert.Equals(target[1, 0, 0], source[1, 0, 0]);
            Assert.Equals(target[1, 0, 1], source[1, 0, 1]);
            Assert.Equals(target[1, 1, 0], source[1, 1, 0]);
            Assert.Equals(target[1, 1, 1], source[1, 1, 1]);
        }
    }
}
