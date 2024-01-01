using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Management.Instrumentation;
using System.Windows.Automation;
using OpenQA.Selenium;
using System.Windows.Input;
using System.Windows.Forms;
using OpenQA.Selenium.Interactions;
using PowerPoint;

namespace PowerPointTests
{
    public class Robot
    {
        private WindowsDriver<WindowsElement> _driver;
        private Dictionary<string, string> _windowHandles;
        private string _root;
        private const string CONTROL_NOT_FOUND_EXCEPTION = "The specific control is not found!!";
        private const string WIN_APP_DRIVER_URI = "http://127.0.0.1:4723";

        // constructor
        public Robot(string targetAppPath, string root)
        {
            this.Initialize(targetAppPath, root);
        }

        // initialize
        public void Initialize(string targetAppPath, string root)
        {
            _root = root;
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", targetAppPath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            options.AddAdditionalCapability(Constant.WORKING_DIRECTORY, Path.GetFullPath(Path.Combine(solutionPath, Constant.PROJECT_NAME, Constant.BINARY_DIRECTORY, Constant.DEBUG_DIRECTORY)));

            _driver = new WindowsDriver<WindowsElement>(new Uri(WIN_APP_DRIVER_URI), options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _windowHandles = new Dictionary<string, string>
            {
                { _root, _driver.CurrentWindowHandle }
            };
        }

        // clean up
        public void CleanUp()
        {
            SwitchTo(_root);
            _driver.CloseApp();
            _driver.Dispose();
        }

        // test
        public void SwitchTo(string formName)
        {
            if (_windowHandles.ContainsKey(formName))
            {
                _driver.SwitchTo().Window(_windowHandles[formName]);
            }
            else
            {
                foreach (var windowHandle in _driver.WindowHandles)
                {
                    _driver.SwitchTo().Window(windowHandle);
                    try
                    {
                        _driver.FindElementByAccessibilityId(formName);
                        _windowHandles.Add(formName, windowHandle);
                        return;
                    }
                    catch
                    {

                    }
                }
            }
        }

        // test
        public void Sleep(Double time)
        {
            Thread.Sleep(TimeSpan.FromSeconds(time));
        }

        // test
        public void ClickByElementName(string name)
        {
            _driver.FindElementByName(name).Click();
        }
        
        // test
        public WindowsElement FindElementByName(string name)
        {
            return _driver.FindElementByName(name);
        }
        
        // test
        public WindowsElement FindElementById(string id)
        {
            return _driver.FindElementByAccessibilityId(id);
        }
        
        // test
        public void PerformAction(IList<ActionSequence> action)
        {
            _driver.PerformActions(action);
        }
        
        // test
        public WindowsDriver<WindowsElement> GetDriver()
        {
            return _driver;
        }

        // test
        public IOptions Manage()
        {
            return _driver.Manage();
        }
    }
}