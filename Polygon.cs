using UnityEngine;
using UnityEditor;
using System.Collections;

public class Polygon : MonoBehaviour {
	public void drawGizmos(){
		Vector3 lastPoint = transform.position;
		bool isFirstPoint = true;

		foreach(Point point in Points){
			Vector3 _position = point.transform.position;

			if(!isFirstPoint){
				Gizmos.DrawLine(lastPoint, _position);
			}

			point.drawGizmo();

			lastPoint = _position;
			isFirstPoint = false;
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector2[] getVectors(int _size){
		Vector2[] _return = new Vector2[_size];

		for(int _i = 0; _i < _size; _i++){
			_return[_i] = getVector((distance / _size) * _i);
		}

		return _return;
	}

	public Vector2 getVector(float _ratio){
		Debug.Log("lookup :" + _ratio);

		float _distance = 0;
		float _requestedDistance = distance * _ratio;
		int _lastPointIndex = 0;
		float _lastDistance = 0;

		float _distanceAtPoint1 = 0;
		float _distanceAtPoint2 = 0;

		float _distanceBetweenPoints = 0;

		for(int _i = 1; _i < Length && _distance <= _requestedDistance; _i++){
			_lastPointIndex = _i;
			_lastDistance = EdgeLength(_i);
			_distance += _lastDistance;
		}
//		_lastPointIndex++;

		_distanceAtPoint1 = _distance - _lastDistance;
		_distanceAtPoint2 = _distance;
		_distanceBetweenPoints = _lastDistance;

		float _ratioBetweenPoints = Mathf.Abs(_distanceAtPoint1 - _requestedDistance) / _distanceBetweenPoints;

		Vector2 _point1Position = Points[_lastPointIndex - 1].transform.position;
		Vector2 _point2Position = Points[_lastPointIndex].transform.position;

		Vector2 _magnitude = ((Vector2)_point1Position - (Vector2)_point2Position);
		_magnitude.Scale(new Vector2(_ratioBetweenPoints, _ratioBetweenPoints));

		Vector2 _return = _point1Position - _magnitude;

		return _return;

	}

	public float EdgeLength(int _index) {
		Vector2 _point1 = Points[_index - 1].transform.position;
		Vector2 _point2 = Points[_index].transform.position;

		return ((Vector2)_point1 - (Vector2)_point2)
				.magnitude;
	}

	public Point[] Points {
		get{return GetComponentsInChildren<Point>();}
	}

	public Point this[int index] {
		get{
			return Points[index];
		}
	}

	public int Length {
		get{
			return Points.Length;
		}
	}

	public float distance {
		get{
			float _distance = 0;
			bool _isFirst = true;
			Vector2 _lastPosition;

			foreach(Point _point in Points){
				var _position = _point.transform.position;
				if(!_isFirst){
					_distance += ((Vector2)_lastPosition - (Vector2)_position).magnitude;
				}
				_isFirst = false;
				_lastPosition = _position;
			}

			return _distance;
		}
	}
}

[CustomEditor(typeof(Polygon))]
public class PolygonEditor : Editor
{
	
	override public void OnInspectorGUI()
	{
		Polygon _target = ((Polygon)target);
		if (GUILayout.Button("Add Point"))
		{
			GameObject go = new GameObject("Point");
			go.transform.parent = _target.transform;
			go.transform.position = _target.transform.position;
			go.AddComponent<Point>();

		}
		GUILayout.TextField(_target.Length.ToString());
		GUILayout.TextField(_target.distance.ToString());

		DrawDefaultInspector();
	}
}