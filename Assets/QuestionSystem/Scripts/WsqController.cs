using System.Collections.Generic;
using DG.Tweening;
using MEC;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.Serialization;
using UnityEngine.Playables;

namespace QuestionSystem.Scripts{
	public class WsqController : OverridableMonoBehaviour{

		public WsqManager SystemManager;
		public LayoutHelper SystemHelper;
		public WsqFader EnunciadoFaders;
		public WsqFader[] AnswersFaders;
		public Image[] ImageItens;
		public Button[] Buttons;
		public Color DefaultColor = Color.white;
		public Color HighlightedColor = Color.yellow;
		public float CrossFadeSpeed = .5f;
		public int selectedAnswer;
		public int correctAnswer;
		public Button ConfirmButton;
		public TextMeshProUGUI textComponent;
		public CanvasGroup HoleCanvasGroup;
		public QuestionResult[] QuestionsR;
		public CanvasGroup SecondPanel;
		public Image ClockImage;
		public RectTransform ClockImageTransform;
		public PlayableDirector ClockPlayableDirector;
		public Animator ClockPlayableAnimator;
		
		[Button("Open Window")]
		public void OpenQuestionScreen()
		{
			ClockPlayableDirector.time = 0.0;
			ClockPlayableDirector.enabled = true;
			ClockPlayableAnimator.enabled = true;
			ClearAllIcons();
			SystemManager.InitializeSystem();
			EnunciadoFaders = SystemHelper.GetEnunciadoComponent();
			QuestionsR = FindObjectsOfType<QuestionResult>();
			ShowWindow();
		}
		[Button("Test Animation")]
		public void ShowWindow()
		{
			var windowAnimation = DOTween.Sequence();
			windowAnimation.AppendCallback(() => ClockPlayableDirector.Play());
			windowAnimation.AppendInterval((float) ClockPlayableDirector.playableAsset.duration);
			windowAnimation.AppendCallback(() => SecondPanel.interactable = true);
			windowAnimation.AppendCallback(() => SecondPanel.blocksRaycasts = true);
			windowAnimation.AppendCallback(SetTextQuestions);
			windowAnimation.AppendCallback(SetupQuestions);
			//WindowAnimation.Append(SecondPanel.DOFade(1f, .6f));

		}

		public void SetupQuestions(){
			ClearAllItens(-1);
			ButtonInteractables(true);
			selectedAnswer = -1;
			
			EnunciadoFaders.ShowImage();
			EnunciadoFaders.ShowText();
			//question.OptionAnswers.Suffle();
			int countTemp = AnswersFaders.Length;
			for (int i = 0; i < countTemp; i++){
				/*AnswersFaders[i].TextMeshProComponent.SetText(question.OptionAnswers[i].Value as string);
				if (question.OptionAnswers[i].IsCorrect){
					correctAnswer = i;
				}*/
				AnswersFaders[i].ShowImage();
				AnswersFaders[i].ShowText();
			}

			//HoleCanvasGroup.DOFade(1f, .6f);
			HoleCanvasGroup.interactable = true;
			HoleCanvasGroup.blocksRaycasts = true;
		}

		public void SetTextQuestions()
		{
			var question = SystemManager.GetQuestionBase();
			EnunciadoFaders.TextMeshProComponent.SetText(question.Value as string);
			question.OptionAnswers.Suffle();
			int countTemp = AnswersFaders.Length;
			for (int i = 0; i < countTemp; i++)
			{
				AnswersFaders[i].TextMeshProComponent.SetText(question.OptionAnswers[i].Value as string);
				if (question.OptionAnswers[i].IsCorrect)
				{
					correctAnswer = i;
				}
			}
		}

		public void UpdateCorrectSelected(int i){
			selectedAnswer = i;
			ClearAllItens(i);
		}

		public void ClearAllItens(int j){
			int tempCount = ImageItens.Length;
			for (int i = 0; i < tempCount; i++){
				ImageItens[i].CrossFadeColor(i == j ? HighlightedColor : DefaultColor, CrossFadeSpeed, false, false);
			}

			if (j < 0) return;
			ConfirmButton.interactable = true;
			textComponent.color = ConfirmButton.colors.normalColor;
		}
		
		public void ShowRightIcon(bool isRight)
		{
			
			
			ClearAllIcons();
			if (isRight) {
				AnswersFaders[selectedAnswer].ShowRight();
			}
			else
			{
				AnswersFaders[selectedAnswer].ShowWrong();
				AnswersFaders[correctAnswer].ShowRight();
			}
		}

		public void ButtonInteractables(bool enable = true){
			for (int i = 0; i < Buttons.Length; i++){
				Buttons[i].interactable = enable;
			}
		}

		public void ClearAllIcons()
		{
			int tempCount = AnswersFaders.Length;
			for (int i = 0; i < tempCount; i++)
			{
				AnswersFaders[i].HideIcon();
			}
		}
		

		public void ConfirmButtonAction(){
			ButtonInteractables(false);
			textComponent.color = ConfirmButton.colors.disabledColor;
			ClockPlayableDirector.enabled = false;
			ClockPlayableAnimator.enabled = false;
			Timing.RunCoroutine(ShowButtonsThenFinalize());
			//SendEventIsCorrect(correctAnswer == selectedAnswer);
			//HoleCanvasGroup.DOFade(0f, .5f);
		}

		IEnumerator<float> ShowButtonsThenFinalize()
		{
			ShowRightIcon(correctAnswer == selectedAnswer);
			yield return Timing.WaitForSeconds(2f);
			SendEventIsCorrect(correctAnswer == selectedAnswer);
			HoleCanvasGroup.DOFade(0f, .5f);
		}


		public void SendEventIsCorrect(bool isRight){
			if (QuestionsR == null) return;
			foreach (var e in QuestionsR){
				e.IsCorrect(isRight);
			}
		}

	}
}
