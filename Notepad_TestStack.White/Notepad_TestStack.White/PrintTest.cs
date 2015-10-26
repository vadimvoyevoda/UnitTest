using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestStack.White.UIItems.Finders;

namespace Notepad_TestStack.White
{       
    [TestFixture]
    public class PrintTest
    {
        [Test]
        public void should_print_and_save_notepad_file()
        {
            //arrange
            string path = @"E:\test.txt";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            //act
            Process excelProc = new Process();
            excelProc.StartInfo.FileName = "notepad.exe";
            excelProc.Start();
            excelProc.WaitForInputIdle();

            TestStack.White.Application app = TestStack.White.Application.Attach("notepad");

            var wndMain = app.GetWindows()[0];
            SendKeys.SendWait("Some Text For Printing and Saving");
            Thread.Sleep(200);
            //Call Print Dialog
            SendKeys.SendWait("^{p}");

            //Print
            ClickBtn(FindWnd(app), "1");
            //Cancel
            ClickBtn(FindWnd(app), "2");
            //Abort Accept
            ClickBtn(FindWnd(app), "2");

            //Call Save Dialog
            SendKeys.SendWait("^{s}");
            
            var saveDlg = FindWnd(app);
            var list = saveDlg.Get<TestStack.White.UIItems.TreeItems.Tree>(SearchCriteria.ByAutomationId("100"));
            var desktop = list.Nodes[1];
            desktop.Expand();
            var thisPC = desktop.Nodes[2];
            thisPC.Expand();
            var diskC = thisPC.Nodes.FirstOrDefault(n => n.Text == "Локальный диск (E:)");
            diskC.Select();

            var fileName = saveDlg.Get<TestStack.White.UIItems.TextBox>(SearchCriteria.ByAutomationId("1001"));
            fileName.Text = "test";

            var btnSave = saveDlg.Get<TestStack.White.UIItems.Button>(SearchCriteria.ByAutomationId("1"));
            btnSave.Click();

            SendKeys.SendWait("%{F4}");
            excelProc.WaitForExit();

            bool res = File.Exists(path);

            //assert
            Assert.AreEqual(true, res);
        }

        public TestStack.White.UIItems.WindowItems.Window FindWnd(TestStack.White.Application app)
        {
            app = TestStack.White.Application.Attach("notepad");
            TestStack.White.UIItems.WindowItems.Window dlg = null;
            while (true)
            {
                try
                {
                    dlg = app.GetWindows()[1];
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(200);
                    continue;
                }
            }

            return dlg;
        }

        public void ClickBtn(TestStack.White.UIItems.WindowItems.Window dlg, string btnAutomationId)
        {
            var btn = dlg.Get<TestStack.White.UIItems.Button>(SearchCriteria.ByAutomationId(btnAutomationId));
            btn.Click();
        }
    }
}
