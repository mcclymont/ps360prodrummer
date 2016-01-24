# PS360ProDrummer

With this program you can connect your Rock Band 3 Pro drum with cymbals or Xbox360 GHWT/GH5 drum to your computer and play it like a real e-drum!

It reads raw usb data from the drum and translates it to midi events.

## Features
* Supports PS3 RockBand Pro Drum and Xbox360 GHWT/GH5 Drum
* Velocity sensitive (except kick on ProDrum but that's the hardware)
* Midi out
* Select note mapping
* Boost volume of individual pads

## Requirements
* LoopBe1 ( http://www.nerds.de/en/download.html )
* Any program that accepts MIDI (ex: Ableton Live, FL Studio and Addictive Drums, ..)

### Drivers
#### Windows 10
Note that Windows 10 shipped with updated drivers for the Xbox Wireless Receiver that DO NOT work with this program.
The older drivers for Windows 7 need to be installed.

Installation instructions in [this blog post](http://www.s-config.com/chinese-xbox-360-wireless-receiver-driver-setup/)
#### Windows XP, 7, and 8
[Driver download](https://www.microsoft.com/hardware/en-us/d/xbox-360-wireless-controller-for-windows)

Installation instructions in [this blog post](http://www.s-config.com/archived-xbox-360-receiver-install-for-win-xp-and-win-7/)

## Code Origin
This program is based on the code of PS360Drum which was essentially the same but for the guitar hero world tour kit. It was made by Magnus Ellinge so all credit about finding out how to interface USB and MIDI goes to him.

Further changes were done by Bastian Damman on the [Google Code repository](https://code.google.com/p/ps360prodrummer/)
that this repo was exported from:
* Making it work for the Rock Band 3 Pro Drum
* A hit filter: prevents double hitting
* Rewritten/reorganized the code
* New GUI

I (Chris McClymont) exported the repo from Google Code to investigate why it wasn't working on Windows 10 and also
* Fixed latency issue
