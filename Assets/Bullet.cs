using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		var choque = collision.gameObject;
		var xp = choque.GetComponent<Health>();
		if (xp  != null)
		{
			xp.TakeDamage(10);
		}

		Destroy(gameObject);
	}
}