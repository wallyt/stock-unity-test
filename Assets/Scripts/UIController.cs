using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	public float fadeSpeed = 2f;
	public AnimationCurve acAlpha;
	
	private TextMesh label;

	void Start () {
		label = transform.parent.GetComponentInChildren<TextMesh>();
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.CompareTag("Player")) {
			StartCoroutine("FadeIn");
		}
	}

	void OnTriggerExit (Collider collider) {
		if (collider.CompareTag("Player")) {
			StartCoroutine("FadeOut");
		}
	}

	IEnumerator FadeIn() {
		for (float f = 1f; f >= -0.1f; f -= 0.1f) {
			Color c = label.color;
			c.a = 1 - f;
			label.color = c;
			yield return new WaitForSeconds(0.1f);
		}
	}

	IEnumerator FadeOut() {
		for (float f = 1f; f >= -0.1f; f -= 0.1f) {
			Color c = label.color;
			c.a = f;
			label.color = c;
			yield return new WaitForSeconds(0.1f);
		}
	}

}
