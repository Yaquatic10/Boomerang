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
        Debug.Log("AIRBORN STATE");
        InitializeSubState();
    }

    public override void UpdateState()
    {
        Ctx.IsGrounded = Physics2D.OverlapCircle(Ctx.GroundCheckPoint.position, Ctx.GroundCheckRadius, Ctx.GroundLayer);   //Comprobación de contacto con el suelo.
        
        //Esta variante del código permite controlar el salto con una altura establecida de forma precisa sin importar la masa y gravedad.
        //Ctx.JumpForce = Mathf.Sqrt(Ctx.JumpHeight * -3 * (Physics.gravity.y * Ctx.GravityScale));
        Ctx.RbChar.AddForce(Physics.gravity * (Ctx.CurrentGravityScale - 1) * Ctx.RbChar.mass); //Aplicación de gravedad dinamica.
        
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
        if (Ctx.IsGrounded && !Ctx.IsJumping)
        {
            Debug.Log("Tocó suelo");
            SwitchState(Factory.Grounded());
        }
    }

    public override void InitializeSubState()
    {
        if (Ctx.IsJumpPressed)
        {
            SetSubState(Factory.Jump());
        }
        else if (Ctx.RbChar.linearVelocity.y <= 0)
        {
            SetSubState(Factory.Fall());
        }
    }
}
