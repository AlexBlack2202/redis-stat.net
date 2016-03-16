// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WithOptionsAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   The with options attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace redis_stat.net.common.Attributes
{
    using System;
    using System.Reflection;

    using Autofac;
    using Autofac.Extras.AttributeMetadata;

    using redis_stat.net.common.Models;

    /// <summary>The with options attribute.</summary>
    public sealed class WithOptionsAttribute : ParameterFilterAttribute
    {
        #region Public Methods and Operators

        /// <summary>Resolves a constructor parameter based on keyed service requirements.
        /// </summary>
        /// <param name="parameter">The specific parameter being resolved that is marked with this attribute.</param>
        /// <param name="context">The component context under which the parameter is being resolved.</param>
        /// <returns>The instance of the object that should be used for the parameter value.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="parameter"/> or <paramref name="context"/> is <see langword="null"/>.</exception>
        public override object ResolveParameter(ParameterInfo parameter, IComponentContext context)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var options = context.Resolve<IOptions>();
            var key = "console";
            if (!string.IsNullOrEmpty(options.Csv))
            {
                key = "csv";
            }
            else if (options.Daemon)
            {
                key = "daemon";
            }

            object obj;
            context.TryResolveKeyed(key, parameter.ParameterType, out obj);
            return obj;
        }

        #endregion
    }
}