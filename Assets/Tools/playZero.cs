#if UNITY_EDITOR || UNITY_EDITOR_WIN
using UnityEngine;
using UnityEditor;

public class playZero {

	

	[MenuItem("Tools/ResetPlayerPrefs")]
	private static void ResetPlayerPrefs(){
		
		PlayerPrefs.DeleteAll();
		
	}

}

#endif
