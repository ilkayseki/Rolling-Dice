using System.Collections;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public Transform target; // Hedef nesnenin referansı
    [SerializeField] private float _rollSpeed = 5;
    private bool _isMoving;

    private Vector3 dir;
    private Vector3 anchor;
    private Vector3 axis;
    private float angle;
    
    
    private void Update()
    {
        if (_isMoving)
            return;

        CheckDistance();
    }

    private void CheckDistance()
    {
        dir = (target.position - transform.position).normalized;

        if (dir.x > 0.5f)
        {
            Assemble(Vector3.right);
        }
        else if (dir.x < -0.5f)
        {
            Assemble(Vector3.left);
        }
        else if (dir.z > 0.5f)
        {
            Assemble(Vector3.forward);
        }
        else if (dir.z < -0.5f)
        {
            Assemble(Vector3.back);
        }
    }

 
    private void Assemble(Vector3 dir)
    {
        anchor = transform.position + (Vector3.down + dir) * 0.5f;
        axis = Vector3.Cross(Vector3.up, dir);

        StartCoroutine(Roll(anchor, axis));
    }

    private IEnumerator Roll(Vector3 anchor, Vector3 axis) {
        
        _isMoving = true;
        for (var i = 0; i < 90 / _rollSpeed; i++) {
            transform.RotateAround(anchor, axis, _rollSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        _isMoving = false;
    }
}