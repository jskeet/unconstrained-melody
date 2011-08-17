#region License and Terms
// Unconstrained Melody
// Copyright (c) 2009-2011 Jonathan Skeet. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ConstraintChanger
{
    class Program
    {
        const string InputAssembly = "UnconstrainedMelody.dll";
        const string OutputAssembly = InputAssembly;
        const string OutputDirectory = @"../../../Rewritten";

        private static readonly string[] SdkPaths = 
        {
            @"Microsoft SDKs\Windows\v6.0A\bin",
            @"Microsoft SDKs\Windows\v7.0A\bin",
            @"Microsoft SDKs\Windows\v7\bin"
        };

        static int Main()
        {
            string ildasmExe = FindIldasm();
            if (ildasmExe == null)
            {
                // Error message will already have been written
                return 1;                 
            }
            string windows = Environment.GetFolderPath(Environment.SpecialFolder.System);
            string framework2Directory = Path.Combine(windows, @"..\Microsoft.NET\Framework\v2.0.50727");
            string ilasmExe = Path.Combine(framework2Directory, "ilasm.exe");
            if (!File.Exists(ilasmExe))
            {
                Console.WriteLine("Can't find ilasm. Aborting. Expected it at: {0}", ilasmExe);
                return 1;
            }

            try
            {
                string ilFile = Decompile(ildasmExe);
                ChangeConstraints(ilFile);
                Recompile(ilFile, ilasmExe);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}\r\n{1}", e.Message, e.StackTrace);
                return 1;
            }
            return 0;
        }

        private static string FindIldasm()
        {
            string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            foreach (string sdkPath in SdkPaths)
            {
                string directory = Path.Combine(programFiles, sdkPath);
                if (Directory.Exists(directory))
                {
                    string ilasm = Path.Combine(directory, "ildasm.exe");
                    if (File.Exists(ilasm))
                    {
                        return ilasm;
                    }
                }
            }
            Console.WriteLine("Unable to find SDK directory containing ilasm.exe. Aborting.");
            return null;
        }

        private static string Decompile(string ildasmExe)
        {
            string ilFile = Path.GetTempFileName();
            Console.WriteLine("Decompiling to {0}", ilFile);
            Process process = Process.Start(new ProcessStartInfo
            {
                FileName = ildasmExe,
                Arguments = "\"/OUT=" + ilFile + "\" " + InputAssembly,
                WindowStyle = ProcessWindowStyle.Hidden
            });
            process.WaitForExit();
            return ilFile;
        }

        private static void Recompile(string ilFile, string ilasmExe)
        {
            if (!Directory.Exists(OutputDirectory))
            {
                Console.WriteLine("Creating output directory");
                Directory.CreateDirectory(OutputDirectory);
            }

            string output = Path.Combine(OutputDirectory, OutputAssembly);
            Console.WriteLine("Recompiling {0} to {1}", ilFile, output);
            Process process = Process.Start(new ProcessStartInfo
            {
                FileName = ilasmExe,
                Arguments = "/OUTPUT=" + output + " /DLL " + "\"" + ilFile + "\"",
                WindowStyle = ProcessWindowStyle.Hidden
            });
            process.WaitForExit();
        }

        private static void ChangeConstraints(string ilFile)
        {
            string[] lines = File.ReadAllLines(ilFile);
            lines = lines.Select<string, string>(ChangeLine).ToArray();
            File.WriteAllLines(ilFile, lines);
        }

        private static string ChangeLine(string line)
        {
            // Surely this is too simple to actually work...
            return line.Replace("(UnconstrainedMelody.DelegateConstraint)", "([mscorlib]System.Delegate)")
                       .Replace("([mscorlib]System.ValueType, UnconstrainedMelody.IEnumConstraint)", "([mscorlib]System.Enum)");
        }
    }
}
