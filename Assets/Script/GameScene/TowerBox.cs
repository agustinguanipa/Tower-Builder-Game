using UnityEngine;
using System.Collections;
using System;

public class TowerBox : MonoBehaviour
{

    public Action<string, GameObject, int, int> OnCollision = null;
    public int towerBoxID = 0;
    public bool lockshaking = true; 

	
	public Rigidbody towerRigidbody = null;
	private BoxCollider towerBoxCollider = null;

    public float radius = 2f;		    // 阴影距离parent的半径
    public float maxAngle = 2.0f;		// 阴影左右摇摆的最大角度
    public float period = 4.0f;			// 一次摇摆需要多少秒

    private Transform trans;

	void Awake(){
		towerBoxCollider = this.gameObject.AddComponent<BoxCollider> ();
		towerBoxCollider.center = new Vector3 (0f, 5f, 0f);
		towerBoxCollider.size = new Vector3 (9f, 10f, 6f);

		towerRigidbody = this.gameObject.AddComponent<Rigidbody>();
		towerRigidbody.mass = 2;
		towerRigidbody.useGravity = false;
	}

    void Start()
    {
        trans = this.transform;
    }

    void Update()
    {
        if (!lockshaking && towerBoxID > 1)
        {
            float phase = Time.realtimeSinceStartup * (2.0f * Mathf.PI) / period;
            float angle = Mathf.Sin(phase) * (Mathf.Deg2Rad * maxAngle * towerBoxID);

            float startX = 0.0f;
            float startY = -radius;
            float sinAngle = Mathf.Sin(angle);
            float cosAngle = Mathf.Cos(angle);
            float x = cosAngle * startX - sinAngle * startY;
            float y = sinAngle * startX + cosAngle * startY;

            trans.localEulerAngles = new Vector3(x, 0, 0);
        }
    }

	void OnCollisionEnter(Collision collision){
		//Debug.Log ("TowerBox OnCollisionEnter: " + collision.gameObject.name + " tag: " + collision.gameObject.tag);

		if(towerRigidbody != null) {
			towerRigidbody.useGravity = false;
			towerRigidbody.isKinematic = true;
			towerRigidbody.Sleep();
		}
		
		switch (collision.gameObject.tag) {
		case "Floor":
			if(OnCollision != null)
                OnCollision("Floor", collision.gameObject, 0, 0);
			break;
		case "Tower":
            if (OnCollision != null)
            {
                string[] _mytower = this.gameObject.name.Split('_');

                string[] _tower = collision.gameObject.name.Split('_');
                OnCollision("Tower", collision.gameObject , Convert.ToInt32(_tower[1]), Convert.ToInt32(_mytower[1]));

                this.transform.parent = collision.gameObject.transform;
            }
			break;
		}

	}
}
