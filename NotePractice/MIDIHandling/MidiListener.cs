using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using NotePractice.MIDIHandling;
using NotePractice.Music.Symbols;
using NotePractice.Piano;
namespace NotePractice.MIDIHandling
{
    public class MidiListener
    {
        private InputDevice ?_inputDevice;

        public event EventHandler<NoteEventArgs> NoteReceived;
        public string DeviceName { get; set; } = string.Empty;
        public MidiListener()
        {
            DeviceName = string.Empty;
        }
        public void FindDevice()
        {
            using DeviceSelectForm dsf =
                new DeviceSelectForm(InputDevice.GetAll().Select(device => device.Name).ToList(), "Select device for input.");
            if (dsf.ShowDialog() == DialogResult.OK)
            {
                DeviceName = dsf.SelectedDeviceName;
            }
        }
        public void StartListening()
        {
            if (string.IsNullOrEmpty(DeviceName))
                throw new Exception("No selected device!");

            _inputDevice = InputDevice.GetByName(DeviceName);
            _inputDevice.EventReceived += OnEventReceived;
            _inputDevice.StartEventsListening();
        }

        private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            if (e.Event is NoteOnEvent noteOnEvent)
            {
                var note = ConvertMidiToNote(noteOnEvent.NoteNumber);
                NoteReceived?.Invoke(this, new NoteEventArgs(note));
            }
        }

        private Note ConvertMidiToNote(int midiNoteNumber)
        {
            int octave = (midiNoteNumber / 12) - 1; // MIDI octave mapping
            int noteIndex = midiNoteNumber % 12;

            NoteLetter noteLetter;
            Accidental accidental = Accidental.None;

            // Map MIDI note numbers to your enums
            switch (noteIndex)
            {
                case 0: noteLetter = NoteLetter.C; break;
                case 1: noteLetter = NoteLetter.C; accidental = Accidental.Sharp; break;
                case 2: noteLetter = NoteLetter.D; break;
                case 3: noteLetter = NoteLetter.D; accidental = Accidental.Sharp; break;
                case 4: noteLetter = NoteLetter.E; break;
                case 5: noteLetter = NoteLetter.F; break;
                case 6: noteLetter = NoteLetter.F; accidental = Accidental.Sharp; break;
                case 7: noteLetter = NoteLetter.G; break;
                case 8: noteLetter = NoteLetter.G; accidental = Accidental.Sharp; break;
                case 9: noteLetter = NoteLetter.A; break;
                case 10: noteLetter = NoteLetter.A; accidental = Accidental.Sharp; break;
                case 11: noteLetter = NoteLetter.B; break;
                default: throw new Exception("Invalid MIDI note.");
            }

            return new Note(noteLetter, octave, accidental);
        }
        public void SimulateKeyPress(int midiNoteNumber)
        {
            var simulatedEvent = new NoteOnEvent((SevenBitNumber)midiNoteNumber, (SevenBitNumber)100); // 100 is velocity
            var midiEventArgs = new MidiEventReceivedEventArgs(simulatedEvent);

            OnEventReceived(null, midiEventArgs); // Directly call your MIDI event handler
        }
    }
}
