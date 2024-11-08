using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class LoadingManager : MonoBehaviour {
    public static LoadingManager instance;

    private void Awake() {
        instance = this;
        DontDestroyOnLoad (this.gameObject);
    }

    private string activeLoadingCode;

    // Fungsi utama untuk menampilkan loading berdasarkan kode
    public void ShowLoading(string loadingCode) {
        activeLoadingCode = loadingCode;
        if (loadingCode == "Loading_1") {
            ActivateLoading1();
        } else if (loadingCode == "Loading_2") {
            ActivateLoading2();
        }
    }

    // Fungsi utama untuk menyembunyikan loading berdasarkan kode
    public void HideLoading(string loadingCode) {
        activeLoadingCode = loadingCode;
        if (loadingCode == "Loading_1") {
            DeactivateLoading1();
        } else if (loadingCode == "Loading_2") {
            DeactivateLoading2();
        }
    }

    #region Loading_1
    [SerializeField] private Image loading1Image;

    private void ActivateLoading1() {
        loading1Image.gameObject.SetActive(true);
    }

    private void DeactivateLoading1() {
        loading1Image.gameObject.SetActive(false);
    }
    #endregion

    #region Loading_2
    [SerializeField] private Image loading2Image;

    private void ActivateLoading2() {
        loading2Image.gameObject.SetActive(true);
    }

    private void DeactivateLoading2() {
        loading2Image.gameObject.SetActive(false);
    }
    #endregion

    #region Invisible Loading Management
    public bool isInvisibleLoadingActive = false;
    private UnityAction onInvisibleLoadingCompleted;
    [SerializeField] private List<string> invisibleLoadingQueue = new List<string>();
    private int currentLoadingProgress;
    private string invisibleLoadingType;

    // DataGameManager
    public void StartInvisibleLoading(string loadingType, UnityAction onCompleteCallback, int progress) {
        if (!isInvisibleLoadingActive) {
            isInvisibleLoadingActive = true;
            currentLoadingProgress = progress;
            onInvisibleLoadingCompleted = onCompleteCallback;
            invisibleLoadingType = loadingType;

            if (loadingType == "Loading_1" || loadingType == "Loading_2") {
                ShowLoading(loadingType);
            }
        } else {
            Debug.LogError("Loading overlap detected. Check loading structure.");
        }
    }

    public void AddInvisibleLoading(string loadingCode) {
        if (!lateInvisibleLoadingQueue.Contains(loadingCode)) {
            invisibleLoadingQueue.Add(loadingCode);
        } else {
            RemoveLateInvisibleLoading(loadingCode);
        }
    }

    public void RemoveInvisibleLoading(string loadingCode) {
        if (invisibleLoadingQueue.Contains(loadingCode)) {
            invisibleLoadingQueue.Remove(loadingCode);
        } else {
            AddLateInvisibleLoading(loadingCode);
        }
        
        if (isInvisibleLoadingActive) {
            CheckInvisibleLoadingCompletion();
        }
    }

    private void CheckInvisibleLoadingCompletion() {
        if (currentLoadingProgress > 0) {
            currentLoadingProgress--;
        }

        if (invisibleLoadingQueue.Count == 0 && currentLoadingProgress == 0) {
            CompleteInvisibleLoading();
        }
    }

    private void CompleteInvisibleLoading() {
        isInvisibleLoadingActive = false;

        if (invisibleLoadingType == "Loading_1") {
            HideLoading(invisibleLoadingType);
        }
        onInvisibleLoadingCompleted?.Invoke();
    }

    [SerializeField] private List<string> lateInvisibleLoadingQueue = new List<string>();

    private void AddLateInvisibleLoading(string loadingCode) {
        lateInvisibleLoadingQueue.Add(loadingCode);
    }

    private void RemoveLateInvisibleLoading(string loadingCode) {
        lateInvisibleLoadingQueue.Remove(loadingCode);
    }
    #endregion
}