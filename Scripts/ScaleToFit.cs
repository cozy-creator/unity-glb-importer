using UnityEngine;

namespace NootyBallScripts.GLBAnimationImporter
{
    public class ScaleToFit : MonoBehaviour
    {
        SkinnedMeshRenderer meshFilter;
        public Vector3 desiredScale = new Vector3(1,1,1);

        void OnEnable()
        {
            meshFilter = GetComponentInChildren<SkinnedMeshRenderer>();
            // Find the maximum dimension (x, y, or z) of the mesh
            if (meshFilter != null)
            {
                float maxDimension = Mathf.Max(meshFilter.bounds.size.x, meshFilter.bounds.size.y, meshFilter.bounds.size.z);
                float scaleFactor = 1.5f / maxDimension;

                transform.localScale = transform.localScale * scaleFactor;
            }
            else
            {
                transform.localScale = desiredScale;
            }

        }
    }
}