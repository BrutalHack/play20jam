using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SpineToMecanim : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (animator == null)
        {
            return;
        }

        SkeletonAnimation skeletonAnimation = animator.GetComponent<SkeletonAnimation>();
        if (skeletonAnimation == null || skeletonAnimation.state == null)
        {
            string reason = skeletonAnimation == null ? "component not found" : "state not ready yet";
            return;
        }

        AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(layerIndex);
        if (clipInfos == null || clipInfos.Length == 0)
        {
            return;
        }

        AnimationClip animationClip = clipInfos[0].clip;
        if (animationClip == null)
        {
            return;
        }

        skeletonAnimation.state.SetAnimation(0, animationClip.name, animationClip.isLooping);

    }

    private static string GetPath(GameObject go)
    {
        string path = "\"";
        Transform parent = go?.transform;
        while (parent != null)
        {
            path = parent.name + "/" + path;
            parent = parent.transform.parent;
        }
        return path + "\"";
    }
}

    
