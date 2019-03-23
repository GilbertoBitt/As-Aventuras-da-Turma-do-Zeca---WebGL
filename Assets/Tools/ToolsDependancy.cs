#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class ToolsDependancy : MonoBehaviour
{

	public List<GameObject> ResultOfObjects = new List<GameObject>();
	public Object[] DependancyList;

	[Button("Tool Dependancy")]
	public void Init()
	{
		if (ResultOfObjects == null)
		{
			ResultOfObjects = new List<GameObject>();
		}
		ResultOfObjects.Clear();
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
		foreach(GameObject go in allObjects){
			if (go.activeInHierarchy) {
				//AllActiveObjectOnScene.Add((UnityEngine.Object) go);
				DependancyList =  EditorUtility.CollectDependencies(new UnityEngine.Object[] {go});
				int tempCount = DependancyList.Length;
				for (int i = 0; i < tempCount; i++)
				{
					var depObject = DependancyList[i];
					//var typeTemp = depObject as GameObject;
					if (depObject == null) continue;
					if (depObject is Collider2D && !ResultOfObjects.Contains(go))
					{
						Debug.Log("Type:" + depObject.GetType() + "\n" + "Searching for " + typeof(Collider2D));
						ResultOfObjects.Add(go);
					}
				}
				/*foreach (var dep in DependancyList)
				{
					var typeTemp = dep as GameObject;
					if (typeTemp != null) Debug.Log(typeTemp.GetType());
					Debug.Log("Type:" + dep.GetType() + "\n" + "Searching for " + UnitySearchType);
					if (dep.GetType() != UnitySearchType) return;
					Debug.Log(dep.GetType());
					Debug.Log(UnitySearchType);
					ResultOfObjects.Add(go);
				}*/
			}
			
		}
	}

}
#endif