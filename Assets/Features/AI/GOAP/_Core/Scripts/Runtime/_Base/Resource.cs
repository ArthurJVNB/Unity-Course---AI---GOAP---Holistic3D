namespace Project.AI.GOAP
{
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
