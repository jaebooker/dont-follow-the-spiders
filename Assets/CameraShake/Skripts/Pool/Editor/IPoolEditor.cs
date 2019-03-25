using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Metadesc {
	namespace CameraShake {
		[CustomEditor(typeof(IPool)), CanEditMultipleObjects]
		public class IPoolEditor : Editor {
			SerializedProperty PropEntries;
			
			private Color32 textColor1 = new Color32(120, 220, 250, 255);
			private Color32 textColor2 = new Color32(210, 240, 250, 255);
			private Color32 bgColor1 = new Color32(190, 200, 230, 255);
			private Texture2D logo = null;
		
			void OnEnable() {
				PropEntries = serializedObject.FindProperty("entries");
			}
			
			public override void OnInspectorGUI() {
				serializedObject.Update ();
				
				EditorGUILayout.Space();
				
				if (!logo) {
					logo = (Texture2D)Resources.Load("metadesc_logo_32", typeof(Texture2D));
				}
				
				if (logo) {
					GUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("");
					Rect logoArea = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(16));
					GUI.DrawTexture(logoArea, logo, ScaleMode.ScaleToFit, true, 0);
					GUILayout.EndHorizontal();
				}
	
				EditorGUI.BeginChangeCheck();
				
				GUI.contentColor = textColor1;
				EditorGUILayout.HelpBox("IPool is a generic object pool system. In this case it is meant to be for the " +
					"camera shake. By the name the prefab will be provided. The names must be unique!", 
					MessageType.Info);
				
				GUI.backgroundColor = bgColor1;
				
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				GUI.contentColor = textColor2;
		
				EditorGUILayout.PropertyField(PropEntries, true);
			    
				if (EditorGUI.EndChangeCheck()) {
					serializedObject.ApplyModifiedProperties();
				}
			    
			    EditorGUILayout.Space();
			    EditorGUILayout.Space();
		    }
		    
		}
	}
}
