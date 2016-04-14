using UnityEngine;
using System.Collections;

public class WebImageGetter : MonoBehaviour {

	public string url1 = "https://udemy-images.udemy.com/course/750x422/212028_057f_6.jpg";

	// Doesn't work since it's not a jpg or png
	public string url2 = "https://www.instantstreetview.com/@30.384563,-97.710616,-56.61h,-6.84p,1z";

    IEnumerator Start() {
        WWW www = new WWW(url1);
        yield return www;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = www.texture;
    }

}
