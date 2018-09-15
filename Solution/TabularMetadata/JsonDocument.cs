using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TabularMetaData
{
  public class JsonDocument
  {
    private JObject JObject { get; set; }

    public JsonDocument(string FilePath)
    {
      Load(FilePath);
    }

    public void ApplyChanges(JsonChanges changes)
    {
      foreach (JsonChange change in changes)
      {
        change.Report();
        change.Apply(this.JObject);
      }
    }
    
    public void Load(string FilePath)
    {
      // read JSON directly from a file
      using (StreamReader file = File.OpenText(FilePath))
      using (JsonTextReader reader = new JsonTextReader(file))
      {
        this.JObject = (JObject)JToken.ReadFrom(reader);
      }
    }

    public void Save(string FilePath)
    {
      // write JSON directly to a file
      using (StreamWriter file = File.CreateText(FilePath))
      using (JsonTextWriter writer = new JsonTextWriter(file))
      {
        writer.Formatting = Formatting.Indented;
        this.JObject.WriteTo(writer);
      }
    }


  }
}
