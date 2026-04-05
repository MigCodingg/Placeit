using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GridManager _gridManager;

    void Start()
    {
        _gridManager = FindAnyObjectByType<GridManager>();
    }

    void Update()
    {
        Vector2 moveDir = Vector2.zero;

        if (Keyboard.current.wKey.wasPressedThisFrame)
            moveDir = Vector2.up;

        if (Keyboard.current.sKey.wasPressedThisFrame)
            moveDir = Vector2.down;

        if (Keyboard.current.aKey.wasPressedThisFrame)
            moveDir = Vector2.left;

        if (Keyboard.current.dKey.wasPressedThisFrame)
            moveDir = Vector2.right;

        if (moveDir != Vector2.zero)
            TryMove(moveDir);
    }

    void TryMove(Vector2 direction)
    {
        Vector2 currentPos = Vector2Int.RoundToInt(transform.position);
        Vector2 targetPos = currentPos + direction;

        // 🚫 Block if outside grid
        if (_gridManager.GetTileAtPosition(targetPos) == null)
            return;

       // 📦 If there's a box → try pushing
        if (_gridManager.HasBoxAtPosition(targetPos))
        {
            var boxObj = _gridManager.GetBoxAtPosition(targetPos);
           var box = boxObj.GetComponent<Box>();

                // 🚫 Check push limit
            if (!box.CanBePushed())
            return;

            Vector2 boxTargetPos = targetPos + direction;

            // 🚫 Can't push outside grid
            if (_gridManager.GetTileAtPosition(boxTargetPos) == null)
                return;

            // 🚫 Can't push into another box
            if (_gridManager.HasBoxAtPosition(boxTargetPos))
                return;

            // ✅ Move box
            boxObj.transform.position = boxTargetPos;

            box.RegisterPush(); // 🔥 count the push

            _gridManager.MoveBox(targetPos, boxTargetPos);
}

        // ✅ Move player
        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
    }
}