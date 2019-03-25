using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Metadesc {
	namespace CameraShake {
		[CustomEditor(typeof(ShakeTester)), CanEditMultipleObjects]
		public class ShakeTesterEditor : Editor {
			SerializedProperty PropTestEntries;
			
			private Color32 textColor1 = new Color32(120, 220, 250, 255);
			private Color32 textColor2 = new Color32(210, 240, 250, 255);
			private Color32 bgColor1 = new Color32(190, 200, 230, 255);
			private Texture2D logo = null;
			
			private Texture2D playTex = null;
			private Texture2D stopTex = null;
			private Texture2D pauseTex = null;
			private Texture2D continueTex = null;
		
			void OnEnable() {
				PropTestEntries = serializedObject.FindProperty("TestEntries");
			}
			
			public override void OnInspectorGUI() {
				serializedObject.Update ();
				
				EditorGUILayout.Space();
				
				if (!logo) {
					logo = (Texture2D)Resources.Load("metadesc_logo_32", typeof(Texture2D));
				}
				
				if (!playTex) {
					playTex = (Texture2D)Resources.Load("Play", typeof(Texture2D));
				}
				
				if (!stopTex) {
					stopTex = (Texture2D)Resources.Load("Stop", typeof(Texture2D));
				}
				
				if (!pauseTex) {
					pauseTex = (Texture2D)Resources.Load("Pause", typeof(Texture2D));
				}
				
				if (!continueTex) {
					continueTex = (Texture2D)Resources.Load("Continue", typeof(Texture2D));
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
				EditorGUILayout.HelpBox("Testing the shake manager.", MessageType.Info);
				
				GUI.backgroundColor = bgColor1;
				
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				GUI.contentColor = textColor2;
				
				ShakeTester tester = target as ShakeTester;
		
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				for (int i=0; i < PropTestEntries.arraySize; i++) {
					GUILayout.BeginHorizontal();
					
					GUILayout.Label( "Name");
					GUILayout.FlexibleSpace();
					SerializedProperty cNameP = PropTestEntries.GetArrayElementAtIndex(i).FindPropertyRelative("ShakeInfoName");
					EditorGUILayout.PropertyField( cNameP, GUIContent.none, true );
				
					GUILayout.Label( "Source", GUILayout.Width( 50 ) );
					GUILayout.FlexibleSpace();
					SerializedProperty aNameP = PropTestEntries.GetArrayElementAtIndex(i).FindPropertyRelative("ShakeSource");
					EditorGUILayout.PropertyField( aNameP, GUIContent.none, true );
					
					GUILayout.EndHorizontal();
					
					GUILayout.BeginHorizontal();
					
					GUI.contentColor = Color.green;
					if (GUILayout.Button(playTex, GUILayout.MinWidth( 20 ), GUILayout.MaxHeight( 18 ), GUILayout.MaxWidth( 32 ))) {
						if (ShakeManager.I.ShakeInfoDict.ContainsKey(cNameP.stringValue)) {
							Transform tr = aNameP.objectReferenceValue as Transform;
							if (tr == null) {
								ShakeManager.I.AddShake(cNameP.stringValue);
								
							} else {
								ShakeManager.I.AddShake(tr.position, cNameP.stringValue);
							}
						}
					}
					
					GUILayout.Space(-4);
					
					GUI.contentColor = Color.red;
					if (GUILayout.Button(stopTex, GUILayout.MinWidth( 20 ), GUILayout.MaxHeight( 18 ), GUILayout.MaxWidth( 32 ))) {
						ShakeManager.I.Stop(cNameP.stringValue);
					}
					
					GUILayout.Space(-4);
					
					GUI.contentColor = Color.yellow;
					if (GUILayout.Button(pauseTex, GUILayout.MinWidth( 20 ), GUILayout.MaxHeight( 18 ), GUILayout.MaxWidth( 32 ))) {
						ShakeManager.I.Pause(cNameP.stringValue);
					}
					
					GUILayout.Space(-4);
					
					GUI.contentColor = Color.cyan;
					if (GUILayout.Button(continueTex, GUILayout.MinWidth( 20 ), GUILayout.MaxHeight( 18 ), GUILayout.MaxWidth( 32 ))) {
						ShakeManager.I.Continue(cNameP.stringValue);
					}
					
					GUILayout.FlexibleSpace();
					
					GUILayout.Space(-4);
					
					GUI.contentColor = Color.red;
					if (GUILayout.Button("-", GUILayout.MinWidth( 20 ), GUILayout.MaxHeight( 18 ), GUILayout.MaxWidth( 24 ))) {
						Undo.RecordObject(tester, "Add tester entry");
						tester.TestEntries.RemoveAt(i);
						EditorUtility.SetDirty(tester);
					}
					
					GUILayout.Space(-4);
					
					GUI.contentColor = Color.green;
					if (GUILayout.Button("+", GUILayout.MinWidth( 20 ), GUILayout.MaxHeight( 18 ), GUILayout.MaxWidth( 24 ))) {
						Undo.RecordObject(tester, "Add tester entry");
						var te = new TestEntry();
						te.ShakeInfoName = tester.TestEntries[i].ShakeInfoName;
						tester.TestEntries.Insert(i+1, te);
						EditorUtility.SetDirty(tester);
					};
					GUI.contentColor = textColor2;
					
					GUILayout.EndHorizontal();
					EditorGUILayout.Space();
				}
			    
				if (EditorGUI.EndChangeCheck()) {
					serializedObject.ApplyModifiedProperties();
				}
			    
				EditorGUILayout.Space();
					
					
				GUILayout.BeginHorizontal();
				GUILayout.Space(-4);
					
				GUI.contentColor = Color.red;
				if (GUILayout.Button("-", GUILayout.MinWidth( 20 ), GUILayout.MaxHeight( 18 ), GUILayout.MaxWidth( 24 ))) {
					int count = tester.TestEntries.Count;
					if (count > 0) {
						Undo.RecordObject(tester, "Add tester entry");
						tester.TestEntries.RemoveAt(count-1);
						EditorUtility.SetDirty(tester);
					}
				}
					
				GUILayout.Space(-4);
					
				GUI.contentColor = Color.green;
				if (GUILayout.Button("+", GUILayout.MinWidth( 20 ), GUILayout.MaxHeight( 18 ), GUILayout.MaxWidth( 24 ))) {
					Undo.RecordObject(tester, "Add tester entry");
					var te = new TestEntry();
					int count = tester.TestEntries.Count;
					if (count > 0) {
						te.ShakeInfoName = tester.TestEntries[count-1].ShakeInfoName;
					}
					tester.TestEntries.Insert(count, te);
					EditorUtility.SetDirty(tester);
				};
				GUI.contentColor = textColor2;
				GUILayout.EndHorizontal();
				
				
				EditorGUILayout.Space();
					
				GUILayout.BeginHorizontal();
					
				GUILayout.FlexibleSpace();
				GUI.contentColor = Color.red;
				if (GUILayout.Button(stopTex, GUILayout.MinWidth( 40 ), GUILayout.MaxHeight( 28 ), GUILayout.MaxWidth( 40 ))) {
					ShakeManager.I.Stop();
				}
					
				GUILayout.Space(-3);
					
				GUI.contentColor = Color.yellow;
				if (GUILayout.Button(pauseTex, GUILayout.MinWidth( 40 ), GUILayout.MaxHeight( 28 ), GUILayout.MaxWidth( 40 ))) {
					ShakeManager.I.Pause();
				}
					
				GUILayout.Space(-3);
					
				GUI.contentColor = Color.cyan;
				if (GUILayout.Button(continueTex, GUILayout.MinWidth( 40 ), GUILayout.MaxHeight( 28 ), GUILayout.MaxWidth( 40 ))) {
					ShakeManager.I.Continue();
				}
					
				GUILayout.Space(-3);
					
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				
			}
		}
	}
}
