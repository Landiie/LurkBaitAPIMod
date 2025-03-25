# LurkBaitAPIMod
A mod to give access to LurkBait Twitch Fishing's WIP API functionality to send casts from whatever service you'd like!

This could potentially be a future feature of LurkBait Twitch Fishing, however, the game's wonderful developer, Cam, has been going through a lot of life tasks and hasn't had time to work on updates for the game.

In the code, lies a work in progress API call for sending custom casts. All this mod does, is offer a websocket server to connect to, to send a JSON message with a `username` key, which then gets passed into the unused API Cast method.

This mod is primarily meant to be used in conjunction with a SAMMI Extension, which gives a new command in SAMMI to send a cast. SAMMI would handle the Twitch integration, as it uses EventSub instead of the deteriorating PubSub method used within LurkBait Twitch Fishing. This also means you can use other SAMMI Triggers, and extensions from SAMMI, to trigger casts however you'd like.

Since this mod just adds a websocket server to interface with, it is not exclusive to SAMMI. I just happened to make an extension for SAMMI compatible with this mod is all.

## Motive

On Monday, March 24th, 2025, Twitch's PubSub faced a 24 hour blackout. This was anticipated, as for a long time, Twitch has communicated that PubSub would be deprecated, and face several outages right before it's inevitable shutdown on April 14th, 2025.

This left LurkBait Twitch Fishing users stranded, as nobody was able to cast their fishing rods via Twitch events, as the game uses PubSub to connect with Twitch.

This mod exists to provide access to the work in progress API methods contained within the game, via a websocket server for apps like SAMMI, and StreamerBot to connect to, and those apps will handle the connection to Twitch instead of the game itself. In the event that LurkBait Twitch Fishing doesn't update by the time Twitch takes down PubSub, this mod will be there to save you!

## Prerequisites

1. Have LurkBait Twitch Fishing owned, and downloaded onto your machine.
2. Download [LurkBaitAPIMod]([#placeholder](https://github.com/Landiie/LurkBaitAPIMod/releases))
3. Download [BepInEx 5.4.23.2](https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.2/BepInEx_win_x64_5.4.23.2.zip)

## Installation

1. Open your LurkBait Twitch Fishing install folder. You can easily get here by:
   1. Opening your Steam Library
   2. Right clicking the "LurkBait Twitch Fishing" game
   3. Click "Properties..."
   4. Click "Installed Files" on the sidebar of the window that pops up
   5. Click "Browse..." towards the top right of the window
2. Double click on your BepInEx zip file, and drag the contents into the blank space inside your install folder
3. Run the game
4. Close the game after it loads to the main fishing screen
5. Double click on your LurkBaitAPIMod zip file, and drag the contents into the blank space inside your install folder
6. Run the game
7. Done!

## Usage

Depending on what you're using this mod for, please read the section that applies to you!

### For Users

You're probably pointed here because you were told this mod gets your Twitch functionality back via SAMMI, or some other stream assistant application. You can download the accompanying integrations from the list below, I will update the list as people make more things!

    - [LurkBait Twitch Fishing - SAMMI Extension](#placeholder)

If you know how to connect to WebSocket servers and work with them, you can read the developers section too!

### For Developers

In your application, connect to the websocket server endpoint `ws://127.0.0.1:8592/api`.

Once you connect, you should see a "New Websocket Client Connected!" message appear at the top of your game's window.

to send casts, simply send a JSON string formatted like this:

```json
{
    "username":"Landie"
}
```

That's.. really it! Upon submitting your request, you'll get the following response:

```json
{
    "status": 200,
    "message": "successfully queued!"
}
```

And for any errors, the only one's possible are for not providing a username key with a status of `404`, and an invalid json formatting with the status of `400`. each contains "message" keys like the success does aswell.

Please keep in mind that the name you give, is the profile that is passed, and saved in LurkBait Twitch Fishing. Make sure that the casing is consistent and you aren't using different names when trying to refer to one person!
