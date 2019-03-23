using System.Collections.Generic;
using UnityEngine;

public class ToolHere : MonoBehaviour {

    [HideInInspector]
    public bool active = false;
    public List<GameObject> _desactiveGameObjects;
    public List<GameObject> _activeGameObjects;

    public void Toggle() {
        active = !active;

        if (active) {
            int count = _desactiveGameObjects.Count;

            for (int i = 0; i < count; i++) {
                _desactiveGameObjects[i].SetActive(true);
            }

            count = _activeGameObjects.Count;

            for (int i = 0; i < count; i++) {
                _activeGameObjects[i].SetActive(false);
            }
        } else {
            int count = _desactiveGameObjects.Count;

            for (int i = 0; i < count; i++) {
                _desactiveGameObjects[i].SetActive(false);
            }

            count = _activeGameObjects.Count;

            for (int i = 0; i < count; i++) {
                _activeGameObjects[i].SetActive(true);
            }
        }
    }
	
}
