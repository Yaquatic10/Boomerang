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
        /*Ctx.CurrentGravityScale = Ctx.GravityScale; //Descomentar si se usa salto preciso por altura.
        Ctx.RbChar.linearVelocity = new Vector2(Ctx.RbChar.linearVelocity.x, Ctx.JumpForce);
        Ctx.JumpTimeCounter = Ctx.MaxJumpTime;*/

        Ctx.TiempoAlVertice = Ctx.TiempoBaseAlVertice * Ctx.RbChar.mass;
        Ctx.GravedadNecesaria = (2f * Ctx.JumpHeight) / Mathf.Pow(Ctx.TiempoAlVertice, 2);
        Ctx.CurrentGravityScale = 0;
        Ctx.RbChar.gravityScale = Ctx.GravedadNecesaria / Mathf.Abs(Physics2D.gravity.y);
        Ctx.JumpForce = (2f * Ctx.JumpHeight) / Ctx.TiempoAlVertice;
        Ctx.RbChar.linearVelocity = new Vector2(Ctx.RbChar.linearVelocity.x, Ctx.JumpForce);
    }

    public override void UpdateState()
    {
        /*if (Ctx.IsJumpPressed && Ctx.JumpTimeCounter > 0)
        {
            Ctx.RbChar.linearVelocity = new Vector2(Ctx.RbChar.linearVelocity.x, Ctx.JumpForce);
            Ctx.JumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            Ctx.JumpTimeCounter = 0;
        }*/
        
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
