using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Coin
{
	void OnTriggerEnter(Collider other) {

		// trigger coin diamond function if a helicopter collides with this
		other.transform.parent.GetComponent<HeliController>().PickupDiamond();
		Destroy(gameObject);
	}
}
