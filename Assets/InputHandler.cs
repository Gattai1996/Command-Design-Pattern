using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject character;
    private Animator _animator;
    private Command _keyUpArrow;
    private Command _keyQ;
    private Command _keyE;
    private Command _keyR;
    private List<Command> commands = new List<Command>();
    private Coroutine replayCoroutine;
    private bool shouldStartReplay, isReplaying;

    void Start()
    {
        _animator = character.GetComponent<Animator>();
        _keyUpArrow = new CommandWalk();
        _keyQ = new CommandJump();
        _keyE = new CommandPunch();
        _keyR = new CommandKick();
        Camera.main.GetComponent<CameraFollow360>().player = character.transform;
    }

    void Update()
    {
        if (!isReplaying)
            HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _keyUpArrow.Execute(animator: _animator, forward: true);
            commands.Add(_keyUpArrow);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _keyQ.Execute(animator: _animator, forward: true);
            commands.Add(_keyQ);
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            _keyE.Execute(animator: _animator, forward: true);
            commands.Add(_keyE);
        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            _keyR.Execute(animator: _animator, forward: true);
            commands.Add(_keyR);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shouldStartReplay = true;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            UndoLastCommand();
        }
    }

    private void UndoLastCommand()
    {
        if (commands.Count > 0)
        {
            Command c = commands[commands.Count - 1];
            c.Execute(animator: _animator, forward: false);
            commands.RemoveAt(commands.Count - 1);
        }
    }

    private void StartReplay()
    {
        if (shouldStartReplay && commands.Count > 0)
        {
            shouldStartReplay = false;
            
            if (replayCoroutine == null)
            {
                StopCoroutine(replayCoroutine);
            }

            replayCoroutine = StartCoroutine(ReplayCommands());
        }
    }

    private IEnumerator ReplayCommands()
    {
        isReplaying = true;

        for (int i = 0; i < commands.Count; i++)
        {
            commands[i].Execute(animator: _animator, forward: true);
            yield return new WaitForSeconds(1f);
        }

        isReplaying = false;
    }
}
