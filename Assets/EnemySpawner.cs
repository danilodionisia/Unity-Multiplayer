using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

	public GameObject prefabDoInimigo;
	public int numeroDeInimigos;

	public override void OnStartServer()
	{
        
		for (int i=0; i < numeroDeInimigos; i++)
		{
			var posicaoDeSpawn = new Vector3(
				Random.Range(-8.0f, 8.0f),
				0.0f,
				Random.Range(-8.0f, 8.0f));

			var rotacaoDoSpawn = Quaternion.Euler( 
				0.0f, 
				Random.Range(0,180), 
				0.0f);

			var inimigo = (GameObject)Instantiate(prefabDoInimigo, posicaoDeSpawn, rotacaoDoSpawn);
			NetworkServer.Spawn(inimigo);
		}
	}
}




