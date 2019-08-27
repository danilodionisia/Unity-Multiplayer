using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy : MonoBehaviour {

    public GameObject prefabBala;
    float tempo = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        tempo +=Time.deltaTime;

        if(tempo > 2)
        {
            atirar();
            tempo = 0;
        }

        print(tempo.ToString());
	}

    void atirar()
    {

        GameObject bala = (GameObject)Instantiate(prefabBala, new Vector3(PlayerControls.posX, PlayerControls.posY, PlayerControls.posZ), Quaternion.identity);
        bala.GetComponent<Rigidbody>().velocity = bala.transform.forward * 6.0f;
        NetworkServer.Spawn(bala);
        Destroy(bala, 2);

    }
}
