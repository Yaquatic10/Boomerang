using UnityEngine;

public class PlayerStateFactory
{
    PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    //ROOT STATES.
    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, this);
    }

    public PlayerBaseState Airborne()
    {
        return new PlayerAirbornState(_context, this);
    }

    public PlayerBaseState Wallhook()
    {
        return new PlayerWallhookState(_context, this);
    }

    public PlayerBaseState Stunned()
    {
        return new PlayerStunnedState(_context, this);
    }

    //SUB STATES
    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }

    public PlayerBaseState Walk()
    {
        return new PlayerWalkState(_context, this);
    }

    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(_context, this);
    }

    public PlayerBaseState Fall()
    {
        return new PlayerFallState(_context, this);
    }

    public PlayerBaseState Hook()
    {
        return new PlayerHookState(_context, this);
    }

    public PlayerBaseState Boomerang()
    {
        return new PlayerBoomerangState(_context, this);
    }

    public PlayerBaseState Item()
    {
        return new PlayerItemState(_context, this);
    }
}
