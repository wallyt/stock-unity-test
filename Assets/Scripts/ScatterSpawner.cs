using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using DictionaryHelper;

public class ScatterSpawner : MonoBehaviour {

	public GameObject plusHolder;
	public GameObject negHolder;
	
	private PriceFinder priceFinder;


	void Start () {
		priceFinder = GameObject.FindObjectOfType<PriceFinder>();
		//float price = (float)priceFinder.GetClosePrice(DateTime.Parse("2016-03-10"));

		// Uses a struct for Dictionary value so that multiple results per date can be returned
		// This is defined in the DictionaryHelper namespace
		closeDictionary closePriceRange = priceFinder.FindClosePricesByDates("2015-01-01", "2015-12-31");

		// Holder to see if today's close was greater or less than previous day to determine dot color
		double yesterdayClose = 0d;

		// Get max and min values to scale the y axis values
		float minClose = (float)closePriceRange.Min(x => x.Value.closePrice);
		float maxClose = (float)closePriceRange.Max(x => x.Value.closePrice);
		float spreadClose = maxClose - minClose;

		// Plot each scatter dot by date, time, volume and whether it was < or > day before
		foreach (KeyValuePair<DateTime, closeDetails> kvp in closePriceRange.OrderBy(k => k.Key)) {

			// x position is chronological and y position is closing price; z is just placement
			float dayOfYear = kvp.Key.DayOfYear;
			float xPos = dayOfYear/365*20;
			float yPos = ((float)kvp.Value.closePrice - minClose) / spreadClose * 2;
			float zPos = 5f;

			// Width of the dot is volume
			float width = (float)kvp.Value.volume/10000000000;

			// If closing price > yesterday's, green dot, otherwise it was a down day
			GameObject whichDot = kvp.Value.closePrice >= yesterdayClose ? plusHolder : negHolder;
			GameObject dot = Instantiate(whichDot, new Vector3(xPos, yPos, zPos), Quaternion.identity) as GameObject;

			// Set width based on volume
			dot.transform.GetChild(0).transform.localScale += new Vector3(0, 0, width);

			// Keeping everything tidy
			dot.transform.parent = transform;

			// Generate the labels
			dot.GetComponentInChildren<TextMesh>().text = kvp.Key.ToString("d") + " " + kvp.Value.closePrice.ToString("F");

			yesterdayClose = kvp.Value.closePrice;
		}
	}

}
