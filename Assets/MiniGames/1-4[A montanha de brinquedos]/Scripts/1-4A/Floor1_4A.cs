using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Floor1_4A : MonoBehaviour {

	public List<ItemHandler1_4A> floorItems = new List<ItemHandler1_4A>();
	public Image imageComp;

	// Use this for initialization
	void Start () {
		if(imageComp == null){
			imageComp = this.GetComponent<Image>();
		}
	}

	public void CheckFloorDone(){
		if (floorItems.All (x => x.hasEnded == true)) {

			imageComp.color = Color.clear;

		}
	}
}
