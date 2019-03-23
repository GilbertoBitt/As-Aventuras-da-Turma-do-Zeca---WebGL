using UnityEngine;

public class FQBaitHandler : OverridableMonoBehaviour {

    public FQManager manager;

    public void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log(collision.name);
        FishContainer fc = collision.GetComponent<FishContainer>();
        if (fc != null && manager.holdingFish == false) {
            fc.bannerFish.enabled = false;
            fc.textComponent.alpha = 0f;
            manager.timerbannerAnswer.SetText(fc.valueAnswer);
            Time.timeScale = 0f;
            manager.HoldingFish(fc);
        }
    }

}
