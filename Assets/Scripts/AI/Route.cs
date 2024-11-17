using UnityEngine;

namespace Mechadroids {
    [CreateAssetMenu(menuName = "Mechadroids/RouteSettings", fileName = "Route", order = 0)]
    public class Route : ScriptableObject {
        public int routeId;
        public Vector3 [] routePoints;
    }
}