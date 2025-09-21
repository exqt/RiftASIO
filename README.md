# ASIO Mod for Rift of the NecroDancer

> [!NOTE]
> ASIO requires a compatible audio device.
> You can use [ASIO Driver Test](https://forum.vb-audio.com/viewtopic.php?t=1204) to check whether your audio device supports ASIO.

Quickly switch the game's audio between Windows WASAPI and ASIO (lower-latency). You can also override FMOD's DSP buffer length via config.

# Install
- Download the [latest release](https://github.com/exqt/RiftASIO/releases/download/v1/RiftASIO_v1+BepInEx.zip) and extract it into the game folder (where `RiftOfTheNecroDancer.exe` is).
- Ensure the `BepInEx` folder is next to the game executable.

# Usage
- F5: Switch to ASIO
- F6: Switch to WASAPI
- F7: Print current DSP buffer settings to BepInEx log

# Config
- Edit `BepInEx/config/RiftASIO.cfg` and set `Audio -> DSPBufferLength` to a positive integer to force that buffer length
    - recommened values: (32, 64, 128, 256)

