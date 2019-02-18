using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphynxController : MonoBehaviour
{
    public int life = 50;
    public Camera cam;
    public GameObject Explosion;
    public GameObject spawner2;

    public GameObject spawner;
    public GameObject HS1;
    public GameObject HS2;
    public GameObject HS3;
    private bool secondPhase=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
        if (life <= 0&&secondPhase==false)
        {
            Instantiate(Explosion, new Vector3(this.transform.position.x-22, this.transform.position.y+9, this.transform.position.z), this.transform.rotation);
            Instantiate(Explosion, new Vector3(this.transform.position.x - 20, this.transform.position.y + 3, this.transform.position.z), this.transform.rotation);
            Instantiate(Explosion, new Vector3(this.transform.position.x - 18, this.transform.position.y + 6, this.transform.position.z), this.transform.rotation);
            Instantiate(Explosion, new Vector3(this.transform.position.x - 20, this.transform.position.y + 12, this.transform.position.z), this.transform.rotation);
            secondPhase = true;
            cam.enabled = true;
            Destroy(spawner);
            spawner2.SetActive(true);
            life = 5;
            HS1.GetComponent<Spawner>().waitTime /= 3;
            HS2.GetComponent<Spawner>().waitTime /= 3;
            HS3.GetComponent<Spawner>().waitTime /= 3;
        }
        if ( secondPhase == true && life <=0)
        {
            
                spawner2.SetActive(false);
                life = -1;
                HS1.SetActive(false);
                HS2.SetActive(false);
                HS3.SetActive(false);
                HS1.GetComponent<Spawner>().destroyAll();
                HS2.GetComponent<Spawner>().destroyAll();
                HS3.GetComponent<Spawner>().destroyAll();

            Instantiate(Explosion, new Vector3(this.transform.position.x - 22, this.transform.position.y + 9, this.transform.position.z), this.transform.rotation);
                Instantiate(Explosion, new Vector3(this.transform.position.x - 20, this.transform.position.y + 4, this.transform.position.z), this.transform.rotation);
                Instantiate(Explosion, new Vector3(this.transform.position.x - 18, this.transform.position.y + 6, this.transform.position.z), this.transform.rotation);
                Instantiate(Explosion, new Vector3(this.transform.position.x - 24, this.transform.position.y + 8, this.transform.position.z), this.transform.rotation); Instantiate(Explosion, new Vector3(this.transform.position.x - 22, this.transform.position.y + 9, this.transform.position.z), this.transform.rotation);
                Instantiate(Explosion, new Vector3(this.transform.position.x - 21, this.transform.position.y + 3, this.transform.position.z), this.transform.rotation);
                Instantiate(Explosion, new Vector3(this.transform.position.x - 16, this.transform.position.y + 7, this.transform.position.z), this.transform.rotation);
                Instantiate(Explosion, new Vector3(this.transform.position.x - 19, this.transform.position.y + 11, this.transform.position.z), this.transform.rotation);
            
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            life -= 1;
        }
    }
}
