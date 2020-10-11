using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJumpable : IMovable
{
    void handleJump();

    void finishJumpAnim();
}
