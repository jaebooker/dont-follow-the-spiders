using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Metadesc {
	namespace CameraShake {
		/// <summary>
		/// The clock enables the pause and continue features for the shake managers.
		/// The clock also controls the timer.
		/// </summary>
		[System.Serializable]
		public class Clock : MonoBehaviour {
			/// <summary>
			/// Flag for pause and continue.
			/// </summary>
			public bool Paused = false;
			[SerializeField]
			/// The ClockTarget on which the clock works.
			protected ClockTarget clockTarget;
			
			public void SetClockTarget(ClockTarget clockTarget) {
				this.clockTarget = clockTarget;
			}
			
			public void Pause() {
				if (!Paused) {
					clockTarget.Pause();
					Paused = true;
				}
			}
			
			public void Continue() {
				if (Paused) {
					clockTarget.Continue();
					Paused = false;
				}
			}
		}
	}
}
