using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordmanClass : Character {
	[SerializeField]
	private Animator camera;

	[SerializeField]
	private GameObject shipPoint;


	private GameObject ship;

	public GameObject Target{ get; set;}
	[SerializeField]
	private Canvas hpCanvas;
	bool died = false;
	//SidesState Variables
	[SerializeField]
	public Transform LeftPoint;
	[SerializeField]
	public Transform RightPoint;
	private Transform nextVisitPoint;
	private float sidesStateTimer;
	private float sidesStateDuration = 9;
	private float sidesStateAttackTimer = 3;
	private float sidesStateAttackDuration = 3;


	//JumpStates variables
	[SerializeField]
	private Transform[] points;
	private Transform currentPoint;
	private int  visitingPoint = 0;
	public enum States{ SIDES, JUMPING,RANGED};

	//RangedState variables
	int bulletsCount = 0;
	float shootingTimer = 0f;
	float shootingDuration = 1f;
	bool startShooting = false;
	public EdgeCollider2D jumpWeaponCollider;
	// Us this for initialization
	public States currentState;
	void Start () {
		base.Start();

		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
		Target = GameObject.FindGameObjectWithTag("Player");
		nextVisitPoint = RightPoint;
		hpCanvas = transform.GetComponentInChildren<Canvas> ();
	}

	// Update is called once per frame
	void Update () {
		if (!IsDead) {
			if (!TakingDemage) {
				if (currentState == States.SIDES) {
					SidesState ();
				} else if (currentState == States.JUMPING) {
				
					JumpState ();
				} else if (currentState == States.RANGED) {
					RangedState ();
				}

			}
		}
	}
	public void RangedState(){
		jumpWeaponCollider.enabled = false;
		float xDir;
		if (!startShooting) {
			
			xDir = GetComponent<Rigidbody2D> ().velocity.y;
		
			if (xDir > -0.1f && xDir < 0.1f) {
				startShooting = true;
			}
		} else {
			shootingTimer += Time.deltaTime;
			xDir = Target.transform.position.x - transform.position.x;
			if (xDir < 0 && facingRight || xDir > 0 && !facingRight) {
				DirectionChange ();
			}
				if (bulletsCount < 3 && shootingTimer >=shootingDuration) {
				ShootBullet (1);
					bulletsCount++;
				shootingTimer = 0;
			} if(bulletsCount >=3) {
					startShooting = false;
					currentState = States.SIDES;
				bulletsCount = 0;
				}
			
		}



	}
	public void JumpState(){
		jumpWeaponCollider.enabled = true;
		if (!Attack) {
			Anim.SetBool ("jumping",true);
				if (visitingPoint == 0) {
					GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
				}
				float xDir = points [visitingPoint].position.y - transform.position.y;
				Debug.Log (points.Length + " xDir");
				if (xDir > -0.1f && xDir < 0.1f && visitingPoint+1 <points.Length) {
					visitingPoint = visitingPoint + 1;

				}
				Debug.Log (visitingPoint + "df");
				xDir = points [visitingPoint].position.x - transform.position.x;

				Anim.SetFloat ("speed", 1);
				//transform.Translate ((points [visitingPoint].transform.position - transform.position).normalized * speed * Time.deltaTime);
			transform.position = Vector3.MoveTowards (transform.position,   points [visitingPoint].transform.position, Time.deltaTime*speed);
			if (transform.position.y - points [visitingPoint].transform.position.y > 0)
				Anim.SetFloat ("jumpingVelocity",-1f);
			else
				Anim.SetFloat ("jumpingVelocity",1f);
			if (points.Length-1 <= visitingPoint && (xDir > -0.1f && xDir < 0.1f)) {
				currentState = States.RANGED;
				Anim.SetBool ("jumping",false);
				Anim.SetFloat ("jumpingVelocity",-1f);
				visitingPoint = 0;
				GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
				}
		}
	}
	public void SidesState (){
		jumpWeaponCollider.enabled = false;
		sidesStateAttackTimer += Time.deltaTime;
		if (Mathf.Abs (Target.transform.position.x - transform.position.x) > 0.5f || sidesStateAttackTimer<sidesStateAttackDuration) {
			sidesStateTimer += Time.deltaTime;
			if (sidesStateTimer < sidesStateDuration) {
				LookAtTarget (nextVisitPoint);
				Move ();

			} else {
				sidesStateTimer = 0;
				currentState = States.JUMPING;
			}
		} else {
			Anim.SetTrigger ("attack");
			sidesStateAttackTimer = 0;
		}
	}
	public void Move()
	{
		if (!Attack) {
			Anim.SetFloat ("speed", 1);
			transform.Translate (GetDirection () * speed * Time.deltaTime);

		}
	}
	public Vector2 GetDirection(){

		return facingRight ? Vector2.right : Vector2.left;
	}
	private void LookAtTarget( Transform target){
		float xDir =   target.transform.position.x - transform.position.x ;
		if (xDir < 0 && facingRight || xDir > 0 && !facingRight) {
			DirectionChange ();
		}
			if(xDir > -0.1f && xDir < 0.1f){
			nextVisitPoint = (nextVisitPoint != LeftPoint) ?  LeftPoint : RightPoint;
			}
	}


	#region implemented abstract members of Character
	public override void Dead ()
	{
		Anim.ResetTrigger ("die");
		hpCanvas.enabled = false;
		GameObject.Destroy (gameObject);
	}
	public override IEnumerator TakeDemage ()
	{
		if (!hpCanvas.isActiveAndEnabled) {
			hpCanvas.enabled = true;
		}

		healthStat.CurrentValue -= 10;

		if (!IsDead) {
			Anim.SetTrigger ("demage");

		} else {
			if (!died) {
				ship = GameObject.FindGameObjectWithTag("Shipas");
				died = true;
				ship.GetComponent<Animator> ().enabled = true;
				Debug.Log ("ship name" + ship.name);
				camera.GetComponent<Animator> ().SetTrigger ("bossDied");

			}
				Instantiate (LevelManager.Instance.CoinPrfb, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 2), Quaternion.identity);
			//ship.transform.position = Vector3.MoveTowards (ship.transform.position,new Vector3(0, shipPoint.transform.position.y),100f);

				Anim.SetTrigger ("die");

				yield return null;
			}
	}

	public override bool IsDead {
		get {
			return healthStat.CurrentValue <= 0;
		}
	}
	#endregion


}
