using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuestionSystem.Scripts{
	public class LayoutHelper : OverridableMonoBehaviour{
		
		[HideInEditorMode]
		public bool ShowDrid = false;
		
		[SceneObjectsOnly]
		[ListDrawerSettings(ShowPaging = true, NumberOfItemsPerPage = 5)]
		public List<Image> LayoutImages = new List<Image>();
		
		[ReadOnly]
		public List<WsqFader> _faders = new List<WsqFader>();

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
		[Button("Show Grid Layout")]
		[HideIf("ShowDrid")]
		public void ShowGridHelper(){
			
			var tempCount = LayoutImages.Count;
			for (var i = 0; i < tempCount; i++){
				if (!LayoutImages[i].isActiveAndEnabled){
					LayoutImages[i].enabled = true;
				}
			}
			ShowDrid = true;
		}
		[Button("Hide Grid Layout")]
		[ShowIf("ShowDrid")]
		public void HideGridHelper(){
			var tempCount = LayoutImages.Count;
			for (var i = 0; i < tempCount; i++){
				if (LayoutImages[i].isActiveAndEnabled){
					LayoutImages[i].enabled = false;
				}
			}
			ShowDrid = false;
		}
#endif
		[Button("Force Validation")]
		private void OnValidate(){
			GetAllFaders();
		}

		private void GetAllFaders(){
			_faders.Clear();
			var faders = FindObjectsOfType(typeof(WsqFader));
			var tempCount = faders.Length;
			for (var i = 0; i < tempCount; i++){
				if (!_faders.Contains(faders[i] as WsqFader)){
					_faders.Add(faders[i] as WsqFader);
				}
			}
		}
		
		public WsqFader GetEnunciadoComponent(){
			var listComponents = GetAllFadersByLayout();
			return listComponents.FirstOrDefault(t => t.AreaType == WsqAreaType.Enunciado);
		}
		
		public WsqFader[] GetAlternativesComponent(){
			var listComponents = GetAllFadersByLayout();
			return new[]{listComponents.Find(t => t.AreaType == WsqAreaType.TextQuestions && t.TypeFader == WsqFaderType.TextComponent)};
		}
		
		
		
		public List<T> GetComponent<T>(){
			List<T> components = new List<T>();
			var allFaders = GetAllFadersByLayout();
			int tempCount = allFaders.Count;
			for (int i = 0; i < tempCount; i++){
				var componentvalue = allFaders[i].GetComponent<T>();
				if (componentvalue != null){
					components.Add(componentvalue);
				}
			}

			if (components.Count > 0){
				return components;
			} else{
				return null;
			}
		}


		/// <summary>
		/// Retornar lista de components WSQFader filtrados pelo tipo de layout.
		/// Breve usando cache system para melhor performace em tempo real.
		/// TODO cache system ussing arrayList.
		/// </summary>
		/// <param name="filterLayout"></param>
		/// <param name="filterFaderType"></param>
		/// <returns></returns>
		private List<WsqFader> GetAllFadersByLayout(LayoutQuestion filterLayout = LayoutQuestion.TextOnly, WsqFaderType filterFaderType = WsqFaderType.TextComponent){
			var tempCount = _faders.Count;
			var resultList = new List<WsqFader>();
			for (var i = 0; i < tempCount; i++){
				if (_faders[i].ComponentLayout == filterLayout && _faders[i].TypeFader == filterFaderType && !resultList.Contains(_faders[i])){
					resultList.Add(_faders[i]);
				}
			}
			return resultList;
		}
		
		
	}
	
	public enum LayoutQuestion{
		None = 0,
		TextOnly = 1
	}
}
