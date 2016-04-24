using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Management;
using System.Management.Automation;
using System.Net;

namespace L6POC
{
    static class ComputerInformation
    {
        //System Fields
        public static string SystemManufacturer { get; set; }
        public static string SystemModel { get; set; }
        public static string SystemSerial { get; set; }

        //Computer Fields
        public static string ComputerName { get; set; }
        public static string ComputerDomain { get; set; }

        //OS Fields
        public static string OSName { get; set; }
        public static string OSArchitecture { get; set; }

        //Windows Update Fields
        public static string OSLastUpdated { get; set; }

        //AV Fields
        public static string AVName { get; set; }

        //Processor Fields
        public static string ProcessorName { get; set; }
        public static string ProcessorNumberOfCores { get; set; }
        public static string ProcessorNumberOfLogicalProcessors { get; set; }

        //Memory Fields
        public static string MemoryCapacity { get; set; }
        public static string MemorySpeed { get; set; }

        //HDD Fields
        public static string HDDModel { get; set; }
        public static string HDDDriveLetter { get; set; }
        public static string HDDCapacity { get; set; }
        public static string HDDFreeSpace { get; set; }

        //Video Fields
        public static string VideoName { get; set; }
        public static string VideoResolution { get; set; }

        //Network Fields
        public static string NetworkDescription { get; set; }
        public static string NetworkIPAddress { get; set; }
        public static string NetworkSubnet { get; set; }
        public static string NetworkGateway { get; set; }
        public static string NetworkDHCPServer { get; set; }
        public static string NetworkDNSDomain { get; set; }
        public static string NetworkPublicIPAddress { get; set; }

        public static void PullAllInfo()
        {
            PullSystemInfo();
            PullOSInfo();
            PullAVInfo();
            PullCPUInfo();
            PullRAMInfo();
            PullHDDInfo();
            PullVideoInfo();
            PullNetworkInfo();
        }

        static void PullSystemInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"
                SELECT Manufacturer, Model, Name, Domain
                FROM Win32_ComputerSystem");
            ManagementObjectCollection manObjCollection = searcher.Get();

            foreach (ManagementObject value in manObjCollection)
            {
                SystemManufacturer = value["Manufacturer"].ToString();
                SystemModel = value["Model"].ToString();
                ComputerName = value["Name"].ToString();
                ComputerDomain = value["Domain"].ToString();
            }

            searcher = new ManagementObjectSearcher(@"
                SELECT SerialNumber
                FROM Win32_BIOS");
            manObjCollection = searcher.Get();

            foreach (ManagementObject value in manObjCollection)
            {
                SystemSerial = value["SerialNumber"].ToString();
            }
        }

        static void PullOSInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"
                SELECT Caption, OSArchitecture
                FROM Win32_OperatingSystem");
            ManagementObjectCollection manObjCollection = searcher.Get();

            foreach (ManagementObject value in manObjCollection)
            {
                OSName = value["Caption"].ToString();
                OSArchitecture = value["OSArchitecture"].ToString();
            }

            Collection<PSObject> PSObjects;
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddCommand("Get-HotFix");

                ps.AddCommand("Select-Object");
                ps.AddParameter("Property", "InstalledOn");

                ps.AddCommand("Sort-Object");
                ps.AddParameter("Property", "InstalledOn");
                ps.AddParameter("Descending");

                PSObjects = ps.Invoke();
            }

            PSMemberInfo psmi = PSObjects[0].Properties["InstalledOn"];
            OSLastUpdated = psmi.Value.ToString();
        }

        static void PullAVInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                "root\\SecurityCenter2",
                "SELECT displayName " +
                "FROM AntiVirusProduct");
            ManagementObjectCollection moc = searcher.Get();

            NameValueCollection mnvc = new NameValueCollection();

            foreach (ManagementObject value in moc)
            {
                foreach (PropertyData pd in value.Properties)
                {
                    mnvc.Add(pd.Name.ToString(), pd.Value.ToString());
                }
            }

            AVName = mnvc.Get("displayName");
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
                MemoryCapacity = convertToMB(value["Capacity"].ToString());
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
                HDDCapacity += convertToGB(value["Capacity"].ToString()) + " ";
                HDDFreeSpace += convertToGB(value["FreeSpace"].ToString()) + " ";
            }


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

        static void PullNetworkInfo()
        {
            
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"
                SELECT Description, 
                       IPAddress, 
                       IPSubnet, 
                       DefaultIPGateway, 
                       DHCPServer,
                       DNSDomain
                FROM Win32_NetworkAdapterConfiguration
                WHERE IPEnabled = 'true'");
            ManagementObjectCollection manObjCollection = searcher.Get();
            
            foreach (ManagementObject value in manObjCollection)
            {
                NetworkDescription = value["Description"].ToString();

                string[] tempString = (string[])value["IPAddress"];
                NetworkIPAddress = tempString[0].ToString();

                tempString = (string[])value["IPSubnet"];
                NetworkSubnet = tempString[0];

                tempString = (string[])value["DefaultIPGateway"];
                NetworkGateway = tempString[0].ToString();

                NetworkDHCPServer = value["DHCPServer"].ToString();
                NetworkDNSDomain = value["DNSDomain"].ToString();

            }
            
            using (WebClient client = new WebClient())
            {
                NetworkPublicIPAddress = client.DownloadString("http://checkip.amazonaws.com/");
            }
        }

        static string convertToGB(string value)
        {
            double num = double.Parse(value);
            num = ((num / 1024) / 1024) / 1024;
            return Math.Round(num, 2).ToString() + "GB";
        }

        static string convertToMB(string value)
        {
            double num = double.Parse(value);
            num = (num / 1024) / 1024;
            return Math.Round(num, 2).ToString() + "MB";
        }

    }
}
