using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NormalContent : MonoBehaviour, IContentEvent
{
    public GamePlayType gamePlayType = GamePlayType.GameChoice1;

    [SerializeField]
    private bool isActive = false;
    public int index = 0;
    public Text numberText;
    public LocalizationContent clickText;

    [Space]
    public UnityEvent clickEvent;
    public UnityEvent failSoundEvent;

    [Space]
    public Image backgroundImg;
    public Sprite[] backgroundImgList;

    [Space]
    [Title("MoleCatch")]
    public Image moleImg;
    public Sprite[] moleImgList;

    [Space]
    [Title("FilpCard")]
    public Image filpCardImg;
    public Sprite[] filpCardImgList;

    [Space]
    [Title("ButtonAction")]
    public string[] buttonActionStrArray;

    void Awake()
    {
        failSoundEvent.AddListener(() => { GameObject.FindWithTag("FailSound").GetComponent<AudioSource>().Play(); });

        numberText.text = "";

        moleImg.enabled = false;
        filpCardImg.enabled = false;

        clickText.gameObject.SetActive(false);
    }

    public void Initialize(GamePlayType type)
    {
        gamePlayType = type;

        clickEvent.RemoveAllListeners();

        switch (type)
        {
            case GamePlayType.GameChoice1:
                clickEvent.AddListener(() => { GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CheckNumber(index, ChoiceAction); });
                break;
            case GamePlayType.GameChoice2:
                clickEvent.AddListener(() => { GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CheckMole(index, ChoiceMoleAction); });
                break;
            case GamePlayType.GameChoice3:
                clickEvent.AddListener(() => { GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CheckFilpCard(index, isActive, ChoiceCardAction); });
                break;
            case GamePlayType.GameChoice4:
                clickEvent.AddListener(() => { GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CheckButtonAction(index, ChoiceButtonAction); });
                break;
            case GamePlayType.GameChoice5:
                clickText.gameObject.SetActive(true);
                clickEvent.AddListener(() => { GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CheckTimingAction(); });

                isActive = true;
                break;
            case GamePlayType.GameChoice6:
                clickEvent.AddListener(() => { GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CheckFingerSnap(); });
                break;
            case GamePlayType.GameChoice7:
                break;
            case GamePlayType.GameChoice8:
                break;
        }
    }

    public void NormalReset(int number)
    {
        index = number;
        numberText.text = index.ToString();

        backgroundImg.sprite = backgroundImgList[0];
        backgroundImg.enabled = true;

        moleImg.enabled = false;

        isActive = true;
    }

    public void MoleReset()
    {
        moleImg.sprite = moleImgList[0];
        moleImg.enabled = false;

        isActive = true;
    }

    public void FilpCardReset(int number)
    {
        filpCardImg.sprite = filpCardImgList[number];
        filpCardImg.enabled = false;
        index = number;

        isActive = true;
    }

    public void ButtonActionReset(int number)
    {
        numberText.text = buttonActionStrArray[number];
        index = number;

        isActive = true;
    }

    public void NormalFirst()
    {
        backgroundImg.sprite = backgroundImgList[1];
    }

    public void SetMole()
    {
        moleImg.enabled = true;
    }


    public void Choice()
    {
        if(isActive)
        {
            clickEvent.Invoke();
        }
    }

    public void ChoiceAction(bool check)
    {
        if(check)
        {
            isActive = false;

            backgroundImg.enabled = false;
            numberText.text = "";
        }
        else
        {
            failSoundEvent.Invoke();
        }
    }

    public void ChoiceMoleAction(bool check)
    {
        if (check)
        {
            isActive = false;

            moleImg.sprite = moleImgList[1];
        }
        else
        {
            failSoundEvent.Invoke();
        }
    }


    public void ChoiceCardAction(int check)
    {
        switch(check)
        {
            case 0:
                isActive = false;

                filpCardImg.enabled = true;
                break;
            case 1:
                isActive = false;

                filpCardImg.enabled = true;

                break;
            case 2:
                isActive = true;

                filpCardImg.enabled = false;

                failSoundEvent.Invoke();
                break;
        }
    }

    public void ChoiceButtonAction(bool check)
    {
        if(check)
        {

        }
        else
        {
            failSoundEvent.Invoke();
        }
    }

    public int GetIndex()
    {
        return index;
    }
}