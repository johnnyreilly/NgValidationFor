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

        [MaxLength(9)]
        public string NgMaxLengthField { get; set; }

        [MinLength(3)]
        public string NgMinLengthField { get; set; }

        [StringLength(5)]
        public string NgMaxStringLengthField { get; set; }

        [StringLength(7, MinimumLength = 3)]
        public string NgMaxAndMinStringLengthField { get; set; }
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
        public void Given_MaxLengthAttribute_with_max_length_NgValidationFor_returns_ng_maxlength()
        {
            var dummyModel = new DummyModel { NgMaxStringLengthField = "JKS" };

            var result = NgValidationFor.Core.NgValidationForHelper.NgValidationFor(dummyModel, x => x.NgMaxLengthField);

            Assert.AreEqual("ng-maxlength=\"9\"", result);
        }

        [TestMethod]
        public void Given_MinLengthAttribute_with_max_length_NgValidationFor_returns_ng_minlength()
        {
            var dummyModel = new DummyModel { NgMaxStringLengthField = "JKS" };

            var result = NgValidationFor.Core.NgValidationForHelper.NgValidationFor(dummyModel, x => x.NgMinLengthField);

            Assert.AreEqual("ng-minlength=\"3\"", result);
        }

        [TestMethod]
        public void Given_StringLengthAttribute_with_max_length_NgValidationFor_returns_ng_maxlength()
        {
            var dummyModel = new DummyModel { NgMaxStringLengthField = "JKS" };

            var result = NgValidationFor.Core.NgValidationForHelper.NgValidationFor(dummyModel, x => x.NgMaxStringLengthField);

            Assert.AreEqual("ng-maxlength=\"5\"", result);
        }

        [TestMethod]
        public void Given_StringLengthAttribute_with_max_length_and_with_max_length_NgValidationFor_returns_ng_maxlength_and_ng_minlength()
        {
            var dummyModel = new DummyModel { NgMaxAndMinStringLengthField = "JKS" };

            var result = NgValidationFor.Core.NgValidationForHelper.NgValidationFor(dummyModel, x => x.NgMaxAndMinStringLengthField);

            Assert.IsTrue(result.Contains("ng-maxlength=\"7\""));
            Assert.IsTrue(result.Contains("ng-minlength=\"3\""));
        }
    }
}
