using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using System.IO;
using System;
using System.Text;
public class PlayerController2 : MonoBehaviour
{
    public GameObject BodySourceManager;
    public Rigidbody2D player1;
    public Rigidbody2D player2;
    [SerializeField]
    private Image tyuui = null;
    [SerializeField]
    private float p1originPosition = 0;
    [SerializeField]
    private float p2originPosition = 0;
    [SerializeField]
    private Dictionary<ulong, Rigidbody2D> _Bodies = new Dictionary<ulong, Rigidbody2D>();
    private BodySourceManager _BodyManager;

    /*
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
    */

    private string path;
    private string fileName = "onevalue.txt";
    float back = 15.0f;

    private void Start()
    {
        path = Application.dataPath + "/" + fileName;
        back = ReadFile();
        Debug.Log(back);
    }
    float ReadFile()
    {
        FileInfo fi = new FileInfo(path);

        using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8))
        {
            string readTxt = sr.ReadToEnd();
            return float.Parse(readTxt);
        }

    }
    private void Update()
    {
        tyuui.enabled = true;
        if (BodySourceManager == null)
        {
            Debug.Log("BodySourceManager NULL!!");
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            Debug.Log("BodyManager NULL!!");
            return;
        }

        // ここでdata配列にBody情報を格納
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }

        //Debug.Log(data.Length);
        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            // max 13.9ぐらいだったから15くらいでbodyを認識するかどうか分けるとよさそう
            if (GetVector3FromJoint(body.Joints[Windows.Kinect.JointType.SpineShoulder]).z >= back)
                continue;
            // トラッキングしてるbodyはそのIDを登録
            if (body.IsTracked)
            {
                Debug.Log("Add TrackingId" + body.TrackingId);
                trackedIds.Add(body.TrackingId);
                if (!_Bodies.ContainsKey(body.TrackingId))
                {
                    Debug.Log("containkey");
                    if (!_Bodies.ContainsValue(player1))
                    {
                        Debug.Log("add player1" + body.TrackingId);
                        _Bodies.Add(body.TrackingId, player1);
                    }
                    else if(!_Bodies.ContainsValue(player2))
                    {
                        Debug.Log("add player2" + body.TrackingId);
                        _Bodies.Add(body.TrackingId, player2);
                    }
                }
            }
        }

        // まず、トラッキングできていないbodiesを消す
        foreach(ulong knownId in _Bodies.Keys)
        {
            // もし、トラッキングできたidたちにtrakingIdがふくまれていなかったら、そのキーを削除
            if (!trackedIds.Contains(knownId))
            {
                Debug.Log("delete knownId" + knownId);
                _Bodies.Remove(knownId);
            }
        }
        // 何番目に入るかはランダム
        foreach(var body in data)
        {
            if(body == null)
            {
                continue;
            }

            // max 13.9ぐらいだったから15くらいでbodyを認識するかどうか分けるとよさそう
            if (GetVector3FromJoint(body.Joints[Windows.Kinect.JointType.SpineShoulder]).z >= back)
                continue;

            //Debug.Log("abody : " + abody);
            if (body.IsTracked)
            {
                Debug.Log(body.TrackingId);
                tyuui.enabled = false;

                var sourceJoint = new List<Vector3>();
                // 4:ShoulderLeft 5:ElbowLeft 8:ShoulderRight 9:ElbowRight 20:SpineShoulder 0:SpineBase
                foreach (Kinect.JointType jt in new int[] { 5, 9, 0, 20 })
                {
                    sourceJoint.Add(GetVector3FromJoint(body.Joints[jt]));
                }

                if(_Bodies[body.TrackingId])
                {
                    // Playerの移動処理
                    // Player1
                    if (_Bodies[body.TrackingId] == player1)
                    {
                        _Bodies[body.TrackingId].MovePosition(new Vector2(p1originPosition+sourceJoint[2].x, -3.5f));
                    }
                    // Player2
                    else if (_Bodies[body.TrackingId] == player2)
                    {
                        _Bodies[body.TrackingId].MovePosition(new Vector2(p2originPosition + sourceJoint[2].x, 4.3f));
                    }
                    // Playerの回転処理
                    // 左肘と右肘の差ベクトル
                    Vector3 elbowAngle;
                    if(body.ClippedEdges == Kinect.FrameEdges.Left)
                    {
                        elbowAngle = sourceJoint[1] - sourceJoint[3];
                        Debug.LogError("Left!!!!!");
                    }
                    else if (body.ClippedEdges == Kinect.FrameEdges.Right)
                    {
                        elbowAngle = sourceJoint[3] - sourceJoint[0];
                        Debug.LogError("Right");
                    }
                    else
                    {
                        elbowAngle = sourceJoint[1] - sourceJoint[0];
                    }
                    elbowAngle.z = 0;
                    // 左肘と右肘の差ベクトルからその角度（Radian）を求め、RadianからDegreeへ変換
                    float angle = Mathf.Atan2(elbowAngle.y, elbowAngle.x) * Mathf.Rad2Deg;
                    _Bodies[body.TrackingId].MoveRotation(angle);
                }
                else
                {
                    Debug.Log("bodies[body.trackingid] == NULL" + body.TrackingId);
                }

            }
        }
    }



    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z*10);
    }

}