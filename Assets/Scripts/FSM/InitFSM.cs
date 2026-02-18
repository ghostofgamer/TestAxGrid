using AxGrid;
using AxGrid.Base;
using UnityEngine;

namespace FSM
{
    public class InitFSM : MonoBehaviourExt
    {
        [OnAwake]
        private void Create()
        {
            Settings.Fsm = new AxGrid.FSM.FSM();
            Settings.Fsm.Add(new StateIdle());
            Settings.Fsm.Add(new StateSpin());
            Settings.Fsm.Add(new StateStopping());
        }

        [OnStart]
        private void StartFsm() => Settings.Fsm.Start("StateIdle");

        [OnUpdate]
        private void UpdateFsm() => Settings.Fsm?.Update(Time.deltaTime);
    }
}