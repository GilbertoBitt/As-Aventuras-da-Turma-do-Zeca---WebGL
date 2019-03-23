using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using MEC;

public class LoadManager : MonoBehaviour {

	public Slider loadSlider;
	public GameObject loadSliderGameObject;
	private bool isLoading = false;
	public UnityEvent OnBeforeLoad;
	public UnityEvent OnStartScene;
	public UnityEvent OnStartScripts;
	public CanvasScaler thisCanvasScaler;
	public GraphicRaycaster thisGraphicRaycaster;
	public bool FadeOnStart = false;
	public Canvas panelLoad;
	public Image panelLoadImage;
	public float delayStart;
	public AnimationCurve fadeInCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);


	void Start(){
        Application.backgroundLoadingPriority = ThreadPriority.High;
        panelLoad = this.GetComponent<Canvas>();

		if (FadeOnStart) {
			Timing.RunCoroutine(FadeIn(delayStart));

        } else {
			panelLoad.enabled = false;
		}

        Timing.RunCoroutine(removeSplash());

    }

    public void DisablePanel() {
        if (panelLoad != null) {
            panelLoad.enabled = false;
        }
    }

    public IEnumerator<float> removeSplash() {        
        OnStartScene.Invoke();
        OnStartScripts.Invoke();
        yield return Timing.WaitForSeconds(.2f);
        removePanel();

    }




    public void removePanel() {
        panelLoad.enabled = false;
    }

    IEnumerator<float> FadeIn(float _delay){

		yield return Timing.WaitForSeconds(0.1f);
		OnStartScene.Invoke();
		yield return Timing.WaitForSeconds(0.2f);
		OnStartScripts.Invoke();
		/*float duration = _delay;
		float times = 0.0f;
		while (times < duration)
		{

			if (duration == duration / 2) {
				OnStartScripts.Invoke();
			}
			times += Time.deltaTime;
			float s = times / duration;
			panelLoadImage.color = Color.Lerp (Color.white, new Color (1f, 1f, 1f, 0f), fadeInCurve.Evaluate (s));
			yield return 0f;
		}*/
		yield return Timing.WaitForSeconds (_delay);
        if (panelLoad != null) {
            panelLoad.enabled = false;
        }

	}

	public void LoadAsync(string scene){
        Application.backgroundLoadingPriority = ThreadPriority.High;
        thisCanvasScaler.enabled = true;
		thisGraphicRaycaster.enabled = true;
		if (!isLoading) {
            panelLoad.enabled = true;
            panelLoadImage.color = Color.white;
			loadSliderGameObject.SetActive(true);
			//SceneManager.LoadScene (scene, LoadSceneMode.Single);
			StopAllCoroutines();
            Timing.KillCoroutines();
			Timing.RunCoroutine (AsyncLoad (scene));
		}
	
	}

	public void LoadAsync(int scene){
        Application.backgroundLoadingPriority = ThreadPriority.High;
        thisCanvasScaler.enabled = true;
		thisGraphicRaycaster.enabled = true;
		if (!isLoading) {
            panelLoad.enabled = true;
            panelLoadImage.color = Color.white;
			loadSliderGameObject.SetActive(true);
			//SceneManager.LoadScene (scene, LoadSceneMode.Single);
			StopAllCoroutines();
			StartCoroutine (AsyncLoad (scene));
		}

	}



	IEnumerator<float> AsyncLoad(string scene){
		OnBeforeLoad.Invoke();
		//yield return Yielders.Get(0.1f);
		isLoading = true;
		loadSlider.value = 0;
        AsyncOperation loading = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
		//loading.allowSceneActivation = false;

		while (loading.progress < 0.90f) {
            yield return Timing.WaitForOneFrame;
			//Debug.Log (loading.progress);
			loadSlider.value = loading.progress;
		}
        loadSlider.value = 1;

        //changeScener(loading);
        //Timing.RunCoroutine(changeScene(loading));
    }

	IEnumerator AsyncLoad(int scene){
		OnBeforeLoad.Invoke();
		//yield return Yielders.Get(0.1f);
		isLoading = true;
		loadSlider.value = 0f;
		AsyncOperation loading = SceneManager.LoadSceneAsync (scene, LoadSceneMode.Single);
		//loading.allowSceneActivation = false;

		while (loading.progress < 0.9f) {
			yield return Yielders.EndOfFrame;
            //Debug.Log (loading.progress);
            loadSlider.value = loading.progress;
		}
        loadSlider.value = 1;
        // changeScener(loading);
        //Timing.RunCoroutine(changeScene(loading));

    }

	IEnumerator<float> changeScene(AsyncOperation _load){
		yield return Timing.WaitForSeconds(0.1f);
		_load.allowSceneActivation = true;
		loadSlider.value = 1;
		//this.transform.GetChild (0).gameObject.SetActive (false);
		isLoading = false;
	}

    public void changeScener(AsyncOperation _load) {
        _load.allowSceneActivation = true;
        loadSlider.value = 1;
        //this.transform.GetChild (0).gameObject.SetActive (false);
        isLoading = false;
    }
}
