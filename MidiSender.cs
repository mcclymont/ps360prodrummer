using System;
using System.Collections.Generic;
using System.Text;
using CarlsMidiTools; // http://www.franklins.net/dotnet.aspx

namespace _PS360Drum
{
    public class MidiSender
    {
        public string[] MidiDevices { get; private set; }
        private Instrument m_DrumsHandler;
        private FrmMain m_Main;

        public MidiSender(FrmMain main)
        {
            MidiDevices = Instrument.OutDeviceNames();
            m_DrumsHandler = new Instrument();
            m_Main = main;
        }

        public void TriggerNote(DrumPad pad, byte hitVelocity)
        {
            // Maximum value is 127
            hitVelocity = (byte)(hitVelocity / 2);

            byte note = m_Main.GuiLinker.GetMidiNote(pad);
            m_Main.MultiNoteGui.Morph(pad, ref hitVelocity, ref note);

            SendNoteOn(note, hitVelocity);
            m_Main.UpdateVelocityPb(pad, hitVelocity);
            SendNoteOff(note);
        }
        public void UpdateMidiSettings()
        {
            if (m_DrumsHandler.Engaged)
                m_DrumsHandler.Close();
            m_DrumsHandler.InputDeviceName = "";
            m_DrumsHandler.OutputDeviceName = m_Main.GetMidiOutDeviceName();
            m_DrumsHandler.OutputChannel = m_Main.GetMidiChannel();
            m_DrumsHandler.NoteDuration = 0;
            m_DrumsHandler.Volume = 127;
            m_DrumsHandler.PatchNumber = 0;
            if (!m_DrumsHandler.Engaged)
                m_DrumsHandler.Open();
        }
        public void SendNoteOn(byte midiNote, byte midiVelocity)
        {
            System.Diagnostics.Debug.Assert(midiVelocity < 128, "midiVelocity should be < 128");
            m_DrumsHandler.Send(m_Main.GetMidiChannel(),
                (byte)CarlsMidiTools.MIDIStatusMessages.NoteOn,
                midiNote, midiVelocity, (byte)0);
            Console.WriteLine("Sending note: " + midiNote + " - velocity: " + midiVelocity);
        }
        public void SendNoteOff(byte midiNote)
        {
            m_DrumsHandler.Send(m_Main.GetMidiChannel(),
                (byte)CarlsMidiTools.MIDIStatusMessages.NoteOff,
                midiNote, (byte)0, (byte)0);
        }
    }
}
