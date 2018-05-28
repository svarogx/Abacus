using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem{
	public int amountToPool;
	public GameObject objectToPool;
	public bool shouldExpand;
}

public class StageControl : MonoBehaviour {

	public static StageControl sharedInstance = null;

	public bool dataPoolingEnabled = true;
	public List<ObjectPoolItem> itemsToPool;
	private List<GameObject> pooledObjects;

	public Sprite[] backgroundImage;
	private SpriteRenderer backgroundRender;
	private Vector3 screenSize;

	void Awake(){
		if (sharedInstance == null) {
			sharedInstance = this;
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy (this.gameObject);
		}

		backgroundRender = GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
		InitPoolObjects ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void InitPoolObjects(){
		bool tmpBool = dataPoolingEnabled;
		dataPoolingEnabled = false;

		GameObject obj = null;
		pooledObjects = new List<GameObject> ();
		foreach(ObjectPoolItem item in itemsToPool){
			for (int i = 0; i < item.amountToPool; i++) {
				obj = (GameObject)Instantiate (item.objectToPool);
				obj.SetActive (false);
				obj.transform.SetParent (this.transform.GetChild(0).transform);
				pooledObjects.Add (obj);
			}			
		}

		dataPoolingEnabled = tmpBool;
	}

	public GameObject GetPooledObject(string tag){
		if (!dataPoolingEnabled)
			return null;

		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag) {
				return pooledObjects [i];
			}
		}

		foreach (ObjectPoolItem item in itemsToPool) {
			if (item.objectToPool.tag == tag) {
				if (item.shouldExpand) {
					GameObject obj = (GameObject)Instantiate (item.objectToPool);
					obj.transform.SetParent (this.transform.GetChild(0).transform);
					obj.SetActive (false);
					pooledObjects.Add (obj);
					return obj;
				}
			}
		}

		return null;
	}

}
