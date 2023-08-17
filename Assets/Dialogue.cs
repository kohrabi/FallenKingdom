using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public Material InvertColor;
    bool ending = false;
    public AudioClip typeSound;
    AudioSource source;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        InvertColor.SetFloat("_Threshold", 0);
        source = GetComponent<AudioSource>();
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!ending && Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
        if (ending)
        {
            float threshold = InvertColor.GetFloat("_Threshold");
            float InvertColorThreshold = 0.7f;
            InvertColor.SetFloat("_Threshold", Mathf.Lerp(threshold, InvertColorThreshold, 0.04f));
            if (threshold >= InvertColorThreshold - 0.04f)
                StartCoroutine(waitSceneLoad());
        }
    }

    IEnumerator waitSceneLoad()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");

    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            source.PlayOneShot(typeSound);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            ending = true;
        }
    }
}