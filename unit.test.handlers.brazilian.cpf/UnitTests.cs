using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace handlers.brazilian
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestIsValidWithoutFormatting()
        {
            string cpfToTest = "54349534288";

            Assert.IsTrue(Cpf.IsValid(cpfToTest));
        }

        [TestMethod]
        public void TestIsValidWithFormatting()
        {
            string cpfToTest = "543.495.342-88";

            Assert.IsTrue(Cpf.IsValid(cpfToTest));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Invalid argument length [10].")]
        public void TestCheckSmallerNumber()
        {
            string cpfToTest = "543.495.342-8";

            Cpf.Check(cpfToTest);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Invalid argument length [11].")]
        public void TestCheckBiggerNumber()
        {
            string cpfToTest = "1543.495.342-88";

            Cpf.Check(cpfToTest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid CPF number.")]
        public void TestCheckWrongNumber()
        {
            string cpfToTest = "543.493.342-88";

            Cpf.Check(cpfToTest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Value cannot be null.")]
        public void TestFormatWithValueNull()
        {
            string cpfToTest = null;

            Cpf.Format(cpfToTest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Value cannot be empty.")]
        public void TestFormatWithValueEmpty()
        {
            string cpfToTest = "";

            Cpf.Format(cpfToTest);
        }

        [TestMethod]
        public void TestFormat1()
        {
            string cpfToTest = "1";

            Assert.AreEqual("000.000.000-01", Cpf.Format(cpfToTest));
        }

        [TestMethod]
        public void TestFormat2()
        {
            string cpfToTest = "54349534288";

            Assert.AreEqual("543.495.342-88", Cpf.Format(cpfToTest));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid CPF number.")]
        public void TestFormat3()
        {
            string cpfToTest = "54349534278";

            Cpf.Format(cpfToTest, true);
        }
    }
}
