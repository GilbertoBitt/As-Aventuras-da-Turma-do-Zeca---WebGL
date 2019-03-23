using UnityEngine;

public class ControlAvtivePlataform : MonoBehaviour {

   
    public PresetPlataforms1_3 PresetPlataforms1_3P;
    public BoxCollider2D boxcoll;
    bool checkPass;
    void Start () {
        PresetPlataforms1_3P = transform.parent.gameObject.GetComponent<PresetPlataforms1_3>();
        boxcoll = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") == true && checkPass==false) {
            boxcoll.enabled = false;
            checkPass = true;
            PresetPlataforms1_3P.StartGo();
            //Debug.Log("dsgsfd");
        }
    }
    
}

