using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        //Set active Walk Animation.
        //Debug.Log("WALK STATE");
    }

    public override void UpdateState()
    {
        if (!Ctx.SpeedBuffEnabled)
        {
            Ctx.CurrentMoveMultiplier = 1;
        }
        //EXAPNDIR CONDICIONALES CON LOS DIFERENTES MODIFICADORES DE ITEMS.

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