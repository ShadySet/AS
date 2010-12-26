using System;
using System.Diagnostics;
using System.Text;
using UC.Agent.Lib.WinApi;

namespace AS.Lib.SilkTest
{
    public class SilkTestRuntimeStatus
    {
        const Int32 IDC_ELAPSED_SCRIPT = 0x6A;
        const Int32 IDC_ELAPSED_TESTCASE = 0x6B;
        const Int32 IDC_SCRIPT_NAME = 0x64;
        const Int32 IDC_TESTCASE_NAME = 0x65;
        const Int32 IDC_ERRORS_SCRIPT = 0x70;
        const Int32 IDC_ERRORS_TESTCASE = 0x71;
        const Int32 IDC_CURRENT_CALL = 0x76;
        const Int32 IDC_LAST_ERROR = 0x77;
        const Int32 MAX_CHAR = 0x400;

        public readonly string statusWinClass = "QAP_DialogClass";

        public string ElapsedScript { get; set; }
        public string ElapsedTestCase { get; set; }
        public string ScriptName { get; set; }
        public string TestCaseName { get; set; }
        public string ErrorsScript { get; set; }
        public string ErrorsTestCase { get; set; }
        public string CurrentCall { get; set; }
        public string LastError { get; set; }

        private string ReadDlgItem(IntPtr hDlg, Int32 itemId)
        {
            StringBuilder result = new StringBuilder(MAX_CHAR);
            try
            {
                WinFunc.GetDlgItemText(hDlg, itemId, result, MAX_CHAR);
            }
            catch (Exception exception)
            {
                Debug.Write(exception);
            }
            return result.ToString();
        }

        public void UpdateStatus()
        {
            IntPtr hQapDialogClass = WinFunc.FindWindow(statusWinClass, null);

            ElapsedScript = ReadDlgItem(hQapDialogClass, IDC_ELAPSED_SCRIPT);
            ElapsedTestCase = ReadDlgItem(hQapDialogClass, IDC_ELAPSED_TESTCASE);
            ScriptName = ReadDlgItem(hQapDialogClass, IDC_SCRIPT_NAME);
            TestCaseName = ReadDlgItem(hQapDialogClass, IDC_TESTCASE_NAME);
            ErrorsScript = ReadDlgItem(hQapDialogClass, IDC_ERRORS_SCRIPT);
            ErrorsTestCase = ReadDlgItem(hQapDialogClass, IDC_ERRORS_TESTCASE);
            CurrentCall = ReadDlgItem(hQapDialogClass, IDC_CURRENT_CALL);
            LastError = ReadDlgItem(hQapDialogClass, IDC_LAST_ERROR);
        }
    }
}