using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using NotePractice.Music.Symbols;

namespace NotePractice.MIDIHandling
{
    public class MidiSender
    {
        public string DeviceName { get; set; }
        private OutputDevice? _outputDevice;
        public MidiSender()
        {
            DeviceName = string.Empty;
        }
        public void SendNoteToMidi(Note note)
        {
            if (_outputDevice == null) throw new Exception("No selected device!");
            var midiNoteNumber = ConvertNoteToMidiNumber(note);
            var noteOnEvent = new NoteOnEvent((SevenBitNumber)midiNoteNumber, (SevenBitNumber)note.Velocity); // 100 = velocity
            _outputDevice.SendEvent(noteOnEvent);
        }

        private int ConvertNoteToMidiNumber(Note note)
        {
            int baseNoteNumber;
            switch (note.NoteLetter)
            {
                case NoteLetter.C: baseNoteNumber = 0; break;
                case NoteLetter.D: baseNoteNumber = 2; break;
                case NoteLetter.E: baseNoteNumber = 4; break;
                case NoteLetter.F: baseNoteNumber = 5; break;
                case NoteLetter.G: baseNoteNumber = 7; break;
                case NoteLetter.A: baseNoteNumber = 9; break;
                case NoteLetter.B: baseNoteNumber = 11; break;
                default: throw new Exception("Invalid note letter.");
            }

            // Adjust for accidental
            if (note.Accidental == Accidental.Sharp) baseNoteNumber += 1;
            else if (note.Accidental == Accidental.Flat) baseNoteNumber -= 1;

            return (note.Octave + 1) * 12 + baseNoteNumber;
        }
        public void FindDevice()
        {
            using DeviceSelectForm dsf =
                new DeviceSelectForm(InputDevice.GetAll().Select(device => device.Name).ToList());
            if (dsf.ShowDialog() == DialogResult.OK)
            {
                DeviceName = dsf.SelectedDeviceName;
                _outputDevice = OutputDevice.GetByName(DeviceName);
            }
        }
    }
}
