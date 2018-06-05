using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;
using System;
using System.Linq;



[System.Serializable]
public class ARColorProbe : MonoBehaviour {


	public int readingHistorySize = 50;
	private int readingIndex = 0;
	private Color[] readingHistory;
	private Color currentProbedColor;
	public Color averageColor;
	public Color foundSampleColor;
	private CatalogOrble catalogOrble;
	public CatalogOrble CatalogOrble {
		get {
			return catalogOrble;
		}
	}

	public bool DidFoundCatalogOrble() {
		return catalogOrble != null;
	}


	public ColorSample[] samples = new ColorSample[5];

	[HideInInspector] public bool insideArea = false;
	[HideInInspector] public bool isActive = false;
	public bool drawPreview = false;

	private Camera arCamera;
	private Texture2D probingTexture;
	private GameObject probingPreview;
	private TrackableBehaviour trackable;



	void Awake() {
		arCamera = FindObjectOfType<VuforiaBehaviour>().GetComponentInChildren<Camera>();
		readingHistory = new Color[readingHistorySize];
		trackable = this.transform.parent.gameObject.GetComponent<TrackableBehaviour>();
	}


	//triggered when camera in view
	public void SetProbeActive(bool isActive) {


		if (isActive && !this.isActive) {
			//enable only if actively tracking
			if (trackable.CurrentStatus == TrackableBehaviour.Status.DETECTED ||
				trackable.CurrentStatus == TrackableBehaviour.Status.TRACKED ||
				trackable.CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {

				//check if we are inside boundaries
				if (insideArea) {
					//generate preview
					if (drawPreview) {
						probingPreview = GameObject.CreatePrimitive(PrimitiveType.Sphere);
						probingPreview.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Color");
						probingPreview.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
						probingPreview.transform.position = this.transform.position;
					}
					this.isActive = isActive;	
				}
			}
		}
		else if (!isActive && this.isActive) {
			if (probingPreview != null) Destroy(probingPreview.gameObject);
			this.isActive = isActive;	
			Reset();
		}


	}


	public void Reset() {
		catalogOrble = null;
		foundSampleColor = new Color();
		readingHistory = new Color[readingHistorySize];
		readingIndex = 0;
		currentProbedColor = new Color();
		averageColor = new Color();
	}




	void Update() {

		Vector3 probeScreenPosition = arCamera.WorldToScreenPoint(this.transform.position);
		Vector2 normalizedProbeScreenPosition = new Vector2((float)probeScreenPosition.x / (float)Screen.width, (float)probeScreenPosition.y / (float)Screen.height);

		//for some odd reason vuforia is rendering backgroud upside down, inverting in Y axis to read the right pixel
		normalizedProbeScreenPosition = new Vector2(normalizedProbeScreenPosition.x, 1- normalizedProbeScreenPosition.y); 


		//probe for color
		if (isActive) {
			
			if (probingTexture == null) {
				//find probing texture
				if (GameObject.Find("BackgroundPlane") != null) {
					probingTexture = (Texture2D)GameObject.Find("BackgroundPlane").GetComponent<Renderer>().sharedMaterial.mainTexture;
				}
				else {
					Debug.LogError("Cannot find BackgroundPlane");
				}
			}
			else {
				//probe current pixel from buffer texture
				currentProbedColor = probingTexture.GetPixel (Mathf.RoundToInt(normalizedProbeScreenPosition.x * probingTexture.width),
					Mathf.RoundToInt(normalizedProbeScreenPosition.y * probingTexture.height));


				readingHistory[readingIndex % readingHistorySize] = currentProbedColor;

				//reset index if we overflowing
				if (readingIndex < readingHistorySize) {
					readingIndex++;
				} else readingIndex = 0;


				averageColor = Extensions.CombineColors(readingHistory);

				//calculate closest color
				List<Color> sampleColors = new List<Color>();
				sampleColors.Add(samples[0].color);
				sampleColors.Add(samples[1].color);
				sampleColors.Add(samples[2].color);
				sampleColors.Add(samples[3].color);
				sampleColors.Add(samples[4].color);

				int closestIndex = closestColor1(sampleColors, averageColor);
				foundSampleColor = sampleColors[closestIndex];
				catalogOrble = samples[closestIndex].catalogOrble;

				if (probingPreview != null) {
					probingPreview.GetComponent<Renderer>().material.color = currentProbedColor;
				}
			}
		}


	}










	#region COLOR MATH


	// closed match for hues only:
	int closestColor1(List<Color> colors, Color target)
	{
		var hue1 = new HSBColor(target).h;
		var diffs = colors.Select(n => getHueDistance(new HSBColor(n).h, hue1));
		var diffMin = diffs.Min(n => n);
		return diffs.ToList().FindIndex(n => n == diffMin);
	}

	// closed match in RGB space
	int closestColor2(List<Color> colors, Color target)
	{
		var colorDiffs = colors.Select(n => ColorDiff(n, target)).Min(n =>n);
		return colors.FindIndex(n => ColorDiff(n, target) == colorDiffs);
	}

	// weighed distance using hue, saturation and brightness
	int closestColor3(List<Color> colors, Color target)
	{
		float hue1 = new HSBColor(target).h;
		var num1 = ColorNum(target);
		var diffs = colors.Select(n => Math.Abs(ColorNum(n) - num1) + 
			getHueDistance(new HSBColor(n).h, hue1) );
		var diffMin = diffs.Min(x => x);
		return diffs.ToList().FindIndex(n => n == diffMin);
	}

	//helpers

	public float factorSat = 1f;
	public float factorBri = 1f;

	// color brightness as perceived:
	float getBrightness(Color c)  
	{ return (c.r * 0.299f + c.g * 0.587f + c.b *0.114f) / 256f;}

	// distance between two hues:
	float getHueDistance(float hue1, float hue2)
	{ 
		float d = Math.Abs(hue1 - hue2); return d > 180 ? 360 - d : d; }
	float ColorNum(Color c) { return new HSBColor(c).s * factorSat + 
		getBrightness(c) * factorBri; }

	// distance in RGB space
	int ColorDiff(Color c1, Color c2) 
	{ return  (int ) Math.Sqrt((c1.r - c2.r) * (c1.r - c2.r) 
		+ (c1.g - c2.g) * (c1.g - c2.g)
		+ (c1.b - c2.b)*(c1.b - c2.b)); }



	#endregion
}
