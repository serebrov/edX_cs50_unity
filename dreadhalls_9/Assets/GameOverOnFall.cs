using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverOnFall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		foreach (Transform ts in this.transform)
		{
			if (ts.position.y < -10)
			{
				// Game Over
				SceneManager.LoadScene("GameOver");
				// Destroy the static audio source
				if (DontDestroy.instance) {
					Destroy(DontDestroy.instance.gameObject);
				}
			}
		}
    }
}
