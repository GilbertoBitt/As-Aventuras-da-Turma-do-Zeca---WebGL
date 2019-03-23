using UnityEngine;


public class ControlAnimNaoSel : MonoBehaviour {

	public bool arcoIris;
	public int numBrinkPerson;
	Animator animBrink;
    public bool arcoIrisCheck;
    void Start () {
       
            animBrink = GetComponent<Animator>();
            numBrinkPerson = Random.Range(0, 4);
            animBrink.SetInteger("numBrinkPerson", numBrinkPerson);
        
	}
	void Start2 () {
     
            animBrink = GetComponent<Animator>();
            numBrinkPerson = Random.Range(0, 4);
            animBrink.SetInteger("numBrinkPerson", numBrinkPerson);
        }
	
	
	// Update is called once per frame
	
}
