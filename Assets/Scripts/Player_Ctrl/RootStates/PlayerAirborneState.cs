using UnityEngine;

public class PlayerAirbornState : PlayerBaseState
{
    public PlayerAirbornState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
    }

    public override void UpdateState()
    {
        Ctx.IsHookedRgt = Physics2D.OverlapCircle(Ctx.WallCheckPointRgt.position, Ctx.WallCheckRadius, Ctx.WallLayer); //Comprobación de contacto con paredes.
        Ctx.IsHookedLft = Physics2D.OverlapCircle(Ctx.WallCheckPointLft.position, Ctx.WallCheckRadius, Ctx.WallLayer); //Comprobación de contacto con paredes.
        Ctx.IsGrounded = Physics2D.OverlapCircle(Ctx.GroundCheckPoint.position, Ctx.GroundCheckRadius, Ctx.GroundLayer);   //Comprobación de contacto con el suelo.
        Ctx.RbChar.AddForce(Physics.gravity * (Ctx.CurrentGravityScale - 1)); //Aplicación de gravedad dinamica.
        Ctx.AppliedMovementX = Ctx.MovementInput.x * Ctx.MoveSpeed * Ctx.CurrentMoveMultiplier;
        if (Ctx.MovementInput.x != 0)
        {
            Ctx.transform.localScale = new Vector3(Mathf.Sign(Ctx.MovementInput.x), 1, 1);
        }
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchStates()
    {
        if ((Ctx.IsHookedRgt || Ctx.IsHookedLft) && !Ctx.IsJumping)
        {
            SwitchState(Factory.Wallhook());
        }
        else if (Ctx.IsGrounded && !Ctx.IsJumping)
        {
            SwitchState(Factory.Grounded());
        }
    }

    public override void InitializeSubState()
    {
        if (Ctx.IsJumpPressed && !Ctx.IsWallJumping)
        {
            SetSubState(Factory.Jump());
        }
        else if (Ctx.IsJumpPressed && Ctx.IsWallJumping)
        {
            SetSubState(Factory.WallJump());
        }
        else if (Ctx.RbChar.linearVelocity.y <= 0)
        {
            SetSubState(Factory.Fall());
        }
    }
}
