using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;
using UnityEngine;
using Microsoft.CSharp;

namespace LurkBaitAPIMod
{
    public class Events : WebSocketBehavior
    {
        
        private readonly ManualLogSource _mls;
        public Events(ManualLogSource mls)
        {
            _mls = mls;
        }
        protected override void OnOpen()
        {
            _mls.LogInfo($"New WebSocket Client connected!");
            NotificationController.Instance.QueueNotif("New WebSocket Client connected!");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            _mls.LogInfo($"New message recieved: {e.Data}");
            try {
                var dataParsed = JsonConvert.DeserializeObject<WebSocketMessageCast>(e.Data);

                if (dataParsed?.username == null)
                {
                    var resp = new
                    {
                        status = 404,
                        message = "username key not provided"
                    };
                    Send(JsonConvert.SerializeObject(resp));
                    return;
                }

                var api = new LurkBaitAPI();
                api.QueueCast(dataParsed.username, 1);

                var respSuccess = new
                {
                    status = 200,
                    message = "successfully queued!"
                };
                Send(JsonConvert.SerializeObject(respSuccess));
                return;
            }
            catch (Exception ex) {
                var resp = new
                {
                    status = 400,
                    message = "Could not parse response. Did you make sure to send it as valid JSON?"
                };
                Send(JsonConvert.SerializeObject(resp));
            }
            
        }
    }
    public static class Data
    {
        public static ManualLogSource Logger { get; set; }
        public static WebSocketServer wss;

        public static int Port = 8592;

        public static void Init(ManualLogSource logger)
        {
            Logger = logger;
            //server
            wss = new WebSocketServer($"ws://127.0.0.1:{Port}");
            wss.AddWebSocketService<Events>("/api", () => new Events(Logger));
            wss.Start();
            Logger.LogInfo($"WebSocket server started on ws://127.0.0.1:{Port}");
            Logger.LogInfo($"Connect to ws://127.0.0.1:{Port}/api to get started!");
        }
    }
}