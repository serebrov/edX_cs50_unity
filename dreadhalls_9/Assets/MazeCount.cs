using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeCount : MonoBehaviour
{

	// make this static so it's visible across all instances
	public static int count = 0;

	public static void increment() {
		count++;
	}

	public static void reset() {
		count = 0;
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = "Maze: " + count;
    }
}
