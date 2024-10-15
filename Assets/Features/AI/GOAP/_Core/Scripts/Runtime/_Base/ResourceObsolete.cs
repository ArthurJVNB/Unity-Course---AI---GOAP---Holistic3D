namespace Project.AI.GOAP
{
	[System.Obsolete("Obsolete. Use ResourceData instead.", true)]
	public struct ResourceObsolete
	{
		public string Tag;
		public string ModifyState;

		public ResourceObsolete(string tag, string modifyState)
		{
			Tag = tag;
			ModifyState = modifyState;
		}

		public ResourceObsolete(string tag) : this()
		{
			Tag = tag;
		}
	}
}
