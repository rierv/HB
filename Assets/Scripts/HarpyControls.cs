using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpyControls : MonoBehaviour
{
    public float speed;
    private Animator anim;
    private GameObject player;
    private Quaternion tmp;
    public int life;
    public GameObject Explosion;
    private SpriteRenderer rend;
    void Start()
    {
        player = GameObject.Find("player");
        anim = GetComponent<Animator>();
        rend=GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (life == 0)
        {
            Instantiate(Explosion, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
        else if (player)
        {
            tmp = transform.rotation;
            transform.LookAt(player.transform.position+ Vector3.up*3);
            if (transform.forward.x < 0) rend.flipX = true;
            else rend.flipX = false;
            if (Vector2.Distance(transform.position, player.transform.position) < 12 && anim.GetBool("Near_player") == false) anim.SetBool("Near_player", true);
            if (Vector2.Distance(transform.position, player.transform.position) >= 12 && anim.GetBool("Near_player") == true) anim.SetBool("Near_player", false);
            transform.position += transform.forward * speed * Time.deltaTime;
            transform.rotation = tmp;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            life = life - 1;
        }
    }
}
