using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputManager : IInitializable, IDisposable
{
    private PlayerInput _playerInput;

    public void Initialize()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    public void Dispose()
    {
        _playerInput.Disable();
    }
    
    private void LockInput(bool state)
    {
        if (state) _playerInput.Disable();
        else _playerInput.Enable();
    }
    
    public Vector2 MovementDirection()
    {
        return _playerInput.Player.Move.ReadValue<Vector2>().normalized;
    }

    public InputAction Attack()
    {
        return _playerInput.Player.Attack;
    }

    public InputAction SwitchSpell()
    {
        return _playerInput.Player.SwitchSpell;
    }
}