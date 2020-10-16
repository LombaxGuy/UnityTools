using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : CustomEvents
{
    #region Singleton
    private static GameEvents instance;

    public static GameEvents Instance
    {
        get
        {
            if (instance == null)
                instance = new GameEvents();

            return instance;
        }
    }

    protected GameEvents() { }
    #endregion

    // If this is false, each event is displayed in the console when they are raised
    private static readonly bool silentEvents = false;

    #region Events
    public event EventHandler ExampleEventOne;
    public event EventHandler<int> ExampleEventTwo;
    #endregion

    #region Public event raisers
    public void RaiseExampleEventOne()
    {
        RaiseEvent(GetType().Name, nameof(ExampleEventOne), silentEvents, ExampleEventOne);
    }

    public void RaiseExampleEventTwo(int value)
    {
        RaiseEvent(GetType().Name, nameof(ExampleEventTwo), silentEvents, ExampleEventTwo, value);
    }
    #endregion
}
