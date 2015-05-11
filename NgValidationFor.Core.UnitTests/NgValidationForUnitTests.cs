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
    }

    [TestClass]
    public class NgValidationForUnitTests
    {
        [TestMethod]
        public void NgValidationFor_GetAttributes_returns_required()
        {
            var dummyModel = new DummyModel{ RequiredField = "JKS"};

            var result = NgValidationFor.Core.NgValidationForHelper.NgValidationFor(dummyModel, x => x.RequiredField);
            
            Assert.AreEqual("required=\"required\"", result);
        }
    }
}
