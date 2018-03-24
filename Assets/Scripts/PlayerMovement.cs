using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void DeadEventHandler();

public class PlayerMovement : Character {
	private static PlayerMovement instance;

	public bool SpaceDoors {get;set;}

	private IObjectsUse objectsUse;
	public event DeadEventHandler Died;
	public static PlayerMovement Instance{
		get{ 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<PlayerMovement> ();
			}
			return instance;
		}
	}public bool FallingDowm{
		get{ return Rgb.velocity.y < 0;
		}
	}


	private bool immortal = false;
	private float immortalTime = 3f;

	private SpriteRenderer spriteR;

	public Transform [] groundPoints;
	public LayerMask groundOverlapMasks;
	public float radiusOverlaps;



	public float jumpForce;

	public Rigidbody2D Rgb{ get; set;}

	public bool Jump{ get; set;}
	[SerializeField]
	public bool IsGround;
	public bool OnGround{ get; set;}
	// Use this for initialization
	public override void Start () {
		base.Start ();
		SpaceDoors = false;
		Rgb = GetComponent<Rigidbody2D> ();
		spriteR = GetComponent<SpriteRenderer> ();


	}

	void Update(){
		IsGround = OnGround;
		if(!TakingDemage && !IsDead){
			HandleInput ();
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!TakingDemage && !IsDead) {
			float horizontalMovement = Input.GetAxis ("Horizontal");
			float vericalMovement = Input.GetAxis ("Vertical");
			OnGround = IsGrounded ();
			PlayerMovements (horizontalMovement,vericalMovement);
			Flip (horizontalMovement);

			LayersHandling ();

		}
		Jump = false;
	}
	private void Use(){
		if (objectsUse != null) {
			objectsUse.Use ();
		}
	}
	public  void OnCollisionEnter2D(Collision2D other){
		
		if (other.gameObject.tag == "Coin") {
			LevelManager.Instance.AmountOfCoin++;
			Destroy (other.gameObject);
		}
	}
	public override void OnTriggerEnter2D(Collider2D other){
		
		if (other.tag == "ObjectsUse") {
			objectsUse = other.GetComponent<IObjectsUse> ();
		}base.OnTriggerEnter2D (other);

	}
	public void OnTriggerExit2D(Collider2D other){

		if (other.tag == "ObjectsUse") {
			objectsUse = null;
		}

	}

	private bool IsGrounded(){

		foreach(Transform point in groundPoints){
			Collider2D[] overalaps = Physics2D.OverlapCircleAll (point.position, radiusOverlaps,  groundOverlapMasks);
			foreach (Collider2D overlaptObj in overalaps) {
				if (gameObject != overlaptObj.gameObject) {
					return true;
				}
			}
		}
		return false;
	}
	private void PlayerMovements(float horizontal,float vertical){
		if (FallingDowm && transform.parent == null) {
			gameObject.layer = 11;
			Anim.SetBool ("land",true);

		}
		if (!Attack) {
			Rgb.velocity = new Vector2 (horizontal * speed, Rgb.velocity.y);
		}
		if (Jump && Rgb.velocity.y == 0&& !SpaceDoors) {
			Rgb.AddForce (new Vector2 (0,jumpForce));
		}
		if (SpaceDoors) {
			Rgb.velocity = new Vector2 (horizontal * speed, vertical *speed);

		}
		if (Rgb.velocity.y < 0) {
			Rgb.AddForce (new Vector2 (0, -100));
		}
		Anim.SetFloat ("speed", Mathf.Abs(horizontal));
			

	}


	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.E)) {
			Anim.SetTrigger ("attack");
		}
		if (Input.GetKeyDown (KeyCode.Space) && !SpaceDoors && !FallingDowm) {
			Anim.SetTrigger ("jump");
			Jump = true;
		}
		if (Input.GetKey (KeyCode.Q)) {
			Anim.SetTrigger ("shoot");
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			Use ();
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene (0);
		}
	}



	private void Flip(float horizontal){
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			DirectionChange ();
		}
	}
	private void LayersHandling(){
		if (!OnGround) {
			Anim.SetLayerWeight (1, 1);
		} else {
			Anim.SetLayerWeight (1, 0);
		}
		}
	public override void ShootBullet(int value){
		if ((!OnGround && value == 1) || (OnGround && value == 0)) {
			base.ShootBullet (value);
		}
	}

	private IEnumerator IndicateImmortality(){

		while (immortal) {
			spriteR.enabled = false;
			yield return new WaitForSeconds (0.1f);
			spriteR.enabled = true;
			yield return new WaitForSeconds (0.1f);
		}
	}

	#region implemented abstract members of Character
	public override IEnumerator TakeDemage ()
	{
		
		if (!immortal) {
			healthStat.CurrentValue -= 10;
			if (!IsDead) {
				Anim.SetTrigger ("demage");
				immortal = true;
				StartCoroutine (IndicateImmortality());
				yield return new WaitForSeconds (immortalTime);
				immortal = false;
			} else {
				Anim.SetLayerWeight (1, 0);
				Anim.SetTrigger ("die");
			}
			yield return null;
		}
	}

	#region implemented abstract members of Character
	public void OnDead()
	{
		if (Died != null) {
			Died ();
		}
	}
	public override bool IsDead {
		get {
			if (healthStat.CurrentValue <= 0) {
				OnDead ();
			}
			return healthStat.CurrentValue <= 0;
		}
	}


	#region implemented abstract members of Character

	public override void Dead()
	{
		Rgb.velocity = Vector2.zero;
		Anim.SetTrigger ("idle");
		healthStat.CurrentValue = healthStat.MaximumValue;
		transform.position = startPos;
	}

	#endregion
	#endregion
	#endregion
}


