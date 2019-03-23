using UnityEngine;

public class SpriteFitToScreen : OverridableMonoBehaviour {

	// Use this for initialization
	void Start () {
        ResizeToFit();
    }
	
    public void ResizeToFit() {
        var sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1f, 1f, 1f);
        Vector3 localS = transform.localScale;

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        localS.x = worldScreenWidth / width;
        localS.y = worldScreenHeight / height;
        transform.localScale = localS;
      }
}
