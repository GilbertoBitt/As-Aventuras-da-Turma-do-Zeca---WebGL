using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;
//using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUpdater : MonoBehaviour
{
//	public AssetLabelReference PreloadLabel;
	public SceneReference SceneOnStart;
	public Slider SliderComponent;
	public TextMeshProUGUI TextPercentageLoading;

	// Use this for initialization
	void Start () {
		//yield return new WaitForSeconds(2f);
//		var asyncOp = Addressables.PreloadDependencies(PreloadLabel.labelString, null);
//		asyncOp.Completed += LoadEnded;
//		asyncOp.ObserveEveryValueChanged(x => x.PercentComplete).Subscribe(x =>
//		{
//			SliderComponent.value = x;
//			TextPercentageLoading.text = x.ToString("P", CultureInfo.InvariantCulture);
//		});
	}

//	private void LoadEnded(IAsyncOperation<IList<object>> obj)
//	{
//		if (obj.Status == AsyncOperationStatus.Succeeded)
//		{
//			TextPercentageLoading.text = "Atualização Finalizada!";
//			SceneManager.LoadSceneAsync(SceneOnStart);
//		}
//		else
//		{
//			TextPercentageLoading.text = "Falha no download, Tentando Novamente...";
//		    //TODO criar botão para tentar novamente atualizar os assetBundles.
//		}
//	}
}
