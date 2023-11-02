using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class texting : MonoBehaviour
{

    public GameObject text;
    public GameObject text1;
    public GameObject text2;
    public Text T1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("showText");
    }

    private IEnumerator showText()
    {
        yield return new WaitForSeconds(3f);
        text.SetActive(true);
        StartCoroutine("showText2");

    }

    private IEnumerator showText2()
    {
        yield return new WaitForSeconds(3f);
        T1.color = Color.gray;
        text1.SetActive(true);
        text2.SetActive(true);
        StartCoroutine("showText3");

    }

    private IEnumerator showText3()
    {
        yield return new WaitForSeconds(2f);
        text.transform.position = new Vector3(10, 10, 10);
    }

}
