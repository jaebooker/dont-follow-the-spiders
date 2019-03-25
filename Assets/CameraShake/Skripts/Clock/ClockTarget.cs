using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Metadesc {
	namespace CameraShake {
		/// <summary>
		/// To work with the clock this call must be inherited.
		/// </summary>
		public class ClockTarget : MonoBehaviour {
		
			public virtual void Pause() {
			}
				
			public virtual void Continue() {
			}
		}
	}
}
