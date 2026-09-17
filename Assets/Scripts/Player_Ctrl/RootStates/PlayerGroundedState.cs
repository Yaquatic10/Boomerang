using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        
    }

    public override void EnterState()
    {
        Debug.Log("GROUNDED STATE");
        InitializeSubState();
    }

    public override void UpdateState()
    {
        Ctx.IsGrounded = Physics2D.OverlapCircle(Ctx.GroundCheckPoint.position, Ctx.GroundCheckRadius, Ctx.GroundLayer);   //Comprobación de contacto con el suelo.

        CheckSwitchStates();
    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsGrounded)
        {
            SwitchState(Factory.Airborne());
        }
        else if (Ctx.IsJumpPressed)
        {
            SwitchState(Factory.Airborne());
        }
    }

    public override void InitializeSubState()
    {
        if (Ctx.IsMovementPressed)
        {
            SetSubState(Factory.Walk());
        }
        else
        {
            SetSubState(Factory.Idle());
        }
    }
}
