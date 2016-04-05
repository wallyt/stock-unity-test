﻿public class TableCodeGen
{
	const string codeTemplate = @"// This code automatically generated by TableCodeGen
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class $CLASS
{
	public class Row
	{
$ROW_MEMBER_CODE
	}

	List<Row> rowList = new List<Row>();
	bool isLoaded = false;

	public bool IsLoaded()
	{
		return isLoaded;
	}

	public List<Row> GetRowList()
	{
		return rowList;
	}

	public void Load(TextAsset csv)
	{
		rowList.Clear();
		string[][] grid = CsvParser2.Parse(csv.text);
		for(int i = 1 ; i < grid.Length ; i++)
		{
			Row row = new Row();
$ROW_READ_CODE
			rowList.Add(row);
		}
		isLoaded = true;
	}

	public int NumRows()
	{
		return rowList.Count;
	}

	public Row GetAt(int i)
	{
		if(rowList.Count <= i)
			return null;
		return rowList[i];
	}

$FIND_CODE
}";

	const string findCodeTemplate = 
@"	public Row Find_$COLUMN(string find)
	{
		return rowList.Find(x => x.$COLUMN == find);
	}
	public List<Row> FindAll_$COLUMN(string find)
	{
		return rowList.FindAll(x => x.$COLUMN == find);
	}
";

	public static string Generate(string csvText, string className)
	{
		if(string.IsNullOrEmpty(csvText))
			return null;
		
		string[][] grid = CsvParser2.Parse(csvText);
		if(grid.Length < 1)
			return null;

		string rowMemberCode = "";
		string rowReadCode = "";
		string findCode = "";
		for(int i = 0 ; i < grid[0].Length ; i++)
		{
			rowMemberCode += string.Format("\t\tpublic string {0};\n", grid[0][i]);
			rowReadCode += string.Format("\t\t\trow.{0} = grid[i][{1}];\n", grid[0][i], i);
			findCode += findCodeTemplate.Replace("$COLUMN", grid[0][i]);
		}
		
		string code = codeTemplate;
		code = code.Replace("$CLASS", className);
		code = code.Replace("$ROW_MEMBER_CODE", rowMemberCode);
		code = code.Replace("$ROW_READ_CODE", rowReadCode);
		code = code.Replace("$FIND_CODE", findCode);

		return code;
	}
}
