using NUnit.Framework;
using Services.TrilaterationService;
using Services.TrilaterationService.Models;
using System;
using System.Collections.Generic;

namespace Test
{
    public class ServicesTrilaterationTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        

        [Test]
        public void ValidTest()
        {
            var service = new TrilaterationService();
            var trilaterationDataList = new List<TrilatirationData>();

            var p1 = new double[] { 1, 1 };
            var p2 = new double[] { 3, 1 };
            var p3 = new double[] { 2, 2 };            

            trilaterationDataList.Add(new TrilatirationData(p1, 1));
            trilaterationDataList.Add(new TrilatirationData(p2, 1));
            trilaterationDataList.Add(new TrilatirationData(p3, 1));

            double[] expectedPosition = new double[] { 2.0, 1.0 };

            double[] a = service.GetPosition(trilaterationDataList);

            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(expectedPosition[i], a[i], 0.00001);
            }
            
        }

        [Test]
        public void Valid2Test()
        {
            var service = new TrilaterationService();

            var p1 = new double[] { 0, 0 };
            var p2 = new double[] { -1, 0 };
            var p3 = new double[] { 0, -1 };
            
            var trilaterationDataList = new List<TrilatirationData>();
            var t = Math.Sqrt(2);
            trilaterationDataList.Add(new TrilatirationData(p1, t));
            trilaterationDataList.Add(new TrilatirationData(p2, 1));
            trilaterationDataList.Add(new TrilatirationData(p3, 1));

            double[] expectedPosition = new double[] { -1.0, -1.0 };

            double[] a = service.GetPosition(trilaterationDataList);

            Assert.IsNotNull(a);

            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(expectedPosition[i], a[i], 0.00001);
            }

        }

        [Test]
        public void TwoPositionsTest()
        {
            var service = new TrilaterationService();

            var p1 = new double[] { 1, 1 };
            var p2 = new double[] { 2, 1 };
            var d1 = 1;
            var d2 = 1;
            var trilaterationDataList = new List<TrilatirationData>();

            trilaterationDataList.Add(new TrilatirationData(p1, d1));
            trilaterationDataList.Add(new TrilatirationData(p2, d2));

            double[] a = service.GetPosition(trilaterationDataList);

            Assert.IsNull(a);
        }


    }
}