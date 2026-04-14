using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueTyper : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;

    public float textSpeed = 0.05f;   // speed of typing
    public float lineDelay = 5f;      // time before next line

    private int index;

    void Start()
    {
        textComponent.text = "";
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        index = 0;

        while (index < lines.Length)
        {
            yield return StartCoroutine(TypeLine());
            yield return new WaitForSeconds(lineDelay);
            index++;
        }
        
    }

    IEnumerator TypeLine()
    {
        textComponent.text = "";

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
       
    }
   

}