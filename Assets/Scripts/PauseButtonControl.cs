using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseButtonControl : MonoBehaviour
{
    [SerializeField] private Animator PauseButtonAnim;
    [SerializeField] private GameObject PauseButtonMovement, PauseImage, PlayImage;
    [SerializeField] private Image[] ButtonImages;
    [SerializeField] private Image PauseButtonPanel;
    [SerializeField] private Button PauseButton;
    bool isON = false, isPause = false;
    private float AlphaThreshold = 0.1f;

    private void Start()
    {
        for (int i = 0; i < ButtonImages.Length; i++)
            ButtonImages[i].alphaHitTestMinimumThreshold = AlphaThreshold;
    }

    public void GameStart()
    {
        PauseButtonMovement.GetComponent<RectTransform>().DOAnchorPosY(-955.3f, 1.0f).SetEase(Ease.OutBack);
        StartCoroutine(InteractableOnPauseButton());
    }

    IEnumerator InteractableOnPauseButton()
    {
        yield return new WaitForSeconds(1.0f);
        PauseButton.interactable = true;
    }

    public void StopButtonClick()
    {
        Debug.Log("Ŭ��");
        isON = !isON;
        isPause = !isPause;
        PauseImage.SetActive(isON ? false : true);
        PlayImage.SetActive(isON ? true : false);
        PauseButtonAnim.SetBool("isON", isON);
        StartCoroutine(CountDown(isPause));
        PauseButtonPanel.color = new Color(0, 0, 0, isPause ? 0.3f : 0);
    }

    IEnumerator CountDown(bool isPause)
    {
        if(!isPause)
        {
            for(int i = 3; i > 0; i--)
            {
                Debug.Log(i);
                yield return new WaitForSeconds(1.0f);
            }
        }
        Time.timeScale = isPause ? 0 : 1;
    }

    public void ReStartButtonClick()
    {
        Debug.Log("Restart");
    }

    public void SettingButtonClick()
    {
        Debug.Log("Setting");
    }

    public void QuitButtonClick()
    {
        Debug.Log("Quit");
    }
}
