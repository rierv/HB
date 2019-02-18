using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed=1;
    private bool invert = false;
    void Start()
    {
        StartCoroutine("destroyDelay");
        transform.localScale = new Vector3(0.3f, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * 3f * Time.deltaTime;
        transform.localScale = new Vector3(0.3f, speed/3.5f, 0);
        if (speed == 70) invert = true;
        if (invert == true) speed--;
        if (invert == false) speed++;
    }
    public IEnumerator destroyDelay()
    {
        yield return new WaitForSeconds(3f);
        if (this.gameObject != null) Destroy(this.gameObject);
    }
}
