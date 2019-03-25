using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Metadesc {
	namespace CameraShake {
		public class ShakeInfo : MonoBehaviour {
			[HideInInspector]
			public Vector3 SourcePosition = Vector3.zero;
			[HideInInspector]
			public Camera Cam;
			[HideInInspector]
			public bool IsPositionBased = false;
			
			public enum ValueMovementType { LerpType, SlerpType, MoveTowardsType };
			
			// If position based, then above this distance no shake happens.
			[Header("Max camera distance")]
			[Tooltip("If position based, then above this distance no shake happens.")]
			public float ShakeMaxDistance = 80f;
			// Shake strength over distance, if under ShakeMaxDistance.
			[Header("Shake duration")]
			[Tooltip("Shake strength over distance, if under ShakeMaxDistance range.")]
			public AnimationCurve ShakeDistanceWeight = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
			[Tooltip("Experimental feature, that should make the shake position based look like. " +
			"This value is also influences the rotation strength, if it other than 1.")]
			public float PositionBasedInflunceFactor = 1;
			
			// This is simply the duration of the shake. This value should be synchronized with the corresponding effect.
			[Header("Shake duration")]
			[Tooltip("Higher duration will strech the curves, that means nothing must be adapted, if the duration is changed.")]
			public float ShakeDuration = 1f;
			// The rolled position will be actualized so often.
			// The higher the value the rarely the position will be rolled and the more impact the 
			// shake speed and shake range will have, due to the bigger time window.
			[Header("Position sample calculation period")]
			[Tooltip("The shake calculation must not be in each frame. The higher the value the rarely the position will be " +
			"rolled and the more impact the shake speed and shake range will have, due to the bigger time window.")]
			public float ShakeSampleTimeDistanceMin = 0.1f;
			public float ShakeSampleTimeDistanceMax = 0.2f;
			
			[Header("Position")]
			[Tooltip("How the camera should move to the rolled position. This setting will change the speed. " +
			"Set the ShakeSpeed value to achieve similar results after changing this type.")]
			public ValueMovementType ValueMovementTypePos = ValueMovementType.LerpType;
			// How fast to move the camera to the rolled position.
			[Tooltip("Speed of the position movement. This must be higher for the lerp or spler shake movement.")]
			public AnimationCurve ShakeSpeed = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
			
			// Over time max radius in sphare based random position.
			[Tooltip("This is the multiplicator for the rolled normalized position in sphere. This is the strength of the shake.")]
			public AnimationCurve ShakeRange = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
			
			// Weight for rolled posiotion axis.
			[Tooltip("Value weight for position X.")]
			public AnimationCurve ShakeRangeWeightX = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
			[Tooltip("Value weight for position Y.")]
			public AnimationCurve ShakeRangeWeightY = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
			[Tooltip("Value weight for position Z.")]
			public AnimationCurve ShakeRangeWeightZ = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
			
			// Over time axis weight.
			[Tooltip("Over time weight for position X.")]
			public AnimationCurve XDirectionOverTime = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
			[Tooltip("Over time weight for position Y.")]
			public AnimationCurve YDirectionOverTime = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
			[Tooltip("Over time weight for position Z.")]
			public AnimationCurve ZDirectionOverTime = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
			
			[Header("Rotation")]
			[Tooltip("How the camera should rotate to the rolled rotation. This setting will change the speed. " +
			"Set the RotationSpeed value to achieve similar results after changing this type.")]
			public ValueMovementType ValueMovementTypeRot = ValueMovementType.LerpType;
			[Tooltip("Speed of the rotation. This must be higher for the lerp or spler shake rotation.")]
			public float RotationSpeed = 10;
			// Min/Max rotation.
			[Tooltip("Min degree, max 360!")]
			public Vector3 MinRotation = new Vector3(2f, 2f, 2f);
			[Tooltip("Max degree, max 360!")]
			public Vector3 MaxRotation = new Vector3(4f, 4f, 4f);
			
			// Weight for rolled rotation axis.
			[Tooltip("Value weight for rotation X")]
			public AnimationCurve ShakeRotationWeightX = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
			[Tooltip("Value weight for rotation Y")]
			public AnimationCurve ShakeRotationWeightY = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
			[Tooltip("Value weight for rotation Z")]
			public AnimationCurve ShakeRotationWeightZ = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
			
			// Over time axis rotation weight.
			[Tooltip("Over time weight for rotation X")]
			public AnimationCurve XRotationOverTime = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
			[Tooltip("Over time weight for rotation Y")]
			public AnimationCurve YRotationOverTime = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
			[Tooltip("Over time weight for rotation Z")]
			public AnimationCurve ZRotationOverTime = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
		}
	}
}
