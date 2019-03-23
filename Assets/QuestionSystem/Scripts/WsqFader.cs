using DG.Tweening;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuestionSystem.Scripts{
	public class WsqFader : OverridableMonoBehaviour{
		
		[HideLabel]
		[EnumToggleButtons]
		public LayoutQuestion ComponentLayout;
		[HideLabel]
		[EnumToggleButtons]
		public WsqFaderType TypeFader;
		[HideLabel]
		[EnumToggleButtons]
		public WsqAreaType AreaType;

		public Image CurrenImage;
		public TextMeshProUGUI TextMeshProComponent;
		public Image IconImage;
		public Sprite CorrectIcon;
		public Sprite WrongIcon;

		private void OnValidate(){
			if(TypeFader == WsqFaderType.ImageComponent){
				GetCurrentImageComponent();
			} else{
				GetCurrentTextComponent();
			}
		}

		private void Start(){
			if(TypeFader == WsqFaderType.ImageComponent){
				GetCurrentImageComponent();
			} else{
				GetCurrentTextComponent();
			}
		}

		private void GetCurrentImageComponent(){
			if (CurrenImage != null) return;
			CurrenImage = this.GetComponent(typeof(Image)) as Image;
		}

		public void ShowImage(float fadeTime = .5f){
			ImageFader(fadeValue: 1f, fadeTime: fadeTime);
		}

		public void HideImage(float fadeTime = .5f){
			ImageFader(fadeValue: 0f, fadeTime: fadeTime);
		}
		
		public void ShowText(float fadeTime = .5f){
			TextFader(fadeValue: 1f, fadeTime: fadeTime);
		}

		public void HideText(float fadeTime = .5f){
			TextFader(fadeValue: 0f, fadeTime: fadeTime);
		}
		
		public void ShowRight(float fadeTime = .5f)
		{
			IconImage.sprite = CorrectIcon;
			IconImage.DOFade(1f, fadeTime);
		}
		
		public void ShowWrong(float fadeTime = .5f)
		{
			IconImage.sprite = WrongIcon;
			IconImage.DOFade(1f, fadeTime);
		}

		public void HideIcon(float fadeTime = .5f){
			IconImage.DOFade(0f, fadeTime);
		}

		public void ImageFader(float fadeValue = 1f, float fadeTime = .5f){
			GetCurrentImageComponent();
			if (CurrenImage == null) return;
			CurrenImage.DOFade(fadeValue, fadeTime);
		}
		
		public void TextFader(float fadeValue = 1f, float fadeTime = .5f){
			GetCurrentImageComponent();
			if (TextMeshProComponent == null) return;
			TextMeshProComponent.DOFade(fadeValue, fadeTime);
		}

		public void GetCurrentTextComponent(){
			if (TextMeshProComponent != null) return;
			TextMeshProComponent = GetComponent<TextMeshProUGUI>();
		}
		
	}

	public enum WsqFaderType{
		TextComponent,
		ImageComponent
	}

	public enum WsqAreaType{
		None,
		Enunciado,
		TextQuestions
	}
}
