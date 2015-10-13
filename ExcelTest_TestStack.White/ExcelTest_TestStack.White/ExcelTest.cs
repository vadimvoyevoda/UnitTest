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

namespace ExcelTest_TestStack.White
{
    [TestFixture]
    public class ExcelTest
    {
        [Test]
        public void should_excel_save_file_on_desktop()
        {
            //arrange
            string path = @"C:\Users\Вадим\Desktop\test.xlsx";

            if(File.Exists(path))
            {
                File.Delete(path);
            }

            //act
            Process excelProc = new Process();
            excelProc.StartInfo.FileName = "excel.exe";
            excelProc.Start();
            excelProc.WaitForInputIdle();
                        
            TestStack.White.Application app = TestStack.White.Application.Attach("excel");
            
            var wndMain = app.GetWindows()[0];
            var menuCell = wndMain.Get<TestStack.White.UIItems.TextBox>(SearchCriteria.ByAutomationId("1001"));
            
            EnterTextToCell(menuCell, "B2", "Hello");
            EnterTextToCell(menuCell, "C3", "my");
            EnterTextToCell(menuCell, "D2", "Friend!");

            SendKeys.SendWait("^{s}");

            app = TestStack.White.Application.Attach("excel");
            TestStack.White.UIItems.WindowItems.Window saveDlg = null;
            while (true)
            {
                try
                {
                    saveDlg = app.GetWindows()[1];
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(200);
                    continue;
                }
            }
            
            var list = saveDlg.Get<TestStack.White.UIItems.TreeItems.Tree>(SearchCriteria.ByAutomationId("100"));
            var desktop = list.Nodes[1];
            desktop.Expand();
            desktop.Nodes[2].Select();

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

        private void EnterTextToCell(TestStack.White.UIItems.TextBox menuCell, string cell, string text)
        {
            menuCell.Text = cell;
            SendKeys.SendWait("{Enter}");
            SendKeys.SendWait(text);
            SendKeys.SendWait("{Enter}");
        }
    }
}
