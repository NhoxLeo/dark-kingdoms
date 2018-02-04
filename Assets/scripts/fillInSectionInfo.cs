using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class fillInSectionInfo : MonoBehaviour {

	public Text sectionName;
	public int population;
	public Image mapImage;
	public Image interiorImage;
	public Image mainImage;

	public void changeMainImage (Image newImage) {
		mainImage.sprite = newImage.sprite;
	}

	// Use this for initialization
	void Start () {
		GameObject go1;
		kingdom_section mySec;
		
		go1 = GameObject.Find(misc_global_data.section_to_load);
		mySec = (kingdom_section) go1.GetComponent(typeof(kingdom_section));
	
		sectionName.text = mySec.sectionName;
		
		mapImage.sprite = mySec.mapImage;
		interiorImage.sprite = mySec.interiorImage;
		mainImage.sprite = mySec.mapImage;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
