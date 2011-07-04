using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace _PS360Drum
{
    public enum SwitchType
    {
        KeyboardLike,
        Toggle,
        OnOff
    }
    enum GuiLinkerType
    {
        Button,
        ButtonVelocity,
        DPad,
        DPadVelocity,
        DrumPad
    }
    class GuiLinkerTag
    {
        public GuiLinkerTag(GuiLinkerType type, object data)
        {
            this.type = type;
            this.data = data;
        }

        public GuiLinkerType type;
        public object data;
    }
    public class GuiLinker
    {
        public static readonly byte[] allowedBoostValues = new byte[8] { 10, 20, 30, 40, 50, 60, 70, 80 };
        SwitchType[] allowedSwitchTypes = new SwitchType[] { SwitchType.KeyboardLike, SwitchType.OnOff, SwitchType.Toggle };

        NumericUpDown[] m_NupNoteArray = new NumericUpDown[ProDrumController.NUM_PADS + ProDrumController.NUM_BUTTON_STATES + 4];
        NumericUpDown[] m_NupAdvNoteArray = new NumericUpDown[ProDrumController.NUM_PADS + ProDrumController.NUM_BUTTON_STATES + 4];

        NumericUpDown[] m_NupAdvButtonVelArray = new NumericUpDown[ProDrumController.NUM_BUTTON_STATES + 4];
        ComboBox[] m_DdlAdvSwitchStateArray = new ComboBox[ProDrumController.NUM_BUTTON_STATES + 4];

        CheckBox[] m_ChkNoteOnCheck = new CheckBox[ProDrumController.NUM_BUTTON_STATES + 4];

        CheckBox[] m_ChkDrumBoostOn = new CheckBox[ProDrumController.NUM_PADS];
        CheckBox[] m_ChkAdvDrumBoostOn = new CheckBox[ProDrumController.NUM_PADS];

        ComboBox[] m_DdlBoostValue = new ComboBox[ProDrumController.NUM_PADS];
        ComboBox[] m_DdlAdvBoostValue = new ComboBox[ProDrumController.NUM_PADS];

        FrmMain m_Main;

        public GuiLinker(FrmMain main)
        {
            m_Main = main;
        }

        public void AddButton(DrumButton button,
                       NumericUpDown nupNoteNormal, NumericUpDown nupNoteAdv,
                       NumericUpDown nupButtonNoteVelocityAdv, ComboBox ddlSwitchStateAdv,
                       CheckBox chkNoteOnCheck)
        {
            m_NupNoteArray[ProDrumController.NUM_PADS + (byte)button] = nupNoteNormal;
            m_NupAdvNoteArray[ProDrumController.NUM_PADS + (byte)button] = nupNoteAdv;

            m_NupAdvButtonVelArray[(byte)button] = nupButtonNoteVelocityAdv;
            m_DdlAdvSwitchStateArray[(byte)button] = ddlSwitchStateAdv;

            m_ChkNoteOnCheck[(byte)button] = chkNoteOnCheck;


            InitControl((byte)button, GuiLinkerType.Button, nupNoteNormal);
            InitControl((byte)button, GuiLinkerType.ButtonVelocity, nupButtonNoteVelocityAdv);
            InitControl((byte)button, GuiLinkerType.Button, nupNoteAdv);
            InitControl((byte)button, GuiLinkerType.Button, ddlSwitchStateAdv);
            InitControl((byte)button, GuiLinkerType.Button, chkNoteOnCheck);

            InitNumericUpDown(nupNoteNormal);
            InitNumericUpDown(nupNoteAdv);
            InitNumericUpDown(nupButtonNoteVelocityAdv);

            InitDDLSwitchState(ddlSwitchStateAdv);
        }
        public void AddDPad(DrumDPad dpad,
                     NumericUpDown nupNoteNormal, NumericUpDown nupNoteAdv,
                     NumericUpDown nupButtonNoteVelocityAdv, ComboBox ddlSwitchStateAdv,
                     CheckBox chkNoteOnCheck)
        {
            m_NupNoteArray[ProDrumController.NUM_PADS + ProDrumController.NUM_BUTTON_STATES + (byte)dpad / 2] = nupNoteNormal;
            m_NupAdvNoteArray[ProDrumController.NUM_PADS + ProDrumController.NUM_BUTTON_STATES + (byte)dpad / 2] = nupNoteAdv;

            m_NupAdvButtonVelArray[ProDrumController.NUM_BUTTON_STATES + (byte)dpad / 2] = nupButtonNoteVelocityAdv;
            m_DdlAdvSwitchStateArray[ProDrumController.NUM_BUTTON_STATES + (byte)dpad / 2] = ddlSwitchStateAdv;

            m_ChkNoteOnCheck[ProDrumController.NUM_BUTTON_STATES + (byte)dpad / 2] = chkNoteOnCheck;


            InitControl((byte)dpad, GuiLinkerType.DPad, nupNoteNormal);
            InitControl((byte)dpad, GuiLinkerType.DPadVelocity, nupButtonNoteVelocityAdv);
            InitControl((byte)dpad, GuiLinkerType.DPad, nupNoteAdv);
            InitControl((byte)dpad, GuiLinkerType.DPad, ddlSwitchStateAdv);
            InitControl((byte)dpad, GuiLinkerType.DPad, chkNoteOnCheck);

            InitNumericUpDown(nupNoteNormal);
            InitNumericUpDown(nupNoteAdv);
            InitNumericUpDown(nupButtonNoteVelocityAdv);

            InitDDLSwitchState(ddlSwitchStateAdv);
        }
        public void AddDrum(DrumPad pad,
                     NumericUpDown nupNoteNormal, NumericUpDown nupNoteAdv,
                     CheckBox chkBoostOn, CheckBox chkBoostOnAdv,
                     ComboBox ddlBoostValue, ComboBox ddlAdvBoostValue)
        {
            m_NupNoteArray[(byte)pad] = nupNoteNormal;
            m_NupAdvNoteArray[(byte)pad] = nupNoteAdv;

            m_ChkDrumBoostOn[(byte)pad] = chkBoostOn;
            m_ChkAdvDrumBoostOn[(byte)pad] = chkBoostOnAdv;

            m_DdlBoostValue[(byte)pad] = ddlBoostValue;
            m_DdlAdvBoostValue[(byte)pad] = ddlAdvBoostValue;


            InitControl((byte)pad, GuiLinkerType.DrumPad, nupNoteNormal);
            InitControl((byte)pad, GuiLinkerType.DrumPad, nupNoteAdv);
            InitControl((byte)pad, GuiLinkerType.DrumPad, chkBoostOn);
            InitControl((byte)pad, GuiLinkerType.DrumPad, chkBoostOnAdv);
            InitControl((byte)pad, GuiLinkerType.DrumPad, ddlBoostValue);
            InitControl((byte)pad, GuiLinkerType.DrumPad, ddlAdvBoostValue);

            InitNumericUpDown(nupNoteNormal);
            InitNumericUpDown(nupNoteAdv);

            InitDDLBoost(ddlBoostValue);
            InitDDLBoost(ddlAdvBoostValue);

            InitBoostCheckBox(chkBoostOn);
            InitBoostCheckBox(chkBoostOnAdv);
        }

        void InitNumericUpDown(NumericUpDown nup)
        {
            nup.ValueChanged += new EventHandler(nup_ValueChanged);
        }
        void InitBoostCheckBox(CheckBox chk)
        {
            chk.CheckedChanged += new EventHandler(chk_CheckedChanged);
        }

        void InitControl(byte pad, GuiLinkerType type, Control control)
        {
            control.Tag = new GuiLinkerTag(type, pad);
        }

        void InitDDLBoost(ComboBox ddlBoost)
        {
            foreach (byte b in allowedBoostValues)
            {
                ddlBoost.Items.Add(b);
            }
            ddlBoost.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
        }
        void InitDDLSwitchState(ComboBox ddlSwitchState)
        {
            foreach (SwitchType t in allowedSwitchTypes)
            {
                ddlSwitchState.Items.Add(t);
            }
            ddlSwitchState.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
        }

        #region events
        void nup_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown obj = sender as NumericUpDown;
            GuiLinkerTag tag = obj.Tag as GuiLinkerTag;

            Debug.Assert(tag != null, "wrong tag");
            switch (tag.type)
            {
                case GuiLinkerType.DrumPad:
                    SetMidiNote((DrumPad)tag.data, (byte)obj.Value); break;
                case GuiLinkerType.Button:
                    SetMidiNote((DrumButton)tag.data, (byte)obj.Value); break;
                case GuiLinkerType.ButtonVelocity:
                    SetButtonVelocity((DrumButton)tag.data, (byte)obj.Value); break;
                case GuiLinkerType.DPad:
                    SetMidiNote((DrumDPad)tag.data, (byte)obj.Value); break;
                case GuiLinkerType.DPadVelocity:
                    SetButtonVelocity((DrumDPad)tag.data, (byte)obj.Value); break;
                default:
                    Debug.Assert(false, "unkown type"); break;
            }
        }
        void chk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox obj = sender as CheckBox;
            GuiLinkerTag tag = obj.Tag as GuiLinkerTag;

            Debug.Assert(tag != null, "wrong tag");
            switch (tag.type)
            {
                case GuiLinkerType.DrumPad:
                    SetBoostEnabled((DrumPad)tag.data, obj.Checked); break;
                case GuiLinkerType.Button:
                case GuiLinkerType.ButtonVelocity:
                case GuiLinkerType.DPad:
                case GuiLinkerType.DPadVelocity:
                    Debug.Assert(false, "wrong type"); break;
                default:
                    Debug.Assert(false, "unkown type"); break;
            }
        }
        void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox obj = sender as ComboBox;
            GuiLinkerTag tag = obj.Tag as GuiLinkerTag;

            Debug.Assert(tag != null, "wrong tag");
            switch (tag.type)
            {
                case GuiLinkerType.DrumPad:
                    SetBoostValue((DrumPad)tag.data, (byte)obj.SelectedItem); break;
                case GuiLinkerType.Button:
                    SetButtonSwitchType((DrumButton)tag.data, (SwitchType)obj.SelectedItem); break;
                case GuiLinkerType.DPad:
                    SetButtonSwitchType((DrumDPad)tag.data, (SwitchType)obj.SelectedItem); break;

                case GuiLinkerType.DPadVelocity:
                case GuiLinkerType.ButtonVelocity:
                    Debug.Assert(false, "wrong type"); break;
                default:
                    Debug.Assert(false, "unkown type"); break;
            }
        }        
        #endregion

        #region Getters/Setters
        internal void CheckboxButton(DrumButton b, bool check)
        {
            m_ChkNoteOnCheck[(byte)b].Checked = check;
        }
        internal void CheckboxButton(DrumDPad dp, bool check)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            m_ChkNoteOnCheck[ProDrumController.NUM_BUTTON_STATES + (byte)dp / 2].Checked = check;
        }

        internal bool GetButtonChecked(DrumDPad dp)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            return m_ChkNoteOnCheck[ProDrumController.NUM_BUTTON_STATES + (byte)dp / 2].Checked;
        }

        internal byte GetMidiNote(DrumPad pad)
        {
            return (byte)m_NupAdvNoteArray[(byte)pad].Value;
        }
        internal byte GetMidiNote(DrumButton b)
        {
            return (byte)m_NupAdvNoteArray[ProDrumController.NUM_PADS + (byte)b].Value;
        }
        internal byte GetMidiNote(DrumDPad dp)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            return (byte)m_NupAdvNoteArray[ProDrumController.NUM_PADS + ProDrumController.NUM_BUTTON_STATES + (byte)dp / 2].Value;
        }

        internal void SetMidiNote(DrumPad pad, byte note)
        {
            m_NupAdvNoteArray[(byte)pad].Value = note;
            m_NupNoteArray[(byte)pad].Value = note;
        }
        internal void SetMidiNote(DrumButton b, byte note)
        {
            m_NupAdvNoteArray[ProDrumController.NUM_PADS + (byte)b].Value = note;
            m_NupNoteArray[ProDrumController.NUM_PADS + (byte)b].Value = note;
        }
        internal void SetMidiNote(DrumDPad dp, byte note)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            m_NupAdvNoteArray[ProDrumController.NUM_PADS + ProDrumController.NUM_BUTTON_STATES + (byte)dp / 2].Value = note;
            m_NupNoteArray[ProDrumController.NUM_PADS + ProDrumController.NUM_BUTTON_STATES + (byte)dp / 2].Value = note;
        }

        internal byte GetBoost(DrumPad pad)
        {
            if (m_Main.InvokeRequired)
            {
                return (byte)m_Main.Invoke(new Byte_DrumPadDelegate(GetBoost), new object[] { pad });
            }
            else
            {
                return (byte)m_DdlAdvBoostValue[(byte)pad].SelectedItem;
            }
        }
        internal bool GetBoostEnabled(DrumPad pad)
        {
            if (m_Main.InvokeRequired)
            {
                return (bool)m_Main.Invoke(new Bool_DrumPadDelegate(GetBoostEnabled), new object[] { pad });
            }
            else
            {
                return m_ChkAdvDrumBoostOn[(byte)pad].Checked;
            }
        }

        internal void SetBoostValue(DrumPad pad, byte value)
        {
            SetSelectIndex(ref m_DdlAdvBoostValue[(byte)pad], value);
            SetSelectIndex(ref m_DdlBoostValue[(byte)pad], value);
        }
        internal void SetBoostEnabled(DrumPad pad, bool enabled)
        {
            m_ChkDrumBoostOn[(byte)pad].Checked = enabled;
            m_ChkAdvDrumBoostOn[(byte)pad].Checked = enabled;
        }

        internal void SetButtonVelocity(DrumButton drumButton, byte vel)
        {
            m_NupAdvButtonVelArray[(byte)drumButton].Value = vel;
        }
        internal void SetButtonVelocity(DrumDPad drumDPad, byte vel)
        {
            Debug.Assert((byte)drumDPad % 2 == 0, "only left, up, right, down is allowed here");
            m_NupAdvButtonVelArray[ProDrumController.NUM_BUTTON_STATES + (byte)drumDPad / 2].Value = vel;
        }

        internal void SetButtonSwitchType(DrumButton b, SwitchType type)
        {
            SetSelectIndex(ref m_DdlAdvSwitchStateArray[(byte)b], type);
        }
        internal void SetButtonSwitchType(DrumDPad dp, SwitchType type)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            SetSelectIndex(ref m_DdlAdvSwitchStateArray[ProDrumController.NUM_BUTTON_STATES + (byte)dp / 2], type);
        }

        internal byte GetButtonVelocity(DrumDPad drumDPad)
        {
            Debug.Assert((byte)drumDPad % 2 == 0, "only left, up, right, down is allowed here");
            return (byte)m_NupAdvButtonVelArray[ProDrumController.NUM_BUTTON_STATES + (byte)drumDPad / 2].Value;
        }
        internal byte GetButtonVelocity(DrumButton drumButton)
        {
            return (byte)m_NupAdvButtonVelArray[(byte)drumButton].Value;
        }

        internal SwitchType GetButtonSwitchType(DrumDPad drumDPad)
        {
            Debug.Assert((byte)drumDPad % 2 == 0, "only left, up, right, down is allowed here");
            return (SwitchType)m_DdlAdvSwitchStateArray[ProDrumController.NUM_BUTTON_STATES + (byte)drumDPad / 2].SelectedItem;
        }
        internal SwitchType GetButtonSwitchType(DrumButton drumButton)
        {
            return (SwitchType)m_DdlAdvSwitchStateArray[(byte)drumButton].SelectedItem;
        }
        #endregion

        public void SetSelectIndex(ref ComboBox currentCombobox, object value)
        {
            int index = 0;
            for (int i = 0; i < currentCombobox.Items.Count; ++i)
            {
                if (currentCombobox.Items[i].ToString() == value.ToString())
                {
                    index = i;
                }
            }
            currentCombobox.SelectedIndex = index;
        }
    }
}
