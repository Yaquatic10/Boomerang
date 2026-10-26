using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        //Set active Walk Animation.
        Ctx.AnimCtrl.SetBool("isWalking", true);
    }

    public override void UpdateState()
    {
        Ctx.AppliedMovementX = Ctx.MovementInput.x * Ctx.MoveSpeed * Ctx.CurrentMoveMultiplier;
        if (Ctx.MovementInput.x != 0)
        {
            Ctx.transform.localScale = new Vector3(Mathf.Sign(Ctx.MovementInput.x), 1, 1);
        }
        CheckSwitchStates();
    }

    public override void ExitState() 
    {
        //Set inactive Walk Animation
        Ctx.AnimCtrl.SetBool("isWalking", false);
    }

    public override void CheckSwitchStates()
    {
       if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void InitializeSubState()
    {
        
    }
}