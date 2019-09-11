

using System.Collections.Generic;
using UnityEngine;
using Pvr_UnitySDKAPI;

public class KeyInput : MonoBehaviour
{

    private GameObject currentTrigerobj;
    /// <summary>
    /// 是否大写
    /// </summary>
    private bool isUp = false;
    /// <summary>
    /// 显示输入的内容
    /// </summary>
    public TextMesh Acctext;
    /// <summary>
    /// 存放按键的列表
    /// </summary>
    public static List<GameObject> mList = new List<GameObject>();
    /// <summary>
    /// 键盘
    /// </summary>
    public GameObject Keyboard;
    /// <summary>
    /// 键盘父物体
    /// </summary>
    public GameObject KeyboardFather;
    /// <summary>
    /// 是否隐藏
    /// </summary>
    private bool isHide = true;
    /// <summary>
    /// 限制输入的字符数
    /// </summary>
    private float Cha_max = 15;
    private float Cha_min = 0;
    /// <summary>
    /// 当前输入的字符数
    /// </summary>
    public static float Cha_now = 0;


    public TextMesh bug;

    private void Awake()
    {
        Init();
        KeyboardFather.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Pvr_Controller.CurrColliderGameObject != null)
        {
            //获取选中的按键
            CurrentTrigerObj = Pvr_Controller.CurrColliderGameObject;
            //播放选中动画
            if (CurrentTrigerObj.tag == "Key" || CurrentTrigerObj.tag == "Back" || CurrentTrigerObj.tag == "Change" || CurrentTrigerObj.tag == "Ent" || CurrentTrigerObj.tag == "Sure" || CurrentTrigerObj.tag == "Return")
            {
                CurrentTrigerObj.GetComponent<Animation>().Play();
            }
            //获取选中按键的名称
            string s = CurrentTrigerObj.name;

            if (Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TOUCHPAD) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                bug.text = "点击的按键为："+CurrentTrigerObj.name;
                switch (CurrentTrigerObj.tag)
                {
                    case "Account":
                        //点击输入框显示键盘
                        Acctext = CurrentTrigerObj.GetComponentInChildren<TextMesh>();
                        Cha_now = Acctext.text.Length;
                        Keyboard.SetActive(true);
                        break;
                    case "Key":
                        if (Cha_min <= Cha_now && Cha_now < Cha_max)
                        {
                            //判断一下是不是“.”
                            if (s == "。")
                            {
                                s = ".";
                            }
                            //将输入的内容显示出来
                            Acctext.text += s;
                            //统计输入的字符
                            Cha_now++;
                        }
                        break;
                    case "Back":
                        //删除输入的内容
                        if (Cha_min < Cha_now && Cha_now <= Cha_max)
                        {
                            if (Acctext.text != "")
                            {
                                Acctext.text = DelString(Acctext.text);
                                Cha_now--;
                            }
                        }
                        break;
                    case "Change":
                        //修改大小写
                        Capslk();
                        isUp = !isUp;
                        break;
                    case "Ent":
                        Keyboard.SetActive(false);
                        break;
                    case "Sure":
                        //退出
                        if (Acctext.text == "zxc")
                        {
#if UNITY_EDITOR

                            UnityEditor.EditorApplication.isPlaying = false;
#else
                            Application.Quit();
#endif
                        }
                        else
                        {
                            bug.text = "您输入的内容为：" + Acctext.text;
                        }

                        break;
                    case "Return":
                        Keyboard.SetActive(false);
                        KeyboardFather.SetActive(false);
                        break;
                }
            }
        }
    }
    public GameObject CurrentTrigerObj
    {
        get { return currentTrigerobj; }
        set
        {
            if (currentTrigerobj != value)
            {

            }
            currentTrigerobj = value;
        }
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private string DelString(string s)
    {
        if (s.Length > 0)
        {
            int len = s.Length;
            Debug.Log(len);
            s = s.Substring(0, len - 1);
        }
        return s;
    }
    /// <summary>
    /// 大小写锁定
    /// </summary>
    private void Capslk()
    {
        Debug.Log("切换大小写");
        for (int i = 0; i < mList.Count; i++)
        {
            mList[i].name = ChangeStr(mList[i].name);
            if (!isUp)
            {
                mList[i].GetComponent<MeshRenderer>().materials[0].CopyPropertiesFromMaterial((Material)Resources.Load("Key/" + mList[i].name.ToString() + mList[i].name.ToString(), typeof(Material)));
            }
            if (isUp)
            {
                mList[i].GetComponent<MeshRenderer>().materials[0].CopyPropertiesFromMaterial((Material)Resources.Load("Key/" + mList[i].name.ToString(), typeof(Material)));
            }
            Debug.Log("success");
        }
    }
    /// <summary>
    /// 改变字符的大小写
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private string ChangeStr(string s)
    {
        if (!isUp)
        {
            s = s.ToUpper();
        }
        if (isUp)
        {
            s = s.ToLower();
        }
        return s;
    }
    /// <summary>
    /// 将所有按键放到队列中
    /// </summary>
    public void Init()
    {
        Cha_now = 0;
        try
        {
            GameObject go = GameObject.Find("Key");
            foreach (Transform item in go.transform)
            {
                mList.Add(item.gameObject);
            }
        }
        catch (System.Exception)
        {

        }
        Keyboard.SetActive(false);
    }  

}

