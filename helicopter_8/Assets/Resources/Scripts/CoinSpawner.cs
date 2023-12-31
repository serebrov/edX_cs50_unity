﻿using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour {

	public GameObject[] prefabs;
	public int maxCoinInRow = 4;
	public int minSpawnInterval = 1;
	public int maxSpawnInterval = 5;
	public int startPos = 26;

	// Use this for initialization
	void Start () {

		// infinite coin spawning function, asynchronous
		StartCoroutine(SpawnCoins());
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator SpawnCoins() {
		while (true) {

			// number of coins we could spawn vertically
			int coinsThisRow = Random.Range(1, maxCoinInRow);

			// instantiate all coins in this row separated by some random amount of space
			for (int i = 0; i < coinsThisRow; i++) {
				Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(startPos, Random.Range(-10, 10), 10), Quaternion.identity);
			}

			// pause 1-5 seconds until the next coin spawns
			yield return new WaitForSeconds(Random.Range(
				minSpawnInterval, maxSpawnInterval)
			);
		}
	}
}
