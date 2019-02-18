using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private Controls player;
    private Transform start;
    public GameObject Explode;
    void Start()
    {
        player = FindObjectOfType<Controls>();
        start = GameObject.Find("Start").transform;
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && (player.damaging==false || tag=="Spikes"))
        {
            StartCoroutine("respawndelay");
        }
    }
    
    public IEnumerator respawndelay()
    {
        Instantiate(Explode, player.transform.position, player.transform.rotation);
        player.enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        player.transform.position = start.position;
        player.GetComponent<Renderer>().enabled = true;
        player.enabled = true;
        //player.respown();
    }
}
