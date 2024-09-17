using UnityEngine;

namespace Project.AI.GOAP
{
	public class Spawn : MonoBehaviour
	{
		[SerializeField] private GameObject _patientPrefab;
		[Min(0)]
		[SerializeField] private int _numPatients = 1;

		private void Start()
		{
			for (int i = 0; i < _numPatients; i++)
			{
				SpawnPatient();
			}
		}

		private void SpawnPatient()
		{
			Instantiate(_patientPrefab, transform.position, transform.rotation);
		}
	}
}
