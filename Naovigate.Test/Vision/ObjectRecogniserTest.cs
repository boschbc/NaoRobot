using System;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

using NUnit.Framework;

using Naovigate.Vision;

namespace Naovigate.Test.Vision
{[TestFixture]
    class ObjectRecogniserTest
    {
        ObjectRecogniser recogniser;

        [SetUp]
        public void SetUp()
        {
            recogniser = new Naovigate.Vision.ObjectRecogniser();
        }

        [Test]
        public void EmptyImageTest()
        {
            Image<Gray, Byte> img = new Image<Gray, Byte>(400,400);
            Assert.AreEqual(new Rectangle(0, 0, 0, 0), recogniser.getBoundingBox(img));
        }
        
        [Test]
        public void TooSmallTest()
        {
            Image<Gray, Byte> img = new Image<Gray, Byte>(400, 400);
            img.Draw(new Rectangle(150, 100, 5, 5), new Gray(255), -1);
            Assert.AreEqual(new Rectangle(0, 0, 0, 0), recogniser.getBoundingBox(img));
        }

        [Test]
        public void FindOneTest()
        {
            Image<Gray, Byte> img = new Image<Gray, Byte>(400, 400);
            img.Draw(new Rectangle(150,100,10,10),new Gray(255), -1);
            Assert.AreEqual(new Rectangle(150, 100, 10, 10), recogniser.getBoundingBox(img));
        }

        [Test]
        public void FindTwoTest()
        {
            Image<Gray, Byte> img = new Image<Gray, Byte>(400, 400);
            img.Draw(new Rectangle(100, 100, 50, 50), new Gray(255), -1);
            img.Draw(new Rectangle(200, 200, 100, 100), new Gray(255), -1);
            Assert.AreEqual(new Rectangle(200, 200, 100, 100), recogniser.getBoundingBox(img));
        }
    }
}
