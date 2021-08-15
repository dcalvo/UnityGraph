using UnityEngine;

public class Graph : MonoBehaviour {
    [SerializeField] private Transform pointPrefab;
    [SerializeField] [Range(10, 100)] private int resolution = 10;
    [SerializeField] private FunctionLibrary.FunctionName function;

    private Transform[] points;

    private void Awake() {
        float step = 2f / resolution;
        Vector3 position = Vector3.zero;
        Vector3 scale = Vector3.one * step;

        points = new Transform[resolution];
        for (int i = 0; i < points.Length; i++) {
            Transform point = points[i] = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
        }
    }

    private void Update() {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        float time = Time.time;
        foreach (Transform point in points) {
            Vector3 position = point.localPosition;
            position.y = f(position.x, time);
            point.localPosition = position;
        }
    }
}