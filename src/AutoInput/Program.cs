using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

var service = ChromeDriverService.CreateDefaultService();
service.EnableVerboseLogging = false;
service.HideCommandPromptWindow = true;

IWebDriver driver = new ChromeDriver(service);

try
{
    Console.Write("Input site you want to navigate (e.g. \"https://www.cisco.com\"): ");
    var site = Console.ReadLine();

    try { driver.Navigate().GoToUrl(site); }
    catch { throw new Exception("Unable to load a site with specified URL. Check URL correctness or internet connection."); }

    Console.Write("\nIs there a button on the same page you want to click to get desired input box?" +
        "\nIf yes, input its XPath (e.g. \"/html/body/div[13]/div/span[1]\").\n" +
        "To find element's XPath you should find this element in browser's Inspector (F12 for Chrome), right-click on it -> Copy -> Copy XPath.\n" +
        "If you do not want to past anything here, just press \"Enter\": ");
    var buttonToClick = Console.ReadLine();

    if (buttonToClick != "")
        try { driver.FindElement(By.XPath(buttonToClick)).Click(); }
        catch { throw new Exception("Unable to find specified element."); }

    Console.Write("Insert XPath of input in which you want insert data: ");
    var targetXPath = Console.ReadLine();

    IWebElement targetInput;
    try
    {
        targetInput =
            driver.FindElement(By.XPath(targetXPath));
    }
    catch { throw new Exception("Unable to find specified element."); }

    Console.Write("Which data do you want to insert?: ");
    var data = Console.ReadLine();

    var actionsProvider = new Actions(driver);
    actionsProvider = actionsProvider.SendKeys(targetInput, data);
    actionsProvider.Build().Perform();

    PromptUserToClose();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    PromptUserToClose();
}

void PromptUserToClose()
{
    Console.WriteLine("Press something to exit...");
    Console.ReadLine();
}
