using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace L6POC
{
    static class ComputerInformation
    {
        //Processor Fields
        public static string ProcessorName { get; set; }
        public static string ProcessorNumberOfCores { get; set; }
        public static string ProcessorNumberOfLogicalProcessors { get; set; }

        //Memory Fields
        private static double memCap;
        public static string MemoryCapacity
        {
            get { return memCap.ToString() + "MB"; }
            set
            {
                memCap = Convert.ToDouble(value);
                memCap = (memCap / 1024) / 1024;
            }
        }
        public static string MemorySpeed { get; set; }

        //HDD Fields
        public static string HDDModel { get; set; }
        public static string HDDDriveLetter { get; set; }
        public static string HDDCapacity { get; set; }
        public static string HDDFreeSpace { get; set; }

        //Video Fields
        public static string VideoName { get; set; }
        public static string VideoResolution { get; set; }

        public static void PullSysInfo()
        {
            PullCPUInfo();
            PullRAMInfo();
            PullHDDInfo();
            PullNetworkInfo();
            PullVideoInfo();
        }

        static void PullCPUInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"
                SELECT Name, NumberOfCores, NumberOfLogicalProcessors
                FROM Win32_Processor");
            ManagementObjectCollection manObjCollection = searcher.Get();

            foreach (ManagementObject value in manObjCollection)
            {
                ProcessorName = value["Name"].ToString();
                ProcessorNumberOfCores = value["NumberOfCores"].ToString();
                ProcessorNumberOfLogicalProcessors = value["NumberOfLogicalProcessors"].ToString();
            }
        }

        static void PullRAMInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"
                SELECT Capacity, Speed
                FROM Win32_PhysicalMemory
                WHERE Name IS NOT NULL");
            ManagementObjectCollection manObjCollection = searcher.Get();

            foreach (ManagementObject value in manObjCollection)
            {
                MemoryCapacity = value["Capacity"].ToString();
                MemorySpeed = value["Speed"].ToString() + "mhz";
            }
        }

        static void PullHDDInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"
                SELECT Model
                FROM Win32_DiskDrive");
            ManagementObjectCollection manObjCollection = searcher.Get();

            foreach (ManagementObject value in manObjCollection)
            {
                HDDModel += value["Model"].ToString() + " ";
            }

            searcher = new ManagementObjectSearcher(@"
                SELECT DriveLetter, Capacity, FreeSpace
                FROM Win32_Volume
                WHERE DriveLetter IS NOT NULL");
            manObjCollection = searcher.Get();

            foreach (ManagementObject value in manObjCollection)
            {
                HDDDriveLetter += value["DriveLetter"].ToString() + " ";
                HDDCapacity += value["Capacity"].ToString() + " ";
                HDDFreeSpace += value["FreeSpace"].ToString() + " ";
            }


        }

        static void PullNetworkInfo()
        {

        }

        static void PullVideoInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"
                SELECT Name,
                       CurrentHorizontalResolution,
                       CurrentVerticalResolution
                FROM Win32_VideoController");
            ManagementObjectCollection manObjCollection = searcher.Get();

            foreach (ManagementObject value in manObjCollection)
            {
                VideoName = value["Name"].ToString();
                VideoResolution = value["CurrentHorizontalResolution"].ToString() + " x " + value["CurrentVerticalResolution"].ToString();
            }
        }
    }
}
