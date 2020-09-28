using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSequenceGame : MonoBehaviour
{
    public Transform alphabet;
    public Vector3 pivot;
    public float letterOffset;
    bool vertical = false;
    public Vector3[] positions;
    public string[] wordStrings;
    public string[] clues;
    float targetRotation;
    int step = 0;
    List<WordToFind> words;
    List<Transform> foundWords;

    struct WordToFind
    {
        public Vector3 position;
        public string word;
        public string clue;

        public WordToFind(Vector3 pos, string wordString, string clueString)
        {
            position = pos;
            word = wordString;
            clue = clueString;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        words = new List<WordToFind>();
        foundWords = new List<Transform> ();
        for(int i = 0; i < positions.Length; i++)
        {
            words.Add(new WordToFind(positions[i], wordStrings[i], clues[i]));
        }
        GenerateWord(words[0]);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < step; i++)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Camera.main.transform.position - foundWords[i].position);
            foundWords[i].rotation = targetRotation;
            foundWords[i].Rotate(Vector3.up, 180);

        }
        Debug.Log(Camera.main.transform.rotation.eulerAngles.y);
        if(Mathf.Abs(targetRotation - Camera.main.transform.rotation.eulerAngles.y) < 3)
        {
            step++;
            GenerateWord(words[step]);
        }
    }

    void GenerateWord(WordToFind nextWord)
    {
        GameObject wordWrapper = new GameObject();
        wordWrapper.transform.position = pivot;
        char[] letters = nextWord.word.ToCharArray();
        for(int i = 0;  i < letters.Length;  i++) {
            Transform newLetterObj = DuplicateLetterModel(letters[i]);
            newLetterObj.transform.parent = wordWrapper.transform;
                if(vertical)
                {
                    newLetterObj.transform.localPosition = new Vector3(0, i * letterOffset, Random.Range(-4,4));
                }
                else
                {
                    newLetterObj.transform.localPosition = new Vector3(i * letterOffset, 0, Random.Range(-4,4));
                }
            newLetterObj.gameObject.AddComponent<LetterBehavior>();

        }
        targetRotation = Random.Range(0, 360);
        wordWrapper.transform.position = nextWord.position;
        wordWrapper.transform.Rotate(0, targetRotation, 0);
        foundWords.Add(wordWrapper.transform);

    }

    Transform DuplicateLetterModel(char letter)
    {
        Transform[] children = alphabet.transform.GetComponentsInChildren<Transform>();
        foreach(Transform child in children) {
            if(child.name == letter.ToString())
            {
                Debug.Log("creating copy of " + letter);
                return GameObject.Instantiate(child.gameObject).transform;
            }
        }
        return Camera.main.transform;
     }
}
