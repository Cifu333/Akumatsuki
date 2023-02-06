using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    public GameObject target;
    public float speed = 1.0f;
    public float offsetX = 0;
    public float offsetY = 0;

    private HorizontalMovement targetMovementComponent;
    private Jump targetFall;

    void Start()
    {
        targetMovementComponent = target.GetComponent<HorizontalMovement>();
    }

    void LateUpdate()
    {
        targetFall = target.GetComponent<Jump>();
        //transform.position = Vector3.Lerp(transform.position, target.position + offset, speed * Time.deltaTime);
        Vector3 offset_y = Vector3.zero;
        Vector3 offset_x = Vector3.zero;
        if (targetMovementComponent != null){
            if (targetMovementComponent.dir == HorizontalMovement.Direction.LEFT){
                offset_x = Vector3.left * offsetX;
            }else  if (targetMovementComponent.dir == HorizontalMovement.Direction.RIGHT){
                offset_x = Vector3.right * offsetX;
            }
            if (targetFall.fall == true)
            {
                offset_y = Vector3.down  * offsetY;
                offset_y = offset_y * 2;
            }
            else
            {
                offset_y = Vector3.up * offsetY;
            }
            if (targetMovementComponent.currentSpeed != 0)
            {
                offset_x = offset_x * 2;
            }
        }
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset_x + offset_y, speed * Time.deltaTime);
    }
}
