using CefSharp;
using CefSharp.Wpf;
using NLog;
using Playnite.Controls;
using Playnite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Playnite.Providers.Humble
{
    public class WebApiClient : IDisposable
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CefSharp.OffScreen.ChromiumWebBrowser browser;

        public WebApiClient()
        {
            browser = new CefSharp.OffScreen.ChromiumWebBrowser(automaticallyCreateBrowser: false);
            browser.BrowserInitialized += Browser_BrowserInitialized;
            browser.CreateBrowser(IntPtr.Zero);
        }

        public void Dispose()
        {
            browser?.Dispose();
        }

        private AutoResetEvent browserInitializedEvent = new AutoResetEvent(false);
        private void Browser_BrowserInitialized(object sender, EventArgs e)
        {
            browserInitializedEvent.Set();
        }

        #region GetLoginRequired
        private void getLoginRequired_StateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading == false)
            {
                var b = (CefSharp.OffScreen.ChromiumWebBrowser)sender;
                Console.WriteLine(b.Address);
                if (b.Address.Contains("humblebundle.com/login"))
                {
                    loginRequired = true;
                }
                else
                {
                    loginRequired = false;
                }

                loginRequiredEvent.Set();
            }
        }

        private bool loginRequired = true;
        private AutoResetEvent loginRequiredEvent = new AutoResetEvent(false);
        public bool GetLoginRequired()
        {
            if (!browser.IsBrowserInitialized)
            {
                browserInitializedEvent.WaitOne(5000);
            }

            try
            {
                browser.LoadingStateChanged += getLoginRequired_StateChanged;
                browser.Load(libraryUrl);
                loginRequiredEvent.WaitOne(10000);
                return loginRequired;
            }
            finally
            {
                browser.LoadingStateChanged -= getLoginRequired_StateChanged;
            }
        }

        #endregion GetLoginRequired

        #region GetOwnedGames
        private async void getOwnedGames_StateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading == false)
            {
                var b = (CefSharp.OffScreen.ChromiumWebBrowser)sender;
                gamesList = await b.GetTextAsync();
                gamesGotEvent.Set();                
            }
        }

        private string gamesList = string.Empty;
        private AutoResetEvent gamesGotEvent = new AutoResetEvent(false);
        public string GetOwnedGames()
        {
            if (!browser.IsBrowserInitialized)
            {
                browserInitializedEvent.WaitOne(5000);
            }

            try
            {
                gamesList = string.Empty;
                browser.LoadingStateChanged += getOwnedGames_StateChanged;
                browser.Load(libraryUrl);
                gamesGotEvent.WaitOne(10000);
                return gamesList;
            }
            finally
            {
                browser.LoadingStateChanged -= getOwnedGames_StateChanged;
            }
        }

        #endregion GetOwnedGames

        #region GetOrderInfo
        private async void getOrderInfo_StateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading == false)
            {
                var b = (CefSharp.OffScreen.ChromiumWebBrowser)sender;
                orderInfo = await b.GetTextAsync();
                orderinfoGotEvent.Set();
            }
        }

        private string orderInfo = string.Empty;
        private AutoResetEvent orderinfoGotEvent = new AutoResetEvent(false);
        private string orderinfoURL = @"https://www.humblebundle.com/api/v1/order/{0}?all_tpkds=true";

        public string GetOrderInfo(string orderKey)
        {
            if (!browser.IsBrowserInitialized)
            {
                browserInitializedEvent.WaitOne(5000);
            }

            try
            {
                orderInfo = string.Empty;
                browser.LoadingStateChanged += getOrderInfo_StateChanged;//
                browser.Load(string.Format(orderinfoURL, orderKey));
                orderinfoGotEvent.WaitOne(10000);
                return orderInfo;
            }
            finally
            {
                browser.LoadingStateChanged -= getOrderInfo_StateChanged;
            }
        }


        #endregion GetOrderInfo

        #region Login
        private void login_StateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading == false)
            {
                var b = (ChromiumWebBrowser)sender;

                b.Dispatcher.Invoke(() =>
                {

                    //this browser does not automatically go to login form (contrary to full browser - tested with Chrome and Firefox)
                    //therefore execute JavaScript in the browser window to click the login button ONCE as soon as it is available
                    b.GetMainFrame().ExecuteJavaScriptAsync("var loginButtonClicked;if (!loginButtonClicked){var loginButton = document.getElementsByClassName('js-account-login')[0]; if(loginButton){loginButton.click(); loginButtonClicked=true}}");

                    if (b.Address.EndsWith(loginSuccessUrl))
                    {
                        loginWindow.Dispatcher.Invoke(() =>
                        {
                            loginSuccess = true;
                            loginWindow.Close();
                        });
                    }
                    else
                    {
                        loginSuccess = false;
                    }
                });
            }
        }

        private bool loginSuccess = false;
        private string loginUrl = @"https://www.humblebundle.com/login?goto=%2Fhome%2Fcoupons";
        private string loginSuccessUrl = @"humblebundle.com/home/coupons";
        private string libraryUrl = @"https://www.humblebundle.com/api/v1/user/order";

        LoginWindow loginWindow;
        public bool Login(Window parent = null)
        {
            loginSuccess = false;
            loginWindow = new LoginWindow()
            {
                Height = 650,
                Width = 500
            };
            loginWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            loginWindow.Browser.LoadingStateChanged += login_StateChanged;
            loginWindow.Owner = parent;
            loginWindow.Browser.Address = loginUrl;
            loginWindow.ShowDialog();
            return loginSuccess;
        }
        #endregion Login
    }
}
