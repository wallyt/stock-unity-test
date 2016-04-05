using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ScatterSpawner : MonoBehaviour {

	public GameObject plusDot;
	public GameObject negDot;
	
	private PriceFinder priceFinder;

	void Start () {
        priceFinder = GameObject.FindObjectOfType<PriceFinder>();

        //float price = (float)priceFinder.GetClosePrice(DateTime.Parse("2016-03-10"));

        Dictionary<DateTime, double> closePriceRange = priceFinder.FindClosePricesByDates("2016-01-01", "2016-03-01");
		double yesterdayClose = 0d;

		// Plot each scatter dot by date, time and whether it was < or > day before
		foreach (KeyValuePair<DateTime, double> kvp in closePriceRange) {

			float dayOfYear = kvp.Key.DayOfYear;
			float xPos = dayOfYear/365*10;
			float yPos = (float)kvp.Value/1000;

			GameObject whichDot = kvp.Value >= yesterdayClose ? plusDot : negDot;

			GameObject dot = Instantiate(whichDot, new Vector3(xPos, yPos, 0f), Quaternion.identity) as GameObject;
			dot.transform.parent = transform;

			// Holder to see if today's close was greater or less than previous day to determine dot color
			yesterdayClose = kvp.Value;
		}
	}

	Vector2 PriceMapper (double price, int volume) {
		return new Vector2(0f, 0f);
	}
}
