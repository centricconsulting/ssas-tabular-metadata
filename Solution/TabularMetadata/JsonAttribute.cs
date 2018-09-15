using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabularMetaData
{
  public class JsonAttribute
  {
    public JsonAttribute(string Name, string Value)
    {
      this.Name = Name;
      this.Value = Value;
    }

    public string Name { get; set; }
    public string Value { get; set; }
  }
}
