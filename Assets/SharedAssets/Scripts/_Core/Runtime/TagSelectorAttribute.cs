using System;
using UnityEngine;

namespace Project
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public class TagSelectorAttribute : PropertyAttribute
	{
		public bool UseDefaultTagFieldDrawer = false;
	}
}
