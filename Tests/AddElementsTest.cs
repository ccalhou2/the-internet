using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TheInternet;

[TestFixture]
public class AddElementsTest
{
    protected IWebDriver Driver;
    private readonly string _baseUrl = "https://the-internet.herokuapp.com/";
    private readonly By _addRemovePage = By.XPath("//html/body/div[2]/div/ul/li[2]/a");
    private readonly By _addButton = By.XPath("//html/body/div[2]/div/div/button");
    private readonly By _deleteButton = By.XPath("//html/body/div[2]/div/div/div/button[1]");
    private readonly By _elements = By.XPath("//html/body/div[2]/div/div/div/button");

    [SetUp]
    public void SetUp()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArguments("--headless");
        new DriverManager().SetUpDriver(new ChromeConfig());
        Driver = new ChromeDriver(options);
        Driver.Navigate().GoToUrl(_baseUrl);
    }

    [Test]
    [TestCase("abc")]
    [TestCase('a')]
    [TestCase(-3)]
    [TestCase(0)]
    [TestCase(5)]
    [TestCase(3.2)]
    public void VerifyAddedElements (dynamic n)
    {
        
        // Display Add/Remove Elements page
        Driver.FindElement(_addRemovePage).Click();

        // Check if n is negative or non-integer
        if (n is string || n is char || n < 0) {
            n = 0;
        } else {
            n = (int) n;
        }
    
        // Click Add Element button to add n number of elements
        for (int i = 0; i < n; i++) {
            Driver.FindElement(_addButton).Click();
        }

        Assert.AreEqual(n, Driver.FindElements(_elements).Count);
    }

    [TearDown]
    public void TearDown()
    {
        Driver.Quit();
    }
}
