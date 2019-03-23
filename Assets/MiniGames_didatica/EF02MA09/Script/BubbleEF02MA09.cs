using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class BubbleEF02MA09 : OverridableMonoBehaviour {

    public Image imageComponent;
    public TextMeshProUGUI textComponent;
	public Transform textTransformComponent;
    public Image iconOk;
	public Sprite correctIcon;
	public Sprite wrongIcon;
	public Vector3 InitiallocalPosition;
    public AnimationCurve curveMovement;
    public Image circleImage;
    public Color defaultCircleColor;
    public Color circleColorRed;
    public bool founded = false;
	public int correctValue = -1;
	public TextMeshProUGUI correctValueText;
	public ManagerEF02MA09 mngr;

	public void Start(){
		textTransformComponent = textComponent.transform;
        InitiallocalPosition = textTransformComponent.localPosition;
	}

	public bool isCorrected(int corretPos){
		return corretPos == correctValue;
	}

	public Tweener IsCorrect(int corretPos, bool isCorrect = false){
		iconOk.sprite = isCorrected(corretPos) ? correctIcon : wrongIcon;
		return iconOk.DOFade(1f, .5f);
	}

	public Tweener ShowCorrect(int valueRight){
		correctValueText.SetText(valueRight.ToString());
		return correctValueText.DOFade(1f, .5f);
	}

	public void HideOk(){
		correctValueText.DOFade(0f, 0.1f);
		iconOk.DOFade(0f, 0.1f);
	}

    public void ShowText(string _string){
	    correctValue = int.Parse(_string);
        textComponent.SetText(_string);
        textComponent.DOFade(1f,.1f);
    }

	public void ShowText(int _string){
		correctValue = _string;
		textComponent.SetText(_string.ToString());
		textComponent.DOFade(1f,.1f);
        //founded = true;
	}

    public void HideText(){
	    correctValue = -1;
        textComponent.DOFade(0f,.1f);
        founded = false;
    }

	public void ShowText(int textString,Vector3 startPosition){
		if (textTransformComponent == null) {
			Start ();
		}
		Vector3 localPosition = transform.InverseTransformPoint (startPosition);
		textTransformComponent.localPosition = localPosition;
		textComponent.fontSize = 44.75f;
		ShowText (textString);
		Sequence showText = DOTween.Sequence();
		showText.Append(textTransformComponent.DOLocalMove(InitiallocalPosition, .1f, false).SetEase(curveMovement));
		showText.Append(DOTween.To(x => textComponent.fontSize = x, 44.75f, 36f, .1f).SetEase(curveMovement));
		//showText.AppendCallback(() => Time.timeScale = 0f);
        //founded = true;
    }

	public void StopTimer(){
		Time.timeScale = 0f;
	}
	
	public void ShowTextStopTimer(int textString,Vector3 startPosition, GameObject panel, Tweener tweener){
		if (textTransformComponent == null) {
			Start ();
		}
		Vector3 localPosition = transform.InverseTransformPoint (startPosition);
		textTransformComponent.localPosition = localPosition;
		textComponent.fontSize = 44.75f;
		ShowText (textString);
		//textTransformComponent.DOLocalMove (InitiallocalPosition, .1f, false).SetEase(curveMovement);
		//DOTween.To(x => textComponent.fontSize = x, 44.75f, 24f, .1f).SetEase(curveMovement);
		Sequence showText = DOTween.Sequence();
		
		showText.Append(textTransformComponent.DOLocalMove(InitiallocalPosition, .1f, false).SetEase(curveMovement));
		showText.Append(DOTween.To(x => textComponent.fontSize = x, 44.75f, 36f, .1f).SetEase(curveMovement));
		showText.AppendCallback(() => panel.SetActive(true));
		showText.Append(tweener);
		showText.AppendCallback(() => Time.timeScale = 0f);
		//founded = true;
	}
	
	

    public void WrongTextEffect(int textString, Vector3 startPosition) {
        if (textTransformComponent == null) {
            Start();
        }
        Vector3 localPosition = transform.InverseTransformPoint(startPosition);
        textTransformComponent.localPosition = localPosition;
        textComponent.fontSize = 44.75f;
        ShowText(textString);
        Sequence WrongTextEffect = DOTween.Sequence();
        if (founded == false) {
            WrongTextEffect.Append(textTransformComponent.DOLocalMove(InitiallocalPosition, .1f, false).SetEase(curveMovement));
            WrongTextEffect.Join(DOTween.To(x => textComponent.fontSize = x, 44.75f, 36f, .1f).SetEase(curveMovement));
        }
        WrongTextEffect.Append(textTransformComponent.DOShakePosition(.1f, new Vector3(1f, 1f, 0f), 5, 90, false, true));
        WrongTextEffect.Join(imageComponent.transform.DOShakePosition(.1f, new Vector3(1f, 1f, 0f), 5, 90, false, true));
        WrongTextEffect.Append(textTransformComponent.DOLocalMoveY(InitiallocalPosition.y - 300f, 0.1f));
        WrongTextEffect.Append(textComponent.DOFade(0f, .1f));
        WrongTextEffect.Play();

        founded = false;
    }

}
