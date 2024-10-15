namespace Project.AI.GOAP
{
	[System.Obsolete("Marked to be deprecated. Use ResourceData instead.")]
	public struct Resource
	{
		public string Tag;
		public string ModifyState;

		public Resource(string tag, string modifyState)
		{
			Tag = tag;
			ModifyState = modifyState;
		}

		public Resource(string tag) : this()
		{
			Tag = tag;
		}
	}
}
