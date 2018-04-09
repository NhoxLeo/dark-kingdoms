using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
 * Stores the name of the kingdom map button which was pressed so that fillInSection knows which kingdon sectiob
 * object to retrieve.
 */
public class setSectionToLoad : MonoBehaviour {

	public void setit(string name) {
		misc_global_data.section_to_load = name;
		
		SceneManager.LoadScene("section info");
	}
}
