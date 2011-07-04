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
            settings.RedTom.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumPad.RedTom);
            settings.RedTom.BoostAmount = m_Main.GuiLinker.GetBoost(DrumPad.RedTom);
            settings.RedTom.UseBoost = m_Main.GuiLinker.GetBoostEnabled(DrumPad.RedTom);
            //YellowTom
            settings.YellowTom.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumPad.YellowTom);
            settings.YellowTom.BoostAmount = m_Main.GuiLinker.GetBoost(DrumPad.YellowTom);
            settings.YellowTom.UseBoost = m_Main.GuiLinker.GetBoostEnabled(DrumPad.YellowTom);
            //BlueTom
            settings.BlueTom.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumPad.BlueTom);
            settings.BlueTom.BoostAmount = m_Main.GuiLinker.GetBoost(DrumPad.BlueTom);
            settings.BlueTom.UseBoost = m_Main.GuiLinker.GetBoostEnabled(DrumPad.BlueTom);
            //GreenTom
            settings.GreenTom.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumPad.GreenTom);
            settings.GreenTom.BoostAmount = m_Main.GuiLinker.GetBoost(DrumPad.GreenTom);
            settings.GreenTom.UseBoost = m_Main.GuiLinker.GetBoostEnabled(DrumPad.GreenTom);
            //YellowCymbal
            settings.YellowCymbal.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumPad.YellowCymbal);
            settings.YellowCymbal.BoostAmount = m_Main.GuiLinker.GetBoost(DrumPad.YellowCymbal);
            settings.YellowCymbal.UseBoost = m_Main.GuiLinker.GetBoostEnabled(DrumPad.YellowCymbal);
            //BlueCymbal
            settings.BlueCymbal.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumPad.BlueCymbal);
            settings.BlueCymbal.BoostAmount = m_Main.GuiLinker.GetBoost(DrumPad.BlueCymbal);
            settings.BlueCymbal.UseBoost = m_Main.GuiLinker.GetBoostEnabled(DrumPad.BlueCymbal);
            //GreenCymbal
            settings.GreenCymbal.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumPad.GreenCymbal);
            settings.GreenCymbal.BoostAmount = m_Main.GuiLinker.GetBoost(DrumPad.GreenCymbal);
            settings.GreenCymbal.UseBoost = m_Main.GuiLinker.GetBoostEnabled(DrumPad.GreenCymbal);

            //DPadUp
            settings.DPadUp.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumDPad.Up);
            settings.DPadUp.Velocity = m_Main.GuiLinker.GetButtonVelocity(DrumDPad.Up);
            settings.DPadUp.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(DrumDPad.Up);
            //DPadRight
            settings.DPadRight.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumDPad.Right);
            settings.DPadRight.Velocity = m_Main.GuiLinker.GetButtonVelocity(DrumDPad.Right);
            settings.DPadRight.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(DrumDPad.Right);
            //DPadDown
            settings.DPadDown.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumDPad.Down);
            settings.DPadDown.Velocity = m_Main.GuiLinker.GetButtonVelocity(DrumDPad.Down);
            settings.DPadDown.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(DrumDPad.Down);
            //DPadLeft
            settings.DPadLeft.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumDPad.Left);
            settings.DPadLeft.Velocity = m_Main.GuiLinker.GetButtonVelocity(DrumDPad.Left);
            settings.DPadLeft.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(DrumDPad.Left);

            //Triangle
            settings.Triangle.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumButton.Triangle);
            settings.Triangle.Velocity = m_Main.GuiLinker.GetButtonVelocity(DrumButton.Triangle);
            settings.Triangle.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(DrumButton.Triangle);
            //Circle
            settings.Circle.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumButton.Circle);
            settings.Circle.Velocity = m_Main.GuiLinker.GetButtonVelocity(DrumButton.Circle);
            settings.Circle.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(DrumButton.Circle);
            //X
            settings.X.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumButton.X);
            settings.X.Velocity = m_Main.GuiLinker.GetButtonVelocity(DrumButton.X);
            settings.X.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(DrumButton.X);
            //Rectangle
            settings.Rectangle.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumButton.Rectangle);
            settings.Rectangle.Velocity = m_Main.GuiLinker.GetButtonVelocity(DrumButton.Rectangle);
            settings.Rectangle.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(DrumButton.Rectangle);

            //Select
            settings.Select.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumButton.Select);
            settings.Select.Velocity = m_Main.GuiLinker.GetButtonVelocity(DrumButton.Select);
            settings.Select.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(DrumButton.Select);
            //Start
            settings.Start.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumButton.Start);
            settings.Start.Velocity = m_Main.GuiLinker.GetButtonVelocity(DrumButton.Start);
            settings.Start.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(DrumButton.Start);
            //BigButton
            settings.BigButton.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumButton.BigButton);
            settings.BigButton.Velocity = m_Main.GuiLinker.GetButtonVelocity(DrumButton.BigButton);
            settings.BigButton.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(DrumButton.BigButton);

            //PedalLeft
            settings.PedalLeft.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumButton.PedalLeft);
            settings.PedalLeft.Velocity = m_Main.GuiLinker.GetButtonVelocity(DrumButton.PedalLeft);
            settings.PedalLeft.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(DrumButton.PedalLeft);
            //PedalRight
            settings.PedalRight.MidiNote = m_Main.GuiLinker.GetMidiNote(DrumButton.PedalRight);
            settings.PedalRight.Velocity = m_Main.GuiLinker.GetButtonVelocity(DrumButton.PedalRight);
            settings.PedalRight.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(DrumButton.PedalRight);

            //Midi
            settings.MidiSettings.MidiChannel = m_Main.GetMidiChannel();
            settings.MidiSettings.MidiDevice = m_Main.GetMidiOutDeviceName();
        }
        private void ApplySettings(Settings settings)
        {
            //RedTom
            m_Main.GuiLinker.SetMidiNote(DrumPad.RedTom, settings.RedTom.MidiNote);
            m_Main.GuiLinker.SetBoostValue(DrumPad.RedTom, settings.RedTom.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(DrumPad.RedTom, settings.RedTom.UseBoost);
            //YellowTom
            m_Main.GuiLinker.SetMidiNote(DrumPad.YellowTom, settings.YellowTom.MidiNote);
            m_Main.GuiLinker.SetBoostValue(DrumPad.YellowTom, settings.YellowTom.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(DrumPad.YellowTom, settings.YellowTom.UseBoost);
            //BlueTom
            m_Main.GuiLinker.SetMidiNote(DrumPad.BlueTom, settings.BlueTom.MidiNote);
            m_Main.GuiLinker.SetBoostValue(DrumPad.BlueTom, settings.BlueTom.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(DrumPad.BlueTom, settings.BlueTom.UseBoost);
            //GreenTom
            m_Main.GuiLinker.SetMidiNote(DrumPad.GreenTom, settings.GreenTom.MidiNote);
            m_Main.GuiLinker.SetBoostValue(DrumPad.GreenTom, settings.GreenTom.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(DrumPad.GreenTom, settings.GreenTom.UseBoost);
            //YellowCymbal
            m_Main.GuiLinker.SetMidiNote(DrumPad.YellowCymbal, settings.YellowCymbal.MidiNote);
            m_Main.GuiLinker.SetBoostValue(DrumPad.YellowCymbal, settings.YellowCymbal.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(DrumPad.YellowCymbal, settings.YellowCymbal.UseBoost);
            //BlueCymbal
            m_Main.GuiLinker.SetMidiNote(DrumPad.BlueCymbal, settings.BlueCymbal.MidiNote);
            m_Main.GuiLinker.SetBoostValue(DrumPad.BlueCymbal, settings.BlueCymbal.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(DrumPad.BlueCymbal, settings.BlueCymbal.UseBoost);
            //GreenCymbal
            m_Main.GuiLinker.SetMidiNote(DrumPad.GreenCymbal, settings.GreenCymbal.MidiNote);
            m_Main.GuiLinker.SetBoostValue(DrumPad.GreenCymbal, settings.GreenCymbal.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(DrumPad.GreenCymbal, settings.GreenCymbal.UseBoost);     

            //DPadUp
            m_Main.GuiLinker.SetMidiNote(DrumDPad.Up, settings.DPadUp.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(DrumDPad.Up, settings.DPadUp.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(DrumDPad.Up, settings.DPadUp.SwitchType);
            //DPadRight
            m_Main.GuiLinker.SetMidiNote(DrumDPad.Right, settings.DPadRight.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(DrumDPad.Right, settings.DPadRight.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(DrumDPad.Right, settings.DPadRight.SwitchType);
            //DPadDown
            m_Main.GuiLinker.SetMidiNote(DrumDPad.Down, settings.DPadDown.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(DrumDPad.Down, settings.DPadDown.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(DrumDPad.Down, settings.DPadDown.SwitchType);
            //DPadLeft
            m_Main.GuiLinker.SetMidiNote(DrumDPad.Left, settings.DPadLeft.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(DrumDPad.Left, settings.DPadLeft.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(DrumDPad.Left, settings.DPadLeft.SwitchType);

            //Triangle
            m_Main.GuiLinker.SetMidiNote(DrumButton.Triangle, settings.Triangle.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(DrumButton.Triangle, settings.Triangle.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(DrumButton.Triangle, settings.Triangle.SwitchType);
            //Circle
            m_Main.GuiLinker.SetMidiNote(DrumButton.Circle, settings.Circle.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(DrumButton.Circle, settings.Circle.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(DrumButton.Circle, settings.Circle.SwitchType);
            //X
            m_Main.GuiLinker.SetMidiNote(DrumButton.X, settings.X.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(DrumButton.X, settings.X.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(DrumButton.X, settings.X.SwitchType);
            //Rectangle
            m_Main.GuiLinker.SetMidiNote(DrumButton.Rectangle, settings.Rectangle.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(DrumButton.Rectangle, settings.Rectangle.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(DrumButton.Rectangle, settings.Rectangle.SwitchType);

            //Select
            m_Main.GuiLinker.SetMidiNote(DrumButton.Select, settings.Select.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(DrumButton.Select, settings.Select.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(DrumButton.Select, settings.Select.SwitchType);
            //Start
            m_Main.GuiLinker.SetMidiNote(DrumButton.Start, settings.Start.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(DrumButton.Start, settings.Start.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(DrumButton.Start, settings.Start.SwitchType);
            //BigButton
            m_Main.GuiLinker.SetMidiNote(DrumButton.BigButton, settings.BigButton.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(DrumButton.BigButton, settings.BigButton.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(DrumButton.BigButton, settings.BigButton.SwitchType);

            //PedalLeft
            m_Main.GuiLinker.SetMidiNote(DrumButton.PedalLeft, settings.PedalLeft.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(DrumButton.PedalLeft, settings.PedalLeft.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(DrumButton.PedalLeft, settings.PedalLeft.SwitchType);
            //PedalRight
            m_Main.GuiLinker.SetMidiNote(DrumButton.PedalRight, settings.PedalRight.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(DrumButton.PedalRight, settings.PedalRight.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(DrumButton.PedalRight, settings.PedalRight.SwitchType);

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

            [XmlElement("DPadUp")]
            public Button DPadUp { get; set; }
            [XmlElement("DPadRight")]
            public Button DPadRight { get; set; }
            [XmlElement("DPadDown")]
            public Button DPadDown { get; set; }
            [XmlElement("DPadLeft")]
            public Button DPadLeft { get; set; }


            [XmlElement("Triangle")]
            public Button Triangle { get; set; }
            [XmlElement("Circle")]
            public Button Circle { get; set; }
            [XmlElement("X")]
            public Button X { get; set; }
            [XmlElement("Rectangle")]
            public Button Rectangle { get; set; }

            [XmlElement("Select")]
            public Button Select { get; set; }
            [XmlElement("Start")]
            public Button Start { get; set; }
            [XmlElement("BigButton")]
            public Button BigButton { get; set; }

            [XmlElement("PedalLeft")]
            public Button PedalLeft { get; set; }
            [XmlElement("PedalRight")]
            public Button PedalRight { get; set; }

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

                DPadUp = new Button(60, 100, SwitchType.KeyboardLike);      //Hi Bongo: 60
                DPadRight = new Button(61, 100, SwitchType.KeyboardLike);   //Low Bongo: 61
                DPadDown = new Button(63, 100, SwitchType.KeyboardLike);    //Hi Open Conga: 63
                DPadLeft = new Button(64, 100, SwitchType.KeyboardLike);    //Low Conga: 64

                Triangle = new Button(58, 100, SwitchType.KeyboardLike);    //Vibraslap: 58
                Circle = new Button(54, 100, SwitchType.KeyboardLike);      //Tambourine: 54
                X = new Button(52, 100, SwitchType.KeyboardLike);           //Chinese Cymbal: 52
                Rectangle = new Button(39, 100, SwitchType.KeyboardLike);   //Hand Clap: 39

                Select = new Button(76, 100, SwitchType.KeyboardLike);      //Hi Wood Block: 76
                Start = new Button(77, 100, SwitchType.KeyboardLike);       //Low Wood Block: 77
                BigButton = new Button(37, 100, SwitchType.KeyboardLike);   //Side Stick: 37

                PedalLeft = new Button(44, 100, SwitchType.OnOff);          //HiHat Pedal: 44
                PedalRight = new Button(36, 100, SwitchType.KeyboardLike);  //Bass Drum: 36

                MidiSettings = new MidiSettings("LoopBe Internal MIDI", 10);
            }
        }
        public class Trigger
        {
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
                byte returnValue = 0;
                foreach (byte b in GuiLinker.allowedBoostValues)
	            {
		            if (b == value)  
                    {
                        returnValue = value;
                        break;
                    }
	            }
                if (returnValue == 0)
                    returnValue = 20;
                return returnValue;
            }
        }
        public class Button
        {
            public Button() { }

            public Button(byte midiNote, byte velocity, SwitchType switchType)
            {
                MidiNote = midiNote;
                Velocity = velocity;
                SwitchType = switchType;
            }

            [XmlAttribute("MidiNote")]
            public byte MidiNote { get; set; }

            [XmlAttribute("Velocity")]
            public byte Velocity { get; set; }

            [XmlAttribute("SwitchType")]
            public SwitchType SwitchType { get; set; }
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
