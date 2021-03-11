using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitTrigger : MonoBehaviour
{
    GameObject gameController;
    private void Start()
    {
        gameController = GameObject.Find("GameController");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //this.GetComponent<SpriteRenderer>().enabled = active;
        //active = !active;
        gameController.SendMessage("IsGameOver", true) ;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameController.SendMessage("IsGameOver", false);
    }
}
