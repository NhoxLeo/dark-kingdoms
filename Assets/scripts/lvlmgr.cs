using UnityEngine;
using System.Collections;

public class lvlmgr : MonoBehaviour {

	public void loadlevel(string name) {
		Application.LoadLevel(name);
	}
	
	public void quitGame() {
		Application.Quit();
	}
}
