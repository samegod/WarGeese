using UnityEngine;

namespace Game.Scripts.Animator
{
  public class AnimatorStateReporter : StateMachineBehaviour
  {
    private IAnimationStateReader _stateReader;

    public override void OnStateEnter(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      base.OnStateEnter(animator, stateInfo, layerIndex);
      FindReader(animator);

      _stateReader.EnteredState(stateInfo.shortNameHash);
      int[] arr = new int[3]{1, 2, 3};
    }

    public override void OnStateExit(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      base.OnStateExit(animator, stateInfo, layerIndex);
      FindReader(animator);

      _stateReader.ExitedState(stateInfo.shortNameHash);
    }

    private void FindReader(UnityEngine.Animator animator)
    {
      if (_stateReader != null)
        return;

      _stateReader = animator.gameObject.GetComponent<IAnimationStateReader>();
    }
  }
}