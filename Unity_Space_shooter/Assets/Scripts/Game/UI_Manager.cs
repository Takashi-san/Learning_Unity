using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameoverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _hpImage;
    [SerializeField]
    private Sprite[] _hpSprites;

    private bool _isGameover = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _hpImage.sprite = _hpSprites[3];
        _gameoverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // This should be in a GameManager object a part (recomended by the course)
        if(_isGameover)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(LoadAsyncScence());
            }
        }
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateHP(int hp)
    {
        _hpImage.sprite = _hpSprites[hp];
    }

    public void GameOver()
    {
        StartCoroutine("GameOver_Flicker");
        _restartText.gameObject.SetActive(true);
        _isGameover = true;
    }

    IEnumerator GameOver_Flicker()
    {
        bool flick = false;
        while(true)
        {
            flick = !flick;
            _gameoverText.gameObject.SetActive(flick);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator LoadAsyncScence()
    {
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1); //Need to adjust in File >> Build Settings >> Scenes in Build

        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
