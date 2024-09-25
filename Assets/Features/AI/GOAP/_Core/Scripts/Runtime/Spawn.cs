using UnityEngine;

namespace Project.AI.GOAP
{
	public class Spawn : MonoBehaviour
	{
		[SerializeField] private GameObject _patientPrefab;
		[Min(0)]
		[SerializeField] private int _numPatients = 1;
		[Min(0)]
		[SerializeField] private float _timeBetweenWaves = 2;
		[SerializeField] private bool _spawnWaves = false;

		private void Start()
		{
			SpawnWave();
		}

		private void SpawnWave()
		{
			for (int i = 0; i < _numPatients; i++)
				SpawnPatient();
			if (_spawnWaves)
				Invoke(nameof(SpawnWave), _timeBetweenWaves);
		}

		private void SpawnPatient()
		{
			Instantiate(_patientPrefab, transform.position, transform.rotation);
		}
	}
}
