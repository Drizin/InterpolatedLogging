using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using static InterpolatedLogging.NamedProperties;

namespace InterpolatedLogging.Tests
{
    public class BasicTests
    {

        [Test]
        public void ColonSyntax()
        {

            string name = "RickDrizin";
            long elapsedTime = 315;
            int orderId = 1001;
            DateTime now = DateTime.Now;
            var msg = new StructuredLogMessage($"User '{name:UserName}' created Order {orderId:OrderId} at {now:Date}, operation took {elapsedTime:OperationElapsedTime}ms");

            Assert.AreEqual("User '{UserName}' created Order {OrderId} at {Date}, operation took {OperationElapsedTime}ms", msg.MessageTemplate);
            Assert.AreEqual(4, msg.Properties.Length);
            Assert.AreEqual(name, msg.Properties[0]);
            Assert.AreEqual(orderId, msg.Properties[1]);
            Assert.AreEqual(now, msg.Properties[2]);
            Assert.AreEqual(elapsedTime, msg.Properties[3]);
        }

        [Test]
        public void AnonymousSyntax()
        {

            string name = "RickDrizin";
            long elapsedTime = 315;
            int orderId = 1001;
            DateTime now = DateTime.Now;
            var msg = new StructuredLogMessage($"User '{new { UserName = name }}' created Order {new { OrderId = orderId}} at {new { Date = now }}, operation took {new { OperationElapsedTime = elapsedTime }}ms");

            Assert.AreEqual("User '{UserName}' created Order {OrderId} at {Date}, operation took {OperationElapsedTime}ms", msg.MessageTemplate);
            Assert.AreEqual(4, msg.Properties.Length);
            Assert.AreEqual(name, msg.Properties[0]);
            Assert.AreEqual(orderId, msg.Properties[1]);
            Assert.AreEqual(now, msg.Properties[2]);
            Assert.AreEqual(elapsedTime, msg.Properties[3]);
        }

        [Test]
        public void SerilogDestructureAnonymous()
        {

            var input = new { Latitude = 25, Longitude = 134 };
            var time = 34;

            // Serilog has this @ destructuring operator, which in plain Serilog should be used as " {@variable} " 
            // but here in interpolated strings you should use " @{variable} " 
            var msg = new StructuredLogMessage($"Processed @{ new { SensorInput = input }} in { new { TimeMS = time}:000} ms.");

            Assert.AreEqual("Processed {@SensorInput} in {TimeMS:000} ms.", msg.MessageTemplate);
            Assert.AreEqual(2, msg.Properties.Length);
            Assert.AreEqual(input, msg.Properties[0]);
            Assert.AreEqual(time, msg.Properties[1]);
        }
        [Test]
        public void SerilogDestructureNamedPropertySyntax()
        {

            var input = new { Latitude = 25, Longitude = 134 };
            var time = 34;

            // Serilog has this @ destructuring operator, which in plain Serilog should be used as " {@variable} " 
            // but here in interpolated strings you should use " @{variable} " 
            var msg = new StructuredLogMessage($"Processed {NP(input, "@SensorInput")} in {NP(time, "TimeMS"):000} ms.");

            Assert.AreEqual("Processed {@SensorInput} in {TimeMS:000} ms.", msg.MessageTemplate);
            Assert.AreEqual(2, msg.Properties.Length);
            Assert.AreEqual(input, msg.Properties[0]);
            Assert.AreEqual(time, msg.Properties[1]);
        }
        [Test]
        public void SerilogDestructureColonSyntax()
        {

            var input = new { Latitude = 25, Longitude = 134 };
            var time = 34;

            // Serilog has this @ destructuring operator, which in plain Serilog should be used as " {@variable} " 
            // but here in interpolated strings you should use " @{variable} " 
            var msg = new StructuredLogMessage($"Processed {input:@SensorInput} in {time:TimeMS:000} ms.");

            Assert.AreEqual("Processed {@SensorInput} in {TimeMS:000} ms.", msg.MessageTemplate);
            Assert.AreEqual(2, msg.Properties.Length);
            Assert.AreEqual(input, msg.Properties[0]);
            Assert.AreEqual(time, msg.Properties[1]);
        }


        [Test]
        public void ColonSyntax_with_ExplicitFormat()
        {
            DateTime now = DateTime.Now;

            // The format comes after the Property Name, and is forwarded to the template. Colons should be escaped inside single-quotes
            var msg = new StructuredLogMessage($"Date {now:Timestamp:yyyy-MM-dd HH':'mm':'sss}");

            Assert.AreEqual("Date {Timestamp:yyyy-MM-dd HH:mm:sss}", msg.MessageTemplate);
            Assert.AreEqual(1, msg.Properties.Length);
            Assert.AreEqual(now, msg.Properties[0]);
        }

        [Test]
        public void ColonSyntax_Trim_PropName_Whitespace()
        {
            string name = "RickDrizin";
            long elapsedTime = 315;
            int orderId = 1001;
            DateTime now = DateTime.Now;
            var msg = new StructuredLogMessage($"User '{name: UserName}' created Order {orderId: OrderId} at {now : Date}, operation took {elapsedTime : OperationElapsedTime}ms");

            Assert.AreEqual("User '{UserName}' created Order {OrderId} at {Date}, operation took {OperationElapsedTime}ms", msg.MessageTemplate);
            Assert.AreEqual(4, msg.Properties.Length);
            Assert.AreEqual(name, msg.Properties[0]);
            Assert.AreEqual(orderId, msg.Properties[1]);
            Assert.AreEqual(now, msg.Properties[2]);
            Assert.AreEqual(elapsedTime, msg.Properties[3]);
        }

        [Test]
        public void AnonymousSyntax_with_ExplicitFormat()
        {
            DateTime now = DateTime.Now;

            // The format is explicitly forwarded to the template
            var msg = new StructuredLogMessage($"Date {new { Timestamp = now }:yyyy-MM-dd HH:mm:sss}");

            Assert.AreEqual("Date {Timestamp:yyyy-MM-dd HH:mm:sss}", msg.MessageTemplate);
            Assert.AreEqual(1, msg.Properties.Length);
            Assert.AreEqual(now, msg.Properties[0]);
        }

        /*
        [Test]
        public void NamedProperty_ExpressionSyntax()
        {
            DateTime now = DateTime.Now;

            // The format is explicitly forwarded to the template
            var msg = new StructuredLogMessage($"Date {NP(x => now) }");

            Assert.AreEqual("Date {now}", msg.MessageTemplate);
            Assert.AreEqual(1, msg.Properties.Length);
            Assert.AreEqual(now, msg.Properties[0]);
        }
        */

        [Test]
        public void NamedProperty_ExplicitSyntax()
        {
            DateTime now = DateTime.Now;

            // The format is explicitly forwarded to the template
            var msg = new StructuredLogMessage($"Date {NP(now, "Timestamp") }");

            Assert.AreEqual("Date {Timestamp}", msg.MessageTemplate);
            Assert.AreEqual(1, msg.Properties.Length);
            Assert.AreEqual(now, msg.Properties[0]);
        }


        [Test]
        public void PerformanceTests()
        {
            DateTime now = DateTime.Now;
            int iteractions = 10000;

            // Explicit NP syntax
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iteractions; i++)
            {
                var msg = new StructuredLogMessage($"... at {NP(now, "Date")}");
                string output = msg.MessageTemplate;
            }
            System.Diagnostics.Debug.WriteLine($"Syntax 1: {sw.ElapsedMilliseconds}ms");

            // Anonymous syntax
            sw = Stopwatch.StartNew();
            for (int i = 0; i < iteractions; i++)
            {
                var msg = new StructuredLogMessage($"... at {new { Date = now }}");
                string output = msg.MessageTemplate;
            }
            System.Diagnostics.Debug.WriteLine($"Syntax 2: {sw.ElapsedMilliseconds}ms");

            // Colon syntax
            sw = Stopwatch.StartNew();
            for (int i = 0; i < iteractions; i++)
            {
                var msg = new StructuredLogMessage($"...at {now:Date}");
                string output = msg.MessageTemplate;
            }
            System.Diagnostics.Debug.WriteLine($"Syntax 3: {sw.ElapsedMilliseconds}ms");

            // Turns out Expression syntax is quite slow (without any benefit?), so removed.
            // Expression NP syntax
            /*
            sw = Stopwatch.StartNew();
            for (int i = 0; i < iteractions; i++)
            {
                var msg  = new StructuredLogMessage($"... at {NP(x => now)}");
                string output = msg.MessageTemplate;
            }
            System.Diagnostics.Debug.WriteLine($"Syntax 4: {sw.ElapsedMilliseconds}ms");

            // Expression NP syntax without reflecting over propertyName
            sw = Stopwatch.StartNew();
            for (int i = 0; i < iteractions; i++)
            {
                var msg = new StructuredLogMessage($"... at {NP(x => now, "Date")}");
                string output = msg.MessageTemplate;
            }
            System.Diagnostics.Debug.WriteLine($"Syntax 5: {sw.ElapsedMilliseconds}ms");
            */
        }

    }
}
