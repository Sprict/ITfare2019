using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class PlayersManager : MonoBehaviour
{
    Kinect.KinectSensor kinect;
    Kinect.BodyIndexFrameReader bodyIndexFrameReader;
    Kinect.FrameDescription bodyIndexFrameDesc;

    // データ取得用
    byte[] bodyIndexBuffer = null;

    public void Start()
    {
        if(kinect != null)
        {
            //Kinectを開く
            kinect = Kinect.KinectSensor.GetDefault();
            kinect.Open();

            // 表示のためのデータを作成
            bodyIndexFrameDesc = kinect.DepthFrameSource.FrameDescription;

            // ボディーリーダーを開く
            bodyIndexFrameReader = kinect.BodyIndexFrameSource.OpenReader();
            bodyIndexFrameReader.FrameArrived += bodyIndexFrameReader_FrameArrived;
        }
    }

    void bodyIndexFrameReader_FrameArrived(object sender, Kinect.BodyIndexFrameArrivedEventArgs e )
    {
        UpdateBodyIndexFrame(e);
        Debug.Log("bodyIndexBuffer" + bodyIndexBuffer);
        // DrawBodyIndexFrame();
    }

    // ボディインデックスフレームの更新
    private void UpdateBodyIndexFrame( Kinect.BodyIndexFrameArrivedEventArgs e)
    {
        using (var bodyIndexFrame = e.FrameReference.AcquireFrame())
        {
            if(bodyIndexFrame == null)
            {
                return;
            }

            // ボディインデックスデータを取得する
            bodyIndexFrame.CopyFrameDataToArray(bodyIndexBuffer);
        }
    }
}
