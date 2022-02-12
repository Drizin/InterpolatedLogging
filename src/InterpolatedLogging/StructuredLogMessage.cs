using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace InterpolatedLogging
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class StructuredLogMessage
    {
        #region Properties
        /// <summary>
        /// Template to be passed to your Logging library.
        /// Parsed with property names instead of positions. 
        /// E.g. "User '{UserName}' logged in at {Date}, operation took {OperationElapsedTime}ms".
        /// </summary>
        public string MessageTemplate { get { Parse(); return _messageTemplate; } }

        /// <summary>
        /// Property values to be passed to your Logging library
        /// </summary>
        public object[] Properties { get { Parse(); return _properties; } }
        #endregion

        #region ctor
        /// <inheritdoc/>
        public StructuredLogMessage(FormattableString fs)
        {
            _fs = fs; // just save it for later (lazy rendering) - depending on levels we might not even have to render this message
        }
        #endregion

        #region Private Members
        private FormattableString _fs;
        private string _messageTemplate;
        private object[] _properties;
        StringBuilder sb = new StringBuilder();
        List<object> propertiesLst = new List<object>();
        int unamedProperties = 0;
        #endregion


        #region Static/Constant
        private static Regex _formattableArgumentRegex = new Regex(
              "(?<PrefixModifier>@)?{(?<ArgPos>\\d*)(:(?<Format>[^}]*))?}",
            RegexOptions.IgnoreCase
            | RegexOptions.Singleline
            | RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled
            );
        private static Regex colonSplit = new Regex("(^|:)(?<ArgFormat>(\\'([^'])*'|[^:']*)*)",
            RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled
            );

        #endregion

        #region Parse

        private void Parse()
        {
            // already parsed?
            if (_messageTemplate != null)
                return;
            ParseInner(_fs.Format, _fs.GetArguments());

            _messageTemplate = sb.ToString();
            _properties = propertiesLst.ToArray();

        }

        private void ParseInner(string format, object[] arguments)
        {
            if (string.IsNullOrEmpty(format))
                return;

            var matches = _formattableArgumentRegex.Matches(format);
            int currentBlockEndPos = 0;
            int previousBlockEndPos = 0;
            for (int i = 0; i < matches.Count; i++)
            {
                // unescape escaped curly braces
                previousBlockEndPos = currentBlockEndPos;
                currentBlockEndPos = matches[i].Index + matches[i].Length;
                string currentTextBlock = format.Substring(previousBlockEndPos, matches[i].Index - previousBlockEndPos).Replace("{{", "{").Replace("}}", "}");

                // arguments[i] may not work because same argument can be used multiple times
                int argPos = int.Parse(matches[i].Groups["ArgPos"].Value);
                string argFormat = matches[i].Groups["Format"].Value;

                // Serilog @ destructuring operator
                string prefixModifier = matches[i].Groups["PrefixModifier"].Value;

                sb.Append(currentTextBlock);

                // Split multiple formats by colon (:), and accept literal colons surrounded by single quotes
                //List<string> argFormats = argFormat.Split(new char[] { ':' }).Select(f => f.Replace("':'", ":")).ToList();
                List<string> argFormats = string.IsNullOrEmpty(argFormat) ? new List<string>() :
                    colonSplit.Matches(argFormat).Cast<Match>()
                    .Select(m => m.Groups["ArgFormat"].Value.Replace("':'", ":")).ToList();

                object arg = arguments[argPos];
                if (argFormats.Contains("raw")) // interpolated arguments which are supposed to be rendered as raw strings, not as isolated properties
                {
                    sb.Append(arg);
                    continue;
                }
                if (arg is FormattableString fsArg) // Support nested FormattableString
                {
                    ParseInner(fsArg.Format, fsArg.GetArguments());
                    continue;
                }

                Type argType = arg.GetType();
                PropertyInfo[] props;

                //if (argType.IsGenericType && argType.GetGenericTypeDefinition() == typeof(NamedProperty<>))
                if (argType.IsSubclassOf(typeof(NamedProperty)))
                {
                    sb.Append("{" + prefixModifier + ((NamedProperty)arg).Name + (argFormat.Length > 0 ? ":" + argFormat : "") + "}");
                    propertiesLst.Add(((NamedProperty)arg).Value);
                    continue;
                }

                // anonymous type with single property - get property name
                // e.g: " User {new { UserName = user }} " 
                if (argType.Name.StartsWith("<>f__AnonymousType") && (props = argType.GetProperties()) != null && props.Length == 1)
                {
                    sb.Append("{" + prefixModifier + props[0].Name + (argFormat.Length > 0 ? ":" + argFormat : "") + "}");
                    propertiesLst.Add(props[0].GetValue(arg));
                    continue;
                }

                // Format contains Property Name
                // e.g: " User {user:UserName} "
                if (argFormats.Count == 1)
                {
                    sb.Append("{" + prefixModifier + argFormat.Trim(' ') + "}");
                    propertiesLst.Add(arg);
                    continue;
                }

                // Format contains Property Name and format
                // e.g: " Date {now:Timestamp:yyyy-MM-dd HH:mm:sss} "
                if (argFormats.Count == 2)
                {
                    sb.Append("{" + prefixModifier + argFormats[0].Trim(' ') + ":" + argFormats[1] + "}");
                    propertiesLst.Add(arg);
                    continue;
                }

                sb.Append("{" + prefixModifier + (unamedProperties++).ToString() + (argFormat.Length > 0 ? ":" + argFormat : "") + "}");
                propertiesLst.Add(arg);

            }
            string lastPart = format.Substring(currentBlockEndPos).Replace("{{", "{").Replace("}}", "}");
            sb.Append(lastPart);
        }
        #endregion

        #region Misc

        private string DebuggerDisplay 
        { 
            get 
            {
                Parse();
                //return $"\"{_messageTemplate}\": {}";
                StringBuilder sb = new StringBuilder($"\"{_messageTemplate}\"");
                for (int i = 0; i < _properties.Length; i++)
                    System.Diagnostics.Debug.WriteLine($"Arg[{i}]: {_properties[i]}");
                return sb.ToString();
            } 
        }
        #endregion

    }
}
