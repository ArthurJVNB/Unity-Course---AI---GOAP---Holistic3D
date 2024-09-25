using UnityEngine;

namespace Project
{
	public class TimeDilation : MonoBehaviour
	{
		[Range(0, 5)]
		[SerializeField] private float _timeScale = 1;

		public float TimeScale
		{
			get => _timeScale;
			set
			{
				_timeScale = value;
				DilateTime();
			}
		}

		private void OnValidate()
		{
			if (!Application.isPlaying) return;
			DilateTime();
		}

		private void OnEnable()
		{
			DilateTime();
		}

		private void DilateTime()
		{
			Time.timeScale = _timeScale;
		}
	}
}
