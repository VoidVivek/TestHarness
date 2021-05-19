using Aveva.Engineering.PartsBreakdown.Repositories;
using Aveva.Engineering.PartsBreakdown.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

namespace PartsBreakdown.Tests
{
    /// <summary>
    /// Unit test for PartsBreakdownService.
    /// </summary>
    [TestClass]
    public class PartsBreakdownServiceTests
    {
        [TestMethod]
        public void IsSelectionValid_When_Selection_Has_Multiple_Classes_Returns_False()
        {
            var mockRepository = Substitute.For<IPartsBreakdownRepository>();
            mockRepository.GetSelectionClassCount().Returns(2);
            var service = new PartsBreakdownService(mockRepository);
            bool val = service.IsSelectionValid();

            Assert.IsFalse(val);
        }

        [TestMethod]
        public void IsSelectionValid_When_Selection_Has_No_Classes_Returns_False()
        {
            var mockRepository = Substitute.For<IPartsBreakdownRepository>();
            mockRepository.GetSelectionClassCount().Returns(0);
            var service = new PartsBreakdownService(mockRepository);
            bool val = service.IsSelectionValid();

            Assert.IsFalse(val);
        }

        [TestMethod]
        public void IsSelectionValid_When_Selection_Has_One_Class_Returns_True()
        {
            var mockRepository = Substitute.For<IPartsBreakdownRepository>();
            mockRepository.GetSelectionClassCount().Returns(1);
            var service = new PartsBreakdownService(mockRepository);
            bool val = service.IsSelectionValid();

            Assert.IsTrue(val);
        }
    }
}