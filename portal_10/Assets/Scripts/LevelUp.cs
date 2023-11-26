using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public GameObject levelUpText;
    public Color color;
    public string text;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other) {
        Text uiText = levelUpText.GetComponent<Text>();
        uiText.color = color;
        uiText.text = text;

    }
}
