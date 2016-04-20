using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace L6POC
{
    class Program
    {
        static void Main(string[] args)
        {
            ComputerInformation.PullAllInfo();
            printComputerInfo();

            
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
                "- - - - -",
                "Processor Name:",
                "Processor Core Count:",
                "Processor Logical Processors:",
                "- - - - -",
                "Memory Capacity:",
                "Memory Speed:",
                "- - - - -",
                "HDD Model:",
                "HDD Drive Letter(s):",
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
                "- - - - -"};

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
                "- - - - -",
                ComputerInformation.ProcessorName,
                ComputerInformation.ProcessorNumberOfCores,
                ComputerInformation.ProcessorNumberOfLogicalProcessors,
                "- - - - -",
                ComputerInformation.MemoryCapacity,
                ComputerInformation.MemorySpeed,
                "- - - - -",
                ComputerInformation.HDDModel,
                ComputerInformation.HDDDriveLetter,
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

            Console.WriteLine("{0, 30}", "L6");
            for(int i = 0; i < names.Length; i++)
            {
                Console.WriteLine("{0, 30} {1}", names[i], values[i]);
            }
            Console.ReadLine();
        }
    }
}
