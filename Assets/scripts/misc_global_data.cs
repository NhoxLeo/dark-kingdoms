using UnityEngine;
using System.Collections;

public class misc_global_data : MonoBehaviour {

	public static string section_to_load = "DEFAULT";
	
	// Use this for initialization
	void Awake () {
		GameObject.DontDestroyOnLoad(gameObject);
	}
}
