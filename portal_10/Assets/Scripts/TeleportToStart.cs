using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class TeleportToStart : MonoBehaviour
{
    public GameObject teleportTo;
    public GameObject levelUpText;
    public int offset = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log ("Trigger entered!");

        Text uiText = levelUpText.GetComponent<Text>();
        uiText.color = new Color(0, 0, 0, 0);

        // set the player's position of the start block
        other.transform.SetPositionAndRotation(
            teleportTo.transform.position,
            Quaternion.identity);
    }
}
