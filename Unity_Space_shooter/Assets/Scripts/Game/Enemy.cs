using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField]
	private float _speed = 4.0f;
	[SerializeField]
	private float _randomRange = 1.0f;
	private Player _player;

	void Start() {
		_player = GameObject.FindWithTag("Player").GetComponent<Player>();
		if (!_player) {
			Debug.LogError("No 'Player' Tagged object!");
		}
	}

	void Update() {
		Movement();
	}

	void Movement() {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		if (transform.position.y < -Constants.vLimit) {
			transform.position = new Vector3(Random.Range(-Constants.hLimit, Constants.hLimit), Constants.vLimit, 0);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.Equals("Player")) {
			Player player = other.GetComponent<Player>();
			if (player) {
				player.DealDamage();
			}
			Destroy(gameObject);
		}
		else if (other.tag.Equals("Laser")) {
			Destroy(other.gameObject);
			_player.AddScore(10);
			Destroy(gameObject);
		}
	}
}
