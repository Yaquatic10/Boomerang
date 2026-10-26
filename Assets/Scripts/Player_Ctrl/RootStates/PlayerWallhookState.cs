using UnityEngine;

public class PlayerWallhookState : PlayerBaseState
{
    public PlayerWallhookState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        Ctx.RbChar.sharedMaterial = Ctx.FrictionMat;
        
    }

    public override void UpdateState()
    {
        Ctx.IsHookedRgt = Physics2D.OverlapCircle(Ctx.WallCheckPointRgt.position, Ctx.WallCheckRadius, Ctx.WallLayer); //Comprobación de contacto con paredes.
        Ctx.IsHookedLft = Physics2D.OverlapCircle(Ctx.WallCheckPointLft.position, Ctx.WallCheckRadius, Ctx.WallLayer); //Comprobación de contacto con paredes.

        if (Ctx.IsHookedRgt) Ctx.WallJumpOrient = -1;
        else if (Ctx.IsHookedLft) Ctx.WallJumpOrient = 1;

        Ctx.IsGrounded = Physics2D.OverlapCircle(Ctx.GroundCheckPoint.position, Ctx.GroundCheckRadius, Ctx.GroundLayer);   //Comprobación de contacto con el suelo.
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Ctx.RbChar.sharedMaterial = Ctx.FrictionlessMat;
    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsHookedRgt && !Ctx.IsHookedLft)
        {
            SwitchState(Factory.Airborne());
        }
        else if (Ctx.IsJumpPressed)
        {
            Ctx.IsWallJumping = true;
            SwitchState(Factory.Airborne());
        }
    }

    public override void InitializeSubState()
    {
        SetSubState(Factory.Hook());
    }
}

