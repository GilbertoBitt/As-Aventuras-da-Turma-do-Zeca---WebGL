using UnityEngine;
using TMPro;
using DG.Tweening;

public class CloudsEF02MA09 : OverridableMonoBehaviour {

    public Transform cloudTransform;
    public ManagerEF02MA09 manager;
    public BoxCollider2D boxCollider2D;
    public TextMeshPro textComponent;
    public SpriteRenderer spriteRender;
    public Rigidbody2D rigidbody2DThis;
    public int charCloud;
    public bool canMove = false;

    public void Start() {
        if(spriteRender == null) {
            spriteRender = this.GetComponent<SpriteRenderer>();
        }
    }

    /*public override void UpdateMe() {
        
        if(canMove && manager != null && manager.isPlaying) {
            Vector2 posNew = cloudTransform.localPosition;
            posNew.x = Mathf.Lerp(posNew.x,posNew.x + manager.cloudsStepSize,manager.cloudsMovementSpeed * Time.deltaTime);
            cloudTransform.localPosition = posNew;
        }

    }*/

    public void StartMoving() {
        rigidbody2DThis.velocity = new Vector2(manager.cloudsStepSize, 0f);
    }
    
    public void StopMoving() {
        rigidbody2DThis.velocity = Vector2.zero;
    }

    public void ResetCloud() {
        StopMoving();
        this.gameObject.SetActive(false);
        canMove = false;
        spriteRender.color = Color.white;
        manager.cloudsPool.Enqueue(this);
        manager.cloudsInstanced.Remove(this);
    }

    public void StartCloudMovement() {
        charCloud = Random.Range(manager.minRandomChar, manager.maxRandomChar);
        textComponent.SetText(charCloud.ToString());
        textComponent.DOFade(1f, .1f);
        spriteRender.color = Color.white;
        canMove = true;
        this.gameObject.SetActive(true);        
        StartMoving();
    }

    private void OnBecameInvisible() {
        ResetCloud();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag ("Player") && manager.isPlaying) {
			manager.PlayerGotCloud (charCloud, textComponent.transform.position);
			textComponent.DOFade (0f, .2f);
			spriteRender.DOFade (0f, 1f);
		}
    }


}
