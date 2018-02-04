using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class kingdom_section : MonoBehaviour {

	public string sectionName;
	public Sprite mapImage;
	public Sprite interiorImage;
	
	public int population;
	public int military;
	public int slaves;
	
	public int loyalty;
	public int fear;
	
	public int rp_lumber;
	public int rp_stone;
	public int rp_iron;
	public int rp_gold;
	public int rp_grain;
	public int rp_meat;
	
	public int rp2_grog;
	public int rp2_armor;
	public int rp2_weaps;
	

	// Use this for initialization
	void Awake () {
		GameObject.DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
