using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Metadesc {
	namespace CameraShake {
		[CustomEditor(typeof(Clock)), CanEditMultipleObjects]
		public class ClockEditor : Editor {
			
			private Color32 textColor1 = new Color32(120, 220, 250, 255);
			private Color32 textColor2 = new Color32(210, 240, 250, 255);
			private Color32 bgColor1 = new Color32(190, 200, 230, 255);
			private Texture2D logo = null;
		
			void OnEnable() {
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
				EditorGUILayout.HelpBox("The clock enables the pause and continue features for the shake manager. " +
					"The clock also controls the timer.", 
					MessageType.Info);
				
				GUI.backgroundColor = bgColor1;
				
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				GUI.contentColor = textColor2;
				
				Clock clock = target as Clock;
			    
			    if (EditorGUI.EndChangeCheck()) {
				    serializedObject.ApplyModifiedProperties();
			    }
			    
			    EditorGUILayout.Space();
			    EditorGUILayout.Space();
				    
			    if (GUILayout.Button("Pause")) {
			    	clock.Pause();
			    }
				    
			    if (GUILayout.Button("Continue")) {
			    	clock.Continue();
			    }
		    }
		    
		}
	}
}
