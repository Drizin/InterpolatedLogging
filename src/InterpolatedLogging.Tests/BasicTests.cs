using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace InterpolatedLogging.Tests
{
    public class BasicTests
    {

        [Test]
        public void Test1()
        {

            string name = "RickDrizin";
            long elapsedTime = 315;
            int orderId = 1001;
            DateTime now = DateTime.Now;
            var msg = new StructuredLogMessage($"User '{new { UserName = name }}' created Order {new { OrderId = orderId}} at {new { Date = now }}, operation took {new { OperationElapsedTime = elapsedTime }}ms");

            Assert.AreEqual("User '{UserName}' created Order {OrderId} {Date}, operation took {OperationElapsedTime}ms", msg.MessageTemplate);
            Assert.AreEqual(4, msg.Properties.Length);
            Assert.AreEqual(name, msg.Properties[0]);
            Assert.AreEqual(orderId, msg.Properties[1]);
            Assert.AreEqual(now, msg.Properties[2]);
            Assert.AreEqual(elapsedTime, msg.Properties[3]);
        }

        [Test]
        public void Test2()
        {

            var input = new { Latitude = 25, Longitude = 134 };
            var time = 34;

            // Serilog has this @ destructuring operator, which in plain Serilog should be used as " {@variable} " 
            // but here in interpolated strings you should use " @{variable} " 
            var msg = new StructuredLogMessage($"Processed @{ new { SensorInput = input }} in { new { TimeMS = time}:000} ms.");

            Assert.AreEqual("Processed {@SensorInput} in {TimeMS:000} ms.", msg.MessageTemplate);
            //Assert.AreEqual("Processed {SensorInput} in {TimeMS:000} ms.", msg.MessageTemplate);
            Assert.AreEqual(2, msg.Properties.Length);
            Assert.AreEqual(input, msg.Properties[0]);
            Assert.AreEqual(time, msg.Properties[1]);
        }


    }
}
