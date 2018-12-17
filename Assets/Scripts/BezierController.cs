using System;
using UnityEngine;
using Utility;

public class BezierController : MonoBehaviour
{
	private Vector3 _endPoint;
	private Vector3 _startPoint;
	private Vector3 _controlPoint1;
	private Vector3 _controlPoint2;
	private float _t = 0.0f;
	private BezierCurve _b;
	private Vector3[] _controlPoints;
	float _time = 0.0f;
 	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		_startPoint = transform.position;
			_endPoint = GameObject.Find("EndPoint").gameObject.transform.position;
		_controlPoint1 = GameObject.Find("ControlPoint_1").gameObject.transform.position;
		_controlPoint2 = GameObject.Find("ControlPoint_2").gameObject.transform.position;

		_controlPoints = new[] {_startPoint,_controlPoint1, _controlPoint2, _endPoint};
		_b = new BezierCurve(_controlPoints);
		_time += Time.deltaTime;

		if (_time >= 0.01f)
		{
			_t += 0.01f;
			_time = 0.0f;
		}

		if (_t >= 1.0f)
		{
			_t = 0.0f;
		}
		
		transform.position = _b.Evaluate(_t);
		float degZ = GetZRotation(_b.Evaluate(_t), _b.Evaluate(_t + 0.2f));
		float degY = GetYRotation(_b.Evaluate(_t), _b.Evaluate(_t + 0.2f));
		float degX = GetXRotation(_b.Evaluate(_t), _b.Evaluate(_t + 0.2f));
			
		transform.rotation =
			Quaternion.Euler(new Vector3(0, -degY, degZ));
		//transform.LookAt(new Vector3(-degX, -degY, -degZ));
	}
	
	public float GetZRotation(Vector3 p1, Vector3 p2) {
		float dx = p2.x - p1.x;
		float dy = p2.y - p1.y;
		float rad = Mathf.Atan2(dy, dx);
		return rad * Mathf.Rad2Deg;
	}
	
	public float GetYRotation(Vector3 p1, Vector3 p2) {
		float dx = p2.x - p1.x;
		float dz = p2.z - p1.z;
		float rad = Mathf.Atan2(dz, dx);
		return rad * Mathf.Rad2Deg;
	}
	
	public float GetXRotation(Vector3 p1, Vector3 p2) {
		float dy = p2.y - p1.y;
		float dz = p2.z - p1.z;
		float rad = Mathf.Atan2(dz, dy);
		return rad * Mathf.Rad2Deg;
	}

	
}
