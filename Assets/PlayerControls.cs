using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControls : NetworkBehaviour {

	public GameObject prefabBala;
	public Transform spawnaBala;
    public static float posY, posX, posZ;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer) {
			return;
		}

        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;


		float x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		float z = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate (0, x, 0);
		transform.Translate (0, 0, z);

		if (Input.GetKeyDown (KeyCode.Space)) {
			Cmdatirar ();
		}

	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer> ().material.color = Color.blue;
	}

	[Command]
	void Cmdatirar(){

		GameObject bala = (GameObject)Instantiate (prefabBala, spawnaBala.position, spawnaBala.rotation);
		bala.GetComponent<Rigidbody> ().velocity = bala.transform.forward * 6.0f;
		NetworkServer.Spawn (bala);
		Destroy (bala, 2);

	}

}
