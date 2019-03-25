using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Metadesc {
	namespace CameraShake {
		/// <summary>
		/// IPool is a generic object pool system. In this case it is meant to be for the
		/// shake manager. By the name the prefab will be provided. The names must be unique!
		/// </summary>
	    public class IPool : MonoBehaviour {
	
		    [HideInInspector]
		    /// [HideInInspector]
		    public static IPool I;
		    // The prefabs entry, for which instances will be generated.
	        public List<IPoolEntry> entries;
	
		    Dictionary<string, IPoolEntry> stringMap;
		    /// <summary>
		    /// Speeds up the pooling system. For each instance the corresponding IPoolEntry can be received.
		    /// </summary>
		    public Dictionary<GameObject, IPoolEntry> allUsedMap;
	
	        void Awake() {
		        I = this;
	            stringMap = new Dictionary<string, IPoolEntry>();
	            allUsedMap = new Dictionary<GameObject, IPoolEntry>();
	
	            foreach (IPoolEntry entry in entries) {
	                stringMap.Add(entry.Name, entry);
	                entry.Init(this);
	            }
	        }
	
	        /// <summary>
	        /// Spawn an instantiated object.
	        /// Search by name for the object.
	        /// </summary>
	        /// <param name="name"></param>
	        /// <returns></returns>
	        public GameObject GetInstance(string name) {
	            if (!stringMap.ContainsKey(name)) {
		            Debug.Log("The name is not a part of the pool: " + name);
		            return null;
	            }
	            IPoolEntry pe = stringMap[name];
	            GameObject go = pe.GetInstance();
	            return go;
	        }
	
	        /// <summary>
		    /// For sending back the instance.
	        /// </summary>
	        /// <param name="go"></param>
		    public void ReturnInstance(GameObject go) {
	            if (!allUsedMap.ContainsKey(go)) {
		            //Debug.Log("GameObject is not a part of the pool: " + go);
		            return;
	            }
	            IPoolEntry pe = allUsedMap[go];
			    pe.ReturnInstance(go);
	        }
	    }
	
	    [System.Serializable]
	    public class IPoolEntry {
	        public string Name;
	        public GameObject Object;
	        // Just for debug public
	        public List<GameObject> objects;
	        public List<GameObject> objectsInUse;
	        public int MinNumber;
	        public int MaxNumber;
	        [HideInInspector]
	        public IPool PoolManager;
		    GameObject parentObj;
		    
	
		    public void Init(IPool pool) {
	            PoolManager = pool;
	            //objects = new List<GameObject>();
	            //objectsInUse = new List<GameObject>();
	            parentObj = new GameObject(Name);
	            parentObj.transform.SetParent(PoolManager.transform);
	            parentObj.transform.localPosition = Vector3.zero;
	            parentObj.transform.localRotation = Quaternion.identity;
	
	            for (int i = 0; i < MinNumber; i++) {
	                GameObject obj = GameObject.Instantiate(Object, Vector3.zero, Quaternion.identity) as GameObject;
	                obj.transform.SetParent(parentObj.transform);
	                obj.SetActive(false);
	                objects.Add(obj);
	            }
	        }
	
		    public GameObject GetInstance() {
			    GameObject obj = null;
			    
			    if (objects.Count == 0 && objectsInUse.Count == MaxNumber)
				    return null;
			    
	            if (objects.Count == 0 && objectsInUse.Count < MaxNumber) {
	                obj = GameObject.Instantiate(Object, Vector3.zero, Quaternion.identity) as GameObject;
	                obj.transform.SetParent(parentObj.transform);
	                obj.SetActive(false);
	                objectsInUse.Add(obj);
		            // For fast GO returning.
		            PoolManager.allUsedMap.Add(obj, this);
	
	            } else {
	                obj = objects[0];
	                objectsInUse.Add(obj);
		            // For fast GO returning.
		            PoolManager.allUsedMap.Add(obj, this);
	                objects.Remove(obj);
	            }
	
	            return obj;
	        }
	        
	        public void ReturnInstance(GameObject obj) {
	            if (obj != null && objectsInUse.Contains(obj)) {
	                obj.transform.SetParent(parentObj.transform);
	                obj.transform.localPosition = Vector3.zero;
	                obj.transform.localRotation = Quaternion.identity;
	                obj.SetActive(false);
	
	                objects.Add(obj);
	                objectsInUse.Remove(obj);
		            // For fast GO returning.
		            PoolManager.allUsedMap.Remove(obj);
	            }
	        }
	
	    }
	}
}
