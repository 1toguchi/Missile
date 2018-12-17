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
		_startPoint = transform.position;
		_endPoint = GameObject.Find("EndPoint").gameObject.transform.position;
		_controlPoint1 = GameObject.Find("ControlPoint_1").gameObject.transform.position;
		_controlPoint2 = GameObject.Find("ControlPoint_2").gameObject.transform.position;

		_controlPoints = new[] {_startPoint,_controlPoint1, _controlPoint2, _endPoint};
		_b = new BezierCurve(_controlPoints);
	}
	
	// Update is called once per frame
	void Update ()
	{
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
	}
}
