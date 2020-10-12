using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	Define.CameraMode _mode = Define.CameraMode.QuarterView;

	[SerializeField]
	Vector3 _delta = new Vector3(0, 3.5f, -4.5f);

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

	float rotationSpeed = 5.0f;

	float shakeAmount = 0.1f;
	float shakeTime;

	float angle;

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
				transform.position = _target.transform.position + new Vector3(0, 0.5f, 0) + _delta.normalized * dist;

			}
			else
			{
				transform.position = _target.transform.position + _delta;
				transform.LookAt(_target.transform.position + _delta);
			}

			if (shakeTime > 0)
			{
				transform.position += Random.insideUnitSphere * shakeAmount;
				shakeTime -= Time.deltaTime;
			}
		}
	}

	public void SetQuarterView(Vector3 delta)
	{
		_mode = Define.CameraMode.QuarterView;
		_delta = delta;
	}

	public void VibrateForTime(float time)
	{
		shakeTime = time;
	}
}
