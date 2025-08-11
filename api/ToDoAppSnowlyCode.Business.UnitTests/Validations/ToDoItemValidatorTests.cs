
using FluentValidation.TestHelper;
using TodoAppSnowlyCode.Business.Validations;
using TodoAppSnowlyCode.Data.Models;

namespace ToDoAppSnowlyCode.Business.UnitTests.Validations
{
    [TestFixture]
    public class ToDoItemValidatorTests
    {
        private ToDoItemValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new ToDoItemValidator();
        }

        private static TestCaseData[] getInvalidTitleTestData()=> 
            [
                new TestCaseData(string.Empty, new string[] {"'Title' must not be empty.", "The length of 'Title' must be at least 3 characters. You entered 0 characters."}).SetName("Empty_Title"),
                new TestCaseData("ab", new string[] {"The length of 'Title' must be at least 3 characters. You entered 2 characters."}).SetName("Short_Title"), // 2, minimum is 3
                new TestCaseData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", new string[] {"The length of 'Title' must be 30 characters or fewer. You entered 31 characters."}).SetName("Long_title") // 31, maximum is 30
            ];

        [TestCaseSource(nameof(getInvalidTitleTestData))]
        public void Title_Invalid_Test(string invalidTitle, string[] errorMessages)
        {
            // Arrange
            var item = new ToDoItem { Title = invalidTitle, IsCompleted = true };

            // Act
            var result = _validator.TestValidate(item);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Count, Is.EqualTo(errorMessages.Count()));
            result.ShouldHaveValidationErrorFor(x => x.Title);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Is.EquivalentTo(errorMessages));
        }

        [Test]
        [TestCase("abc")] // Min allowed length (3)
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // Max allowed length (30)
        public void Title_Valid_Test(string validTitle)
        {
            // Arrange
            var item = new ToDoItem { Title = validTitle, IsCompleted = false };

            // Act
            var result = _validator.TestValidate(item);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.That(result.Errors.Count, Is.EqualTo(0));
        }
    }
}
