using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

[State("StateIdle")]
public class StateIdle : FSMState
{
    public override void Enter()
    {
        Settings.Invoke("UIStartEnabled", true);
        Settings.Invoke("UIStopEnabled", false);
    }

    [Bind("OnStartPressed")]
    private void StartPressed() => Parent.Change("StateSpin");
}