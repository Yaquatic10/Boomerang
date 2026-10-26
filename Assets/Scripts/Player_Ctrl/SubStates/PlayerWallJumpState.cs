using UnityEngine;

public class PlayerWallJumpState : PlayerBaseState
{
    public PlayerWallJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Ctx.transform.localScale = new Vector3(Ctx.WallJumpOrient, 1, 1);
        Ctx.AppliedMovementX = Ctx.WallJumpOrient * Ctx.WallJumpForce;
        Ctx.RbChar.linearVelocity = new Vector2(Ctx.AppliedMovementX, 20f);
        
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState()
    {
        Ctx.IsWallJumping = false;
    }
    public override void CheckSwitchStates()
    {
        if (Ctx.RbChar.linearVelocity.y < 0)
        {
            SwitchState(Factory.Fall());
        }
    }
    public override void InitializeSubState()
    {
        
    }
}
