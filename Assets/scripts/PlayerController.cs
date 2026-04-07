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
        {
            RotateSprite(moveDir); 
            TryMove(moveDir);
        }
    }

    void RotateSprite(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

       
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void TryMove(Vector2 direction)
    {
        Vector2 currentPos = Vector2Int.RoundToInt(transform.position);
        Vector2 targetPos = currentPos + direction;

       
        if (_gridManager.GetTileAtPosition(targetPos) == null)
            return;

        
        if (_gridManager.HasBoxAtPosition(targetPos))
        {
            var boxObj = _gridManager.GetBoxAtPosition(targetPos);
            var box = boxObj.GetComponent<Box>();

          
            if (!box.CanBePushed())
                return;

            Vector2 boxTargetPos = targetPos + direction;

           
            if (_gridManager.GetTileAtPosition(boxTargetPos) == null)
                return;

          
            if (_gridManager.HasBoxAtPosition(boxTargetPos))
                return;

           
            boxObj.transform.position = boxTargetPos;

            box.RegisterPush(); 

            _gridManager.MoveBox(targetPos, boxTargetPos);
        }

        
        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
    }
}