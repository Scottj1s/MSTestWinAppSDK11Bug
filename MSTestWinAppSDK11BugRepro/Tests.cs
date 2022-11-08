#if TESTRUNNER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;

namespace MSTestWinAppSDK11BugRepro
{
    [TestClass]
    public class Tests
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);

        [TestMethod]
        public void ShouldAlwaysSucceed()
        {
            MessageBox(IntPtr.Zero, "ShouldAlwaysSucceed", "WinAppSDK Test", 0);
            Assert.IsTrue(1 > 0);
        }

        [TestMethod]
        public void IsPackaged()
        {
            Assert.IsTrue(PackagingData.IsPackagedProcess(),
                $"{nameof(PackagingData)}.{nameof(PackagingData.IsPackagedProcess)} returned false when true was expected.");
        }

        // Note: On Test11|x86 config, this test will crash the test host, thereby aborting this and all remaining tests due to a
        // COMException with 0x80040154 (REGDB_E_CLASSNOTREG) when trying to start the app (details should be in the Output window's Test output)
        [UITestMethod]
        public void MainWindowContentIsStackPanel()
        {
            Assert.IsInstanceOfType((App.Current as App).Window, typeof(MainWindow),
                $"Expected App.Window to be of type {nameof(MainWindow)} but it is of type {(App.Current as App).Window?.GetType().FullName ?? "(null)"}.");
            Assert.IsInstanceOfType((App.Current as App).Window.Content, typeof(StackPanel),
                $"Expected Content of {nameof(MainWindow)} to be {nameof(StackPanel)} but instead it is {(App.Current as App).Window.Content?.GetType().FullName ?? "(null)"}.");
        }
    }
}
#endif