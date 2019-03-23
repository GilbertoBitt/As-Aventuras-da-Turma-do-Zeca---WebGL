using UnityEngine;

public class control_limb :MonoBehaviour {

    public GameObject[] limb;
    public Animator animPerson;
    void Start() {
        Invoke("ativar",0.5f);
        animPerson = GetComponent<Animator>();

    }

    void ativar() {
        for (int i = 0; i < limb.Length; i++) {
            limb[i].SetActive(true);

        }

    }

    public void voltar0() {
        animPerson.SetInteger("danceAnim",0);
    }
}
