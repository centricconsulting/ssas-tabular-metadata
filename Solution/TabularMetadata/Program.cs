using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace TabularMetaData
{
  public class Program
  {
  
    static void Main(string[] args)
    {

      Parser.Default
        .ParseArguments<ApplicationOptions>(args)
        .WithParsed<ApplicationOptions>(ao => {
          Main(ao);
        });
    }

    static void Main(ApplicationOptions ao)
    {

      try
      {
        Console.WriteLine("Loading Input File...");
        Console.WriteLine(ao.JsonFilePath);
        JsonDocument doc = new JsonDocument(ao.JsonFilePath);

        Console.WriteLine();

        Console.WriteLine("Loading Changes File...");
        Console.WriteLine(ao.ChangesFilePath);
        JsonChanges changes = new JsonChanges(ao.ChangesFilePath);

        Console.WriteLine();

        Console.WriteLine("Applying Changes...");
        Console.WriteLine();
        doc.ApplyChanges(changes);

        Console.WriteLine();

        Console.WriteLine("Saving Output File...");
        Console.WriteLine(ao.FinalOutputFilePath);
        doc.Save(ao.FinalOutputFilePath);

        Console.WriteLine();
        Console.WriteLine("Changes successfully applied.");
      }
      catch(Exception e)
      {

        Console.WriteLine();
        Console.WriteLine("Changes failed.");
        Console.WriteLine(e.Message);
      }

      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
 
    }
  }
}
