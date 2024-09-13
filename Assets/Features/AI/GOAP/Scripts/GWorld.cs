namespace Project.AI.GOAP
{
	public sealed class GWorld
    {
		private static readonly GWorld _instance = new();
		private static WorldStates _world;

		static GWorld()
		{
			_world = new();
		}

		private GWorld() { }

		public static GWorld Instance => _instance;

		public WorldStates GetWorld() => _world	;
	}
}
