using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;
using AutumnFramework;
[Bean]
public class Localization : MonoBehaviour
{
    AsyncOperationHandle m_InitializeOperation;
    public Locale _chineseLocale;
    public Locale _englishLocale;

    void Start()
    {
        // SelectedLocaleAsync将确保区域设置已经初始化，并且已经选择了一个区域设置。
        m_InitializeOperation = LocalizationSettings.SelectedLocaleAsync;
        if (m_InitializeOperation.IsDone)
        {
            InitializeCompleted(m_InitializeOperation);
        }
        else
        {
            m_InitializeOperation.Completed += InitializeCompleted;
        }
    }

    void InitializeCompleted(AsyncOperationHandle obj)
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;
        for (int i = 0; i < locales.Count; ++i)
        {
            var locale = locales[i];
            if (locale.LocaleName == "Chinese (Simplified) (zh-Hans)")
            {
                _chineseLocale = locale;
            }
            else if (locale.LocaleName == "English (en)")
            {
                _englishLocale = locale;
            }
        }
    }
}


