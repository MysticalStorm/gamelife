using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public float globalUpdateInterval = 1f;  // Set to 1 second

    void Start() {
        // Start the Coroutine that updates the game every second
        StartCoroutine(GlobalUpdateCoroutine());
    }

    IEnumerator GlobalUpdateCoroutine() {
        // This loop will run indefinitely, updating the game every `globalUpdateInterval`
        while (true) {
            // Call all update functions for bots, tiles, etc.
            UpdateBots();
            UpdateTiles();
            RenderGame();

            // Wait for the next interval
            yield return new WaitForSeconds(globalUpdateInterval);
        }
    }

    void UpdateBots() {
        // Here, trigger each bot's move function or update logic
        foreach (Bot bot in FindObjectsOfType<Bot>()) {
            //bot.MoveBug();  // Make sure this is accessible to trigger moves
        }
    }

    void UpdateTiles() {
        // Any global tilemap updates or other synced mechanics can go here
    }

    void RenderGame() {
        // If you need to force any rendering updates manually, do it here
    }
}
