using System;
using UnityEditor;
using UnityEngine;

namespace BzKovSoft.VRCorrector.Editor
{
	[CustomEditor(typeof(VRCorrector))]
	//[CanEditMultipleObjects]
	public class VRCorrectorEditor : UnityEditor.Editor
	{
		SerializedProperty _factorProp;
		SerializedProperty _countProp;
		SerializedProperty _correctorModeProp;
		SerializedProperty _controllerNamesProp;
		SerializedProperty _controllersProp;
		SerializedProperty _controllerPivotsProp;

		bool foldOut = true;
		void OnEnable()
		{
			_factorProp = serializedObject.FindProperty("_factor");
			_countProp = serializedObject.FindProperty("_count");
			_correctorModeProp = serializedObject.FindProperty("_correctorMode");
			_controllerNamesProp = serializedObject.FindProperty("_controllerNames");
			_controllersProp = serializedObject.FindProperty("_controllers");
			_controllerPivotsProp = serializedObject.FindProperty("_controllerPivots");
		}

		GUIContent addButtonContent = new GUIContent("+", "add element");
		GUIContent applyButtonContent = new GUIContent("Apply changes");
		GUIContent deleteButtonContent = new GUIContent("-", "delete");
		GUIContent nameFieldContent = new GUIContent("Name");
		GUIContent foldContent = new GUIContent("Correctors");
		GUIContent controllerFieldContent = new GUIContent("Controller");
		GUIContent ctrlPivotFieldContent = new GUIContent("Controller Pivot");

		GUIContent factorSliderContent = new GUIContent("Factor");

		GUILayoutOption miniButtonWidth = GUILayout.Width(20f);

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			ValidateData();

			EditorGUILayout.PropertyField(_correctorModeProp);

			float prevFactor = _factorProp.floatValue;
			float newFactor = prevFactor;
			newFactor = EditorGUILayout.Slider(factorSliderContent, newFactor, 0.5f, 0.9f);
			newFactor = Mathf.Round(newFactor * 10f) / 10f;
			if (prevFactor != newFactor)
			{
				_factorProp.floatValue = newFactor;
			}

			foldOut = EditorGUILayout.Foldout(foldOut, foldContent);
			if (foldOut)
			{
				++EditorGUI.indentLevel;
				EditorGUILayout.BeginVertical();

				int deleteButtonId = -1;
				int count = _countProp.intValue;
				for (int i = 0; i < count; i++)
				{
					DrawItem(ref deleteButtonId, i);
				}

				EditorGUILayout.EndVertical();

				bool addItem = GUILayout.Button(addButtonContent, EditorStyles.miniButton, miniButtonWidth);

				if (addItem)
				{
					_countProp.intValue += 1;
					_controllerNamesProp.arraySize += 1;
					_controllersProp.arraySize += 1;
					_controllerPivotsProp.arraySize += 1;
				}

				if (deleteButtonId != -1)
				{
					_countProp.intValue -= 1;
					DeleteArrayElementAtIndex(_controllerNamesProp, deleteButtonId);
					DeleteArrayElementAtIndex(_controllersProp, deleteButtonId);
					DeleteArrayElementAtIndex(_controllerPivotsProp, deleteButtonId);
				}

				--EditorGUI.indentLevel;
			}

			serializedObject.ApplyModifiedProperties();
			
			var corr = target as VRCorrector;
			bool stateIsOk = corr.IsCorrect();
			if (stateIsOk)
			{
				if (Application.isPlaying && GUILayout.Button(applyButtonContent))
				{
					corr.ApplyChanges();
				}
			}
			else
			{
				EditorGUILayout.HelpBox("Not all fields was correctly filled", MessageType.Warning);
			}
		}

		private void DeleteArrayElementAtIndex(SerializedProperty sp, int deleteButtonId)
		{
			// Unity3d has a bug, sometimes it do not delete array at first attempt
			int size = sp.arraySize;
			for (int i = 0; i < 10; i++)
			{
				sp.DeleteArrayElementAtIndex(deleteButtonId);
				if (size != sp.arraySize)
					return;
			}

			throw new InvalidOperationException("This is Unity bug with 'DeleteArrayElementAtIndex' method");
		}

		private void DrawItem(ref int deleteButtonId, int index)
		{
			var controllerName = _controllerNamesProp.GetArrayElementAtIndex(index);
			var controller = _controllersProp.GetArrayElementAtIndex(index);
			var controllerPivot = _controllerPivotsProp.GetArrayElementAtIndex(index);

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.BeginVertical();
			

			EditorGUILayout.PropertyField(controllerName, nameFieldContent);
			EditorGUILayout.PropertyField(controller, controllerFieldContent);
			EditorGUILayout.PropertyField(controllerPivot, ctrlPivotFieldContent);
			EditorGUILayout.EndVertical();

			bool deleteButton = GUILayout.Button(deleteButtonContent, EditorStyles.miniButton, miniButtonWidth);
			EditorGUILayout.EndHorizontal();

			if (deleteButton)
			{
				deleteButtonId = index;
			}
		}

		private void ValidateData()
		{
			int count = _countProp.intValue;

			if (_controllerNamesProp.arraySize != count)
			{
				_controllerNamesProp.arraySize = count;
				serializedObject.ApplyModifiedProperties();
				throw new InvalidOperationException("ctrlName != count");
			}
			if (_controllersProp.arraySize != count)
			{
				_controllersProp.arraySize = count;
				serializedObject.ApplyModifiedProperties();
				throw new InvalidOperationException("ctrl != count");
			}
			if (_controllerPivotsProp.arraySize != count)
			{
				_controllerPivotsProp.arraySize = count;
				serializedObject.ApplyModifiedProperties();
				throw new InvalidOperationException("ctrlPivot != count");
			}
		}
	}
}
