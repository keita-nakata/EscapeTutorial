using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenJudge : MonoBehaviour
{
    private bool _isOpen = false;

    public TapObjectChange[] TapChanges;
    public int[] AnswerIndexes;
    public string OpenPositionName;
    public GameObject OpenCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOpen) return;

        for(int i = 0; i < TapChanges.Length; i++)
        {
            if (TapChanges[i].Index != AnswerIndexes[i])
                return;
        }
        //ここにきたら正解
        _isOpen = true;

        foreach(var TapChange in TapChanges)
        {
            TapChange.enabled = false;
            TapChange.gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        Invoke(nameof(CameraMove), 0.5f);
    }

    private void CameraMove()
    {
        CameraManager.Instance.ChangeCameraPosition(OpenPositionName);
        OpenCollider.SetActive(true);
    }
}
