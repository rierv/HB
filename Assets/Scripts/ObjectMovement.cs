using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour {

    public float amounttomovex;
    public float speed;
    private float currentposx;
    private float currentposy;
    private int facing;

    void Start()
    {
        currentposx = gameObject.transform.position.x;
        facing = 0;
    }

    void Update()
    {
        if (facing == 1 && gameObject.transform.position.x < currentposx - amounttomovex)
        {
            facing = 0;
        }

        if (facing == 0 && gameObject.transform.position.x > currentposx)
        {
            facing = 1;
        }

        if (facing == 0)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (facing == 1)
        {
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }
    }
}