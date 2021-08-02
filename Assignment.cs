using OpenQA.Selenium;
using sitecore_assignment_consoleApp.pages;
using sitecore_assignment_consoleApp.Utilities;
using System;

namespace sitecore_assignment_consoleApp
{
    class Assignment
    {
        static void Main(string[] args)
        {
            IWebDriver webDriver;
            AmazonHomePage amazonHomePage;
            AmazonSearchResultsPage amazonSearchResultPage;
            AmazonProductDetailsPage amazonProductsDetailsPage;
            string BrowserName = "chrome";
            string url = "https://www.amazon.com/";
            string keyWord = "laptop";
            int productNumber = 1;
            decimal CompareAmount = 100;

            //Browser setup 
            webDriver = Utils.SetupBrowser(BrowserName);
            Console.WriteLine("Browser setup completed");

            // Navigate to URL
            Utils.GoToLink(webDriver, url);
            Console.WriteLine("Navigated to URL : " + url);

            // Page object model
            // Get Home page object using driver object
            amazonHomePage = new AmazonHomePage(webDriver);
            string homePageTitle = amazonHomePage.GetCurrentPageTitile();
            Console.WriteLine("Amazon home page loaded successfully, Page Title : " + homePageTitle);
            amazonSearchResultPage = amazonHomePage.SearchForKeyWord(keyWord);

            //Goto search result page by entering a keyword
            string searchResultsPage = amazonSearchResultPage.GetCurrentPageTitile();
            Console.WriteLine("Amazon search results page loaded successfully, Page Title : " + searchResultsPage);
            amazonProductsDetailsPage = amazonSearchResultPage.selectProduct(productNumber);
            Console.WriteLine("Selected product number " + productNumber + " from search results page successfully");

            //Goto product details page by selecting the first product
            string productDetailsPageTitle = amazonProductsDetailsPage.GetCurrentPageTitile();
            Console.WriteLine("Amazon product details page loaded successfully, Page Title : " + productDetailsPageTitle);
            String ActualAmount = amazonProductsDetailsPage.GetActualPrice();
            Console.WriteLine("Amount of the product : " + ActualAmount);
            Asserter.AssertIfAmountIsGreater(Decimal.Parse(ActualAmount.Trim().Replace("$", "")), CompareAmount);
            Console.WriteLine("Product amount " + ActualAmount + " is greater than $" + CompareAmount);

            //Quit Browser
            Utils.QuitBrowser(webDriver);
            Console.WriteLine("Browser closed successfully");
        }
    }
}
