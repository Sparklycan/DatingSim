// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;

namespace Fungus
{
    /// <summary>
    /// Displays a timer bar and executes a target block if the player fails to select a menu option in time.
    /// </summary>
    [CommandInfo("Narrative", 
                 "Menu Timer", 
                 "Displays a timer bar and executes a target block if the player fails to select a menu option in time.")]
    [AddComponentMenu("")]
    [ExecuteInEditMode]
    public class MenuTimer : Command, IBlockCaller
    {
        [Tooltip("Length of time to display the timer for")]
        [SerializeField] protected FloatData _duration = new FloatData(1);

        [FormerlySerializedAs("targetSequence")]
        [Tooltip("Block to execute when the timer expires")]
        [SerializeField] protected Block targetBlock;

        public event System.Action<Command, Block> onCall;

        #region Public members

        public override void OnEnter()
        {
            var menuDialog = MenuDialog.GetMenuDialog();

            if (menuDialog != null &&
                targetBlock != null)
            {
                menuDialog.ShowTimer(_duration.Value, targetBlock, targetBlock => { onCall?.Invoke(this, targetBlock); });
            }

            Continue();
        }

        public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
        {
            if (targetBlock != null)
            {
                connectedBlocks.Add(targetBlock);
            }       
        }

        public override string GetSummary()
        {
            if (targetBlock == null)
            {
                return "Error: No target block selected";
            }

            return GetCallFraction() + targetBlock.BlockName;
        }

        public override Color GetButtonColor()
        {
            return new Color32(184, 210, 235, 255);
        }

        public override bool HasReference(Variable variable)
        {
            return _duration.floatRef == variable ||
                base.HasReference(variable);
        }

        public bool MayCallBlock(Block block)
        {
            return block == targetBlock;
        }

        private string GetCallFraction()
        {
            Flowchart flowchart = GetFlowchart();
            if (flowchart == null)
                return "";

            FlowchartStatistics statistics = flowchart.GetComponent<FlowchartStatistics>();
            if (statistics == null)
                return "";

            int calls = statistics.CallCount(ItemId);
            int total = statistics.BlockCount(ParentBlock.BlockName);
            if (calls < 0 || total < 0 || calls > total)
                return "";

            float frac = (float)calls / (float)total;
            return frac.ToString("P") + "\t";
        }

        #endregion

        #region Backwards compatibility

        [HideInInspector] [FormerlySerializedAs("duration")] public float durationOLD;

        protected virtual void OnEnable()
        {
            if (durationOLD != default(float))
            {
                _duration.Value = durationOLD;
                durationOLD = default(float);
            }
        }

        #endregion
    }
}