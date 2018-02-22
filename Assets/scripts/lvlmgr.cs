using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class lvlmgr : MonoBehaviour {

	public void loadlevel(string name) {
		SceneManager.LoadScene(name);
	}
}
