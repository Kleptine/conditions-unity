/* The CuttingEdge.Conditions library enables developers to validate pre- and postconditions in a fluent 
* manner.
* 
* Copyright (C) 2008 S. van Deursen
* 
* To contact me, please visit my blog at http://www.cuttingedge.it/blogs/steven/ 
*
* This library is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser 
* General Public License as published by the Free Software Foundation; either version 2.1 of the License, or
* (at your option) any later version.
*
* This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the 
* implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public
* License for more details.
*/

using System;
using System.Globalization;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CuttingEdge.Conditions.UnitTests.StringTests
{
    /// <summary>
    /// Tests the ValidatorExtensions.StartsWith method.
    /// </summary>
    [TestClass]
    public class StringStartsWithTests
    {
        [TestMethod]
        [Description("Calling StartsWith on string x with 'x StartsWith x' should pass.")]
        public void StartsWithTest01()
        {
            string a = "test";
            Condition.Requires(a).StartsWith(a);
        }

        [TestMethod]
        [Description("Calling StartsWith on string x (\"test\") with 'x StartsWith \"tes\"' should pass.")]
        public void StartsWithTest02()
        {
            string a = "test";
            Condition.Requires(a).StartsWith("tes");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [Description("Calling StartsWith on string x (\"test\") with 'x StartsWith null' should fail.")]
        public void StartsWithTest03()
        {
            string a = "test";
            // A null value will never be found
            Condition.Requires(a).StartsWith(null);
        }

        [TestMethod]
        [Description("Calling StartsWith on string x (\"test\") with 'x StartsWith \"\"' should pass.")]
        public void StartsWithTest04()
        {
            string a = "test";
            // An empty string will always be found
            Condition.Requires(a).StartsWith(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Description("Calling StartsWith on string x (null) with 'x StartsWith \"\"' should fail.")]
        public void StartsWithTest05()
        {
            string a = null;
            // A null string only contains other null strings.
            Condition.Requires(a).StartsWith(String.Empty);
        }

        [TestMethod]
        [Description("Calling StartsWith on string x (\"test\") with 'x StartsWith null' should pass.")]
        public void StartsWithTest06()
        {
            string a = null;
            Condition.Requires(a).StartsWith(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [Description("Calling StartsWith on string x (\"test\") with 'x StartsWith \"test me\"' should fail.")]
        public void StartsWithTest07()
        {
            string a = "test";
            Condition.Requires(a).StartsWith("test me");
        }

        [TestMethod]
        [Description("Calling StartsWith on string x (\"test\") with 'x StartsWith \"test me\"' should fail with a correct exception message.")]
        public void StartsWithTest08()
        {
            string expectedMessage =
                "a should start with 'test me'." + Environment.NewLine +
                TestHelper.ArgumentExceptionParameterName + ": a";

            try
            {
                string a = "test";
                Condition.Requires(a, "a").StartsWith("test me");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(expectedMessage, ex.Message);
            }
        }

        [TestMethod]
        [Description("Calling StartsWith with conditionDescription parameter should pass.")]
        public void StartsWithTest09()
        {
            string a = null;
            Condition.Requires(a).StartsWith(null, string.Empty);
        }

        [TestMethod]
        [Description("Calling a failing StartsWith should throw an Exception with an exception message that contains the given parameterized condition description argument.")]
        public void StartsWithTest10()
        {
            string a = null;
            try
            {
                // A null string is considered to have a length of 0.
                Condition.Requires(a, "a").StartsWith("test", "qwe {0} xyz");
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("qwe a xyz"));
            }
        }

        [TestMethod]
        [Description("Calling StartsWith should be language dependent.")]
        public void StartsWithTest11()
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;

            string a = "hi ya'all";

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");

                // We check this using the Turkish-I problem.
                // see: http://msdn.microsoft.com/en-us/library/ms973919.aspx#stringsinnet20_topic5
                string turkishUpperCase = "Hİ";

                Condition.Requires(a).StartsWith(turkishUpperCase, StringComparison.CurrentCultureIgnoreCase);
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = originalCulture;
            }   
        }
    }
}