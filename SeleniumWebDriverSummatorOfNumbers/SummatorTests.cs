using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace SeleniumWebDriverSummatorOfNumbers
{
    public class SummatorTests
    {
            IWebDriver driver;
            //private WebDriver driver;

            [SetUp]
            public void OpenBrouser()
            {
                var options = new ChromeOptions();
                
                this.driver = new ChromeDriver(options);

                driver.Manage().Window.Maximize();
                driver.Url = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";
            }

            [TearDown]
            public void CloseBrouser()
            {
                driver.Quit();
            }

        [Test]
        public void Test_Sumator_Check_Title()
        {
            var pageTitle = driver.Title;

            Assert.That("Number Calculator", Is.EqualTo(pageTitle));
        }

        [Test]
        public void Test_Sum_Two_positive_numbers()
        {
            //Act
            var FirstNumber = driver.FindElement(By.Id("number1"));
            FirstNumber.SendKeys("15" + Keys.Enter);

            var Operation = driver.FindElement(By.Id("operation"));
            {
                var dropdown = driver.FindElement(By.Id("operation"));
                dropdown.FindElement(By.XPath("//option[. = '+ (sum)']")).Click();
            }

            var SecondNumber = driver.FindElement(By.Id("number2"));
            SecondNumber.SendKeys("7" + Keys.Enter);

            driver.FindElement(By.Id("calcButton")).Click();

            //Arrange
            var resultText = driver.FindElement(By.CssSelector("#result")).Text;

            //Assert
            Assert.That("Result: 22", Is.EqualTo(resultText));
        }

        [Test]
        public void Test_Add_Two_Numbers_Invalid_Input()
        {
            //Act
            var FirstNumber = driver.FindElement(By.Id("number1"));
            FirstNumber.SendKeys("hello" + Keys.Enter);

            var Operation = driver.FindElement(By.Id("operation"));
            {
                var dropdown = driver.FindElement(By.Id("operation"));
                dropdown.FindElement(By.XPath("//option[. = '+ (sum)']")).Click();
            }

            var SecondNumber = driver.FindElement(By.Id("number2"));
            SecondNumber.SendKeys("" + Keys.Enter);

            driver.FindElement(By.Id("calcButton")).Click();

            //Arrange
            var resultText = driver.FindElement(By.CssSelector("#result")).Text;

            //Assert
            Assert.That("Result: invalid input", Is.EqualTo(resultText));
        }

        [Test]
        public void Test_Subtract_Two_positive_numbers()
        {
            //Act
            var FirstNumber = driver.FindElement(By.Id("number1"));
            FirstNumber.SendKeys("5" + Keys.Enter);

            var Operation = driver.FindElement(By.Id("operation"));
            {
                var dropdown = driver.FindElement(By.Id("operation"));
                dropdown.FindElement(By.XPath("//option[. = '- (subtract)']")).Click();
            }

            var SecondNumber = driver.FindElement(By.Id("number2"));
            SecondNumber.SendKeys("2" + Keys.Enter);

            driver.FindElement(By.Id("calcButton")).Click();

            //Arrange
            var resultText = driver.FindElement(By.CssSelector("#result")).Text;

            //Assert
            Assert.That("Result: 3", Is.EqualTo(resultText));
        }

        [Test]
        public void Test_Multiply_Two_positive_numbers()
        {
            //Act
            var FirstNumber = driver.FindElement(By.Id("number1"));
            FirstNumber.SendKeys("5" + Keys.Enter);

            var Operation = driver.FindElement(By.Id("operation"));
            {
                var dropdown = driver.FindElement(By.Id("operation"));
                dropdown.FindElement(By.XPath("//option[. = '* (multiply)']")).Click();
            }

            var SecondNumber = driver.FindElement(By.Id("number2"));
            SecondNumber.SendKeys("4" + Keys.Enter);

            driver.FindElement(By.Id("calcButton")).Click();

            //Arrange
            var resultText = driver.FindElement(By.CssSelector("#result")).Text;

            //Assert
            Assert.That("Result: 20", Is.EqualTo(resultText));
        }

        [Test]
        public void Test_Add_Two_positive_numbers_And_Reset()
        {
            //Act
            var FirstNumber = driver.FindElement(By.Id("number1"));
            FirstNumber.SendKeys("5" + Keys.Enter);

            var Operation = driver.FindElement(By.Id("operation"));
            {
                var dropdown = driver.FindElement(By.Id("operation"));
                dropdown.FindElement(By.XPath("//option[. = '* (multiply)']")).Click();
            }

            var SecondNumber = driver.FindElement(By.Id("number2"));
            SecondNumber.SendKeys("4" + Keys.Enter);

            driver.FindElement(By.Id("calcButton")).Click();

            //Arange
            var resultText = driver.FindElement(By.CssSelector("#result")).Text;

            //Assert
            Assert.That("Result: 20", Is.EqualTo(resultText));
            Assert.IsNotEmpty(FirstNumber.GetAttribute("value"));
            Assert.IsNotEmpty(Operation.GetAttribute("value"));
            Assert.IsNotEmpty(SecondNumber.GetAttribute("value"));
            Assert.IsNotEmpty(resultText);

            //Arange
            var resetButton = driver.FindElement(By.Id("resetButton"));
            resetButton.Click();

            //Assert
            Assert.IsEmpty(FirstNumber.GetAttribute("value"));
            Assert.IsEmpty(SecondNumber.GetAttribute("value"));
            var expectedTextOperationButton = "-- select an operation --";
            var actualText = Operation.GetAttribute("value");
            Assert.That(expectedTextOperationButton, Is.EqualTo(actualText));

        }

    }
}