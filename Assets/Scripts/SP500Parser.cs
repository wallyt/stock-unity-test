using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SP500Parser : MonoBehaviour {

	public TextAsset file;

	void Awake() {
		Load(file);
    }

	public class Row
	{
		public DateTime Date;
		public double Open;
		public double High;
		public double Low;
		public double Close;
		public Int64 Volume;
		public double AdjClose;
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
			row.Date = DateTime.Parse(grid[i][0]);
			row.Open = Convert.ToDouble(grid[i][1]);
			row.High = Convert.ToDouble(grid[i][2]);
			row.Low = Convert.ToDouble(grid[i][3]);
			row.Close = Convert.ToDouble(grid[i][4]);
			row.Volume = Convert.ToInt64(grid[i][5]);
			row.AdjClose = Convert.ToDouble(grid[i][6]);

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

	public Row Find_Date(DateTime find)
	{
		return rowList.Find(x => x.Date == find);
	}
	public List<Row> FindAll_Date(DateTime find)
	{
		return rowList.FindAll(x => x.Date == find);
	}
	public Row Find_Open(double find)
	{
		return rowList.Find(x => x.Open == find);
	}
	public List<Row> FindAll_Open(double find)
	{
		return rowList.FindAll(x => x.Open == find);
	}
	public Row Find_High(double find)
	{
		return rowList.Find(x => x.High == find);
	}
	public List<Row> FindAll_High(double find)
	{
		return rowList.FindAll(x => x.High == find);
	}
	public Row Find_Low(double find)
	{
		return rowList.Find(x => x.Low == find);
	}
	public List<Row> FindAll_Low(double find)
	{
		return rowList.FindAll(x => x.Low == find);
	}
	public Row Find_Close(double find)
	{
		return rowList.Find(x => x.Close == find);
	}
	public List<Row> FindAll_Close(double find)
	{
		return rowList.FindAll(x => x.Close == find);
	}
	public Row Find_Volume(int find)
	{
		return rowList.Find(x => x.Volume == find);
	}
	public List<Row> FindAll_Volume(int find)
	{
		return rowList.FindAll(x => x.Volume == find);
	}
	public Row Find_AdjClose(double find)
	{
		return rowList.Find(x => x.AdjClose == find);
	}
	public List<Row> FindAll_AdjClose(double find)
	{
		return rowList.FindAll(x => x.AdjClose == find);
	}
	public List<Row> FindAllCloseDateRange(DateTime start, DateTime end) {
        return rowList.FindAll(x => (x.Date >= start && x.Date <= end));
	}

}
