using System;
using UnityEngine;

namespace NootyBallScripts.GLBAnimationImporter
{
    public class ImportedAnimationPlayer : MonoBehaviour
    {
        private Animation anim;

        private ListOfAnimations possibleAnimationsList;
        public void Init(Animation glbAnimation, ListOfAnimations animationList)
        {
            anim = glbAnimation;
            possibleAnimationsList = animationList;
        }
        public void Init(Animation glbAnimation)
        {
            anim = glbAnimation;
        }
        public void PlayAnimation(string animationName)
        {
            anim.Play(animationName);
        }
        public void PlayAnimationQueued(string animationName)
        {
            anim.PlayQueued(animationName);
        }

        public void PlayAnimation(int index)
        {
            if (possibleAnimationsList != null)
            {
                anim.Play(possibleAnimationsList.ImportedAnimations[index].animationName);
            }
        }

        public void PlayAnimationQueued(int index)
        {
            if (possibleAnimationsList != null)
            {
                anim.PlayQueued(possibleAnimationsList.ImportedAnimations[index].animationName);
            }
        }
    }
}