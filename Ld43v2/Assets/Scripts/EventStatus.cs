using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс представляет собой статус от эвента
/// </summary>
[CreateAssetMenu(fileName = "NewEventStatus", menuName = "New Event Status")]
public class EventStatus : ScriptableObject
{
    /// <summary>
    /// Описание статуса
    /// </summary>
    [SerializeField]
    public string Description = "";

    /// <summary>
    /// Длительность статуса в циклах
    /// </summary>
    [SerializeField]
    public uint duration = 1;

    /// <summary>
    /// Изменение населения
    /// </summary>
    [SerializeField]
    public int populationChangeInCycle = 0;

    /// <summary>
    /// Изменение богатства
    /// </summary>
    [SerializeField]
    public int moneyChangeInCycle = 0;

    /// <summary>
    /// Изменение страха
    /// </summary>
    [SerializeField]
    public int fearChangeInCycle = 0;

    /// <summary>
    /// Изменение силs первого бога
    /// </summary>
    [SerializeField]
    public int god1ChangeInCycle = 0;

    /// <summary>
    /// Изменение силы второго бога
    /// </summary>
    [SerializeField]
    public int god2ChangeInCycle = 0;

    /// <summary>
    /// Изменение силы третьего бога
    /// </summary>
    [SerializeField]
    public int god3ChangeInCycle = 0;
}
