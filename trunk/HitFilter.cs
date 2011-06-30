using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Diagnostics;

namespace _PS360Drum
{
    class HitFilter
    {
        Byte?[] m_HitVelocities = new Byte?[ProDrumController.NUM_PADS];
        Timer[] m_Timers = new Timer[ProDrumController.NUM_PADS];

        FrmMain m_Main;

        const int MAX_HIT_PER_SECOND = 30; //33.3333ms delay
        private byte m_MinVelocitySensitivity = 42;

        public HitFilter(FrmMain main)
        {
            m_Main = main;

            for (int i = 0; i < ProDrumController.NUM_PADS; ++i)
            {
                m_HitVelocities[i] = null;

                m_Timers[i] = new Timer(1.0f / 30 * 1000);
                m_Timers[i].AutoReset = true;
                m_Timers[i].Elapsed += new ElapsedEventHandler(HitFilterTimer_Elapsed);
            }
        }

        void HitFilterTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Timer timer = sender as Timer;
            timer.Stop();

            for (int i = 0; i < ProDrumController.NUM_PADS; ++i)
            {
                if (m_Timers[i] == timer)
                {
                    DrumPad pad = (DrumPad)i;
                    m_Main.MidiSender.TriggerNote(pad, m_HitVelocities[i].Value);
                    m_HitVelocities[i] = null;
                    break;
                }
            }
        }

        public void TriggerNotes(byte color, byte type, byte[] velocities, int velocityArrayOffset)
        {
            int isRed = color & ((byte)PadColor.Red);
            int isYellow = color & ((byte)PadColor.Yellow);
            int isBlue = color & ((byte)PadColor.Blue);
            int isGreen = color & ((byte)PadColor.Green);

            bool OneColor = ((isRed != 0 ? 1 : 0) + (isYellow != 0 ? 1 : 0) + (isBlue != 0 ? 1 : 0) + (isGreen != 0 ? 1 : 0)) > 1;

            int isTom = type & ((byte)PadType.Tom);
            int isCymbal = type & ((byte)PadType.Cymbal);

            if (isRed != 0)
            {
                TriggerNote(DrumPad.RedTom, velocities[velocityArrayOffset + 1]);
            }
            if (isYellow != 0)
            {
                if (isTom != 0 && isCymbal != 0 && OneColor)
                {
                    TriggerNote(DrumPad.YellowTom, velocities[velocityArrayOffset + 0]);
                    TriggerNote(DrumPad.YellowCymbal, velocities[velocityArrayOffset + 1]);
                }
                else if (isTom != 0)
                {
                    TriggerNote(DrumPad.YellowTom, velocities[velocityArrayOffset + 0]);
                }
                else
                {
                    TriggerNote(DrumPad.YellowCymbal, velocities[velocityArrayOffset + 0]);
                }
            }
            if (isBlue != 0)
            {
                if (isTom != 0 && isCymbal != 0 && OneColor)
                {
                    TriggerNote(DrumPad.BlueTom, velocities[velocityArrayOffset + 3]);
                    TriggerNote(DrumPad.BlueCymbal, velocities[velocityArrayOffset + 1]);
                }
                else if (isTom != 0)
                {
                    TriggerNote(DrumPad.BlueTom, velocities[velocityArrayOffset + 3]);
                }
                else
                {
                    TriggerNote(DrumPad.BlueCymbal, velocities[velocityArrayOffset + 3]);
                }
            }
            if (isGreen != 0)
            {
                if (isTom != 0 && isCymbal != 0 && OneColor)
                {
                    TriggerNote(DrumPad.GreenTom, velocities[velocityArrayOffset + 2]);
                    TriggerNote(DrumPad.GreenCymbal, velocities[velocityArrayOffset + 1]);
                }
                else if (isTom != 0)
                {
                    TriggerNote(DrumPad.GreenTom, velocities[velocityArrayOffset + 2]);
                }
                else
                {
                    TriggerNote(DrumPad.GreenCymbal, velocities[velocityArrayOffset + 2]);
                }
            }
        }
        private byte Boost(DrumPad pad, byte velocity)
        {
            if (m_Main.GetBoostEnabled(pad))
            {
                velocity = (byte)Math.Min((byte)255, velocity + m_Main.GetBoost(pad));
            }
            return velocity;
        }
        private void TriggerNote(DrumPad pad, byte velocity)
        {
            if (m_HitVelocities[(int)pad] == null)
            {
                velocity = (byte)(Math.Max(0, Math.Min(255, 255 - (velocity - m_MinVelocitySensitivity))));
                velocity = Boost(pad, velocity);
                m_HitVelocities[(int)pad] = velocity;
                m_Timers[(int)pad].Start();
            }
        }

    }
}
