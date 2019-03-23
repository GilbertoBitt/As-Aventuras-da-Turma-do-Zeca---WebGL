using UnityEngine;

public class ControlCarrinhos : MonoBehaviour {

    public Manager1_3A Manager1_3AR;
    public GameObject[] carrinhosPipoca;
    public GameObject[] carrinhosHospital;
    void Start()
    {
        this.Manager1_3AR = Camera.main.GetComponent<Manager1_3A>();

        if (Manager1_3AR.numbCont < 19)
        {
            for (int i = 0; i < carrinhosPipoca.Length; i++)
            {
                carrinhosPipoca[i].SetActive(true);
            }

            for (int i = 0; i < carrinhosHospital.Length; i++)
            {
                carrinhosHospital[i].SetActive(false);
            }

        }

        else {

            for (int i = 0; i < carrinhosPipoca.Length; i++)
            {
                carrinhosPipoca[i].SetActive(false);
            }

            for (int i = 0; i < carrinhosHospital.Length; i++)
            {
                carrinhosHospital[i].SetActive(true);
            }


        }
        //imgComp.sprite = imgs[1];     

    }


    // Update is called once per frame
   
}
