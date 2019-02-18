using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Touch : MonoBehaviour
{
    private Controls player;
    public Transform start;
    public bool smash=false;
    void Start()
    {
        player = FindObjectOfType<Controls>();
    }

    public void LeftArrow()
    {
        player.moveright = false;
        player.moveleft = true;
    }
    public void RightArrow()
    {
        player.moveright = true;
        player.moveleft = false;
    }
    public void ReleaseLeftArrow()
    {
        player.moveleft = false;
    }
    public void ReleaseRightArrow()
    {
        player.moveright = false;

    }

    public void Jump()
    {
        if(player.onGround == true) player.jump = true;
        else if (smash)
        {
            player.smash = true;
            player.jump = false;
            smash = false;
        }
    }

    public void PreparetoSmash()
    {
        if(player.onGround!=true) smash = true; 
    }

    public void restart ()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        //start.position = new Vector3 (-63.0596f, 7.330134f, 0.3305764f);
        //player.transform.position = start.position;
    }
}