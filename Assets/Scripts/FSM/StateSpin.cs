using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

namespace FSM
{
    [State("StateSpin")]
    public class StateSpin : FSMState
    {
        public override void Enter()
        {
            Settings.Invoke("StartSpin");
            Settings.Invoke("UIStartEnabled", false);
            Settings.Invoke("UIStopEnabled", false);
        }

        [One(3f)]
        private void EnableStop() => Settings.Invoke("UIStopEnabled", true);

        [Bind("OnStopPressed")]
        private void StopPressed() => Parent.Change("StateStopping");
    }
}