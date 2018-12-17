using UnityEngine;

namespace Utility
{
	/// <summary>
	/// ベジェ曲線を描く
	/// </summary>
	public class BezierCurve
	{
		public Vector3[] ControlPoints { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BezierCurve(Vector3[] controlPoints)
		{
			ControlPoints = controlPoints;
		}

		/// <summary>
		/// ベジェ曲線関数
		/// </summary>
		public Vector3 Evaluate(float t)
		{
			if (ControlPoints == null)
			{
				return Vector3.zero;
			}

			Vector3 result = Vector3.zero;
			int n = ControlPoints.Length;
			for (int i = 0; i < n; i++)
			{
				result += ControlPoints[i] * Bernstein(n - 1, i, t);
			}

			return result;
		}

		/// <summary>
		/// バーンスタイン基底関数
		/// </summary>
		static float Bernstein(int n, int i, float t)
		{
			return Binomial(n, i) * Mathf.Pow(t, i) * Mathf.Pow(1 - t, n - i);
		}

		/// <summary>
		/// 二項係数を計算する
		/// </summary>
		static float Binomial(int n, int k)
		{
			return Factorial(n) / (Factorial(k) * Factorial(n - k));
		}

		/// <summary>
		/// 階乗を計算する
		/// </summary>
		static float Factorial(int a)
		{
			float result = 1f;
			for (int i = 2; i <= a; i++)
			{
				result *= i;
			}

			return result;
		}
	}
}
