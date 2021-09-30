using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 15f;
    public Transform largeObject;
    //
    private bool isClimbing;
    Rigidbody body;
    private MeshCollider statueCollider;
    private Mesh _mesh;
    Vector3 wallPoint;
    Vector3 wallNormal;
    private float _radius;
    private LoadScene _loadScene;
    private Animator _animator;
    public float OffsetValue = 5.0f;
    public LayerMask wallLayerMask;
    


    void Start()
    {
        isClimbing = false;
        body = GetComponent<Rigidbody>();
        statueCollider = largeObject.GetComponent<MeshCollider>();
        _radius = GetComponent<SphereCollider>().radius;
        _animator = GameObject.FindGameObjectWithTag("spider").GetComponent<Animator>();
    }

    void loadStatue()
    {
        _loadScene = GetComponent<LoadScene>();
        _loadScene.load();
        statueCollider = _loadScene.ChosenStatue.GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isClimbing)
        {
            Walk();
        }
        else
        {
            ClimbWallVs2();
        }

    }
    
    void Walk()
    {
        body.useGravity = true;
        var v = Input.GetAxis("Vertical");
        var h = Input.GetAxis("Horizontal");
        if (v!=0 || h != 0)
        {
            _animator.SetBool("ShouldWalk",true);
        }
        else
        {
            _animator.SetBool("ShouldWalk",false);

        }
        var move = transform.forward * v + transform.right * h;
        body.MovePosition(transform.position + move * speed * 2.5f * Time.deltaTime);
    }
    
    void ClimbWall()
    {
        body.useGravity = false;
        var v = Input.GetAxis("Vertical");
        var h = Input.GetAxis("Horizontal");
        //set upraycaster 
        RaycastHit hit;
        //were going up
        bool facingWall = Physics.Raycast(transform.position, transform.forward, out hit, _radius + 1f);
        //calculating normal

        wallPoint = hit.point;
        wallNormal = hit.normal;
        
        if (wallNormal == Vector3.zero)
        {
            findCorrectRayCasting(hit);
        }
        
        //grabwall
        //
        if (wallNormal == Vector3.zero)
        {
            Debug.Log("this mother fucker");
            var move1 = transform.forward * v + transform.right * h;
            body.MovePosition(transform.position + move1 * speed * 2.5f * Time.deltaTime);
            return;
        }
        
        Vector3 newPosition = wallPoint + wallNormal * (_radius - 0.1f);
        newPosition = Vector3.Lerp(transform.position, newPosition, 10 * Time.deltaTime);

        transform.rotation = Quaternion.LookRotation(-wallNormal);
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, wallNormal);
        
        //aftergrabwall
        Vector3 move = Vector3.zero;
        if (h > 0f)
        {
            move = transform.up * h + transform.right * h;
         
        }
        else
        {
            move = transform.up * v + transform.right * h;
        }
        
        //Vector3 pointOnStatue = statueCollider.ClosestPoint (body.position + move * speed * Time.deltaTime);
        
        body.MovePosition(body.position + move * speed * Time.deltaTime);
        
    }

    void ClimbWallVs2()
    {
        body.useGravity = false;
        RaycastHit hit;
        var v = Input.GetAxis("Vertical");
        var h = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.Normalize(transform.rotation * new Vector3(h, 0, v)) * speed * Time.deltaTime;
        if (FindClosestPoint(transform.position + movement, out hit))
        {
            body.MovePosition(hit.point);
        }


    }

    bool FindClosestPoint(Vector3 targetPos, out RaycastHit hit)
    {
        RaycastHit[] results = new RaycastHit[1];
        int numRes = 0;
        Vector3[] direction = new[] {Vector3.zero, Vector3.up, Vector3.left, Vector3.right, Vector3.down/2};
        for (int i = 0; i < direction.Length; i++)
        {
            if (Physics.SphereCast(targetPos + transform.up * 0.5f, _radius, (transform.up * -1) + direction[i], out hit,3000,wallLayerMask))
            {
                //hit = results[0];
                return true;
            }
        }

        hit = new RaycastHit();
        return false;
    }
    

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        RaycastHit hit;
        Gizmos.color = Color.yellow;
        if (FindClosestPoint(transform.position, out hit))
        {
            Gizmos.DrawSphere(hit.point, _radius);
        }
        
    }
    bool rayCasting(RaycastHit hit, int stat)
    {
        bool facingWall;
        Vector3 point;
        switch (stat)
        {
            case 2://back
                point = transform.position + Vector3.back * OffsetValue;
                facingWall = Physics.Raycast(point, Vector3.back, out hit, 1.5f);
                wallPoint = hit.point;
                wallNormal = hit.normal;
                return wallNormal.Equals(Vector3.zero);
            case 3://left
                point = transform.position + Vector3.left * OffsetValue;
                facingWall = Physics.Raycast(transform.position, Vector3.left, out hit, 1.5f);
                wallPoint = hit.point;
                wallNormal = hit.normal;
                return wallNormal.Equals(Vector3.zero);
            case 4://right
                point = transform.position + Vector3.right * OffsetValue;
                facingWall = Physics.Raycast(transform.position, Vector3.right, out hit, 1.5f);
                wallPoint = hit.point;
                wallNormal = hit.normal;
                return wallNormal.Equals(Vector3.zero);
            case 5://up
                point = transform.position + Vector3.up * OffsetValue;
                facingWall = Physics.Raycast(transform.position, Vector3.up, out hit, 1.5f);
                wallPoint = hit.point;
                wallNormal = hit.normal;
                return wallNormal.Equals(Vector3.zero);
            case 6://down
                point = transform.position + Vector3.down * OffsetValue;

                facingWall = Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f);
                wallPoint = hit.point;
                wallNormal = hit.normal;
                return wallNormal.Equals(Vector3.zero);
                
        }
        return false;

    }

    void findCorrectRayCasting(RaycastHit hit)
    {
        if (rayCasting(hit, 2)) {return;}
        if (rayCasting(hit, 3)) {return;}
        if (rayCasting(hit, 4)) {return;}
        if (rayCasting(hit, 5)) {return;}
        if (rayCasting(hit, 6)) {return;}
    }
    
    
    void climbwallvs1()
    {

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        
        Vector3 move = transform.up * v + transform.right * h;        
        
        Vector3 newLocation = transform.position + (move * speed * Time.deltaTime);
        Vector3 point = statueCollider.ClosestPointOnBounds(body.position);
        
        //float dist = Vector3.Distance(newLocation, point);
        transform.position = newLocation;
    }


    void OnCollisionEnter(Collision col)    
    {
        if (col.gameObject.CompareTag("statue"))
        {
            isClimbing = true;
            Debug.Log("Enter");
        }
    }
    

}
