using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metadesc {
	namespace CameraShake {
		public class ShakeManager : ClockTarget {
		
			/// Singleton instance
			public static ShakeManager I;
			public Camera CameraTarget = null;
			
			[HideInInspector]
			public ShakeResult ShakeResult = new ShakeResult();
		
			[HideInInspector, SerializeField]
			/// [HideInInspector]
			public List<ShakeInfo> ShakeInfos = new List<ShakeInfo>();
			[HideInInspector, SerializeField]
			/// [HideInInspector]
			public Dictionary<string, ShakeInfo> ShakeInfoDict = new Dictionary<string, ShakeInfo>();
			/// [HideInInspector]
			public Dictionary<string, List<ShakeProcessor>> ShakeProcessors = new Dictionary<string, List<ShakeProcessor>>();
		
			Clock ShakeClock;
			
		
			void Awake () {
				I = this;
				
				if (CameraTarget == null) {
					CameraTarget = Camera.main;
				}
				
				if (ShakeClock == null) {
					ShakeClock = GetComponent<Clock>();
				}
				
				ShakeClock.SetClockTarget(this);
			}
			
			void Start () {
				ShakeInfos.Clear();
				ShakeInfo[] shakes = transform.GetComponentsInChildren<ShakeInfo>();
				foreach (ShakeInfo entry in shakes) {
					entry.Cam = CameraTarget;
					if (string.IsNullOrEmpty(entry.gameObject.name)) {
						continue;
					}
					ShakeInfos.Add(entry);
				}
			
				ShakeInfoDict.Clear();
				foreach (ShakeInfo entry in ShakeInfos) {
					ShakeInfoDict.Add(entry.gameObject.name, entry);
				}
			}
			
			public ShakeResult UpdateAndGetShakeResult() {
				ShakeResult.ShakeLocalPos = Vector3.zero;
				ShakeResult.ShakeLocalRot = Quaternion.identity;
				ShakeResult.DoProcessShake = false;
				foreach (var values in ShakeProcessors.Values) {
					for (int i = values.Count - 1; i >= 0; i--) {
						ShakeProcessor entry = values[i];
						bool targetPointChanged = entry.Actualize();
						if (targetPointChanged) {
							ShakeResult.ShakeLocalPos += entry.ShakeLocalPos;
							ShakeResult.ShakeLocalRot = entry.ShakeLocalRot;
							ShakeResult.DoProcessShake = true;
						}
					}
				}
				
				return ShakeResult;
			}
		
			public ShakeProcessor AddShake(string shakeInfoName) {
				if (!ShakeInfoDict.ContainsKey(shakeInfoName)) {
					Debug.LogError("CameraShake, sake info with this name does not exists: " + shakeInfoName);
					return null;
				}
			
				ShakeInfo info = ShakeInfoDict[shakeInfoName];
				info.IsPositionBased = false;
				GameObject shakeProcessorObj = IPool.I.GetInstance("Shake");
				if (shakeProcessorObj == null) {
					// No more instances in the pool.
					return null;
				}
				ShakeProcessor shakeProcessor = shakeProcessorObj.GetComponent<ShakeProcessor>();
				shakeProcessor.Info = info;
				shakeProcessor.Init();
				shakeProcessor.gameObject.SetActive(true);
			
				if (!ShakeProcessors.ContainsKey(shakeInfoName)) {
					ShakeProcessors.Add(shakeInfoName, new List<ShakeProcessor>());
				}
				ShakeProcessors[shakeInfoName].Add(shakeProcessor);
			
				return shakeProcessor;
			}
		
			public ShakeProcessor AddShake(Vector3 position, string shakeInfoName) {
				if (!ShakeInfoDict.ContainsKey(shakeInfoName)) {
					Debug.LogError("CameraShake, sake info with this name does not exists: " + shakeInfoName);
					return null;
				}
			
				ShakeInfo info = ShakeInfoDict[shakeInfoName];
				info.SourcePosition = position;
				info.IsPositionBased = true;
				GameObject shakeProcessorObj = IPool.I.GetInstance("Shake");
				if (shakeProcessorObj == null) {
					// No more instances in the pool.
					return null;
				}
				ShakeProcessor shakeProcessor = shakeProcessorObj.GetComponent<ShakeProcessor>();
				shakeProcessor.Info = info;
				shakeProcessor.Init();
				shakeProcessor.gameObject.SetActive(true);
			
				if (!ShakeProcessors.ContainsKey(shakeInfoName)) {
					ShakeProcessors.Add(shakeInfoName, new List<ShakeProcessor>());
				}
				ShakeProcessors[shakeInfoName].Add(shakeProcessor);
			
				return shakeProcessor;
			}
			
			public void RemoveShake(ShakeProcessor shakeProcessor) {
				if (shakeProcessor.Info == null || !ShakeProcessors.ContainsKey(shakeProcessor.Info.gameObject.name)) {
					return;
				}
				
				ShakeProcessors[shakeProcessor.Info.gameObject.name].Remove(shakeProcessor);
			}
		
			// For all processors
			public void Stop() {
				foreach (var values in ShakeProcessors.Values) {
					foreach (ShakeProcessor entry in values) {
						entry.Stop();
					}
				}
			}

			public void SetPaused(bool flag) {
				if (flag) {
					Pause();
				} else {
					Continue();
				}
			}
		
			public override void Pause() {
				foreach (var values in ShakeProcessors.Values) {
					foreach (ShakeProcessor entry in values) {
						entry.Pause();
					}
				}
			}
		
			public override void Continue() {
				foreach (var values in ShakeProcessors.Values) {
					foreach (ShakeProcessor entry in values) {
						entry.Continue();
					}
				}
			}
		
			// For all processors on single shake infos
			public void Stop(string shakeInfoName) {
				if (!ShakeProcessors.ContainsKey(shakeInfoName)) {
					return;
				}
				
				foreach (ShakeProcessor entry in ShakeProcessors[shakeInfoName]) {
					entry.Stop();
				}
			}

			public void SetPaused(string name, bool flag) {
				if (flag) {
					Pause();
				} else {
					Continue();
				}
			}
		
			public void Pause(string shakeInfoName) {
				if (!ShakeProcessors.ContainsKey(shakeInfoName)) {
					return;
				}
				
				foreach (ShakeProcessor entry in ShakeProcessors[shakeInfoName]) {
					entry.Pause();
				}
			}
		
			public void Continue(string shakeInfoName) {
				if (!ShakeProcessors.ContainsKey(shakeInfoName)) {
					return;
				}
				
				foreach (ShakeProcessor entry in ShakeProcessors[shakeInfoName]) {
					entry.Continue();
				}
			}
		}
		
		public class ShakeResult {
			public bool DoProcessShake = false;
			public Vector3 ShakeLocalPos = Vector3.zero;
			public Quaternion ShakeLocalRot = Quaternion.identity;
		}
		
	}
}
