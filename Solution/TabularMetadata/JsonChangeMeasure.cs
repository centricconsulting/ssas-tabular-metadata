using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TabularMetaData
{
  public class JsonChangeMeasure : JsonChange
  {
    public string Table { get; set; }
    public string Measure { get; set; }

    public override void Report()
    {
      Console.WriteLine("[{0}].[{1}]", Table, Measure);
    }

    public override string BuildPath()
    {
      return String.Format("$.model.tables[?(@.name=='{0}')].measures[?(@.name=='{1}')]", Table, Measure);
    }    
  }
}
