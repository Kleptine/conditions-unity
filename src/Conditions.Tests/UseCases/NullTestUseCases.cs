using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Conditions.Tests.UseCases
{
    [TestClass]
    public class NullTestUseCases
    {
        [TestMethod]
        [Description("Throws no exceptions in normal case.")]
        public void CheckBasicNoExceptions()
        {
            object o = new object();
            Condition.Requires(o).IsNotNull();
        }

        [TestMethod]
        [Description("Use Case code should match with use of IsNotNull.")]
        public void CheckIsNotNull01()
        {
            object param = new object();

            UseCaseTestHelper.Test(() =>
            {
                // Use Case: this is what the user would write without conditions.
                if (param == null)
                {
                    throw new ArgumentNullException("param");
                }
            },
                () =>
                {
                    // This is what the user should write with conditions.
                    Condition.Requires(param, "param").IsNotNull();
                });
        }

        [TestMethod]
        [Description("Use Case code should match with use of IsNotNull.")]
        public void CheckIsNotNull02()
        {
            object param = null;

            UseCaseTestHelper.Test(() =>
            {
                // Use Case: this is what the user would write without conditions.
                if (param == null)
                {
                    throw new ArgumentNullException("param");
                }
            },
                () =>
                {
                    // This is what the user should write with conditions.
                    Condition.Requires(param, "param").IsNotNull();
                });
        }

        [TestMethod]
        [Description("Use Case code should match with use of IsNull.")]
        public void CheckIsNull01()
        {
            object param = new object();

            UseCaseTestHelper.Test(() =>
            {
                // Use Case: this is what the user would write without conditions.
                if (param != null)
                {
                    throw new ArgumentException("param should not be null.", "param");
                }
            },
                () =>
                {
                    // This is what the user should write with conditions.
                    Condition.Requires(param, "param").IsNull();
                });
        }

        [TestMethod]
        [Description("Use Case code should match with use of IsNull.")]
        public void CheckIsNull02()
        {
            object param = null;

            UseCaseTestHelper.Test(() =>
            {
                // Use Case: this is what the user would write without conditions.
                if (param != null)
                {
                    throw new ArgumentException("param should not be null.", "param");
                }
            },
                () =>
                {
                    // This is what the user should write with conditions.
                    Condition.Requires(param, "param").IsNull();
                });
        }
    }
}