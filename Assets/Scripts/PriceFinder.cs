using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using DictionaryHelper;

public class PriceFinder : MonoBehaviour {

	public SP500Parser parser;
	private Text text;

	void Awake () {
		parser = GameObject.FindObjectOfType<SP500Parser>();

//		text = GetComponent<Text>();
//		TestClosePrice("2016-03-10");
	}

	// For SP500 test scene
//	void TestClosePrice (string date) {
//		double closePrice = parser.Find_Date(date).Close;
//		text.text = closePrice.ToString();
//	}

	public double GetClosePrice (DateTime date) {
		return parser.Find_Date(date).Close;
	}

	public closeDictionary FindClosePricesByDates(string start, string end) {
		closeDictionary closeDatePrice = new closeDictionary();

		var results = parser.FindAllCloseDateRange(DateTime.Parse(start), DateTime.Parse(end));
		foreach(var result in results) {
			closeDatePrice.Add(result.Date, result.Close, result.Volume);
		}

		return closeDatePrice;
	}
}
