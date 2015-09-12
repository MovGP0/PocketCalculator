using NUnit.Framework;
using TechTalk.SpecFlow;

namespace PocketCalculator.Tests
{
    [Binding]
    public sealed class ParsingNumbers
    {
        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(string input)
        {
            ScenarioContext.Current["input"] = input;
        }

        [When(@"I parse the input")]
        public void WhenIParseTheInput()
        {
            var input = (string)ScenarioContext.Current["input"];
            var result = Parser.calculator(input);
            ScenarioContext.Current["result"] = result;
        }

        [Then(@"the result should be the number (.*)")]
        public void ThenTheResultShouldBeTheNumber(decimal expectedResult)
        {
            var result = (decimal)ScenarioContext.Current["result"];
            Assert.That(result.IsApproximatly(expectedResult));
        }
    }
}
