using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace _PS360Drum
{
    public enum MultNoteCheckType
    {
        Greater,
        Lesser
    }
    public class MultiNote
    {
        public byte Velocity { get; private set; }
        public MultNoteCheckType CheckType { get; private set; }

        public DrumPad Pad { get; private set; }
        public byte NoteTo { get; private set; }

        public float VelocityMult { get; private set; }
        public byte VelocityAdd { get; private set; }

        public MultiNote(MultNoteCheckType checkType, byte velocity,
                         DrumPad pad, byte noteTo,
                         float velMult, byte velAdd)
        {
            Velocity = velocity;
            CheckType = checkType;

            Pad = pad;
            NoteTo = noteTo;

            VelocityMult = velMult;
            VelocityAdd = velAdd;
        }

        public bool Morph(DrumPad pad, ref byte velocity, ref byte note)
        {
            if (Pad == pad && VelocityCheck(velocity))
            {
                note = NoteTo;
                velocity = (byte)Math.Max(0, Math.Min(127, velocity * VelocityMult + VelocityAdd));
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool VelocityCheck(byte velocity)
        {
            if (CheckType == MultNoteCheckType.Greater)
            {
                return velocity > Velocity;
            }
            else
            {
                return velocity < Velocity;
            }
        }

        public override string ToString()
        {
            return "if velocity " + (CheckType == MultNoteCheckType.Greater?"> ":"< ") 
                + Velocity + " ==> " + Pad + " = " + NoteTo + ", vel = vel * "
                + string.Format("{0:f2}", VelocityMult) + " + " + VelocityAdd;
        }
    }
    public class MultiNoteGui
    {
        ComboBox m_ddlVelCheck;
        NumericUpDown m_nupVelCheck;

        ComboBox m_ddlNote;
        NumericUpDown m_nupNoteTo;

        NumericUpDown m_nupVelMult;
        NumericUpDown m_nupVelAdd;

        Button m_btnAdd;
        Button m_btnRemove;

        ListBox m_lb;

        public MultiNoteGui(ComboBox ddlVelCheck, NumericUpDown nupVelCheck,
                         ComboBox ddlNote, NumericUpDown nupNoteTo,
                         NumericUpDown nupVelMult, NumericUpDown nupVelAdd,
                         Button btnAdd, Button btnRemove,
                         ListBox lb)
        {
            m_ddlVelCheck = ddlVelCheck;
            m_ddlVelCheck.Items.Add(MultNoteCheckType.Greater);
            m_ddlVelCheck.Items.Add(MultNoteCheckType.Lesser);
            m_ddlVelCheck.SelectedIndex = 0;
            m_nupVelCheck = nupVelCheck;

            m_ddlNote = ddlNote;
            m_ddlNote.Items.Add(DrumPad.RedTom);
            m_ddlNote.Items.Add(DrumPad.YellowTom);
            m_ddlNote.Items.Add(DrumPad.YellowCymbal);
            m_ddlNote.Items.Add(DrumPad.BlueTom);
            m_ddlNote.Items.Add(DrumPad.BlueCymbal);
            m_ddlNote.Items.Add(DrumPad.GreenTom);
            m_ddlNote.Items.Add(DrumPad.GreenCymbal);
            m_ddlNote.SelectedIndex = 0;
            m_nupNoteTo = nupNoteTo;

            m_nupVelMult = nupVelMult;
            m_nupVelAdd = nupVelAdd;

            m_btnAdd = btnAdd;
            m_btnAdd.Click += new EventHandler(m_btnAdd_Click);
            m_btnRemove = btnRemove;
            m_btnRemove.Click += new EventHandler(m_btnRemove_Click);

            m_lb = lb;
        }

        void m_btnAdd_Click(object sender, EventArgs e)
        {
            Add(new MultiNote((MultNoteCheckType)m_ddlVelCheck.SelectedItem,
                (byte)m_nupVelCheck.Value, (DrumPad)m_ddlNote.SelectedItem, (byte)m_nupNoteTo.Value,
                (float)m_nupVelMult.Value, (byte)m_nupVelAdd.Value));
        }
        void m_btnRemove_Click(object sender, EventArgs e)
        {
            if (m_lb.SelectedIndex >= 0)
                m_lb.Items.RemoveAt(m_lb.SelectedIndex);
        }

        public bool Morph(DrumPad pad, ref byte velocity, ref byte note)
        {
            bool changed = false;
            foreach (MultiNote mn in m_lb.Items)
            {
                changed = mn.Morph(pad, ref velocity, ref note);
            }
            return changed;
        }

        public void Add(MultiNote multiNote)
        {
            m_lb.Items.Add(multiNote);
        }

        public object[] GetMultiNotes()
        {
            object[] mn = new object[m_lb.Items.Count];
            m_lb.Items.CopyTo(mn, 0);
            return mn;
        }

        public void Clear()
        {
            m_lb.Items.Clear();
        }
    }
}
