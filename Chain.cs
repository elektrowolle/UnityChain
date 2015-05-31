using UnityEngine;
using UnityEditor;
using System.Collections;

public class Chain : MonoBehaviour
{
	public int length;
	public GameObject prefab;
	public GameObject[] chained = {};
	public Polygon path = null;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

	public void updateChain(){
		GameObject previous = gameObject;
//		GameObject root = gameObject;

		foreach(GameObject go in this.chained){
			DestroyImmediate(go);
		}

		chained = new GameObject[this.length];


		for(int _i = 0; _i < this.length; _i++){
			GameObject go = Instantiate<GameObject>(prefab);
			chained[_i] = go;
			HingeJoint2D hingejoint = go.GetComponent<HingeJoint2D>();
			hingejoint.connectedBody = previous.GetComponent<Rigidbody2D>();
			if(path != null && path.Length > 1){
				Vector2 _position = path.getVector(1.0f / this.length * _i);
				go.transform.position = (new Vector3(_position.x, _position.y));
			}else{
				go.transform.position = previous.transform.position + (new Vector3(hingejoint.connectedAnchor.x + -hingejoint.anchor.x, hingejoint.connectedAnchor.y + -hingejoint.anchor.y));
			}
			previous = go;
		}
		for(int _i = 0; _i < this.length; _i++){
			chained[_i].transform.parent = transform;
		}
	}
}

[CustomEditor(typeof(Chain))]
public class ChainEditor : Editor
{	


    override public void OnInspectorGUI()
    {
		Chain self = ((Chain)target);
		GUILayout.Label("Length of chain");
		self.length = int.Parse(GUILayout.TextField(self.length.ToString()));
		if(GUILayout.Button("update")){
			self.updateChain();
		}
		DrawDefaultInspector();
    }
}
