using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace TabularMetaData
{
  public abstract class JsonChange
  { 

    public List<JsonAttribute> Attributes = new List<JsonAttribute>();

    public abstract string BuildPath();
    public abstract void Report();

    public void Apply(JObject TargetObject)
    {
      JToken token = TargetObject.SelectToken(this.BuildPath());

      if (token != null)
      {
        ApplyAttributes(token as JObject);
      }
    } 

    internal void ApplyAttributes(JObject target)
    {

      // exit if there are no attributes to apply
      if (this.Attributes.Count == 0) return;

      Console.WriteLine();

      foreach (JsonAttribute attribute in this.Attributes)
      {

        // ignore this attribute if the value is empty
        if (String.IsNullOrEmpty(attribute.Name) || String.IsNullOrEmpty(attribute.Value)) break;

        // add or replace the attribute value
        if (target.Property(attribute.Name) == null)
        {
          Console.WriteLine("\t\"{0}\" = \"{1}\"", attribute.Name, attribute.Value);
          target.Add(new JProperty(attribute.Name, attribute.Value));
        }
        else if (!target.Property(attribute.Name).Value.Equals(attribute.Value))
        {
          Console.WriteLine("\t\"{0}\" = \"{1}\"", attribute.Name, attribute.Value);
          target.Property(attribute.Name).Value = attribute.Value;
        }        
      }

      Console.WriteLine();

    }
   
           
  }
}
