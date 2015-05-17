using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NgValidationFor.Core;

namespace NgValidationFor.UnitTests
{
    public class DummyModel
    {
        [Required]
        public string RequiredField { get; set; }

        [StringLength(5)]
        public string NgMaxLengthField { get; set; }

        [StringLength(7, MinimumLength = 3)]
        public string NgMaxAndMinLengthField { get; set; }
    }

    [TestClass]
    public class NgValidationForUnitTests
    {
        [TestMethod]
        public void Given_RequiredAttribute_NgValidationFor_returns_required()
        {
            var dummyModel = new DummyModel { RequiredField = "JKS" };

            var result = NgValidationFor.Core.NgValidationForHelper.NgValidationFor(dummyModel, x => x.RequiredField);

            Assert.AreEqual("required=\"required\"", result);
        }

        [TestMethod]
        public void Given_StringLengthAttribute_with_max_length_NgValidationFor_returns_ng_maxlength()
        {
            var dummyModel = new DummyModel { NgMaxLengthField = "JKS" };

            var result = NgValidationFor.Core.NgValidationForHelper.NgValidationFor(dummyModel, x => x.NgMaxLengthField);

            Assert.AreEqual("ng-maxlength=\"5\"", result);
        }

        [TestMethod]
        public void Given_StringLengthAttribute_with_max_length_and_with_max_length_NgValidationFor_returns_ng_maxlength_and_ng_minlength()
        {
            var dummyModel = new DummyModel { NgMaxAndMinLengthField = "JKS" };

            var result = NgValidationFor.Core.NgValidationForHelper.NgValidationFor(dummyModel, x => x.NgMaxAndMinLengthField);

            Assert.IsTrue(result.Contains("ng-maxlength=\"7\""));
            Assert.IsTrue(result.Contains("ng-minlength=\"3\""));
        }
    }
}
