using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
	Vector3 targetPosition;
	public int layerMask;
	public float radius = 1f;

	public ParticleSystem explosion;

	void Start()
	{
		explosion.Stop();
		Invoke("EnableCollider", 0.2f);
		targetPosition = Managers.Game.GetPlayer().transform.position;
		layerMask = (-1) - (1 << (int)Define.Layer.Enemy);
	}

	private void OnEnable()
	{
		targetPosition = Managers.Game.GetPlayer().transform.position;
	}

	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, targetPosition, 0.05f);
	}

	void EnableCollider()
	{
		gameObject.GetOrAddComponent<Collider>().enabled = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

		for (int i = 0; i < colliders.Length; ++i)
		{
			PlayerController pc = colliders[i].GetComponent<PlayerController>();
			if(pc != null) pc.OnAttacked(10.0f);
		}

		explosion.Play();

		StartCoroutine("Wait");
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(explosion.main.duration);
		Managers.Pool.Push(GetComponent<Poolable>());
	}
}
