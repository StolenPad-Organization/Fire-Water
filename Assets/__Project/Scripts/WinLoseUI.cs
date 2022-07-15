using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    [Space]
    [SerializeField] private CanvasGroup winGroup;
    [SerializeField] private CanvasGroup loseGroup;
    [SerializeField] private Transform winPanel;
    [SerializeField] private Transform losePanel;
    [SerializeField] private Button winButton;
    [SerializeField] private Button loseButton;
    bool gameEnded;

    private void OnEnable ()
    {
        EventManager.TriggerWin += WinUI;
        EventManager.TriggerLose += LoseUI;

        winButton.onClick.AddListener(LoadNextLevel);
        loseButton.onClick.AddListener(RestartLevel);
    }

    private void OnDisable ()
    {
        EventManager.TriggerWin -= WinUI;
        EventManager.TriggerLose -= LoseUI;

        winButton.onClick.RemoveAllListeners();
        loseButton.onClick.RemoveAllListeners();
    }

    public void WinUI ()
    {
        if (gameEnded) return;
        gameEnded = true;
        ShowPanel(winGroup, winPanel);
    }

    public void LoseUI ()
    {
        if (gameEnded) return;
        gameEnded = true;
        ShowPanel(loseGroup, losePanel);
    }

    private void ShowPanel (CanvasGroup group, Transform panel)
    {
        group.alpha = 0f;
        group.gameObject.SetActive(true);
        panel.localScale = Vector3.zero;
        group.DOFade(1f, 0.2f);
        panel.gameObject.SetActive(true);
        panel.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.4f).OnComplete(() => panel.DOScale(Vector3.one, 0.2f));
    }

    private void LoadNextLevel ()
    {
        DOTween.KillAll();
        EventManager.loadNextScene?.Invoke();
    }

    private void RestartLevel ()
    {
        DOTween.KillAll();
        EventManager.loadSameScene?.Invoke();
    }
}
