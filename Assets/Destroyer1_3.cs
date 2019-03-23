using UnityEngine;

public class Destroyer1_3 : MonoBehaviour {
    public PresetPlataforms1_3 PresetPlataforms1_3R;
     void Start() {
        PresetPlataforms1_3R = gameObject.transform.parent.parent.gameObject.GetComponent<PresetPlataforms1_3>();
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name == "collDesttotPlat" && PresetPlataforms1_3R.numbPlataform<=28) {
            Destroy(gameObject.transform.parent.parent.gameObject);
          //  Invoke("DestroyG",5f);

        }

    }
    private void DestroyG() {
        Destroy(gameObject.transform.parent.parent.gameObject);

    }

}
