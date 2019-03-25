using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Metadesc {
	namespace CameraShake {
		/// <summary>
		/// Testing the different shake variants.
		/// </summary>
		public class ShakeTester : MonoBehaviour {
		
			[Header("Shake entries, which will be processed by the ShakeManager.")]
			/// Shake entries, which will be processed by the ShakeManager.
			public List<TestEntry> TestEntries = new List<TestEntry>();
		}
		
		[System.Serializable]
		public class TestEntry {
			public string ShakeInfoName;
			public Transform ShakeSource;
		}
	}
}
