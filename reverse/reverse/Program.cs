using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace reverse
{
    class Program
    {
        static void Main(string[] args)
        {

            ProgramController controller = new ProgramController();

            string SolutionDirectoryName = @"C:\work\tfs";
            //SolutionDirectoryName = @"C:\work\tfsdev";

            // create solution file list
            // controller.CreateSolutionFilesList(SolutionDirectoryName);

            // creates file extension list
            // controller.GetUniqueFileTypeList(SolutionDirectoryName); 

            //controller.FindTextInFile(SolutionDirectoryName, "*.cs", "^using"); 

            // Catalog files and projects
            // controller.ProcessSolutionFiles();

            //controller.ListSolutionFiles(@"C:\work\tfs");
 
            // Catalog a specific project
            //controller.Run2();

            controller.DebugMode = true;
            //controller.ProcessSolutionFile(@"C:\work\tfs\SystemEngine\Components\DexClientFX\src\main\DexFormatFX", @"DexFormatFX.sln");

            controller.ProcessSolutionFile(@"C:\work\tfs\SystemEngine\Core\DEX\RealEC.DEX.Client\trunk\RealEC.DEX.Client", @"RealEC.DEX.Client.sln"); 
        }

    }
}
