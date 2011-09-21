using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace _PS360Drum.Serialize
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
            settings.RedTom.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumPad.RedTom);
            settings.RedTom.BoostAmount = m_Main.GuiLinker.GetBoost(GuiDrumPad.RedTom);
            settings.RedTom.UseBoost = m_Main.GuiLinker.GetBoostEnabled(GuiDrumPad.RedTom);
            //YellowTom
            settings.YellowTom.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumPad.YellowTom);
            settings.YellowTom.BoostAmount = m_Main.GuiLinker.GetBoost(GuiDrumPad.YellowTom);
            settings.YellowTom.UseBoost = m_Main.GuiLinker.GetBoostEnabled(GuiDrumPad.YellowTom);
            //BlueTom
            settings.BlueTom.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumPad.BlueTom);
            settings.BlueTom.BoostAmount = m_Main.GuiLinker.GetBoost(GuiDrumPad.BlueTom);
            settings.BlueTom.UseBoost = m_Main.GuiLinker.GetBoostEnabled(GuiDrumPad.BlueTom);
            //GreenTom
            settings.GreenTom.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumPad.GreenTom);
            settings.GreenTom.BoostAmount = m_Main.GuiLinker.GetBoost(GuiDrumPad.GreenTom);
            settings.GreenTom.UseBoost = m_Main.GuiLinker.GetBoostEnabled(GuiDrumPad.GreenTom);
            //YellowCymbal
            settings.YellowCymbal.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumPad.YellowCymbal);
            settings.YellowCymbal.BoostAmount = m_Main.GuiLinker.GetBoost(GuiDrumPad.YellowCymbal);
            settings.YellowCymbal.UseBoost = m_Main.GuiLinker.GetBoostEnabled(GuiDrumPad.YellowCymbal);
            //BlueCymbal
            settings.BlueCymbal.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumPad.BlueCymbal);
            settings.BlueCymbal.BoostAmount = m_Main.GuiLinker.GetBoost(GuiDrumPad.BlueCymbal);
            settings.BlueCymbal.UseBoost = m_Main.GuiLinker.GetBoostEnabled(GuiDrumPad.BlueCymbal);
            //GreenCymbal
            settings.GreenCymbal.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumPad.GreenCymbal);
            settings.GreenCymbal.BoostAmount = m_Main.GuiLinker.GetBoost(GuiDrumPad.GreenCymbal);
            settings.GreenCymbal.UseBoost = m_Main.GuiLinker.GetBoostEnabled(GuiDrumPad.GreenCymbal);            
            //PedalLeft
            settings.PedalLeft.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumPad.PedalLeft);
            settings.PedalLeft.BoostAmount = m_Main.GuiLinker.GetBoost(GuiDrumPad.PedalLeft);
            settings.PedalLeft.UseBoost = m_Main.GuiLinker.GetBoostEnabled(GuiDrumPad.PedalLeft);
            //PedalRight
            settings.PedalRight.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumPad.PedalRight);
            settings.PedalRight.BoostAmount = m_Main.GuiLinker.GetBoost(GuiDrumPad.PedalRight);
            settings.PedalRight.UseBoost = m_Main.GuiLinker.GetBoostEnabled(GuiDrumPad.PedalRight);

            //DPadUp
            settings.DPadUp.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumDPad.Up);
            settings.DPadUp.Velocity = m_Main.GuiLinker.GetButtonVelocity(GuiDrumDPad.Up);
            settings.DPadUp.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(GuiDrumDPad.Up);
            //DPadRight
            settings.DPadRight.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumDPad.Right);
            settings.DPadRight.Velocity = m_Main.GuiLinker.GetButtonVelocity(GuiDrumDPad.Right);
            settings.DPadRight.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(GuiDrumDPad.Right);
            //DPadDown
            settings.DPadDown.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumDPad.Down);
            settings.DPadDown.Velocity = m_Main.GuiLinker.GetButtonVelocity(GuiDrumDPad.Down);
            settings.DPadDown.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(GuiDrumDPad.Down);
            //DPadLeft
            settings.DPadLeft.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumDPad.Left);
            settings.DPadLeft.Velocity = m_Main.GuiLinker.GetButtonVelocity(GuiDrumDPad.Left);
            settings.DPadLeft.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(GuiDrumDPad.Left);

            //Triangle
            settings.Triangle.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumButton.Triangle);
            settings.Triangle.Velocity = m_Main.GuiLinker.GetButtonVelocity(GuiDrumButton.Triangle);
            settings.Triangle.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(GuiDrumButton.Triangle);
            //Circle
            settings.Circle.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumButton.Circle);
            settings.Circle.Velocity = m_Main.GuiLinker.GetButtonVelocity(GuiDrumButton.Circle);
            settings.Circle.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(GuiDrumButton.Circle);
            //X
            settings.X.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumButton.X);
            settings.X.Velocity = m_Main.GuiLinker.GetButtonVelocity(GuiDrumButton.X);
            settings.X.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(GuiDrumButton.X);
            //Rectangle
            settings.Rectangle.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumButton.Rectangle);
            settings.Rectangle.Velocity = m_Main.GuiLinker.GetButtonVelocity(GuiDrumButton.Rectangle);
            settings.Rectangle.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(GuiDrumButton.Rectangle);

            //Select
            settings.Select.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumButton.Select);
            settings.Select.Velocity = m_Main.GuiLinker.GetButtonVelocity(GuiDrumButton.Select);
            settings.Select.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(GuiDrumButton.Select);
            //Start
            settings.Start.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumButton.Start);
            settings.Start.Velocity = m_Main.GuiLinker.GetButtonVelocity(GuiDrumButton.Start);
            settings.Start.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(GuiDrumButton.Start);
            //BigButton
            settings.BigButton.MidiNote = m_Main.GuiLinker.GetMidiNote(GuiDrumButton.BigButton);
            settings.BigButton.Velocity = m_Main.GuiLinker.GetButtonVelocity(GuiDrumButton.BigButton);
            settings.BigButton.SwitchType = m_Main.GuiLinker.GetButtonSwitchType(GuiDrumButton.BigButton);

            //Midi
            settings.MidiSettings.MidiChannel = m_Main.GetMidiChannel();
            settings.MidiSettings.MidiDevice = m_Main.GetMidiOutDeviceName();

            //MultiNotes
            settings.MultiNotes.Clear();
            foreach (_PS360Drum.MultiNote m in m_Main.MultiNoteGui.GetMultiNotes())
            {
                settings.MultiNotes.Add(new MultiNote(m.CheckType, m.Velocity,
                    m.Pad, m.NoteTo, m.VelocityMult, m.VelocityAdd));
            }
        }
        private void ApplySettings(Settings settings)
        {
            //RedTom
            m_Main.GuiLinker.SetMidiNote(GuiDrumPad.RedTom, settings.RedTom.MidiNote);
            m_Main.GuiLinker.SetBoostValue(GuiDrumPad.RedTom, settings.RedTom.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(GuiDrumPad.RedTom, settings.RedTom.UseBoost);
            //YellowTom
            m_Main.GuiLinker.SetMidiNote(GuiDrumPad.YellowTom, settings.YellowTom.MidiNote);
            m_Main.GuiLinker.SetBoostValue(GuiDrumPad.YellowTom, settings.YellowTom.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(GuiDrumPad.YellowTom, settings.YellowTom.UseBoost);
            //BlueTom
            m_Main.GuiLinker.SetMidiNote(GuiDrumPad.BlueTom, settings.BlueTom.MidiNote);
            m_Main.GuiLinker.SetBoostValue(GuiDrumPad.BlueTom, settings.BlueTom.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(GuiDrumPad.BlueTom, settings.BlueTom.UseBoost);
            //GreenTom
            m_Main.GuiLinker.SetMidiNote(GuiDrumPad.GreenTom, settings.GreenTom.MidiNote);
            m_Main.GuiLinker.SetBoostValue(GuiDrumPad.GreenTom, settings.GreenTom.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(GuiDrumPad.GreenTom, settings.GreenTom.UseBoost);
            //YellowCymbal
            m_Main.GuiLinker.SetMidiNote(GuiDrumPad.YellowCymbal, settings.YellowCymbal.MidiNote);
            m_Main.GuiLinker.SetBoostValue(GuiDrumPad.YellowCymbal, settings.YellowCymbal.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(GuiDrumPad.YellowCymbal, settings.YellowCymbal.UseBoost);
            //BlueCymbal
            m_Main.GuiLinker.SetMidiNote(GuiDrumPad.BlueCymbal, settings.BlueCymbal.MidiNote);
            m_Main.GuiLinker.SetBoostValue(GuiDrumPad.BlueCymbal, settings.BlueCymbal.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(GuiDrumPad.BlueCymbal, settings.BlueCymbal.UseBoost);
            //GreenCymbal
            m_Main.GuiLinker.SetMidiNote(GuiDrumPad.GreenCymbal, settings.GreenCymbal.MidiNote);
            m_Main.GuiLinker.SetBoostValue(GuiDrumPad.GreenCymbal, settings.GreenCymbal.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(GuiDrumPad.GreenCymbal, settings.GreenCymbal.UseBoost);
            //PedalLeft
            m_Main.GuiLinker.SetMidiNote(GuiDrumPad.PedalLeft, settings.PedalLeft.MidiNote);
            m_Main.GuiLinker.SetBoostValue(GuiDrumPad.PedalLeft, settings.PedalLeft.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(GuiDrumPad.PedalLeft, settings.PedalLeft.UseBoost);
            //PedalRight
            m_Main.GuiLinker.SetMidiNote(GuiDrumPad.PedalRight, settings.PedalRight.MidiNote);
            m_Main.GuiLinker.SetBoostValue(GuiDrumPad.PedalRight, settings.PedalRight.BoostAmount);
            m_Main.GuiLinker.SetBoostEnabled(GuiDrumPad.PedalRight, settings.PedalRight.UseBoost);     

            //DPadUp
            m_Main.GuiLinker.SetMidiNote(GuiDrumDPad.Up, settings.DPadUp.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(GuiDrumDPad.Up, settings.DPadUp.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(GuiDrumDPad.Up, settings.DPadUp.SwitchType);
            //DPadRight
            m_Main.GuiLinker.SetMidiNote(GuiDrumDPad.Right, settings.DPadRight.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(GuiDrumDPad.Right, settings.DPadRight.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(GuiDrumDPad.Right, settings.DPadRight.SwitchType);
            //DPadDown
            m_Main.GuiLinker.SetMidiNote(GuiDrumDPad.Down, settings.DPadDown.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(GuiDrumDPad.Down, settings.DPadDown.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(GuiDrumDPad.Down, settings.DPadDown.SwitchType);
            //DPadLeft
            m_Main.GuiLinker.SetMidiNote(GuiDrumDPad.Left, settings.DPadLeft.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(GuiDrumDPad.Left, settings.DPadLeft.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(GuiDrumDPad.Left, settings.DPadLeft.SwitchType);

            //Triangle
            m_Main.GuiLinker.SetMidiNote(GuiDrumButton.Triangle, settings.Triangle.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(GuiDrumButton.Triangle, settings.Triangle.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(GuiDrumButton.Triangle, settings.Triangle.SwitchType);
            //Circle
            m_Main.GuiLinker.SetMidiNote(GuiDrumButton.Circle, settings.Circle.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(GuiDrumButton.Circle, settings.Circle.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(GuiDrumButton.Circle, settings.Circle.SwitchType);
            //X
            m_Main.GuiLinker.SetMidiNote(GuiDrumButton.X, settings.X.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(GuiDrumButton.X, settings.X.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(GuiDrumButton.X, settings.X.SwitchType);
            //Rectangle
            m_Main.GuiLinker.SetMidiNote(GuiDrumButton.Rectangle, settings.Rectangle.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(GuiDrumButton.Rectangle, settings.Rectangle.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(GuiDrumButton.Rectangle, settings.Rectangle.SwitchType);

            //Select
            m_Main.GuiLinker.SetMidiNote(GuiDrumButton.Select, settings.Select.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(GuiDrumButton.Select, settings.Select.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(GuiDrumButton.Select, settings.Select.SwitchType);
            //Start
            m_Main.GuiLinker.SetMidiNote(GuiDrumButton.Start, settings.Start.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(GuiDrumButton.Start, settings.Start.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(GuiDrumButton.Start, settings.Start.SwitchType);
            //BigButton
            m_Main.GuiLinker.SetMidiNote(GuiDrumButton.BigButton, settings.BigButton.MidiNote);
            m_Main.GuiLinker.SetButtonVelocity(GuiDrumButton.BigButton, settings.BigButton.Velocity);
            m_Main.GuiLinker.SetButtonSwitchType(GuiDrumButton.BigButton, settings.BigButton.SwitchType);

            //Midi
            m_Main.SetMidiChannel(settings.MidiSettings.MidiChannel);
            m_Main.SetMidiOutDeviceName(settings.MidiSettings.MidiDevice);

            //MultiNotes
            m_Main.MultiNoteGui.Clear();
            foreach (MultiNote m in settings.MultiNotes)
	        {
                m_Main.MultiNoteGui.Add(new _PS360Drum.MultiNote(m.CheckType, m.Velocity,
                    m.Pad, m.NoteTo, m.VelocityMult, m.VelocityAdd));
	        }
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
            [XmlElement("PedalLeft")]
            public Trigger PedalLeft { get; set; }
            [XmlElement("PedalRight")]
            public Trigger PedalRight { get; set; }

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

            [XmlElement("MidiSettings")]
            public MidiSettings MidiSettings { get; set; }

            [XmlElement("MultiNotes")]
            public List<MultiNote> MultiNotes { get; set; }

            public void SetDefaultValues()
            {
                RedTom = new Trigger(38, true, 20);             //        Snare: 38
                YellowTom = new Trigger(50, true, 20);          //     High tom: 50
                YellowCymbal = new Trigger(42, true, 40);       //Closed Hi-hat: 42
                BlueTom = new Trigger(47, true, 20);            //      Mid tom: 47
                BlueCymbal = new Trigger(49, true, 40);         //       Crash : 49
                GreenTom = new Trigger(43, true, 20);           //      Low tom: 43
                GreenCymbal = new Trigger(51, true, 40);        //         Ride: 51
                PedalLeft = new Trigger(44, true, 20);          //  HiHat Pedal: 44
                PedalRight = new Trigger(36, true, 20);         //    Bass Drum: 36

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

                MidiSettings = new MidiSettings("LoopBe Internal MIDI", 10);

                MultiNotes = new List<MultiNote>();
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
        public class MultiNote
        {
            [XmlAttribute("Velocity")]
            public byte Velocity { get; set; }
            [XmlAttribute("CheckType")]
            public MultNoteCheckType CheckType { get; set; }

            [XmlAttribute("Pad")]
            public GuiDrumPad Pad { get; set; }
            [XmlAttribute("NoteTo")]
            public byte NoteTo { get; set; }

            [XmlAttribute("VelocityMult")]
            public float VelocityMult { get; set; }
            [XmlAttribute("VelocityAdd")]
            public byte VelocityAdd { get; set; }

            public MultiNote ()
	        {
	        }

            public MultiNote(MultNoteCheckType checkType, byte velocity,
                         GuiDrumPad pad, byte noteTo,
                         float velMult, byte velAdd)
            {
                CheckType = checkType;
                Velocity = velocity;
                Pad = pad;
                NoteTo = noteTo;
                VelocityMult = velMult;
                VelocityAdd = velAdd;
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
