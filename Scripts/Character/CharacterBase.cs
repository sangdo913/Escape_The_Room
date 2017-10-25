using UnityEngine;
using System.Collections;

public abstract class CharacterBase : MonoBehaviour {

    Animator anim;
    Rigidbody rigid;
    RectTransform rect;

    Vector3 destination ;

    bool isGround = true;
    bool isJump = false;
    bool IsMove = false;
    float moveSpeed = Setting.SetmoveSpeed();
    float JumpPower = Setting.SetJumpPower();

    protected CharacterInfo thisCharacter;

    void Awake()
    {

        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        rect = GetComponent<RectTransform>();

        if(anim == null)
        {
            anim = GetComponent<Animator>();
        }

        destination = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }

    void Start()
    {
    }

    void Update()
    {      
        //만약 목적지가 있다면 이동합니다.
        if (Mathf.Abs(destination.x - transform.position.x) > 1.0f || Mathf.Abs(destination.z - transform.position.z) > 1.1f)
        {
            destination.y = transform.position.y;
            Vector3 direction = destination - transform.position;
            Rotate(direction);
            transform.position += Vector3.Normalize(destination - transform.position) * Time.deltaTime * moveSpeed;

            IsMove = true;
        }

        else
        {
            IsMove = false;
        }

        anim.SetFloat("Speed", IsMove ? 1.0f : 0.0f);
        
    }

    //캐릭터가 앞을 바라보게합니다.
    void setforward()
    {
        rigid.rotation = Quaternion.LookRotation(transform.forward);
    }

    //캐릭터의 이동방향을 설정합니다.
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    public void SetPosition(Vector3 position)
    {
        destination = position;
        transform.position = position;
        rigid.rotation = Quaternion.LookRotation(position - transform.position);
    }

    void OnCollisionStay(Collision collider)
    {
        if (collider.gameObject.transform.tag == "Floor")
        {
            isGround = true;
        }
    }

    void OnCollisionExit(Collision collider)
    {
        if (collider.gameObject.transform.tag == "Floor")
        {
            isGround = false;
        }
    }

    void Rotate(Vector3 direction)
    {
        direction.y = 0;
        rigid.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 20f);
    }

    public void jump()
    {
        if ((isGround && rigid.velocity.y < 1))
        {
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            anim.SetTrigger("jump");
            isJump = true;
            isGround = false;
        }
    }

    public RectTransform GetRect()
    {
        return rect;
    }

    public bool IsJump()
    {
        return !isGround;
    }

    public bool IsJumpEnd()
    {
        return isJump && isGround;
    }

    public void ClearJump()
    {
        isJump = false;
    }

	public int GetCharacter()
	{
		return (int)thisCharacter;
	}
}