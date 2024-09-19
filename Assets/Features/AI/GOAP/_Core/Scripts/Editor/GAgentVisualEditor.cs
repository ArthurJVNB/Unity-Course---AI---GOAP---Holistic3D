using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace Project.AI.GOAP
{
	[CustomEditor(typeof(GAgentVisual))]
	[CanEditMultipleObjects]
	public class GAgentVisualEditor : Editor
	{
		//private SerializedProperty _actions;
		//private SerializedProperty _goals;
		//private SerializedProperty _currentGoal;
		//private SerializedProperty _currentAction;
		//private SerializedProperty _preConditions;

		//private void OnEnable()
		//{
		//	_actions = serializedObject.FindProperty("_actions");
		//	_goals = serializedObject.FindProperty("_goals");
		//	_currentGoal = serializedObject.FindProperty("_currentGoal");
		//	_currentAction = serializedObject.FindProperty("_currentAction");
		//	_preConditions = serializedObject.FindProperty("_preConditions");
		//}

		#region Backup (original teacher's code)
		//public override void OnInspectorGUI()
		//{
		//	DrawDefaultInspector();
		//	serializedObject.Update();
		//	GAgentVisual agent = (GAgentVisual)target;
		//	GUILayout.Label("Name: " + agent.name);
		//	GUILayout.Label("Current Action: " + agent.gameObject.GetComponent<GAgent>().currentAction);
		//	GUILayout.Label("Actions: ");
		//	foreach (GAction a in agent.gameObject.GetComponent<GAgent>().actions)
		//	{
		//		string pre = "";
		//		string eff = "";

		//		foreach (KeyValuePair<string, int> p in a.preconditions)
		//			pre += p.Key + ", ";
		//		foreach (KeyValuePair<string, int> e in a.Effects)
		//			eff += e.Key + ", ";

		//		GUILayout.Label("====  " + a.ActionName + "(" + pre + ")(" + eff + ")");
		//	}
		//	GUILayout.Label("Goals: ");
		//	foreach (KeyValuePair<SubGoal, int> g in agent.gameObject.GetComponent<GAgent>().goals)
		//	{
		//		GUILayout.Label("---: ");
		//		foreach (KeyValuePair<string, int> sg in g.Key.SGoals)
		//			GUILayout.Label("=====  " + sg.Key);
		//	}
		//	serializedObject.ApplyModifiedProperties();
		//}
		#endregion

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			serializedObject.Update();
			GAgentVisual agent = (GAgentVisual)target;
			GUILayout.Label("Name: " + agent.name);
			GUILayout.Label("Current Action: " + agent.gameObject.GetComponent<GAgent>().CurrentAction);
			GUILayout.Label("Actions: ");
			foreach (GAction a in agent.gameObject.GetComponent<GAgent>().Actions)
			{
				if (a == null) continue;

				string pre = "";
				string eff = "";

				foreach (KeyValuePair<string, int> p in a.PreConditions) // preconditions?
					pre += p.Key + ", ";
				foreach (KeyValuePair<string, int> e in a.Effects)
					eff += e.Key + ", ";

				GUILayout.Label("====  " + a.ActionName + "(" + pre + ")(" + eff + ")");
			}
			GUILayout.Label("Goals: ");
			foreach (KeyValuePair<SubGoal, int> g in agent.gameObject.GetComponent<GAgent>().Goals)
			{
				GUILayout.Label("---: ");
				foreach (KeyValuePair<string, int> sg in g.Key.SGoals)
					GUILayout.Label("=====  " + sg.Key);
			}
			serializedObject.ApplyModifiedProperties();
		}
	}
}