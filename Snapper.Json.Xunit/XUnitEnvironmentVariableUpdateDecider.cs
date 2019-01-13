﻿using System.Linq;
using Snapper.Core;

namespace Snapper.Json.Xunit
{
    internal class XUnitEnvironmentVariableUpdateDecider : ISnapUpdateDecider
    {
        private readonly ISnapUpdateDecider _envUpdateDecider;

        public XUnitEnvironmentVariableUpdateDecider()
        {
            _envUpdateDecider = new EnvironmentVariableUpdateDecider();
        }

        public bool ShouldUpdateSnap()
        {
            if (_envUpdateDecider.ShouldUpdateSnap())
                return true;

            var (method, _) = XUnitTestHelper.GetCallingTestInfo();

            var methodHasAttribute = method?.GetCustomAttributes(typeof(UpdateSnapshots), true).Any() ?? false;
            var classHasAttribute = method?.ReflectedType?.GetCustomAttributes(typeof(UpdateSnapshots), true).Any() ?? false;

            return methodHasAttribute || classHasAttribute;
        }
    }
}
