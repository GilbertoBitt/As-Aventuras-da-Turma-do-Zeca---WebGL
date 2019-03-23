using UnityEngine;

public class ControlMoedaVacina : MonoBehaviour
{
    public GameObject partivulaMoeda;
    GameObject partivulaMoedaR;
    // Use this for initialization
    void Start()
    {
        
      //  partivulaPeR.SetActive(false);
    }

    // Update is called once per frame
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            partivulaMoedaR = Instantiate(partivulaMoeda, transform.position, Quaternion.identity) as GameObject;
            partivulaMoedaR.transform.SetParent(this.transform);
            partivulaMoedaR.transform.localScale = new Vector3(partivulaMoeda.transform.localScale.x, partivulaMoeda.transform.localScale.y, partivulaMoeda.transform.localScale.z);
            Destroy(partivulaMoedaR, 2f);
            this.gameObject.SetActive(false);
           
        }

    }
}
