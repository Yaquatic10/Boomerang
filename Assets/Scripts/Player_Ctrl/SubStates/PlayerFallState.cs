using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("FALL STATE");
        Ctx.CurrentGravityScale = Ctx.FallingGravityScale;    //Descomentar si se usa salto preciso por altura.
    }

    public override void UpdateState()
    {
        Ctx.RbChar.linearVelocity += Vector2.up * Physics.gravity.y * (Ctx.CurrentGravityScale - 1) * Time.deltaTime;

        CheckSwitchStates();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        
    }

    public override void InitializeSubState()
    {

    }
}

