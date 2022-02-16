using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using static InterpolatedLogging.JoinableStrings;

namespace InterpolatedLogging.Tests
{
    public class JoinableStringTests
    {

        [Test]
        public void JoinableString_6_strings()
        {
            string name = "RickDrizin";
            int orderId = 1001;

            var combined = JS()
                + $"User '{name:UserName}'\n"
                + $"User '{name:UserName}'\n"
                + $"User '{name:UserName}'\n"
                + $"created Order {orderId:OrderId}"
                + $"created Order {orderId:OrderId}"
                + $"created Order {orderId:OrderId}";

            Assert.AreEqual("User '{0:UserName}'\nUser '{1:UserName}'\nUser '{2:UserName}'\ncreated Order {3:OrderId}created Order {4:OrderId}created Order {5:OrderId}", combined.Format);
            var msg = new StructuredLogMessage(combined);
            Assert.AreEqual("User '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\ncreated Order {OrderId}created Order {OrderId}created Order {OrderId}", msg.MessageTemplate);
            Assert.AreEqual(6, msg.Properties.Length);
            Assert.AreEqual(name, msg.Properties[0]);
            Assert.AreEqual(name, msg.Properties[1]);
            Assert.AreEqual(name, msg.Properties[2]);
            Assert.AreEqual(orderId, msg.Properties[3]);
            Assert.AreEqual(orderId, msg.Properties[4]);
            Assert.AreEqual(orderId, msg.Properties[5]);
        }

        [Test]
        public void JoinableString_11_strings()
        {
            string name = "RickDrizin";

            var combined = JS()
                + $"User '{name:UserName}'\n"
                + $"User '{name:UserName}'\n"
                + $"User '{name:UserName}'\n"
                + $"User '{name:UserName}'\n"
                + $"User '{name:UserName}'\n"
                + $"User '{name:UserName}'\n"
                + $"User '{name:UserName}'\n"
                + $"User '{name:UserName}'\n"
                + $"User '{name:UserName}'\n"
                + $"User '{name:UserName}'\n"
                + $"User '{name:UserName}'\n";
            
            Assert.AreEqual("User '{0:UserName}'\nUser '{1:UserName}'\nUser '{2:UserName}'\nUser '{3:UserName}'\nUser '{4:UserName}'\nUser '{5:UserName}'\nUser '{6:UserName}'\nUser '{7:UserName}'\nUser '{8:UserName}'\nUser '{9:UserName}'\nUser '{10:UserName}'\n", combined.Format);
            var msg = new StructuredLogMessage(combined);

            Assert.AreEqual("User '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\n", msg.MessageTemplate);
            Assert.AreEqual(11, msg.Properties.Length);
            Assert.AreEqual(name, msg.Properties[0]);
            Assert.AreEqual(name, msg.Properties[1]);
            Assert.AreEqual(name, msg.Properties[2]);
        }

        [Test]
        public void CombineStrings4()
        {
            string name = "RickDrizin";

            var combined = JS()
                + $"User '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\n"
                + $"User '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\nUser '{name:UserName}'\n";

            Assert.AreEqual("User '{0:UserName}'\nUser '{1:UserName}'\nUser '{2:UserName}'\nUser '{3:UserName}'\nUser '{4:UserName}'\nUser '{5:UserName}'\nUser '{6:UserName}'\nUser '{7:UserName}'\nUser '{8:UserName}'\nUser '{9:UserName}'\nUser '{10:UserName}'\nUser '{11:UserName}'\nUser '{12:UserName}'\nUser '{13:UserName}'\nUser '{14:UserName}'\nUser '{15:UserName}'\nUser '{16:UserName}'\nUser '{17:UserName}'\nUser '{18:UserName}'\nUser '{19:UserName}'\nUser '{20:UserName}'\nUser '{21:UserName}'\n", combined.Format);
            var msg = new StructuredLogMessage(combined);


            Assert.AreEqual("User '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\nUser '{UserName}'\n", msg.MessageTemplate);
            Assert.AreEqual(22, msg.Properties.Length);
            Assert.AreEqual(name, msg.Properties[0]);
            Assert.AreEqual(name, msg.Properties[1]);
            Assert.AreEqual(name, msg.Properties[2]);
        }
    }

    

}
