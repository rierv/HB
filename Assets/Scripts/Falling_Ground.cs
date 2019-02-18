using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Ground : MonoBehaviour {

    private Transform start;
    float x;
    float y;
    public float resistingTime = 1;
    private bool tr = true;
    float speed=1.5f;
    private Collider2D[] col;
    private GameObject player;
    private bool ex;
    void Start()
    {
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        col = GetComponents<BoxCollider2D>();
        player = GameObject.Find("player");
    }

    void Update()
    {
        if (tr == false)
        {
            transform.Translate(Vector2.down * speed * 2 * Time.deltaTime);
            speed += 0.08f;
        }
        else speed = 0f;
        if (player!=null && player.transform.position.y > y+1 && col[0].enabled == false)
        {
            col[0].enabled = true;
        }
        else if (player != null && player.transform.position.y < y+1 &&  col[0].enabled == true)
        {
            col[0].enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ex = false;
            other.transform.parent = transform;
            StartCoroutine("respawndelay2");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ex = true;
            other.transform.parent = null;
        }
    }
    public IEnumerator respawndelay2()
    {
        yield return new WaitForSeconds(resistingTime);
        if (ex == false)
        {
            tr = false;
            yield return new WaitForSeconds(5f);
            transform.position = new Vector3(x, y, 0f);
            tr = true;
        }
    }
}
