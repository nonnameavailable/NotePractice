using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private OutputDevice _outputDevice;
        private Stopwatch _stopwatch;
        private int _callCount = 0;
        public int CallCount { get => _callCount; }
        public MidiSender()
        {
            DeviceName = string.Empty;
        }
        private async Task SendNoteToMidi(Note note)
        {
            var midiNoteNumber = ConvertNoteToMidiNumber(note);
            var noteOnEvent = new NoteOnEvent((SevenBitNumber)midiNoteNumber, (SevenBitNumber)note.Velocity); // 100 = velocity
            //using var od = OutputDevice.GetByName(DeviceName);
            //od.SendEvent(noteOnEvent);
            _outputDevice.SendEvent(noteOnEvent);
            await Task.Delay(1000);
            //od.SendEvent(new NoteOffEvent((SevenBitNumber)midiNoteNumber, (SevenBitNumber)0));
            _outputDevice.SendEvent(new NoteOffEvent((SevenBitNumber)midiNoteNumber, (SevenBitNumber)0));
        }
        public void SendNoteToMidiAsync(Note note)
        {
            if (DeviceName == "") return;
            if (_stopwatch.ElapsedMilliseconds < 100)
            {
                return;
            } else
            {
                _stopwatch.Restart();
                //_callCount++;
                Task.Run(() => SendNoteToMidi(note));
            }
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
                new DeviceSelectForm(InputDevice.GetAll().Select(device => device.Name).ToList(), "Select device for output.");
            if (dsf.ShowDialog() == DialogResult.OK)
            {
                DeviceName = dsf.SelectedDeviceName;
                _outputDevice?.Dispose();
                _outputDevice = OutputDevice.GetByName(dsf.SelectedDeviceName);
                _stopwatch = new Stopwatch();
                _stopwatch.Start();
            }
        }
    }
}
