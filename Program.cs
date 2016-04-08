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
            ComputerInformation.PullSysInfo();
            printComputerInfo();

            
        }

        static void printComputerInfo()
        {
            string[] names = {"Processor Name:",
                "Processor Core Count:",
                "Processor Logical Processors:",
                "Memory Capacity:",
                "Memory Speed:",
                "Video Name:",
                "Video Resolution:"};

            string[] values = {
                ComputerInformation.ProcessorName,
                ComputerInformation.ProcessorNumberOfCores,
                ComputerInformation.ProcessorNumberOfLogicalProcessors,
                ComputerInformation.MemoryCapacity,
                ComputerInformation.MemorySpeed,
                ComputerInformation.VideoName,
                ComputerInformation.VideoResolution};

            Console.WriteLine("{0, -30} {1}", "Name", "Value");
            Console.WriteLine("--------------------------------------------------------------------------------");
            for(int i = 0; i < names.Length; i++)
            {
                Console.WriteLine("{0, -30} {1}", names[i], values[i]);
            }
            Console.ReadLine();
        }
    }
}
