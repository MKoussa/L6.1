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
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "MSInfo32.exe";
            prcs.StartInfo = psi;
            prcs.Start();
        }

        public static void RunDXDiag()
        {
            Process prcs = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "DXDiag.exe";
            prcs.StartInfo = psi;
            prcs.Start();
        }

        public static void RunDiskMGMT()
        {
            Process prcs = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "DiskMGMT.msc";
            prcs.StartInfo = psi;
            prcs.Start();
        }

        public static void RunCleanManager()
        {
            Process prcs = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "Cleanmgr.exe";
            prcs.StartInfo = psi;
            prcs.Start();
        } 

        public static void RunInternetProperties()
        {
            Process prcs = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "Inetcpl.cpl";
            prcs.StartInfo = psi;
            prcs.Start();
        }
    }
}
