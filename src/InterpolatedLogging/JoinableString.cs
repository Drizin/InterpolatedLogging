using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InterpolatedLogging
{
    /// <summary>
    /// Extends FormattableString (interpolated string) but allows to append new FormattableStrings using overloaded operators + and +=.
    /// </summary>
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

        private string ShiftArgs(int shift, string newFormat)
        {
            if (shift == 0 || string.IsNullOrEmpty(newFormat))
                return newFormat;

            // if current string has N elements, new.Format should change from {0} to {N}, {1} to {N+1}, etc... 

            string str = _formattableArgumentRegex.Replace(newFormat, match => {
                Group argPosMatch = match.Groups["ArgPos"];
                int argPos = int.Parse(argPosMatch.Value);
                string replace = (argPos + shift).ToString();
                string ret = string.Format("{0}{1}{2}", match.Value.Substring(0, argPosMatch.Index - match.Index), replace, match.Value.Substring(argPosMatch.Index - match.Index + argPosMatch.Length));
                return ret;
            });

            return str;
        }

        #region Static/Constant
        private static Regex _formattableArgumentRegex = new Regex(
              "(?<PrefixModifier>@)?{(?<ArgPos>\\d*)(:(?<Format>[^}]*))?}",
            RegexOptions.IgnoreCase
            | RegexOptions.Singleline
            | RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled
            );
        #endregion

    }

    /// <summary>
    /// Factory to create a JoinableString (a interpolated string that allows to append new interpolated strings to the end)
    /// You can either add "using static InterpolatedLogging.JoinableStrings;" and invoke directly JS() constructor,
    /// or use "using InterpolatedLogging;" and "new JoinableString()".
    /// </summary>
    public class JoinableStrings
    {
        /// <summary>
        /// Creates a JoinableString - a class where we can append multiple interpolated strings.
        /// </summary>
        public static JoinableString JS()
        {
            return new JoinableString();
        }
    }


}
