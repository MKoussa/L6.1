using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L6POC
{
    class WindowsTools
    {
        public static void RunMSInfo32()
        {
            Process prcs = new Process();
            ProcessStartInfo msi32 = new ProcessStartInfo();
            msi32.FileName = "MSInfo32.exe";
            prcs.StartInfo = msi32;
            prcs.Start();
        }

        public static void RunDXDiag()
        {
            Process prcs = new Process();
            ProcessStartInfo msi32 = new ProcessStartInfo();
            msi32.FileName = "DXDiag.exe";
            prcs.StartInfo = msi32;
            prcs.Start();
        }

        public static void RunDiskMGMT()
        {
            Process prcs = new Process();
            ProcessStartInfo msi32 = new ProcessStartInfo();
            msi32.FileName = "DiskMGMT.msc";
            prcs.StartInfo = msi32;
            prcs.Start();
        }
    }
}
