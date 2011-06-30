using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace _PS360Drum
{
    public class Serializer
    {
        FrmMain m_Main;

        public Serializer(FrmMain main)
        {
            m_Main = main;
        }

        public void Save(string filePath)
        {
            Settings currentSettings = new Settings();
            GetSettings(ref currentSettings);
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings));
                TextWriter textWriter = new StreamWriter(filePath, false);
                xmlSerializer.Serialize(textWriter, currentSettings);
                textWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed saving settings: \n" + ex.Message, "Saving Error");
            }
        }
        public void LoadDefaultSettings()
        {
            Settings currentSettings = new Settings();
            ApplySettings(currentSettings);
        }
        public void Load(string filePath)
        {
            try
            {
                Settings currentSettings;

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings));
                TextReader textReader = new StreamReader(filePath);
                currentSettings = (Settings)xmlSerializer.Deserialize(textReader);
                textReader.Close();

                ApplySettings(currentSettings);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed loading settings: \n" + ex.Message, "Saving Error");
            }
        }
        private void GetSettings(ref Settings settings)
        {
            //RedTom
            settings.RedTom.MidiNote = m_Main.GetMidiNote(DrumPad.RedTom);
            settings.RedTom.BoostAmount = m_Main.GetBoost(DrumPad.RedTom);
            settings.RedTom.UseBoost = m_Main.GetBoostEnabled(DrumPad.RedTom);
            //YellowTom
            settings.YellowTom.MidiNote = m_Main.GetMidiNote(DrumPad.YellowTom);
            settings.YellowTom.BoostAmount = m_Main.GetBoost(DrumPad.YellowTom);
            settings.YellowTom.UseBoost = m_Main.GetBoostEnabled(DrumPad.YellowTom);
            //BlueTom
            settings.BlueTom.MidiNote = m_Main.GetMidiNote(DrumPad.BlueTom);
            settings.BlueTom.BoostAmount = m_Main.GetBoost(DrumPad.BlueTom);
            settings.BlueTom.UseBoost = m_Main.GetBoostEnabled(DrumPad.BlueTom);
            //GreenTom
            settings.GreenTom.MidiNote = m_Main.GetMidiNote(DrumPad.GreenTom);
            settings.GreenTom.BoostAmount = m_Main.GetBoost(DrumPad.GreenTom);
            settings.GreenTom.UseBoost = m_Main.GetBoostEnabled(DrumPad.GreenTom);
            //YellowCymbal
            settings.YellowCymbal.MidiNote = m_Main.GetMidiNote(DrumPad.YellowCymbal);
            settings.YellowCymbal.BoostAmount = m_Main.GetBoost(DrumPad.YellowCymbal);
            settings.YellowCymbal.UseBoost = m_Main.GetBoostEnabled(DrumPad.YellowCymbal);
            //BlueCymbal
            settings.BlueCymbal.MidiNote = m_Main.GetMidiNote(DrumPad.BlueCymbal);
            settings.BlueCymbal.BoostAmount = m_Main.GetBoost(DrumPad.BlueCymbal);
            settings.BlueCymbal.UseBoost = m_Main.GetBoostEnabled(DrumPad.BlueCymbal);
            //GreenCymbal
            settings.GreenCymbal.MidiNote = m_Main.GetMidiNote(DrumPad.GreenCymbal);
            settings.GreenCymbal.BoostAmount = m_Main.GetBoost(DrumPad.GreenCymbal);
            settings.GreenCymbal.UseBoost = m_Main.GetBoostEnabled(DrumPad.GreenCymbal);
            //Kick
            settings.Kick.MidiNote = m_Main.GetMidiNote(DrumPad.Pedal);
            settings.Kick.BoostAmount = m_Main.GetBoost(DrumPad.Pedal);
            settings.Kick.UseBoost = m_Main.GetBoostEnabled(DrumPad.Pedal);

            //Midi
            settings.MidiSettings.MidiChannel = m_Main.GetMidiChannel();
            settings.MidiSettings.MidiDevice = m_Main.GetMidiOutDeviceName();
        }
        private void ApplySettings(Settings settings)
        {
            //RedTom
            m_Main.SetMidiNote(DrumPad.RedTom, settings.RedTom.MidiNote);
            m_Main.SetBoost(DrumPad.RedTom, settings.RedTom.UseBoost, settings.RedTom.BoostAmount);
            //YellowTom
            m_Main.SetMidiNote(DrumPad.YellowTom, settings.YellowTom.MidiNote);
            m_Main.SetBoost(DrumPad.YellowTom, settings.YellowTom.UseBoost, settings.YellowTom.BoostAmount);
            //BlueTom
            m_Main.SetMidiNote(DrumPad.BlueTom, settings.BlueTom.MidiNote);
            m_Main.SetBoost(DrumPad.BlueTom, settings.BlueTom.UseBoost, settings.BlueTom.BoostAmount);
            //GreenTom
            m_Main.SetMidiNote(DrumPad.GreenTom, settings.GreenTom.MidiNote);
            m_Main.SetBoost(DrumPad.GreenTom, settings.GreenTom.UseBoost, settings.GreenTom.BoostAmount);
            //YellowCymbal
            m_Main.SetMidiNote(DrumPad.YellowCymbal, settings.YellowCymbal.MidiNote);
            m_Main.SetBoost(DrumPad.YellowCymbal, settings.YellowCymbal.UseBoost, settings.YellowCymbal.BoostAmount);
            //BlueCymbal
            m_Main.SetMidiNote(DrumPad.BlueCymbal, settings.BlueCymbal.MidiNote);
            m_Main.SetBoost(DrumPad.BlueCymbal, settings.BlueCymbal.UseBoost, settings.BlueCymbal.BoostAmount);
            //GreenCymbal
            m_Main.SetMidiNote(DrumPad.GreenCymbal, settings.GreenCymbal.MidiNote);
            m_Main.SetBoost(DrumPad.GreenCymbal, settings.GreenCymbal.UseBoost, settings.GreenCymbal.BoostAmount);
            //Kick
            m_Main.SetMidiNote(DrumPad.Pedal, settings.Kick.MidiNote);
            m_Main.SetBoost(DrumPad.Pedal, settings.Kick.UseBoost, settings.Kick.BoostAmount);       

            //Midi
            m_Main.SetMidiChannel(settings.MidiSettings.MidiChannel);
            m_Main.SetMidiOutDeviceName(settings.MidiSettings.MidiDevice);
        }

        [XmlRoot("Settings")]
        public class Settings
        {
            public Settings()
            {
                SetDefaultValues();
            }

            [XmlElement("RedTom")]
            public Trigger RedTom { get; set; }
            [XmlElement("YellowTom")]
            public Trigger YellowTom { get; set; }
            [XmlElement("YellowCymbal")]
            public Trigger YellowCymbal { get; set; }
            [XmlElement("BlueTom")]
            public Trigger BlueTom { get; set; }
            [XmlElement("BlueCymbal")]
            public Trigger BlueCymbal { get; set; }
            [XmlElement("GreenTom")]
            public Trigger GreenTom { get; set; }
            [XmlElement("GreenCymbal")]
            public Trigger GreenCymbal { get; set; }
            [XmlElement("Kick")]
            public Trigger Kick { get; set; }

            [XmlElement("MidiSettings")]
            public MidiSettings MidiSettings { get; set; }

            public void SetDefaultValues()
            {
                RedTom = new Trigger(38, true, 20);             //        Snare: 38
                YellowTom = new Trigger(50, true, 20);          //     High tom: 50
                YellowCymbal = new Trigger(42, true, 40);       //Closed Hi-hat: 42
                BlueTom = new Trigger(47, true, 20);            //      Mid tom: 47
                BlueCymbal = new Trigger(49, true, 40);         //       Crash : 49
                GreenTom = new Trigger(43, true, 20);           //      Low tom: 43
                GreenCymbal = new Trigger(51, true, 40);        //         Ride: 51
                Kick = new Trigger(36, true, 20);               //         Kick: 36
                MidiSettings = new MidiSettings("LoopBe Internal MIDI", 10);
            }
        }
        public class Trigger
        {
            List<String> m_AllowedBoostValues = new List<String> { "10", "20", "30", "40", "50", "60" };
            private byte m_TriggerNote;
            private bool m_TriggerBoost;
            private byte m_BoostAmount;

            public Trigger() { }

            public Trigger(byte midiNote, bool boostActive, byte boostAmount)
            {
                m_TriggerNote = midiNote;
                m_TriggerBoost = boostActive;
                m_BoostAmount = CheckBoost(boostAmount);
            }

            [XmlAttribute("MidiNote")]
            public byte MidiNote
            {
                get { return m_TriggerNote; }
                set { m_TriggerNote = value; }
            }

            [XmlAttribute("UseBoost")]
            public bool UseBoost
            {
                get { return m_TriggerBoost; }
                set { m_TriggerBoost = value; }
            }

            [XmlAttribute("BoostAmount")]
            public byte BoostAmount
            {
                get { return CheckBoost(m_BoostAmount); }
                set { m_BoostAmount = CheckBoost(value); }
            }

            private byte CheckBoost(byte value)
            {
                byte returnValue;
                if (m_AllowedBoostValues.Contains(value.ToString()))
                    returnValue = value;
                else
                    returnValue = 20;
                return returnValue;
            }
        }
        public class MidiSettings
        {
            private string m_MidiDevice;
            private byte m_MidiChannel;

            public MidiSettings() { }

            public MidiSettings(string midiDevice, byte midiChannel)
            {
                m_MidiDevice = midiDevice;
                m_MidiChannel = CheckValue(midiChannel);
            }

            [XmlAttribute("midiDevice")]
            public String MidiDevice
            {
                get { return m_MidiDevice; }
                set { m_MidiDevice = value; }
            }

            [XmlAttribute("midiChannel")]
            public byte MidiChannel
            {
                get { return CheckValue(m_MidiChannel); }
                set { m_MidiChannel = CheckValue(value); }
            }

            private byte CheckValue(byte value)
            {
                byte returnValue = value;
                if (returnValue > 15) returnValue = 15;
                if (returnValue < 0) returnValue = 0;
                return returnValue;
            }
        }
    }
}
