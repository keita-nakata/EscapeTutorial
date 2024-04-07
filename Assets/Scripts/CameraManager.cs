using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public string CurrentPositionName { get; private set; }

    public GameObject ButtonLeft;
    public GameObject ButtonBack;
    public GameObject ButtonRight;

    /// <summary>
    ///カメラ位置情報クラス
    /// </summary>
    private class CameraPositionInfo
    {
        /// <summary>カメラの位置</summary>
        public Vector3 Position { get; set; }
        /// <summary>カメラの角度</summary>
        public Vector3 Rotate { get; set; }
        /// <summary>ボタンの移動先</summary>
        public MoveNames MoveNames { get; set; }
    }
    /// <summary>
    /// ボタン移動先クラス
    /// </summary>
    private class MoveNames
    {
        public string Left { get; set; }
        public string Back { get; set; }
        public string Right { get; set; }
    }

    private Dictionary<string, CameraPositionInfo> _CameraPositionInfoes = new Dictionary<string, CameraPositionInfo>
    {
        {
            "Door", //位置名
            new CameraPositionInfo
            {
                Position = new Vector3(8, 1, 3),
                Rotate = new Vector3(0, 180, 0),
                MoveNames = new MoveNames
                {
                    Left = "RoomLeft",
                    Right = "RoomRight",
                }
            }
        },
        {
            "RoomLeft", //位置名
            new CameraPositionInfo
            {
                Position = new Vector3(8, 1, 3),
                Rotate = new Vector3(0, 100, 0),
                MoveNames = new MoveNames
                {
                    Right = "Door"
                }
            }
        },
        {
            "RoomRight", //位置名
            new CameraPositionInfo
            {
                Position = new Vector3(8, 1, 3),
                Rotate = new Vector3(0, 264, 0),
                MoveNames = new MoveNames
                {
                    Left = "Door"
                }
            }
        },
        {
            "SandBox", //位置名
            new CameraPositionInfo
            {
                Position = new Vector3(12, 2, 1),
                Rotate = new Vector3(37, 430, 0),
                MoveNames = new MoveNames
                {
                    Back = "RoomLeft"
                }
            }
        },
        {
            "ItemBox", //位置名
            new CameraPositionInfo
            {
                Position = new Vector3(3, 1, 4),
                Rotate = new Vector3(3, 270, 0),
                MoveNames = new MoveNames
                {
                    Back = "RoomRight"
                }
            }
        },
        {
            "ItemBoxOpen", //位置名
            new CameraPositionInfo
            {
                Position = new Vector3(3, 1, 4),
                Rotate = new Vector3(3, 270, 0),
                MoveNames = new MoveNames
                {
                    Back = "ItemBox"
                }
            }
        },
        {
            "Sofa", //位置名
            new CameraPositionInfo
            {
                Position = new Vector3(2, 1, 1),
                Rotate = new Vector3(25, 236, 0),
                MoveNames = new MoveNames
                {
                    Back = "RoomRight"
                }
            }
        },
    };



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        ChangeCameraPosition("Door");

        ButtonBack.GetComponent<Button>().onClick.AddListener(() =>
        {
            ChangeCameraPosition(_CameraPositionInfoes[CurrentPositionName].MoveNames.Back);
        });
        ButtonLeft.GetComponent<Button>().onClick.AddListener(() =>
        {
            ChangeCameraPosition(_CameraPositionInfoes[CurrentPositionName].MoveNames.Left);
        });
        ButtonRight.GetComponent<Button>().onClick.AddListener(() =>
        {
            ChangeCameraPosition(_CameraPositionInfoes[CurrentPositionName].MoveNames.Right);
        });
    }

    /// <summary>
    /// カメラ移動
    /// </summary>
    /// <param name="positionName">カメラ位置名</param>
    public void ChangeCameraPosition(string positionName)
    {
        if (positionName == null) return;

        CurrentPositionName = positionName;

        GetComponent<Camera>().transform.position = _CameraPositionInfoes[CurrentPositionName].Position;
        GetComponent<Camera>().transform.rotation = Quaternion.Euler(_CameraPositionInfoes[CurrentPositionName].Rotate);

        UpdateButtonActive();
    }

    private void UpdateButtonActive()
    {
        if (_CameraPositionInfoes[CurrentPositionName].MoveNames.Back == null)
            ButtonBack.SetActive(false);
        else ButtonBack.SetActive(true);
        if (_CameraPositionInfoes[CurrentPositionName].MoveNames.Left == null)
            ButtonLeft.SetActive(false);
        else ButtonLeft.SetActive(true);
        if (_CameraPositionInfoes[CurrentPositionName].MoveNames.Right == null)
            ButtonRight.SetActive(false);
        else ButtonRight.SetActive(true);
    }
}
