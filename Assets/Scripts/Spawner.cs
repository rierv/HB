using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject toBeSpown;
    private bool spawn=true;
    public float waitTime = 10;
    private float originalOne;
    public float firstDelay;
    public bool lookat=false;
    private GameObject player;
    private List<GameObject> gg;
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;
    private float offset=-90;
    private HarpyControls hc;

    // Start is called before the first frame update
    void Start()
    {
        gg = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player!=null && lookat == true)
        {
            targetPos = player.transform.position;
            thisPos = transform.position;
            targetPos.x = targetPos.x - thisPos.x;
            targetPos.y = targetPos.y - thisPos.y;
            angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        }
        if (firstDelay != 0)
        {
            spawn = false;
            originalOne = waitTime;
            waitTime = firstDelay;
            StartCoroutine("wait");
            waitTime = originalOne;
            firstDelay = 0;
        }
        if (spawn == true)
        {
            gg.Add(Instantiate(toBeSpown, this.transform.position, this.transform.rotation));
            spawn = false;
            StartCoroutine("wait");
        }
    }
    public IEnumerator wait()
    {
        yield return new WaitForSeconds(waitTime);
        spawn = true;
    }
    public void destroyAll()
    {
        if (gg != null)
        {
            foreach (GameObject i in gg)
            {
                if (i != null)
                {
                    hc = i.GetComponent<HarpyControls>();
                    if (hc != null) hc.life = 0;
                }
            }
        }
    }
}
