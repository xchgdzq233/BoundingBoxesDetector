using BoundingBoxesDetector;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BoundingBoxesDetectorTestApp
{
    [TestFixture]
    public class UnitTest1
    {
        #region "development testing"
        //[Test]
        //public void TestGetInputMap()
        //{
        //    char[][] inputMap = BoundingBoxesUtil.Instance.GetInputMap(@"C:\Users\Cui\source\repos\BoundingBoxesDetector\BoundingBoxesDetectorTestApp\groups.txt");

        //    Assert.That(inputMap, Is.EqualTo(new char[][] {
        //        new char[] { '*', '*', '-', '-', '-', '-', '-', '-', '-', '*', '*', '*' },
        //        new char[] { '-', '*', '-', '-', '*', '*', '-', '-', '*', '*', '*', '-' },
        //        new char[] { '-', '-', '-', '-', '-', '*', '*', '*', '-', '-', '*', '*' },
        //        new char[] { '-', '-', '-', '-', '-', '-', '-', '*', '*', '*', '-', '-' }
        //    }));
        //}

        //[Test]
        //public void TestGetAllBoundingBoxes()
        //{
        //    char[][] inputMap = BoundingBoxesUtil.Instance.GetInputMap(@"C:\Users\Cui\source\repos\BoundingBoxesDetector\BoundingBoxesDetectorTestApp\groups.txt");
        //    List<BoundingBox> candidates = BoundingBoxesUtil.Instance.GetAllBoundingBoxes(inputMap);

        //    Assert.That(candidates, Is.EquivalentTo(new List<BoundingBox>() {
        //        new BoundingBox(1, 1, 2, 2),
        //        new BoundingBox(2, 5, 4, 10),
        //        new BoundingBox(1, 9, 3, 12)
        //    }));
        //}

        //[Test]
        //public void TestGetLargestValidBoundingBoxesFromCandidates()
        //{
        //    char[][] inputMap = BoundingBoxesUtil.Instance.GetInputMap(@"C:\Users\Cui\source\repos\BoundingBoxesDetector\BoundingBoxesDetectorTestApp\groups.txt");
        //    List<BoundingBox> candidates = BoundingBoxesUtil.Instance.GetAllBoundingBoxes(inputMap);
        //    List<BoundingBox> validBoxes = BoundingBoxesUtil.Instance.GetLargestValidBoundingBoxesFromCandidates(candidates);

        //    Assert.That(validBoxes, Is.EquivalentTo(new List<BoundingBox>() {
        //        new BoundingBox(1, 1, 2, 2)
        //    }));
        //}
        #endregion

        [Test]
        public void TestBoundingBoxesDetector()
        {

        }
    }
}
