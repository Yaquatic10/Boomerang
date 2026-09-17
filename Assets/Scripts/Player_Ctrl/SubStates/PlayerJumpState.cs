using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        //Set active JUMP Animation.
        Ctx.IsJumping = true;
        Debug.Log("JUMP STATE");
        Ctx.CurrentGravityScale = Ctx.GravityScale; //Descomentar si se usa salto preciso por altura.
        Ctx.RbChar.linearVelocity = new Vector2(Ctx.RbChar.linearVelocity.x, Ctx.JumpForce);
        Ctx.JumpTimeCounter = Ctx.MaxJumpTime;
    }

    public override void UpdateState()
    {
        if (Ctx.IsJumpPressed && Ctx.JumpTimeCounter > 0)
        {
            Ctx.RbChar.linearVelocity = new Vector2(Ctx.RbChar.linearVelocity.x, Ctx.JumpForce);
            Ctx.JumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            Ctx.JumpTimeCounter = 0;
        }
        
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        //Set inactive JUMP Animation
        Ctx.IsJumping = false;
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
