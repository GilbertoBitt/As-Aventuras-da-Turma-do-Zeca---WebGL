using System.Collections.Generic;
using UnityEngine;

public class SceneObjectsPoolManager1_3 : MonoBehaviour {

	public Dictionary<string,Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();

	public static SceneObjectsPoolManager1_3 instance { get; private set; }

	// Use this for initialization
	/*void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/

	public GameObject GetPrefabFromPool(string id){

		GameObject returnFromPool;

		if (pool[id].Count <= 0) {
			//Debug.Log("Sem instancia disponivel!");
			returnFromPool = null;
		} else {
			returnFromPool = pool[id].Dequeue();
		}

		return returnFromPool;

	}

	public void FillPool(GameObject Prefab, Transform parentTransform, int sizeOfPool){

		string instanceID = Prefab.name;

		for (int i = 0; i < sizeOfPool; i++) {
			GameObject prefabPool = Instantiate(Prefab, Vector3.one * 10000, Quaternion.identity, parentTransform) as GameObject;
			prefabPool.name = Prefab.name + "[" + i +"]";
			if (!pool.ContainsKey(instanceID)) {
				pool.Add(instanceID, new Queue<GameObject>());
			}

			pool[instanceID].Enqueue(prefabPool);
			prefabPool.SetActive(false);
		}
		
	}
}
