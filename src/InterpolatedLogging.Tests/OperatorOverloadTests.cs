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
    public class LogFormattableStringTests
    {

        [Test]
        public void CombineStrings()
        {
            string name = "RickDrizin";
            int orderId = 1001;

            var msg = new StructuredLogMessage(new JoinableString()
                + $"User '{name:UserName}'\n"
                + $"created Order {orderId:OrderId}"
            );

            Assert.AreEqual("User '{UserName}'\ncreated Order {OrderId}", msg.MessageTemplate);
            Assert.AreEqual(2, msg.Properties.Length);
            Assert.AreEqual(name, msg.Properties[0]);
            Assert.AreEqual(orderId, msg.Properties[1]);
        }
    }

    public class JoinableString : FormattableString
    {
        private readonly string _format;
        private readonly object[] _arguments;

        public JoinableString()
        {
            _format = "";
            _arguments = new object[0];
        }
        public JoinableString(FormattableString formattableString)
        {
            _format = formattableString.Format;
            _arguments = formattableString.GetArguments();
        }
        public JoinableString(JoinableString a, JoinableString b)
        {
            var args = a.GetArguments().ToList();
            _format = a.Format + ShiftArgs(args.Count, b.Format);
            args.AddRange(b.GetArguments());
            _arguments = args.ToArray();
        }

        public override string Format => _format;
        public override object[] GetArguments() => _arguments;
        public override int ArgumentCount => _arguments.Length;
        public override object GetArgument(int index) => _arguments[index];

        public override string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, _format, _arguments);
        }

        public static JoinableString operator +(JoinableString a, JoinableString b) => new JoinableString(a, b);
        public static JoinableString operator +(JoinableString a, FormattableString b) => new JoinableString(a, new JoinableString(b));

        private string ShiftArgs(int shift, string str)
        {
            // TODO: b.Format should change {0} to {N}, {1} to {N+1}, etc... 
            if (shift == 3)
            {
                str = str.Replace("{2", "{5");
                str = str.Replace("{1", "{4");
                str = str.Replace("{0", "{3");
            }
            else if (shift == 2)
            {
                str = str.Replace("{1", "{3");
                str = str.Replace("{0", "{2");
            }
            else if (shift == 1)
            {
                str = str.Replace("{0", "{1");
            }
            return str;
        }
    }
    public static class Extensions
    {
        public static JoinableString AsJoinableString(FormattableString fs)
        {
            return new JoinableString(fs);
        }
    }


}
