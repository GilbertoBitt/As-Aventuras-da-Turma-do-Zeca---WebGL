using UnityEngine.UI;
using Sirenix.OdinInspector;

public class RadialSlideTool : OverridableMonoBehaviour {

    public Image imageRadial;
    public float offset = 0.0062f;
    public float initValue;
    [MinValue(0)]
    [MaxValue(100)]
    public float slider;
    public float sliderOld;

    private void OnValidate() {
        imageRadial.fillAmount = (slider * offset) + initValue;
    }

    public override void UpdateMe() {
        if(slider != sliderOld){
            sliderOld = slider;
            imageRadial.fillAmount = (slider * offset) + initValue;
        }
    }

}
