using UnityEngine;

public class ControlRaioVisor : MonoBehaviour {

    float randomNumb;
    public Sprite[] img;
    SpriteRenderer imgComp;
    bool chekcImg;
    bool liberNunb;
    public PresetPlataforms1_3 PresetPlataforms1_3s;
    public changeLevel changeLevel2;
    public Manager1_3A manager2;
    public RectTransform topLevel;
    bool checkRaio;
   
    void Start () {
        imgComp = GetComponent<SpriteRenderer>();
        PresetPlataforms1_3s = this.transform.parent.GetComponentInParent<PresetPlataforms1_3>();
        changeLevel2 = PresetPlataforms1_3s.AnimLevel.GetComponent<changeLevel>();
        manager2 = PresetPlataforms1_3s.manager;
        topLevel = changeLevel2.GetComponent<RectTransform>();
        topLevel.offsetMax = new Vector2(topLevel.offsetMax.x, 50f);
    }

    void Mudarspr() {
        if (chekcImg) {
            imgComp.sprite = img[0];
            randomNumb = Random.Range(1f, 1.5f);
            chekcImg = false;

        }      
        else{
            imgComp.sprite = img[1];
            randomNumb = Random.Range(1f, 1.5f);
            chekcImg = true;
        }
    }
	


	void Update () {
        Invoke("Mudarspr", randomNumb);

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") == true) {
           // Debug.Log("raio");
           PresetPlataforms1_3s.AnimLevel.SetActive(true);
           // PresetPlataforms1_3s.anim_AnimLevel.enabled = true;
            if (checkRaio == false) {
                checkRaio = true;
          
                manager2.contadorLevel += 1;
        }
            changeLevel2.startChangeLevelAnimation(manager2.contadorLevel);
            //topLevel.offsetMax = new Vector2(topLevel.offsetMax.x, 33.984f);
        }
    }

}
