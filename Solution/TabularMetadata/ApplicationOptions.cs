using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace TabularMetaData
{
  public class ApplicationOptions
  {
    [Option('i', "input", Required = true, HelpText = "Path of the input Json file path.")]
    public String JsonFilePath { get; set; }

    [Option('c', "changes", Required = true, HelpText = "Path of the CSV changes file.")]
    public String ChangesFilePath { get; set; }

    [Option('o', "output", Required = false, HelpText = "Path of the output Json file.")]
    public String OutputFilePath { get; set;  }

    [Option('w', "overwrite", Required = false, HelpText = "Overwite the original input Json file with the output Json file.")]
    public Boolean Overwrite { get; set; } = false;

    public String FinalOutputFilePath {
      get {return Overwrite ? JsonFilePath : OutputFilePath;}
    }

  }
}
