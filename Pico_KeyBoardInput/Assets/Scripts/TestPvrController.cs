

using Pvr_UnitySDKAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPvrController : MonoBehaviour
{


    private GameObject KeyBoardFather;

    // Use this for initialization
    void Start()
    {
        KeyBoardFather = this.gameObject.GetComponent<KeyInput>().KeyboardFather;

    }

    // Update is called once per frame
    void Update()
    {

        if (Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.APP) || Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.HOME) || Input.GetKeyDown(KeyCode.A))
        {
            KeyBoardFather.SetActive(true);
        }

    }
}
