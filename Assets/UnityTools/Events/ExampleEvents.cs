using UnityTools.Events;

public class ExampleEvents : CustomEvents
{
    #region Singleton
    private static ExampleEvents instance;

    public static ExampleEvents Instance
    {
        get
        {
            if (instance == null)
                instance = new ExampleEvents();

            return instance;
        }
    }

    protected ExampleEvents() { }
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
