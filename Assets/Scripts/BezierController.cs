using UnityEngine;
using Utility;

//ベジェ曲線を使ってミサイルの軌道を制御
//Bezier.csのclass BezierCurveでベジェ曲線自体を定義
public class BezierController : MonoBehaviour
{
    //始点 = ミサイルの生成地点
    private Vector3 _startPoint;
    //終点 = ターゲット
    private Vector3 _endPoint;
    //制御点1
    private Vector3 _controlPoint1;
    //制御点2
    private Vector3 _controlPoint2;
    
    //上の四つの変数を格納する配列
    private Vector3[] _controlPoints;
    
    private BezierCurve _b;
    
    private float _t = 0.0f;
    private float _time = 0.0f;
    private float _randPos = 0.0f;

    void Start()
    {
        _randPos = Random.Range(-10, 5);

        _startPoint = transform.position;
        _controlPoint1 = transform.parent.Find("ControlPoint_1").gameObject.transform.position;
        _controlPoint2 = transform.parent.Find("ControlPoint_2").gameObject.transform.position;
        _controlPoint2.y += _randPos;
    }

    // Update is called once per frame
    void Update()
    {
        //ターゲットは動くのでUpdateに記述
        _endPoint = GameObject.Find("EndPoint").gameObject.transform.position;

        _controlPoints = new[] {_startPoint, _controlPoint1, _controlPoint2, _endPoint};
        
        //ベジェ曲線を生成
        _b = new BezierCurve(_controlPoints);
        
        _time += Time.deltaTime;

        //ミサイルの速度
        if (_time >= 0.01f)
        {
            _t += 0.005f;

            //ランダムでスピードを落とす
            float rand = Random.Range(0, 1);
            if (rand == 1.0f)
            {
                _t -= 0.01f;
            }

            _time = 0.0f;
        }

        if (_t >= 1.0f)
        {
            _t = 0.0f;
        }

        //生成したベジェ曲線を線形補完により位置の算出
        transform.position = _b.Evaluate(_t);

        //ミサイルの向き
        //        float degZ = GetZRotation(_b.Evaluate(_t), _b.Evaluate(_t + 0.2f));
        //        float degY = GetYRotation(_b.Evaluate(_t), _b.Evaluate(_t + 0.2f));
        //        float degX = GetXRotation(_b.Evaluate(_t), _b.Evaluate(_t + 0.2f));

        //transform.rotation =
        //    Quaternion.Euler(new Vector3(0, -degY, degZ));
        //transform.LookAt(new Vector3(-degX, -degY, -degZ));
    }

//ミサイルの向き
//    public float GetZRotation(Vector3 p1, Vector3 p2)
//    {
//        float dx = p2.x - p1.x;
//        float dy = p2.y - p1.y;
//        float rad = Mathf.Atan2(dy, dx);
//        return rad * Mathf.Rad2Deg;
//    }
//
//    public float GetYRotation(Vector3 p1, Vector3 p2)
//    {
//        float dx = p2.x - p1.x;
//        float dz = p2.z - p1.z;
//        float rad = Mathf.Atan2(dz, dx);
//        return rad * Mathf.Rad2Deg;
//    }
//
//    public float GetXRotation(Vector3 p1, Vector3 p2)
//    {
//        float dy = p2.y - p1.y;
//        float dz = p2.z - p1.z;
//        float rad = Mathf.Atan2(dz, dy);
//        return rad * Mathf.Rad2Deg;
//    }
}