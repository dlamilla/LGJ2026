using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageControl : MonoBehaviour
{
    private bool _active = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int ID = PlayerPrefs.GetInt("Language", 0);
        ChangeLocaleID(ID);
    }

    public void ChangeLocaleID(int localID)
    {
        if (_active) return;
        StartCoroutine(SetLocale(localID));
    }

    private IEnumerator SetLocale(int localeID)
    {
        _active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt("Language", localeID);
        _active = false;
    }
}
