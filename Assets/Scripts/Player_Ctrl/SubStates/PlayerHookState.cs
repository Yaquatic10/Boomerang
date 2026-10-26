using UnityEngine;

public class PlayerHookState : PlayerBaseState
{
    public PlayerHookState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        
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

    }

    public override void CheckSwitchStates()
    {

    }

    public override void InitializeSubState()
    {

    }
}

