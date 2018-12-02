using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс представляет собой игровой эвент
/// </summary>
[CreateAssetMenu(fileName = "NewEvent", menuName = "New Event")]
public class Event : ScriptableObject
{
    /// <summary>
    /// Название эвента
    /// </summary>
    [SerializeField]
    public string Name;

    /// <summary>
    /// Описание эвента
    /// </summary>
    [SerializeField]
    public string Description;

    /// <summary>
    /// Требование для эвенту
    /// </summary>
    [SerializeField]
    public EventRequarments eventRequarments = new EventRequarments();

    /// <summary>
    /// Варианты выбора
    /// </summary>
    [SerializeField]
    public EventVariant[] eventVariants;

    /// <summary>
    /// Список эвентов, которые будут активированны, вне зависимости от выбора
    /// </summary>
    [SerializeField]
    public Event[] eventToActivate;

    /// <summary>
    /// Флаг повторения эвента (при false эвент отключит сам себя)
    /// </summary>
    [SerializeField]
    public bool isRepeat = true;

    /// <summary>
    /// Статус активности эвента
    /// </summary>
    [SerializeField]
    public bool isActive = true;

    /// <summary>
    /// Перезарядка эвента в циклах
    /// </summary>
    [SerializeField]
    public int coolDown = 0;

}

/// <summary>
/// Класс требований для эвента
/// </summary>
[System.Serializable]
public class EventRequarments
{
    /// <summary>
    /// Требования к населению (0 - нет требования)
    /// </summary>
    public int populationMin = 0;

    /// <summary>
    /// Требования к населению (0 - нет требования)
    /// </summary>
    public int populationMax = 0;

    /// <summary>
    /// Требования к богатству (0 - нет требования)
    /// </summary>
    public int moneyMin = 0;

    /// <summary>
    /// Требования к богатству (0 - нет требования)
    /// </summary>
    public int moneyMax = 0;

    /// <summary>
    /// Требования к страху (0 - нет требования)
    /// </summary>
    public int fearMin = 0;

    /// <summary>
    /// Требования к страху (0 - нет требования)
    /// </summary>
    public int fearMax = 0;

    /// <summary>
    /// Требования к силе первого бога (0 - нет требования)
    /// </summary>
    public int god1Min = 0;

    /// <summary>
    /// Требования к силе первого бога (0 - нет требования)
    /// </summary>
    public int god1Max = 0;

    /// <summary>
    /// Требования к силе второго бога (0 - нет требования)
    /// </summary>
    public int god2Min = 0;

    /// <summary>
    /// Требования к силе второго бога (0 - нет требования)
    /// </summary>
    public int god2Max = 0;

    /// <summary>
    /// Требования к силе третьего бога (0 - нет требования)
    /// </summary>
    public int god3Min = 0;

    /// <summary>
    /// Требования к силе третьего бога (0 - нет требования)
    /// </summary>
    public int god3Max = 0;
}

/// <summary>
/// Класс вариантов выбора
/// </summary>
[System.Serializable]
public class EventVariant
{
    /// <summary>
    /// Текст варианта
    /// </summary>
    public string text = "";

    /// <summary>
    /// Требования для варианта
    /// </summary>
    public EventRequarments variantRequarments = new EventRequarments();

    /// <summary>
    /// Результат эвента при текущем выборе
    /// </summary>
    public EventResult eventResult;
}

/// <summary>
/// Класс результата эвента
/// </summary>
[System.Serializable]
public class EventResult
{
    /// <summary>
    /// Изменение населения
    /// </summary>
    public int populationChange = 0;

    /// <summary>
    /// Изменение богатства
    /// </summary>
    public int moneyChange = 0;

    /// <summary>
    /// Изменение страха
    /// </summary>
    public int fearChange = 0;

    /// <summary>
    /// Изменение силs первого бога
    /// </summary>
    public int god1Change = 0;

    /// <summary>
    /// Изменение силы второго бога
    /// </summary>
    public int god2Change = 0;

    /// <summary>
    /// Изменение силы третьего бога
    /// </summary>
    public int god3Change = 0;

    /// <summary>
    /// Статусы, которые наложит эвент при текущем выборе
    /// </summary>
    public EventStatus[] eventStatus;

    /// <summary>
    /// Список эвентов, которые будут активированны, в зависимости от текущего выбора
    /// </summary>
    [SerializeField]
    public Event[] eventToActivate;

    /// <summary>
    /// Текст результата
    /// </summary>
    [SerializeField]
    public string resultText = "";
}