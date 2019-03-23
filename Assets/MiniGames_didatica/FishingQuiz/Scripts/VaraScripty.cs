using UnityEngine;

public class VaraScripty : MonoBehaviour {
    public SpriteRenderer spr;
    public string nomeLayer;
    public int NumbLayer;

	// Use this for initialization
	void Start () {
        //spr = GetComponent<SpriteRenderer>();
        spr.sortingLayerName = nomeLayer;
        spr.sortingOrder = NumbLayer;

    }	
	
}
