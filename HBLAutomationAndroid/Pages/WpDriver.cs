using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBLAutomationAndroid.Pages
{
    public class WpDriver : AndroidDriver<AndroidElement>, IHasTouchScreen
    {

        //public WpDriver(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities)
        //    : base(commandExecutor, desiredCapabilities)
        //{
        //    TouchScreen = new RemoteTouchScreen(this);
        //}

        //public WpDriver(ICapabilities desiredCapabilities)
        //    : base(desiredCapabilities)
        //{
        //    TouchScreen = new RemoteTouchScreen(this);
        //}

        //public WpDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
        //    : base(remoteAddress, desiredCapabilities)
        //{
        //    TouchScreen = new RemoteTouchScreen(this);
        //}
        public WpDriver(Uri remoteAddress, AppiumOptions options)
            : base(remoteAddress,options)
        {
            TouchScreen = new RemoteTouchScreen(this);
            //TouchAction
        }

        //public WpDriver(Uri remoteAddress, ICapabilities desiredCapabilities, TimeSpan commandTimeout)
        //    : base(remoteAddress, desiredCapabilities, commandTimeout)
        //{
        //    TouchScreen = new RemoteTouchScreen(this);
        //}

        public ITouchScreen TouchScreen { get; private set; }
        //public ITouchAction TouchAction { get; private set; }
    }
}
