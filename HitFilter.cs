using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Diagnostics;

namespace _PS360Drum
{
    class HitFilter
    {
        private byte?[] m_HitVelocities;
        private Timer[] m_Timers;
        private byte m_NumPads;
        private IRawToGui m_RawToGuiConverter;

        private FrmMain m_Main;

        private const int MAX_HIT_PER_SECOND = 30; //33.3333ms delay        

        public HitFilter(FrmMain main, byte numPads, IRawToGui translater)
        {
            m_RawToGuiConverter = translater;
            m_HitVelocities = new byte?[numPads];
            m_Timers = new Timer[numPads];
            m_NumPads = numPads;
            m_Main = main;
            for (int i = 0; i < m_NumPads; ++i)
            {
                m_HitVelocities[i] = null;

                m_Timers[i] = new Timer(1.0f / MAX_HIT_PER_SECOND * 1000);
                m_Timers[i].AutoReset = true;
                m_Timers[i].Elapsed += new ElapsedEventHandler(HitFilterTimer_Elapsed);
            }
        }

        void HitFilterTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Timer timer = sender as Timer;
            timer.Stop();

            for (byte i = 0; i < m_NumPads; ++i)
            {
                // Find the index of our timer variable.
                if (m_Timers[i] == timer)
                {
                    m_HitVelocities[i] = null;
                    break;
                }
            }
        }
        private byte Boost(byte rawpad, byte velocity)
        {
            GuiDrumPad pad = m_RawToGuiConverter.TranslatePad(rawpad);
            if (m_Main.GuiLinker.GetBoostEnabled(pad))
            {
                velocity = (byte)Math.Min((byte)255, velocity + m_Main.GuiLinker.GetBoost(pad));
            }
            return velocity;
        }
        public void TriggerNote(byte rawpad, byte velocity)
        {
            velocity = Boost(rawpad, velocity);
            if (m_HitVelocities[rawpad] == null) // No note recently triggered
            {
                GuiDrumPad pad = m_RawToGuiConverter.TranslatePad(rawpad);
                m_Main.MidiSender.TriggerNote(pad, velocity);

                m_HitVelocities[(int)rawpad] = velocity;
                m_Timers[rawpad].Start();
            }
            // Otherwise, the note is ignored.
        }
    }
}
