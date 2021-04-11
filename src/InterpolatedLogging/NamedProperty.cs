using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InterpolatedLogging
{
    /// <summary>
    /// Property Wrapper which contains Property Name and Value
    /// </summary>
    public class NamedProperty
    {
        /// <summary>
        /// Property Value
        /// </summary>
        public virtual object Value { get; set; }

        /// <summary>
        /// Property Name, which is rendered in the template 
        /// </summary>
        public string Name { get; set; }
    }
    
    /// <inheritdoc/>
    public class NamedProperty<T> : NamedProperty
    {
        /// <summary>
        /// Property Value
        /// </summary>
        public new T Value { get { return (T)base.Value; } set { base.Value = value; } }
    }

    /// <summary>
    /// Helpers to create Named Properties (properties with Name and Value).
    /// You can either add "using static InterpolatedLogging.NP;" and invoke directly NamedProperty() methods,
    /// or add "using InterpolatedLogging;" and invoke methods as "NP.NamedProperty()".
    /// </summary>
    public class NamedProperties
    {
        /// <summary>
        /// Created a NamedProperty (property with Name and Value) by explicitly providing its value and name
        /// </summary>
        public static NamedProperty<T> NP<T>(T propertyValue, string propertyName)
        {
            return new NamedProperty<T>() { Name = propertyName, Value = propertyValue };
        }

        /* this expression syntax is slow and has no benefit over other formats */
        /*
        /// <summary>
        /// Created a NamedProperty (property with Name and Value) by providing an expression (x) => variableOrProperty
        /// which will be invoked to get the property value and will be reflected upon to get the property name (unless it's explicitly provided).
        /// </summary>
        /// <param name="propertyName">If this is not provided the property will be named based on the lambda expression (name of variableOrProperty)</param>
        public static NamedProperty<T> NP<T>(Expression<Func<string, T>> f, string propertyName = null)
        {
            T propertyValue = f.Compile().Invoke("");
            propertyName = propertyName ?? (f.Body as MemberExpression).Member.Name;
            return new NamedProperty<T>() { Name = propertyName, Value = propertyValue };
        }
        */
    }

}
