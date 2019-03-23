using UnityEngine;
using UnityEngine.Events;

public class MudarClip : StateMachineBehaviour {

	public Motion ClipInfinty;
	public Animator animPerson;
	public Animation anim;
	public AnimationClip animClip;
	public UnityEvent OnUpdate;
	void Start(){
		Debug.Log("asdfds");
	}

	public void SetCurrentAnimation(Animator animator, string animNameToOverride)
	{
		RuntimeAnimatorController myOriginalController = animator.runtimeAnimatorController;
		AnimatorOverrideController myCurrentOverrideController = myOriginalController as AnimatorOverrideController;
		if (myCurrentOverrideController != null) // retrieve original animator controller
		{
			// don't reset if its already overriden
			if (myCurrentOverrideController[animNameToOverride] == animClip)
			{
				Debug.Log("Current state is already" + animClip.name);
				return;
			}
			myOriginalController = myCurrentOverrideController.runtimeAnimatorController;
			// Know issue: Disconnect the orignal controller first otherwise when you will delete this override it will send a callback to the animator
			// to reset the SM
			myCurrentOverrideController.runtimeAnimatorController = null;
		}
		AnimatorOverrideController myNewOverrideController = new AnimatorOverrideController();
		myNewOverrideController.runtimeAnimatorController = myOriginalController;
		myNewOverrideController[animNameToOverride] = animClip;
		animator.runtimeAnimatorController = myNewOverrideController;
		Object.Destroy(myCurrentOverrideController);
	}


	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
