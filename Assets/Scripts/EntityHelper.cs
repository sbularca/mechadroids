using UnityEngine;

namespace Mechadroids {
    public static class EntityHelper {
        public static float HandleSlope(Transform transform, float maxSlopeAngle, float speed) {
            if(Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, 2f)) {
                Vector3 terrainNormal = hit.normal;
                float slopeAngle = Vector3.Angle(terrainNormal, Vector3.up);
                if(slopeAngle > maxSlopeAngle) {
                    return 0;
                }
            }
            return speed;
        }
    }
}
