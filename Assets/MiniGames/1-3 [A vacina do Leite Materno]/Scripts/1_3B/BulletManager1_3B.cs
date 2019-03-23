using UnityEngine;

public class BulletManager1_3B : MonoBehaviour {

	public Rigidbody2D thisRigidbody;
	public Manager1_3B thisManager;
    public bool isOnPool;

	void OnBecameInvisible(){	
		ResetBullet ();		
	}

	public void ResetBullet(){
        
		if (this.transform != null && thisManager.canonShootPosition != null && isOnPool == false) {
			thisRigidbody.velocity = Vector3.zero;
			this.transform.position = this.thisManager.canonShootPosition.position * 100f;
			thisManager.poolOfBullet.Enqueue (this);
			thisRigidbody.isKinematic = true;
		}
	}


	public void ShootBulletOnDirection(Vector3 direction){
		if (this.transform != null && thisManager.canonShootPosition != null) {
			this.transform.position = thisManager.canonShootPosition.position;
			thisRigidbody.isKinematic = false;
            isOnPool = false;
			this.thisRigidbody.AddForce (direction * thisManager.bulletSpeed);
		}
	}

}
