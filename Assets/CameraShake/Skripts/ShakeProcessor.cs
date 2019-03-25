using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Metadesc {
	namespace CameraShake {
		public class ShakeProcessor : MonoBehaviour {
			public ShakeInfo Info = null;
			
			public Vector3 ShakeLocalPos = Vector3.zero;
			public Quaternion ShakeLocalRot = Quaternion.identity;
	
			float CurrentShakeDuration = 0;
			Vector3 rndPos = Vector3.zero;
			Vector3 rndRot = Vector3.zero;
			float timeReady = 0;
			bool doPause = false;
	
			public virtual void Init() {
				CurrentShakeDuration = Info.ShakeDuration;
				doPause = false;
				timeReady = 0;
				rndPos = Vector3.zero;
				rndRot = Vector3.zero;
			}
		
			public void Stop() {
				// Fade out at stop.
				CurrentShakeDuration = Mathf.Min(0.5f, CurrentShakeDuration);
			}
		
			public void Pause() {
				doPause = true;
			}
		
			public void Continue() {
				doPause = false;
			}

			public virtual bool Actualize() {
				if (doPause) {
					return false;
				}
				
				bool targetPointChanged = false;
				
				float dist = 0;
				float distFactor = 0;
				Vector3 dirToPos = Vector3.one;
				
				if (Info.IsPositionBased) {
					dist = Vector3.Distance(Info.Cam.transform.position, Info.SourcePosition);
					if (dist <= Info.ShakeMaxDistance) {
						distFactor = Info.ShakeDistanceWeight.Evaluate(Mathf.Min((Info.ShakeMaxDistance - dist) / Info.ShakeMaxDistance, 1));
					}
					
					dirToPos = Info.Cam.transform.InverseTransformDirection((Info.SourcePosition - Info.Cam.transform.position).normalized * Info.PositionBasedInflunceFactor);
					//dirToPos = Quaternion.LookRotation(dirToPos, Vector3.up).eulerAngles;
				}
				
				if (CurrentShakeDuration > 0) {
					float timeFaktor = (Info.ShakeDuration - CurrentShakeDuration) / Info.ShakeDuration;
					
					// Just make the shake calculation if it is in range or it is not range dependent.
					if (!Info.IsPositionBased || dist <= Info.ShakeMaxDistance) {
						if (timeReady < Time.time || rndPos == Vector3.zero) {
							Vector3 randomSpherePos = Random.insideUnitSphere;
							rndPos = new Vector3(Info.ShakeRangeWeightX.Evaluate(randomSpherePos.x),
								Info.ShakeRangeWeightY.Evaluate(randomSpherePos.y),
								Info.ShakeRangeWeightZ.Evaluate(randomSpherePos.z))
								* Info.ShakeRange.Evaluate(timeFaktor);
								
							rndPos = new Vector3(rndPos.x * Info.XDirectionOverTime.Evaluate(timeFaktor), 
								rndPos.y * Info.YDirectionOverTime.Evaluate(timeFaktor), 
								rndPos.z * Info.ZDirectionOverTime.Evaluate(timeFaktor));
								
							if (Info.IsPositionBased) {
								rndPos *= distFactor;
							}
							
							Vector3 randomRot = new Vector3(Random.Range(Info.MinRotation.x, Info.MaxRotation.x),
								Random.Range(Info.MinRotation.y, Info.MaxRotation.y)
								,Random.Range(Info.MinRotation.z, Info.MaxRotation.z));
							
							rndRot = new Vector3(Info.ShakeRotationWeightX.Evaluate(randomRot.x/360)*360,
								Info.ShakeRotationWeightY.Evaluate(randomRot.y/360)*360,
								Info.ShakeRotationWeightZ.Evaluate(randomRot.z/360)*360);
								
							rndRot = new Vector3(rndRot.x * Info.XRotationOverTime.Evaluate(timeFaktor) * dirToPos.x, 
								rndRot.y * Info.YRotationOverTime.Evaluate(timeFaktor) * dirToPos.y, 
								rndRot.z * Info.ZRotationOverTime.Evaluate(timeFaktor) * dirToPos.z);
							
							timeReady = Time.time + Random.Range(Info.ShakeSampleTimeDistanceMin, Info.ShakeSampleTimeDistanceMax);
							
							targetPointChanged = true;
						}
						
						switch (Info.ValueMovementTypePos) {
						case ShakeInfo.ValueMovementType.LerpType:
							ShakeLocalPos = Vector3.Lerp(Vector3.zero, rndPos, 
								Time.deltaTime * Info.ShakeSpeed.Evaluate(timeFaktor));
							break;
						case ShakeInfo.ValueMovementType.SlerpType:
							ShakeLocalPos = Vector3.Slerp(Vector3.zero, rndPos, 
								Time.deltaTime * Info.ShakeSpeed.Evaluate(timeFaktor));
							break;
						case ShakeInfo.ValueMovementType.MoveTowardsType:
							ShakeLocalPos = Vector3.MoveTowards(Vector3.zero, rndPos, 
								Time.deltaTime * Info.ShakeSpeed.Evaluate(timeFaktor));
							break;
						}
						
						switch (Info.ValueMovementTypePos) {
						case ShakeInfo.ValueMovementType.LerpType:
							ShakeLocalRot = Quaternion.Euler(Vector3.Lerp(Vector3.zero, rndRot, 
								Time.deltaTime * Info.RotationSpeed));
							break;
						case ShakeInfo.ValueMovementType.SlerpType:
							ShakeLocalRot = Quaternion.Euler(Vector3.Slerp(Vector3.zero, rndRot, 
								Time.deltaTime * Info.RotationSpeed));
							break;
						case ShakeInfo.ValueMovementType.MoveTowardsType:
							ShakeLocalRot = Quaternion.Euler(Vector3.MoveTowards(Vector3.zero, rndRot, 
								Time.deltaTime * Info.RotationSpeed));
							break;
						}
						
					} else {
						targetPointChanged = false;
					}
					CurrentShakeDuration -= Time.deltaTime;
					
				} else {
					CurrentShakeDuration = 0f;
					ShakeManager.I.RemoveShake(this);
					IPool.I.ReturnInstance(gameObject);
				}
			
						
				return targetPointChanged;
			}
			
		}
	}
}
