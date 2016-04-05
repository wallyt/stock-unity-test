// based on http://catlikecoding.com/unity/tutorials/graphs/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LineGraphParticles : MonoBehaviour {

	public int resolution = 10;
	public GameObject Label;

	private ParticleSystem.Particle[] points;
	private Text value;

	void Start () {
		if (resolution <10 || resolution > 100) {
			Debug.LogWarning ("Grapher resolution out of bounds. Resetting to minimum", this);
			resolution = 10;
		}
		points = new ParticleSystem.Particle[resolution];
		// X-axis is 0 to 1 and increment is the distance between x values, assuming first value at 0
		float increment = 1f / (resolution - 1);
		for (int i = 0; i < resolution; i++) {
			float x = i * increment;
			points[i].position = new Vector3(x, 0f, 0f);
			points[i].startColor = new Color(x, 0f, 0f);
			points[i].startSize = 0.1f;

			// Add labels
			GameObject label = Instantiate(Label) as GameObject;
			value = label.GetComponentInChildren<Text>();
			value.text = i.ToString();
			value.rectTransform.position = new Vector3(i * 50f, i * 50f, 0f);
			Debug.Log(value.transform.position);
		}

		// Problem with this approach is that the particles aren't interactable objects

	}

	void Update () {
		GetComponent<ParticleSystem>().SetParticles(points, points.Length);

	}
}
