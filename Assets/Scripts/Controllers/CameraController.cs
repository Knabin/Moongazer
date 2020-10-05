using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	Define.CameraMode _mode = Define.CameraMode.QuarterView;

	[SerializeField]
	Vector3 _delta = new Vector3(0, 3f, -3.0f);

	[SerializeField]
	GameObject _target = null;

	Define.CameraMode Mode
	{
		get { return _mode; }
		set
		{
			switch (_mode)
			{
				case Define.CameraMode.QuarterView:
					break;
				case Define.CameraMode.QuarterViewManual:
					break;
				case Define.CameraMode.EventView:
					break;
				default:
					break;
			}
		}
	}

	public float smoothTime = 0.2f;
	Vector3 lastMovingVelocity;
	Vector3 targetPosition;

	float targetZoomSize = 5f;
	const float roundReadyZoomSize = 14.5f;
	const float readyReadyZoomSize = 5f;
	const float trackingZoomSize = 10f;

	float lastZoomSpeed;

	const float rotationSpeed = 5f;

	private void Awake()
	{
		//TODO: 마우스 입력값에 따라서 카메라가 플레이어를 공전
	}

	public void SetTarget(GameObject target)
	{
		_target = target;
	}

	private void LateUpdate()
	{
		if (_mode == Define.CameraMode.QuarterView)
		{
			if (!_target.IsValid())
			{
				return;
			}

			RaycastHit hit;
			if (Physics.Raycast(_target.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
			{
				float dist = (hit.point - _target.transform.position).magnitude * 0.8f;
				transform.position = _target.transform.position + _delta.normalized * dist;
			}
			else
			{
				transform.position = _target.transform.position + _delta;
				transform.LookAt(_target.transform.position + _delta);
			}
		}
	}

	public void SetQuarterView(Vector3 delta)
	{
		_mode = Define.CameraMode.QuarterView;
		_delta = delta;
	}
}
