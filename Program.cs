using System;

namespace L6POC
{
    class Program
    {
        static void Main(string[] args)
        {
            DownloadExternalTools.DownloadCCleaner();
            ComputerInformation.PullAllInfo();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetWindowSize(90, 57);
            Console.SetBufferSize(90, 100);

            printComputerInfo();
            ProcessInputOptions();
        }

        static void printComputerInfo()
        {
            string[] names = {
                "- - - - -",
                "Manufacturer:",
                "Model Number:",
                "Serial Number:",
                "- - - - -",
                "Computer Name:",
                "Domain:",
                "- - - - -",
                "OS Name:",
                "OS Architecture:",
                "OS Last Updated:",
                "- - - - -",
                "AntiVirus:",
                "- - - - -",
                "Processor Name:",
                "Processor Core Count:",
                "Logical Processors:",
                "L2 Cache Size:",
                "L3 Cache Size:",
                "- - - - -",
                "Memory Manufacturer:",
                "Memory Capacity:",
                "Memory Speed:",
                "- - - - -",
                "HDD Model:",
                "HDD Drive Letter(s):",
                "SMART Failure:",
                "HDD Capacity:",
                "HDD Free Space:",
                "- - - - -",
                "Video Name:",
                "Video Resolution:",
                "- - - - -",
                "Network Description:",
                "IP Address:",
                "Subnet:",
                "Gateway:",
                "DHCP Server:",
                "DNS Domain:",
                "Public IP Address:",
                "- - - - -" };

            string[] values = {
                "- - - - -",
                ComputerInformation.SystemManufacturer,
                ComputerInformation.SystemModel,
                ComputerInformation.SystemSerial,
                "- - - - -",
                ComputerInformation.ComputerName,
                ComputerInformation.ComputerDomain,
                "- - - - -",
                ComputerInformation.OSName,
                ComputerInformation.OSArchitecture,
                ComputerInformation.OSLastUpdated,
                "- - - - -",
                ComputerInformation.AVName,
                "- - - - -",
                ComputerInformation.ProcessorName,
                ComputerInformation.ProcessorNumberOfCores,
                ComputerInformation.ProcessorNumberOfLogicalProcessors,
                ComputerInformation.ProcessorL2CacheSize,
                ComputerInformation.ProcessorL3CacheSize,
                "- - - - -",
                ComputerInformation.MemoryManufacturer,
                ComputerInformation.MemoryCapacity,
                ComputerInformation.MemorySpeed,
                "- - - - -",
                ComputerInformation.HDDModel,
                ComputerInformation.HDDDriveLetter,
                ComputerInformation.HDDSmartFailure,
                ComputerInformation.HDDCapacity,
                ComputerInformation.HDDFreeSpace,
                "- - - - -",
                ComputerInformation.VideoName,
                ComputerInformation.VideoResolution,
                "- - - - -",
                ComputerInformation.NetworkDescription,
                ComputerInformation.NetworkIPAddress,
                ComputerInformation.NetworkSubnet,
                ComputerInformation.NetworkGateway,
                ComputerInformation.NetworkDHCPServer,
                ComputerInformation.NetworkDNSDomain,
                ComputerInformation.NetworkPublicIPAddress,
                "- - - - -" };

            Console.WriteLine("{0, 25} {1}", "L6.1", "MKoussa");
            for(int i = 0; i < names.Length; i++)
            {
                Console.WriteLine("{0, 25} {1}", names[i], values[i]);
            }
        }

        static void ProcessInputOptions()
        {
            Console.WriteLine("{0, 25} {1}", "DXDiag:", "[D]");
            Console.WriteLine("{0, 25} {1}", "MSInfo32:", "[M]");
            Console.WriteLine("{0, 25} {1}", "Disk Management:", "[K]");
            Console.WriteLine("{0, 25} {1}", "Disk Cleanup:", "[C]");
            Console.WriteLine("{0, 25} {1}", "Internet Properties:", "[I]");
            Console.WriteLine("{0, 25} {1}", "Performance Monitor:", "[P]");
            Console.WriteLine("{0, 25} {1}", "Exit:", "[X]");
            Console.Write("\n{0, 27}", "Select:  ");
            string input = Console.ReadLine().ToLower();
            
            while (true)
            {
                switch (input)
                {
                    case "m":
                        WindowsTools.RunMSInfo32();
                        input = "";
                        break;
                    case "d":
                        WindowsTools.RunDXDiag();
                        input = "";
                        break;
                    case "k":
                        WindowsTools.RunDiskMGMT();
                        input = "";
                        break;
                    case "c":
                        WindowsTools.RunCleanManager();
                        input = "";
                        break;
                    case "i":
                        WindowsTools.RunInternetProperties();
                        input = "";
                        break;
                    case "p":
                        WindowsTools.RunPerformanceMonitor();
                        input = "";
                        break;
                    case "x":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Write("{0, 27}", "Select:  ");
                        input = Console.ReadLine();
                        break;
                }
            }
        }
    }
}
