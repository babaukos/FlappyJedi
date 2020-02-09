using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerControl : MonoBehaviour 
{
	public int life = 1;
	[SerializeField]
	private float jampForce = 1;
	[SerializeField]
	private float runSpeed = 1;
	[SerializeField]
	private float dirMultipl = 1;
	[SerializeField]
	private float rayLangh = 2;
	[Space]
	public Transform rotObj;
	[Space]
	public AudioClip deadSound;
	private Rigidbody2D rigidbody;
	//private Animator animator;
	private AudioSource source;
	private BoxCollider2D collider;
	public Transform shadow;
	public Transform censorBlock;

	public static PlayerControl Instance;
	private bool isDead;

	// Initialization
	private void Awake() 
	{
		if (Instance == null) 
		{
			Instance = this; 
		} else if(Instance == this)
		{
			Destroy(gameObject); 
		}
	}
	// Use this for initialization
	private void Start () 
	{
		rigidbody = GetComponent<Rigidbody2D> ();
		//animator = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
		collider = GetComponent<BoxCollider2D> ();
		transform.parent = GameObject.Find ("[DinamicObjects]").transform;

		if(GameManager.Instance.censure)
		{
			censorBlock.gameObject.SetActive(true);
		}
		else
		{
			censorBlock.gameObject.SetActive(false);
		}
	}	
	// Update is called once per frame
	private void Update () 
	{
		if (GameManager.Instance == null)
			return;

		if (life <= 0 && isDead == false) 
		{
			source.PlayOneShot (deadSound);
			GameManager.Instance.GameOver();
			isDead = true;
		} 
		else 
		{
			if(!GameManager.Instance.pause)
			{
				GroundEffect ();
				if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown(0))
				{
					rigidbody.velocity = Vector2.up * jampForce;
					//animator.SetBool ("jump", true);
				} 
				else 
				{
					//animator.SetBool ("jump", false);
				}
				rotObj.transform.eulerAngles = new Vector3 (0, 0, rigidbody.velocity.y * dirMultipl);
			}
		}
	}
	//
	private void GroundEffect()
	{
		// Еффект тени и пыли
		RaycastHit2D rh = Physics2D.Raycast(transform.position, Vector3.down, rayLangh);
		if (rh)
		{
			shadow.gameObject.SetActive(true);
			shadow.position = rh.point;
			float sc = Mathf.Clamp01(1 / (rh.distance * 3));
			shadow.localScale = new Vector3(sc, sc, 1);
		}
		else 
		{
			shadow.gameObject.SetActive(false);
		}
	}
	//
	private void SetDemage(int arg)
	{
		life -= arg;
	}
	//
	public float GetSpeed()
	{
		return runSpeed;
	}
	public void SetSpeed( float s)
	{
		runSpeed = s;
	}
}
