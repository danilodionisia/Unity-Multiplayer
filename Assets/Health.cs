using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour {

	public const int xpMaximo = 100;
	public bool destroiQuandoMorre;

	[SyncVar(hook = "OnChangeHealth")]
	public int xpAtual = xpMaximo;

	public RectTransform barraDeXP;

	//Cria um array (vetor) para armazenar a posição dos players
	private NetworkStartPosition[] pontosDeSpawn;

	void Start ()
	{
		//verifica se o player é local e encontra o ponto inicial dele no jogo
		if (isLocalPlayer)
		{
			pontosDeSpawn = FindObjectsOfType<NetworkStartPosition>();
		}
	}

	public void TakeDamage(int amount)
	{
		if (!isServer)
			return;

		xpAtual -= amount;
		if (xpAtual <= 0)
		{
			if (destroiQuandoMorre)
			{
				Destroy(gameObject);
			} 
			else
			{
				xpAtual = xpMaximo;

				// chamado no servidor e executado no clilente
				RpcRespawn();
			}
		}
	}

	void OnChangeHealth (int currentHealth )
	{
		barraDeXP.sizeDelta = new Vector2(currentHealth , barraDeXP.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			// seta o ponto de spawn 
			Vector3 pontoDeSpawn = Vector3.zero;

			// Se o array que armazena os pontos de spawn exeistir e não estiver vazio
			// ele escolhe um ponto aleatório
			if (pontosDeSpawn != null && pontosDeSpawn.Length > 0)
			{
				pontoDeSpawn = pontosDeSpawn[Random.Range(0, pontosDeSpawn.Length)].transform.position;
			}

			// manda o player para o ponto escolhido
			transform.position = pontoDeSpawn;
		}
	}
}