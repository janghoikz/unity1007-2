using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	float _speed = 10.0f;

	[SerializeField]
	AnimationClip move;
	[SerializeField]
	AnimationClip wait;

	bool _moveToDest = false;
	Vector3 _destPos;
	NavMeshAgent agent;
	Animation anim;

	void Start()
	{
		Managers.Input.KeyAction -= OnKeyboard;
		Managers.Input.KeyAction += OnKeyboard;
		Managers.Input.MouseAction -= OnMouseClicked;
		Managers.Input.MouseAction += OnMouseClicked;
		anim = GetComponent<Animation>();
		agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		if (_moveToDest)
		{
			Vector3 dir = _destPos - transform.position;
			if (dir.magnitude < 0.1f)
			{
				_moveToDest = false;
				anim.clip = wait;
				anim.Play();
			}
			else
			{
                agent.SetDestination(_destPos);
                //float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
                //transform.position += dir.normalized * moveDist;
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
                anim.clip = move;
				anim.Play();
				if (dir.magnitude < 0.0001f)
				{
					_moveToDest = false;
					anim.clip = wait;
					anim.Play();
				}
			}
		}
	}

	void OnKeyboard()
	{
		if (Input.GetKey(KeyCode.W))
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
			transform.position += Vector3.forward * Time.deltaTime * _speed;
		}

		if (Input.GetKey(KeyCode.S))
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
			transform.position += Vector3.back * Time.deltaTime * _speed;
		}

		if (Input.GetKey(KeyCode.A))
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
			transform.position += Vector3.left * Time.deltaTime * _speed;
		}

		if (Input.GetKey(KeyCode.D))
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
			transform.position += Vector3.right * Time.deltaTime * _speed;
		}

		_moveToDest = false;
	}

	void OnMouseClicked(Define.MouseEvent evt)
	{
		if (evt != Define.MouseEvent.Click)
			return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Plane")))
		{
			_destPos = hit.point;
			_moveToDest = true;
		}
	}
}
