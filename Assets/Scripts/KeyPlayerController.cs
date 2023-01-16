using UnityEngine;

// キーボード操作でのRigidBody2D移動・回転を追加する
public class KeyPlayerController : MonoBehaviour
{    // Update is called once per frame

    [SerializeField]
    private float movingSpeed = 1.0f;
    [SerializeField]
    private float rotateSpeed = 1.0f;

    public float minX = -10.0f;
    public float maxX = 10.0f;

    private Rigidbody2D player;

    private void Start()
    {
        player = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // キーボードでのプレイヤー移動処理
        float AxisX = Input.GetAxis("Horizontal");
        float AxisY = Input.GetAxis("Vertical");
        float AxisT = Input.GetAxis("Trigger");
        float movedX = Mathf.Clamp(player.position.x + movingSpeed * Time.deltaTime * AxisX, minX, maxX);
        Vector2 movedPosition = new Vector2(movedX, player.position.y);
        player.MovePosition(movedPosition);

        if (AxisY != 0)
        {
            player.MoveRotation(player.rotation + AxisY * rotateSpeed * Time.fixedDeltaTime);
        }
        else if (AxisT != 0)
        {
            player.MoveRotation(player.rotation - AxisT * rotateSpeed * Time.fixedDeltaTime);
        }
    }
}
