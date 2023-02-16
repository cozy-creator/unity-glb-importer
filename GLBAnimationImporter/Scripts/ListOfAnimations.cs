using System.Collections.Generic;
using UnityEngine;

namespace NootyBallScripts
{
    [CreateAssetMenu(fileName = "List Of Animations", menuName = "Glb", order = 0)]
    [System.Serializable]
    public class ListOfAnimations : ScriptableObject
    {
        public List<ImportedAnimation> ImportedAnimations = new List<ImportedAnimation>();
    }

    [System.Serializable]
    public class ImportedAnimation
    {
        public string animationName;
    }
}