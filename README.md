# Tabular Metadata Utility
## Overview
The **Tabular Metadata Utility** alters column and measure properties of a SQL Server Analysis Services Tabular Model.  This is accomplished by altering the JSON in the Visual Studio project BIM file.

Execution is command-line only.  Results of the metadata changes can be viewed in the console window during execution.

Running this application requires installation of **.NET Framework 4.5** or higher.  Windows 10 users will already have this version of .NET installed.

### Repository Contents
#### [Solution Folder](https://github.com/centricconsulting/ssas-tabular-metadata/tree/master/Solution)
This folder contains the Visual Studio 2017 Solution for the Windows application.

#### [Application Folder](https://github.com/centricconsulting/ssas-tabular-metadata/tree/master/Application)
This folder contains a compiled utility **`TabularMetadata.exe`** and supporting DLL files.  An additional file, **`TabularMetdata.bat`** is provided to demonstrate execution with the command line.

Sample BIM and change CSV files are located in the [Application/Files](https://github.com/centricconsulting/ssas-tabular-metadata/tree/master/Application/Files) folder.

## How It Works
Through the command line, you will specify three files: an input BIM file, a change CSV file, and an output BIM file.

**Input and Output BIM Files** These correspond to a BIM file generated in a Visual Studio Analysis Services Tabular project. The resulting BIM file can directly replace the BIM file in your Analysis Services Tabular project.

**Change CSV File** This file contains the instructions to alter or create properties in the BIM file. The utility will find the element in the BIM files and then apply the specified property values. The structure of the CSV file is as follows:

table `‡`                 |elementType `‡`                                   |elementName `‡`               |{property&#x2011;1}         | {property&#x2011;n}
:-------------------------|:-------------------------------------------------|:-----------------------------|:---------------------------|:----------------------------
Name of the cube table.   |Type of element: {**`column`** or **`measure`**}. |Name of the column or measure.|Value of {property&#x2011;1}|Value of {property&#x2011;n}

**`‡`** indicates that a value is required in each row of the CSV.

The term **{property}** corresponds to JSON properties used withing in the BIM file. Inspection of your BIM file is the best way to identify the full list of available properties. Common properties of columns and measures are **`description`**, **`formatString`**, **`displayFolder`**. **Properties names are case sensitive**

The CSV can support as many properties (1..n) as you wish to specify in the file.  Properties are added by creating a column in the CSV with the property name.

Blank property values will be ignored by the utility. In other words, if there is an original property value in-place, a blank property value in the CSV will not erase the original.

In constructing the change CSV file, it is useful to arrange the contents in Excel and export as a CSV.


## Command Line Execution
Command line arguments may be provided in any order:

Argument&nbsp;&nbsp;&nbsp;&nbsp;|Description
:----------------------|:---------------
**-i `{path}`**      | Identifies the input BIM file used in the transformation.  The file path replaces **`{path}`**.
**-c `{path}`**      | Identifies the change CSV file.  The file path replaces **`{path}`**.
**-o `{path}`**      | Identifies the output BIM resulting from appply changes.  The file path replaces **`{path}`**.
**-w**               | Presence of the argument instructs the application to ignore the **`-o`** parameter and overwrite the input BIM file with the output.

**Example 1**
Transform an input BIM file into a different output BIM file.

**```TabularMetdata.exe -i "model.bim" -c "changes.csv" -o "model2.bim"```**

**Example 2**
Transform an input BIM file and then overwrite the input BIM file with its changed version.

**```TabularMetdata.exe -i "model.bim" -c "changes.csv" -w```**