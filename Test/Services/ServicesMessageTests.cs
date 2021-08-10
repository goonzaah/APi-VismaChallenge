using NUnit.Framework;
using Services.MessageService;
using System;
using System.Collections.Generic;

namespace Test
{
    public class ServicesMessageTests
    {
        private DiscountService _service;
        [SetUp]
        public void Setup()
        {
            _service = new DiscountService();
        }

        [Test]
        public void ValidMessageTest()
        {
            
            var messages = new List<string[]>();
            messages.Add(new string[4] { "Hola", "", "", "Juan" });
            messages.Add(new string[4] { "Hola", "", "estas", "" });
            messages.Add(new string[4] { "", "", "", "Juan" });
            messages.Add(new string[4] { "", "como", "", "Juan" });

            var result = _service.JoinMessages(messages);

            Assert.AreEqual(result, "Hola como estas Juan");
        }

        [Test]
        public void InconsistencyMessagesTest()
        {
            var messages = new List<string[]>();

            messages.Add(new string[4] { "Hola", "", "", "Juan" });
            messages.Add(new string[4] { "NoSense", "", "estas", "" });
            messages.Add(new string[4] { "", "", "", "Juan" });
            messages.Add(new string[4] { "", "como", "", "Juan" });

            var result = _service.JoinMessages(messages);

            Assert.AreEqual(result, null);
        }

        [Test]
        public void DifferntLengthListTest()
        {
            var messages = new List<string[]>();

            messages.Add(new string[2] { "Hola", "" });
            messages.Add(new string[4] { "Hola", "", "estas", "" });
            messages.Add(new string[4] { "", "", "", "Juan" });
            messages.Add(new string[3] { "", "como", ""});

            var result = _service.JoinMessages(messages);

            Assert.AreEqual(result, null);
        }
    }
}