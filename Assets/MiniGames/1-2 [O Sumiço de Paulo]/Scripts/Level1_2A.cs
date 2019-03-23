using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Level1_2A {
	public Image ImageBackground;
	public Transform personParent;
	public Transform particlesParent;
	public List<PlacesToHide_1_2A> hidenPlaces = new List<PlacesToHide_1_2A> ();
	public List<PlacesToHide_1_2A> hidenTreePlaces = new List<PlacesToHide_1_2A> ();
	public Transform[] parallaxLayers;
	public Transform touchParent;
	public float timer;

}
