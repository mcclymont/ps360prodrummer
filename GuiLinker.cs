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
    public enum GuiDrumPad : byte
    {
        RedTom = 0,
        YellowTom = 1,
        BlueTom = 2,
        GreenTom = 3,
        YellowCymbal = 4,
        BlueCymbal = 5,
        GreenCymbal = 6,
        PedalLeft = 7,
        PedalRight = 8
    };
    public enum GuiDrumDPad : byte
    {
        Up = 0,
        RightUp = 1,
        Right = 2,
        RightDown = 3,
        Down = 4,
        LeftDown = 5,
        Left = 6,
        LeftUp = 7,
        None = 8
    }
    public enum GuiDrumButton : byte
    {
        Select,
        Start,
        BigButton,
        Triangle,
        Rectangle,
        Circle,
        X
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
        private const int NUM_PADS = 9;
        private const int NUM_BUTTON_STATES = 7;

        SwitchType[] allowedSwitchTypes = new SwitchType[] { SwitchType.KeyboardLike, SwitchType.OnOff, SwitchType.Toggle };

        NumericUpDown[] m_NupNoteArray = new NumericUpDown[NUM_PADS + NUM_BUTTON_STATES + 4];
        NumericUpDown[] m_NupAdvNoteArray = new NumericUpDown[NUM_PADS + NUM_BUTTON_STATES + 4];

        NumericUpDown[] m_NupAdvButtonVelArray = new NumericUpDown[NUM_BUTTON_STATES + 4];
        ComboBox[] m_DdlAdvSwitchStateArray = new ComboBox[NUM_BUTTON_STATES + 4];

        CheckBox[] m_ChkNoteOnCheck = new CheckBox[NUM_BUTTON_STATES + 4];

        CheckBox[] m_ChkDrumBoostOn = new CheckBox[NUM_PADS];
        CheckBox[] m_ChkAdvDrumBoostOn = new CheckBox[NUM_PADS];

        ComboBox[] m_DdlBoostValue = new ComboBox[NUM_PADS];
        ComboBox[] m_DdlAdvBoostValue = new ComboBox[NUM_PADS];

        FrmMain m_Main;

        public GuiLinker(FrmMain main)
        {
            m_Main = main;
        }

        public void AddButton(GuiDrumButton button,
                       NumericUpDown nupNoteNormal, NumericUpDown nupNoteAdv,
                       NumericUpDown nupButtonNoteVelocityAdv, ComboBox ddlSwitchStateAdv,
                       CheckBox chkNoteOnCheck)
        {
            m_NupNoteArray[NUM_PADS + (byte)button] = nupNoteNormal;
            m_NupAdvNoteArray[NUM_PADS + (byte)button] = nupNoteAdv;

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
        public void AddDPad(GuiDrumDPad dpad,
                     NumericUpDown nupNoteNormal, NumericUpDown nupNoteAdv,
                     NumericUpDown nupButtonNoteVelocityAdv, ComboBox ddlSwitchStateAdv,
                     CheckBox chkNoteOnCheck)
        {
            m_NupNoteArray[NUM_PADS + NUM_BUTTON_STATES + (byte)dpad / 2] = nupNoteNormal;
            m_NupAdvNoteArray[NUM_PADS + NUM_BUTTON_STATES + (byte)dpad / 2] = nupNoteAdv;

            m_NupAdvButtonVelArray[NUM_BUTTON_STATES + (byte)dpad / 2] = nupButtonNoteVelocityAdv;
            m_DdlAdvSwitchStateArray[NUM_BUTTON_STATES + (byte)dpad / 2] = ddlSwitchStateAdv;

            m_ChkNoteOnCheck[NUM_BUTTON_STATES + (byte)dpad / 2] = chkNoteOnCheck;


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
        public void AddDrum(GuiDrumPad pad,
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
                    SetMidiNote((GuiDrumPad)tag.data, (byte)obj.Value); break;
                case GuiLinkerType.Button:
                    SetMidiNote((GuiDrumButton)tag.data, (byte)obj.Value); break;
                case GuiLinkerType.ButtonVelocity:
                    SetButtonVelocity((GuiDrumButton)tag.data, (byte)obj.Value); break;
                case GuiLinkerType.DPad:
                    SetMidiNote((GuiDrumDPad)tag.data, (byte)obj.Value); break;
                case GuiLinkerType.DPadVelocity:
                    SetButtonVelocity((GuiDrumDPad)tag.data, (byte)obj.Value); break;
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
                    SetBoostEnabled((GuiDrumPad)tag.data, obj.Checked); break;
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
                    SetBoostValue((GuiDrumPad)tag.data, (byte)obj.SelectedItem); break;
                case GuiLinkerType.Button:
                    SetButtonSwitchType((GuiDrumButton)tag.data, (SwitchType)obj.SelectedItem); break;
                case GuiLinkerType.DPad:
                    SetButtonSwitchType((GuiDrumDPad)tag.data, (SwitchType)obj.SelectedItem); break;

                case GuiLinkerType.DPadVelocity:
                case GuiLinkerType.ButtonVelocity:
                    Debug.Assert(false, "wrong type"); break;
                default:
                    Debug.Assert(false, "unkown type"); break;
            }
        }        
        #endregion

        #region Getters/Setters
        internal void CheckboxButton(GuiDrumButton b, bool check)
        {
            m_ChkNoteOnCheck[(byte)b].Checked = check;
        }
        internal void CheckboxButton(GuiDrumDPad dp, bool check)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            m_ChkNoteOnCheck[NUM_BUTTON_STATES + (byte)dp / 2].Checked = check;
        }

        internal bool GetButtonChecked(GuiDrumDPad dp)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            return m_ChkNoteOnCheck[NUM_BUTTON_STATES + (byte)dp / 2].Checked;
        }

        internal byte GetMidiNote(GuiDrumPad pad)
        {
            return (byte)m_NupAdvNoteArray[(byte)pad].Value;
        }
        internal byte GetMidiNote(GuiDrumButton b)
        {
            return (byte)m_NupAdvNoteArray[NUM_PADS + (byte)b].Value;
        }
        internal byte GetMidiNote(GuiDrumDPad dp)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            return (byte)m_NupAdvNoteArray[NUM_PADS + NUM_BUTTON_STATES + (byte)dp / 2].Value;
        }

        internal void SetMidiNote(GuiDrumPad pad, byte note)
        {
            m_NupAdvNoteArray[(byte)pad].Value = note;
            m_NupNoteArray[(byte)pad].Value = note;
        }
        internal void SetMidiNote(GuiDrumButton b, byte note)
        {
            m_NupAdvNoteArray[NUM_PADS + (byte)b].Value = note;
            m_NupNoteArray[NUM_PADS + (byte)b].Value = note;
        }
        internal void SetMidiNote(GuiDrumDPad dp, byte note)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            m_NupAdvNoteArray[NUM_PADS + NUM_BUTTON_STATES + (byte)dp / 2].Value = note;
            m_NupNoteArray[NUM_PADS + NUM_BUTTON_STATES + (byte)dp / 2].Value = note;
        }

        internal byte GetBoost(GuiDrumPad pad)
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
        internal bool GetBoostEnabled(GuiDrumPad pad)
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

        internal void SetBoostValue(GuiDrumPad pad, byte value)
        {
            SetSelectIndex(ref m_DdlAdvBoostValue[(byte)pad], value);
            SetSelectIndex(ref m_DdlBoostValue[(byte)pad], value);
        }
        internal void SetBoostEnabled(GuiDrumPad pad, bool enabled)
        {
            m_ChkDrumBoostOn[(byte)pad].Checked = enabled;
            m_ChkAdvDrumBoostOn[(byte)pad].Checked = enabled;
        }

        internal void SetButtonVelocity(GuiDrumButton drumButton, byte vel)
        {
            m_NupAdvButtonVelArray[(byte)drumButton].Value = vel;
        }
        internal void SetButtonVelocity(GuiDrumDPad drumDPad, byte vel)
        {
            Debug.Assert((byte)drumDPad % 2 == 0, "only left, up, right, down is allowed here");
            m_NupAdvButtonVelArray[NUM_BUTTON_STATES + (byte)drumDPad / 2].Value = vel;
        }

        internal void SetButtonSwitchType(GuiDrumButton b, SwitchType type)
        {
            SetSelectIndex(ref m_DdlAdvSwitchStateArray[(byte)b], type);
        }
        internal void SetButtonSwitchType(GuiDrumDPad dp, SwitchType type)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            SetSelectIndex(ref m_DdlAdvSwitchStateArray[NUM_BUTTON_STATES + (byte)dp / 2], type);
        }

        internal byte GetButtonVelocity(GuiDrumDPad drumDPad)
        {
            Debug.Assert((byte)drumDPad % 2 == 0, "only left, up, right, down is allowed here");
            return (byte)m_NupAdvButtonVelArray[NUM_BUTTON_STATES + (byte)drumDPad / 2].Value;
        }
        internal byte GetButtonVelocity(GuiDrumButton drumButton)
        {
            return (byte)m_NupAdvButtonVelArray[(byte)drumButton].Value;
        }

        internal SwitchType GetButtonSwitchType(GuiDrumDPad drumDPad)
        {
            Debug.Assert((byte)drumDPad % 2 == 0, "only left, up, right, down is allowed here");
            return (SwitchType)m_DdlAdvSwitchStateArray[NUM_BUTTON_STATES + (byte)drumDPad / 2].SelectedItem;
        }
        internal SwitchType GetButtonSwitchType(GuiDrumButton drumButton)
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
