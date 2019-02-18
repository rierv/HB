using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("destroyDelay");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * 1.6f * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject && other.name != "player")
        {
            if (this.tag != "Smashable" || other.tag =="Player")
                Destroy(this.gameObject);
        }
        
    }

    public IEnumerator destroyDelay()
    {
        yield return new WaitForSeconds(7f);
        if (this.gameObject != null) Destroy(this.gameObject);
    }
}
