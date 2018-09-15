using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericParsing;

namespace TabularMetaData
{
  public class JsonChanges : List<JsonChange>
  {
    public JsonChanges(string ChangesFilePath)
    {
      Load(ChangesFilePath);
    }

    public void Load(string ChangesFilePath)
    {

      using (GenericParser parser = new GenericParser())
      {
        parser.SetDataSource(ChangesFilePath);

        parser.ColumnDelimiter = ',';
        parser.FirstRowHasHeader = true;
        parser.TextQualifier = '\"';
        //parser.SkipStartingDataRows = 10;
        //parser.MaxBufferSize = 4096;
        //parser.MaxRows = 500;

        int ColumnCount = 0;
        string[] ReservedColumnNames = new string[] { "table", "elementType", "elementName" };

        while (parser.Read())
        {

          // get the column count on the first pass
          if(ColumnCount == 0) ColumnCount = parser.ColumnCount;

          string Table = parser["table"];
          string ElementType = parser["elementType"];
          string ElementName = parser["elementName"];

          // determine the type of change
          JsonChange change = null;
          if (ElementType.Equals("column", StringComparison.CurrentCultureIgnoreCase))
          {

            change = new JsonChangeColumn()
              {Table = Table,Column = ElementName};
            
          }

          if (ElementType.Equals("measure", StringComparison.CurrentCultureIgnoreCase))
          {

            change = new JsonChangeMeasure()
            { Table = Table, Measure = ElementName };

          };

          // load the additional columns
          for (int ColumnIndex = 0; ColumnIndex < ColumnCount; ColumnIndex++)
          {
            string ColumnName = parser.GetColumnName(ColumnIndex);

            // ignore reserved columns names
            if (ReservedColumnNames.Contains<string>(ColumnName, 
              StringComparer.CurrentCultureIgnoreCase)) continue;

            // add the attribute
            string Value = parser[ColumnIndex];
            if (!String.IsNullOrEmpty(Value))
            {
              change.Attributes.Add(new JsonAttribute(ColumnName, parser[ColumnIndex]));
            }            
          }

          this.Add(change);

        }
      }

    }

  }
}
