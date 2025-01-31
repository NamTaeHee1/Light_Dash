using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;


public class AcadeManager : MonoBehaviour
{
    public bool isAcadeOn = false, isClickAcadeButton = false;
    bool isReadyShowNextStage = false;
    bool isClickFirstArrowButton = false, isClickSecondArrowButton = false;

    [SerializeField] Vector3 MovingPosition = new Vector3(5.62f, 0, -10);

    Transform CameraTransform;

    [SerializeField] public int AcadeLevel = 0;

    [SerializeField] GameObject[] PowerSocketLines;
    [SerializeField] GameObject[] PowerSocketLineButtons;
    [SerializeField] GameObject RightPowerSocketImage;

    [SerializeField] Button[] Buttons;
    [SerializeField] Button BackToMain;
    [SerializeField] Button[] PositionMoveButtons;

    void Start()
    {
        CameraTransform = Camera.main.GetComponent<Transform>();
    }

    void Update()
    {
        if (isAcadeOn && !isClickAcadeButton)
        {
            CheckShouldShowArrowButton();
            CameraTransform.DOMove(MovingPosition, 0.5f);

            StartCoroutine(WaitShowNextStage());
            BackToMain.interactable = false;
            if (isReadyShowNextStage)
            {
                if(AcadeLevel == 3)
                    {
                    if (isClickFirstArrowButton)
                        {
                        StartCoroutine(ShowArrowButtonAfterStage());
                        }
                    } 
                else if(AcadeLevel == 6)
                    {
                    if (isClickSecondArrowButton)
                        {
                        StartCoroutine(ShowArrowButtonAfterStage());
                        }
                    }
                else
                    {
                    if (AcadeLevel == 8)
                        RightPowerSocketImage.SetActive(true);
                    PowerSocketLines[AcadeLevel].SetActive(true);
                    PowerSocketLineButtons[AcadeLevel].SetActive(true);
                    isReadyShowNextStage = false;
                    }
            }
        }
    }

    void CheckShouldShowArrowButton()
    {
        if (AcadeLevel >= 6)
        {
            PositionMoveButtons[2].gameObject.SetActive(true);
            PositionMoveButtons[3].gameObject.SetActive(true);
        }
        else if (AcadeLevel >= 3)
        {
            PositionMoveButtons[0].gameObject.SetActive(true);
            PositionMoveButtons[1].gameObject.SetActive(true);
        }
    }

    public void ClickBackToMain()
    {
        MovingPosition = new Vector3(0, 0, -10);
        for (int i = 0; i < 4; i++)
            Buttons[i].interactable = true;
    }

    public void ClickAcade()
    {
        isAcadeOn = true;
        MovingPosition = new Vector3(5.62f, 0, -10);
    }

    public void ButtonMovePosition()
    {
        string ClickButtonName = EventSystem.current.currentSelectedGameObject.name;

        switch(ClickButtonName)
        {
            case "PositionOneToPositionTwo":
                MovingPosition = new Vector3(11.5f, 0, -10);
                isClickFirstArrowButton = true;
                break;
            case "PositionTwoToPositionOne":
                MovingPosition = new Vector3(5.62f, 0, -10);
                break;
            case "PositionTwoToPositionThree":
                MovingPosition = new Vector3(17.4f, 0, -10);
                isClickSecondArrowButton = true;
                break;
            case "PositionThreeToPositionTwo":
                MovingPosition = new Vector3(11.5f, 0, -10);
                break;
        }
    }

    IEnumerator WaitShowNextStage()
    {
        yield return new WaitForSeconds(1.0f);
        BackToMain.interactable = true;
        isReadyShowNextStage = true;
    }

    IEnumerator ShowArrowButtonAfterStage()
    {
        yield return new WaitForSeconds(0.5f);
        PowerSocketLines[AcadeLevel].SetActive(true);
        PowerSocketLineButtons[AcadeLevel].SetActive(true);
        isReadyShowNextStage = false;
    }

    public void ClickPowerSocketLineButton()
    {
        GameObject SelectGameObject = EventSystem.current.currentSelectedGameObject;
        Destroy(SelectGameObject);
        isClickAcadeButton = true;
        StartCoroutine(GoToAcadeScene(SelectGameObject.GetComponent<PowerSocketLineButtonInfo>().StageButtonText.text, Camera.main.transform.position.x));
    }

    IEnumerator GoToAcadeScene(string CurrentAcadeLevel, float MainCameraX)
    {
        AcadeSceneManager.AcadeLevel = CurrentAcadeLevel;
        AcadeSceneManager.MainSceneCameraX = MainCameraX;
        FadeManager.instance.FadeOut();
        yield return new WaitForSeconds(0.45f);
        LoadingManager.LoadScene("AcadeScene");
    }
}
