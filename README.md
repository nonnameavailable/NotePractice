# A Simple Music Reading Program
## Features
### Practicing note reading
![pianopreview](https://github.com/user-attachments/assets/73962948-3d8e-4522-ba49-91803c2cfa76)
A random note is generated and you can then "play" it in one of 3 ways:
- Pressing the corresponding letter on a keyboard (C, D, E, F, G, A, B).
- Clicking on the corresponding key on a virtual piano.
- Playing the note on a digital piano connected via USB. - Simply connect your piano to your PC, press the "Input MIDI" button and select your piano.
  
Your input is evaluated and the name of the note is shown in green if played correctly, or in red if played incorrectly.  
You can set the octaves, the key and whether or not sharp and flat notes will be generated as well.
### Practicing Intervals
![intervalshowcase](https://github.com/user-attachments/assets/f6261ea8-9944-425b-8970-c9e14183fa72)
- 2 notes are displayed, you then press the corresponding 1-8 number key (both numpad and top row should work). The displayed interval is never larger than 1 octave.  
- If correct, the interval is shown in green along with the notes that constitute it. If wrong, your input is shown in red and the correct interval is shown in green.  
You can set the interval distance - 0 means the interval is displayed as a chord, incrementing this number will display the notes as a sequence.  
The virtual or physical piano do not work for this mode but it's something I want to implement in the future.
## Controls
### When the main form is focused
- When practicing notes: Press any of the c, d, e, f, g, a, b letters to "play" the corresponding note. Alternatively, play the note on a virtual or physical piano.
- When practicing intervals: Press 1-8 number key to input the displayed interval.
### When the piano form is focused
- H - Enables / disables the "help" mode.
- T - Switches the help mode to treble clef.
- B - Switches the help mode to bass clef.

# The writing mode
There is also a writing mode but currently, it doesn't really do much.  

## External libraries used
[DRYWETMIDI](https://github.com/melanchall/drywetmidi) by [Maxim Dobroselsky (Melanchall)](https://github.com/melanchall) - used for handling all the MIDI stuff.
