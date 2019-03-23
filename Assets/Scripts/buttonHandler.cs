using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class buttonHandler :  OverridableMonoBehaviour{

	private Vector2 startPoint;
	private Vector2 endPoint;
	private float delta;
	public gameSelection gSelection;
	public int minigameID;
    public TextMeshProUGUI text;

	public UnityEvent OnClick = new UnityEvent();

    public void Start() {
        text.ForceMeshUpdate();
        //text.SetAllDirty();
    }

    public void ButtonDown(){
		this.transform.localScale = new Vector3 (1f, 1f, 1f);
	}

	public void ButtonUp(){
		this.transform.localScale = new Vector3 (0.95f, 0.95f, 0.95f);
	}


}
