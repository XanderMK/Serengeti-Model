using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public TMP_Text timeScaleText;
    public DiseaseManager diseaseManager;

    public TMP_InputField initialBuffaloText;
    public TMP_InputField initialWildebeestText;
    public TMP_InputField initialTreesText;

    float timeScale = 1f;

    private void Start()
    {
        initialBuffaloText.text = PlayerPrefs.GetInt("initialBuffalo").ToString();
        initialWildebeestText.text = PlayerPrefs.GetInt("initialWildebeest").ToString();
        initialTreesText.text = PlayerPrefs.GetInt("initialTrees").ToString();
    }

    public void UpdateTimeScale(float scale)
    {
        timeScale = scale;
        timeScaleText.text = scale.ToString("0.0");
    }

    public void UpdateVaccination(bool vaccinated)
    {
        diseaseManager.vaccinated = vaccinated;
    }

    public void UpdateInitialBuffalo(string initialBuffalo)
    {
        PlayerPrefs.SetInt("initialBuffalo", System.Convert.ToInt32(initialBuffalo));
    }

    public void UpdateInitialWildebeest(string initialWildebeest)
    {
        PlayerPrefs.SetInt("initialWildebeest", System.Convert.ToInt32(initialWildebeest));
    }

    public void UpdateInitialTrees(string initialTrees)
    {
        PlayerPrefs.SetInt("initialTrees", System.Convert.ToInt32(initialTrees));
    }

    public void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void OnDisable()
    {
        Time.timeScale = timeScale;
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }
}
