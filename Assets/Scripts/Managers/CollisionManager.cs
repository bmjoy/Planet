using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CollisionManager : MonoBehaviour
{
    private bool firstTime = true;
    GameObject gameController;
    AudioSource Landing_Audio;
    AudioSource Composite_Audio;
    private void Awake()
    {
        Landing_Audio = GameObject.Find("Landing").GetComponent<AudioSource>();
        Composite_Audio = GameObject.Find("Composite").GetComponent<AudioSource>();
        gameController = GameObject.Find("GameController");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Obstacle"))
        {
            if (firstTime)
            {
                Landing_Audio.Play();
                firstTime = false;
                gameController.SendMessage("UnFreeze");
            }
            //如果检测到和自己相同类型的星球碰撞
            if (transform.CompareTag(collision.gameObject.tag))
            {
                Composite_Audio.Play();
                ComposeData composeData = new ComposeData();
                composeData.Position = collision.transform.position;
                composeData.Rotation = collision.transform.rotation;
                int.TryParse(collision.transform.tag, out int type);
                composeData.Type = type;
                gameController.SendMessage("DeliverComposeData", composeData);
                gameController.SendMessage("CanCompose");
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
            }
        }
        
    }

    void NotFirst()
    {
        firstTime = false;
    }
}
