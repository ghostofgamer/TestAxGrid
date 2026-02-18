using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

namespace FSM
{
    [State("StateStopping")]
    public class StateStopping : FSMState
    {
        public override void Enter()
        {
            Settings.Invoke("StopingSpin");
            Settings.Invoke("UIStopEnabled", false);
        }

        [Bind("SpinFinished")]
        private void Finished() => Parent.Change("StateIdle");
    }
}