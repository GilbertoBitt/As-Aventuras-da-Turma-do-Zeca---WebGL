using System.Collections.Generic;
using UnityEngine;

public class ExempleDictionaryHAsh : MonoBehaviour {

    public Dictionary<int,string> dicTest = new Dictionary<int,string>();
    public int[] Keys = new int[20];
    public string[] ListOfWords = new string[20];

	// Use this for initialization
	void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space)) {
            TestDict();
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            DebugLogDict();
        }

	}

    public void TestDict() {
        dicTest.Clear();
        int tempCount = ListOfWords.Length;
        Keys = new int[tempCount];
        for (int i = 0; i < tempCount; i++) {
            Keys[i] = Animator.StringToHash(ListOfWords[i]);
            dicTest.Add(Keys[i],ListOfWords[i]);
        }
    }

    public void DebugLogDict() {
        int tempCount = dicTest.Count;

        for (int i = 0; i < tempCount; i++) {

            Debug.Log("Key: " + Keys[i] + " Value: " + dicTest[Keys[i]]);
            
        }
    }

   
}
