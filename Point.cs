using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {
	void OnDrawGizmosSelected(){
		Polygon parentPolygon = GetComponentInParent<Polygon>();
		if(parentPolygon != null){
			parentPolygon.drawGizmos();
		}
		drawGizmo(Color.red);
	}

	public void drawGizmo(){
		drawGizmo(Color.green);
	}

	public void drawGizmo(Color color){
		Gizmos.color = color;

		Gizmos.DrawLine(
			new Vector2(transform.position.x - .1f, transform.position.y),
			new Vector2(transform.position.x + .1f, transform.position.y)
			);
		
		Gizmos.DrawLine(
			new Vector2(transform.position.x, transform.position.y - .1f),
			new Vector2(transform.position.x, transform.position.y + .1f)
			);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
