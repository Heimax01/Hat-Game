using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTrigger : MonoBehaviour
{
    private GameController gameController;

    private UIController uIController;

    private void Start() {
        gameController = FindObjectOfType<GameController>();
        uIController = FindObjectOfType<UIController>();
    }

    private void OnTriggerEnter2D (Collider2D target) {
        if (target.gameObject.CompareTag("Destroyer")) {

            Destroy(this.gameObject);

        }

        else if (target.gameObject.CompareTag("Point")) {

            gameController.score++;
            uIController.txtScore.text = gameController.score.ToString();
            Destroy(this.gameObject);
        }
    }
}
