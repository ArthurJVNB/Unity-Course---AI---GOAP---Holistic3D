namespace Project.AI.GOAP
{
	public sealed class GWorld
    {
		private static GWorld _instance = new();
		private static readonly WorldStates _world;

		[UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Reset()
		{
			_instance = new();
		}

		static GWorld()
		{
			_world = new();
		}

		private GWorld() { }

		public static GWorld Instance => _instance;

		public WorldStates GetWorld() => _world	;
	}
}
