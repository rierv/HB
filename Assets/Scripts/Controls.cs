using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Controls : MonoBehaviour
{
    public bool moveright;
    public bool moveleft;
    public bool jump;
    public bool smash;
    public float jumpheight;
    public float movespeed;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public GameObject Explode;
    public GameObject Dust;
    public GameObject Bullet;

    public bool damaging = false;
    public bool shooting = false;
    public bool jumping = false;
    public Collider2D onGround;

    private Rigidbody2D rb;
    private Animator anim;
    private bool pippo =false;
    private bool dash = false;
    private GameObject aim;
    private Vector3 bulletpos=Vector3.zero;
    private int bulletdir=0;
    private int lastDirection = 1;
    private Collider2D colPlayer;
    private float oldGravity;
    private float tmpSpeed;
    public Camera cam;
    void Start()
    {
        cam.enabled = false;
        tmpSpeed = movespeed;
        rb = GetComponent<Rigidbody2D>();
        aim = GameObject.Find("aim");
        aim.transform.position = transform.position;
        //anim = GetComponent<Animator>();
        colPlayer = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletdir = 0;
        bulletpos = Vector3.zero;
        movespeed = tmpSpeed;
        if (rb.velocity.x != 0 && onGround)
        {
            //anim.SetBool("Walking", true);
        }
        else
        {
            //anim.SetBool("Walking", false);
        }
        //interface with keyboard
        if (transform.position.x > 14)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.left * 100);
        }

        if (Input.GetKey(KeyCode.C) && onGround)
        {
            movespeed = 0;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (lastDirection > -1) lastDirection = -1;
            if (rb.velocity.x > -13) rb.AddForce(new Vector2(-movespeed*10, 0f));
            if (aim.transform.rotation.z != -45) bulletdir += 90;
            if (aim.transform.position.x > transform.position.x - 10) bulletpos+=Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (lastDirection < 1) lastDirection = 1;
            if (rb.velocity.x < 13) rb.AddForce(new Vector2(movespeed*10, 0f));
            if (aim.transform.rotation.z != 45) bulletdir -= 90;
            if (aim.transform.position.x < transform.position.x + 10) bulletpos += Vector3.right;
            
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) bulletdir /= 2;
            if (aim.transform.position.y < transform.position.y + 10) bulletpos += Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x/2, rb.velocity.y/2);
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) bulletdir += bulletdir / 2;
            else bulletdir = 180;
            if (aim.transform.position.y > transform.position.y - 10) bulletpos += Vector3.down;
        }
        if (Input.GetKeyDown(KeyCode.Z) && !onGround && damaging == false)
        {
            damaging = true;

            //anim.SetBool("Smash", true);
        }
        if (Input.GetKeyDown(KeyCode.Z) && onGround && jumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpheight));
            jumping = true;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                bulletdir = -90 * lastDirection;
                bulletpos += Vector3.right * lastDirection;
            }
            aim.transform.rotation = Quaternion.Euler(Vector3.forward * bulletdir);
            aim.transform.position = transform.position + (bulletpos * 3);
            Instantiate(Bullet, aim.transform.position, aim.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.X)&&dash==false)
        {
            dash = true;
            rb.AddForce(new Vector2(1200*lastDirection, 0));
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            colPlayer.isTrigger = true;
            StartCoroutine("reDash");
            //anim.SetBool("Smash", true);
        }
        
        /*                                    //interface with touch script
        if (moveright)
        {
            rb.velocity = new Vector2(movespeed, rb.velocity.y);
        }
        if (moveleft)
        {
            rb.velocity = new Vector2(-movespeed, rb.velocity.y);
        }
        if (!moveleft && !moveright && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && rb.velocity.x != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x / (1.1f), rb.velocity.y);
        }
        if (jump && onGround && pippo==false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpheight));
            jumping = true;
        }
        if (smash && !onGround)
        {
            damaging = true;
            //anim.SetBool("Smash", true);
        }
        */
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.gameObject!=null && other.tag == "Smashable" && damaging)
        {
            Instantiate(Explode, other.transform.position, other.transform.rotation);
            other.GetComponent<Collider2D>().enabled = false;
            Destroy(other.gameObject);
            rb.velocity= (new Vector2(rb.velocity.x, jumpheight*0.03f));
            //anim.SetBool("Smash", false);
            StartCoroutine("noDamaging");
        }
        if (other.tag == "Ground") {
            
            //anim.SetBool("Smash", false);
            Instantiate(Dust, new Vector3(transform.position.x, transform.position.y - 0.6f, 0f), transform.rotation);
            StartCoroutine("onGroundDelay");
        }
        if (!dash && (other.tag == "StdEnemy" || other.tag == "Hazard" || (other.tag == "Smashable" && !damaging)))
        {
            Instantiate(Explode, rb.transform.position, rb.transform.rotation);
            cam.enabled = true;
            Destroy(this.gameObject);
        }

    }

    public IEnumerator onGroundDelay()
    {
        pippo = true;
        yield return new WaitForSeconds(0.1f);
        damaging = false;
        shooting = false;
        jumping = false;
        pippo = false;
    }
    public IEnumerator noDamaging()
    {
        yield return new WaitForSeconds(0.3f);
        damaging = false;
        
    }
    public IEnumerator reDash()
    {
        yield return new WaitForSeconds(0.5f);
        colPlayer.isTrigger = false;
        rb.velocity = rb.velocity / 3;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.freezeRotation=true;
        dash = false;
    }
    void FixedUpdate()
    {
        onGround = Physics2D.OverlapCircle(rb.position, groundCheckRadius, whatIsGround);
        if (onGround&&pippo==false)
        {
            StartCoroutine("onGroundDelay");

        }
    }
}