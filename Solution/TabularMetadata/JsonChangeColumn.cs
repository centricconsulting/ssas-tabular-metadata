using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TabularMetaData
{
  public class JsonChangeColumn : JsonChange
  {

    public string Table { get; set; }
    public string Column { get; set; }

    public override void Report()
    {
      Console.WriteLine("[{0}].[{1}]", Table, Column);
    }

    public override string BuildPath()
    {
      return String.Format("$.model.tables[?(@.name=='{0}')].columns[?(@.name=='{1}')]", Table, Column);
    }    
  }
}
