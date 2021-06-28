using UnityEngine;

public abstract class Command
{
    public abstract void Execute(Animator animator, bool forward);
}

public class CommandWalk: Command
{
    public override void Execute(Animator animator, bool forward)
    {
        if (forward)
        {
            animator.SetTrigger("isWalking");
        }
        else
        {
            animator.SetTrigger("isWalkingR");
        }
    }
}

public class CommandJump: Command
{
    public override void Execute(Animator animator, bool forward)
    {
        if (forward)
        {
            animator.SetTrigger("isJumping");
        }
        else
        {
            animator.SetTrigger("isJumpingR");
        }
    }
}

public class CommandPunch: Command
{
    public override void Execute(Animator animator, bool forward)
    {
        if (forward)
        {
            animator.SetTrigger("isPunching");
        }
        else
        {
            animator.SetTrigger("isPunchingR");
        }
    }
}

public class CommandKick: Command
{
    public override void Execute(Animator animator, bool forward)
    {
        if (forward)
        {
            animator.SetTrigger("isKicking");
        }
        else
        {
            animator.SetTrigger("isKickingR");
        }
    }
}
