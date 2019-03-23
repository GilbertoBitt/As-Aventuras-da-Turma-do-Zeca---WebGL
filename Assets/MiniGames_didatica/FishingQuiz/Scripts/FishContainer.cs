using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.Playables;

public class FishContainer : OverridableMonoBehaviour {

    public FQManager manager;
    public Rigidbody2D rigidbody2DComp;
    public float minVelocity;
    public float maxVelocity;
    public Vector3 initScale;
    public string valueAnswer;
    public TextMeshPro textComponent;
    public PlayableDirector director;
    public SpriteRenderer bannerFish;

    [Button("Start Movement")]
    public void StartMoving() {
        rigidbody2DComp.velocity = new Vector2(Random.Range(minVelocity, maxVelocity), 0f);
    }

    public void StartMoving(bool isRight) {
        valueAnswer = manager.CurrentQuestion.ReturnRandomOne();
        bannerFish.sprite = manager.RandomBanner;
        textComponent.SetText(valueAnswer);
        initScale = this.transform.localScale;
        float velocity;
        if (isRight) {
            velocity = Random.Range(minVelocity, maxVelocity)*-1f;            
        } else {
            velocity = Random.Range(minVelocity, maxVelocity);   
        }

        if(velocity > 0) {
            textComponent.transform.localScale = new Vector3(Mathf.Abs(textComponent.transform.localScale.x) * -1f, textComponent.transform.localScale.y, textComponent.transform.localScale.z);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        } else {
            textComponent.transform.localScale = new Vector3(Mathf.Abs(textComponent.transform.localScale.x), textComponent.transform.localScale.y, textComponent.transform.localScale.z);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        director.Play();
        rigidbody2DComp.velocity = new Vector2(velocity, 0f);
    }

    public void SpeedUP() {
        Vector2 velocity = rigidbody2DComp.velocity;
        velocity *= 5;
        rigidbody2DComp.velocity = velocity;
    }

    public void StopMoving() {
        rigidbody2DComp.velocity = Vector2.zero;
        director.Stop();
        textComponent.transform.localScale = new Vector3(Mathf.Abs(textComponent.transform.localScale.x), textComponent.transform.localScale.y, textComponent.transform.localScale.z);
    }

    private void OnBecameInvisible() {
        director.Stop();
        Invoke("ResetFish", 3f);
        //ResetFish();
    }



    public void ResetFish() {
        if (manager != null) {
            bannerFish.enabled = true;
            director.Stop();
            textComponent.alpha = 1f;
            rigidbody2DComp.velocity = Vector2.zero;
            this.transform.SetParent(manager.parentTransformPool);
            textComponent.transform.localScale = new Vector3(Mathf.Abs(textComponent.transform.localScale.x), textComponent.transform.localScale.y, textComponent.transform.localScale.z);
            manager.fishPool.Enqueue(this);
            manager.fishInstaces.Remove(this);
            this.transform.position = Vector3.one * 100f;
            this.transform.localScale = initScale;
            this.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

}
