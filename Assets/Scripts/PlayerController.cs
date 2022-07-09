using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
public class PlayerController : MonoBehaviour
{
    public GameObject BodySourceManager;

    public Rigidbody2D rb = null;

    private Dictionary<ulong, Transform> _Bodies = new Dictionary<ulong, Transform>();
    private BodySourceManager _BodyManager;

    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },

        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },

        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },

        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },

        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };

    private void Update()
    {
        if (BodySourceManager == null)
        {
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
 
        // ここでdata配列にBody情報を格納
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }

        int count = 0;
        // Debug.Log(data.Length);
        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }


            // この時点でbodyにはデータが入ってることが確定
            if (body.IsTracked)
            {
                //Debug.Log("trackingid : " + body.TrackingId);
                trackedIds.Add(body.TrackingId);
            }
        }

        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        // まず、トラッキングできていないbodiesを消す
        foreach(ulong trackingId in knownIds)
        {
            Debug.Log("trackingID : " + trackingId);
            // もし、トラッキングできたidたちにtrakingIdがふくまれていなかったら、そのキーを削除
            if (!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach(var body in data)
        {
            if(body == null)
            {
                continue;
            }

            count++;
            if (body.IsTracked)
            {
            Debug.Log(count);

                if(!_Bodies.ContainsKey(body.TrackingId))
                {
                     var sourceJoint = new List<Vector3>();
                    // 4:ShoulderLeft 5:ElbowLeft 8:ShoulderRight 9:ElbowRight 20:SpineShoulder
                    foreach (Kinect.JointType jt in new int[] { 5, 9, 20 })
                    {
                        sourceJoint.Add(GetVector3FromJoint(body.Joints[jt]));                       
                    }
                    Vector3 targetPosition = sourceJoint[2];
                    targetPosition.y = -1.5f;
                    rb.MovePosition(targetPosition);

                    Vector3 elbowAngle = sourceJoint[0] - sourceJoint[1];
                    elbowAngle.z = 0; 
                    //Debug.Log(elbowAngle);
                    float angle = Mathf.Atan2(elbowAngle.y, elbowAngle.x) * Mathf.Rad2Deg;
                    //Debug.Log(angle);
                    // Cube.eulerAngles = elbowAngle;
                    // Cube.transform.Rotate(0, 0, elbowAngle.y, Space.World);
                    rb.MoveRotation(angle);
                }
            }
        }
    }



    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z*10);
    }

    void addForce2Fruit()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "fruit")
        {

        }
    }
}