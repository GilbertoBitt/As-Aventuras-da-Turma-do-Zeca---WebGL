using DG.Tweening;
using UnityEngine;

public class UIBorderWalker : OverridableMonoBehaviour {

    public RectTransform imageTarget;
    public RectTransform followTarget;
    public float speedToFollow;
    public Vector3[] cornerLocalSpace = new Vector3[4];
	public Vector3[] screenCornersOnWorldSpace;
	public Vector3[] screenCorners;
    public AnimationCurve followCurve;
	public float xMinWorld;
	public float xMaxWorld;
	public float yMinWorld;
	public float yMaxWorld;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public Transform transform;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    public Rigidbody2D ridbody2D;
	public float playerInicialPosY;
	public float initialPosY;
	public float deltaPosY;

    public void Start() {
        imageTarget.GetLocalCorners(cornerLocalSpace);
		Rect rectCamera = Camera.main.rect;

		screenCorners = new Vector3[2];
		screenCorners [0] = new Vector2 (rectCamera.xMin, rectCamera.yMin);
		screenCorners [1] = new Vector2 (rectCamera.xMax, rectCamera.yMax);

		screenCornersOnWorldSpace = new Vector3[screenCorners.Length];
		for (int i = 0; i < screenCorners.Length; i++) {
			screenCornersOnWorldSpace [i] = Camera.main.ViewportToWorldPoint (screenCorners [i]);
		}

		xMinWorld = screenCornersOnWorldSpace [0].x;
		xMaxWorld = screenCornersOnWorldSpace [1].x;
		yMinWorld = screenCornersOnWorldSpace [0].y;
		yMaxWorld = screenCornersOnWorldSpace [1].y;

        Sequence followPath = DOTween.Sequence();
        followPath.SetLoops(-1,LoopType.Restart);
        followPath.SetEase(followCurve);
        followPath.Append(followTarget.DOLocalMove(cornerLocalSpace[1],speedToFollow));
        followPath.Append(followTarget.DOLocalMove(cornerLocalSpace[2],speedToFollow));
        followPath.Append(followTarget.DOLocalMove(cornerLocalSpace[3],speedToFollow));
        followPath.Append(followTarget.DOLocalMove(cornerLocalSpace[0],speedToFollow));
        followPath.Play();
    }

	public override void UpdateMe ()
	{
		if (Input.GetMouseButtonDown (0)) {
			initialPosY = Camera.main.ScreenToWorldPoint (Input.mousePosition).y;
			playerInicialPosY = transform.position.y;
		} else if (Input.GetMouseButton (0)) {
			deltaPosY = Camera.main.ScreenToWorldPoint (Input.mousePosition).y;
			float sensibility = Mathf.Abs(deltaPosY - transform.position.y);

			if (sensibility > 0.1f && transform.position.y < deltaPosY && ridbody2D.velocity.y == 0f) {
				SetRidbodyVelocityY (1f);
			} else if (sensibility > 0.1f && transform.position.y > deltaPosY && ridbody2D.velocity.y == 0f) {
				SetRidbodyVelocityY (-1f);
			} else {
				SetRidbodyVelocityY (0f);
			}
		} else if (Input.GetMouseButtonUp (0)) {
			SetRidbodyVelocityY (0f);
		}
	}

	public void SetRidbodyVelocityY(float _y){
		Vector2 newVelocity = ridbody2D.velocity;
		newVelocity.y = _y;
		ridbody2D.velocity = newVelocity;
	}

	public bool isPlayerInsideCameraBounds(){
		if (transform.position.y > yMaxWorld) {
			return false;
		} else if (transform.position.y < yMinWorld) {
			return false;
		} else {
			return true;
		}
	}
    
}
