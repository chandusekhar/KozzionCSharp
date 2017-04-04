using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KozzionGraphicsTest.ImageIO;

namespace KozzionGraphicsTest.ImageIO
{
    [TestClass]
    public class ImageRasterIOTest
    {
        [TestMethod]
        public void TestForm()
        {
            Bitmap image = new Bitmap("D:\\Publications\\Abstracts\\Abstract_SPIE\\images\\1.png");
            Assert.AreEqual(1, image.GetPixel(0,0).A);

        }

    }
}
