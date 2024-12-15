using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using Rocket.Unturned.Chat;
using Rocket.Unturned;
using System;
using UnityEngine;
using System.Collections;
using System.Linq;

namespace RiotGadgets
{
    public class EACMain : RocketPlugin<EACHideyourfaceconfiguration>
    {
        public static EACMain Instance { get; set; }
        GameObject obj;
        bool pl_start = false;
        double timerate;


        public class Ticksec : MonoBehaviour
        {
            public DateTime Check;
            double asec = 1;
            public void Awake()
            {
                Check = DateTime.Now;
            }
            public void FixedUpdate()
            {
                if((DateTime.Now - Check).TotalSeconds >= EACMain.Instance.timerate)
                {
                    Check = DateTime.Now;
                    EACMain.Instance.checkface();


                }
            }   

        }
        protected override void Load()
        {

            Instance = this;
            Rocket.Core.Logging.Logger.Log("HideYourFace made by Ekin");
            Rocket.Core.Logging.Logger.Log("HideYourFace loaded");
            timerate = Configuration.Instance.timerate;
            U.Events.OnPlayerConnected += onplayerjoined;
            if (pl_start)
            {
                obj = new GameObject("Name");
                obj.AddComponent<Ticksec>();
            }
            
        }
        void onplayerjoined(UnturnedPlayer player)
        {
            player.Player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowInteractWithEnemy);
            if (!pl_start)
            {
                starttimer();
            }
            
            
        }
        public void starttimer()
        {
            pl_start = true;
            obj = new GameObject("Name");
            obj.AddComponent<Ticksec>();
        }
        protected override void Unload()
        {
            Rocket.Core.Logging.Logger.Log("HideYourFace Unloaded");
            if (pl_start)
            {
                GameObject.Destroy(obj);

            }

        }
        public void checkface()
        {
            foreach(SteamPlayer client in Provider.clients)
            {

                UnturnedPlayer player = UnturnedPlayer.FromSteamPlayer(client);
                player.Player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowInteractWithEnemy);
                if (Physics.Raycast(player.Player.look.aim.position, player.Player.look.aim.forward, out var raycastHit, 10, RayMasks.PLAYER))
                {
                    try
                    {
                        raycastHit.transform.TryGetComponent<Player>(out Player enemy);
                        UnturnedPlayer enemyplayer = UnturnedPlayer.FromPlayer(enemy);
                        if(enemyplayer.Player.clothing.maskAsset.id != null)
                        {
                            if (!Configuration.Instance.Masklist.Contains(enemyplayer.Player.clothing.maskAsset.id))
                            {
                                player.Player.enablePluginWidgetFlag(EPluginWidgetFlags.ShowInteractWithEnemy);
                            }

                            else
                            {
                                player.Player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowInteractWithEnemy);
                            }
                        }
                        else
                        {
                            player.Player.enablePluginWidgetFlag(EPluginWidgetFlags.ShowInteractWithEnemy);
                        }
                        
                    }
                    catch (Exception e)
                    {
                        player.Player.enablePluginWidgetFlag(EPluginWidgetFlags.ShowInteractWithEnemy);
                    }

                    
                    
                }
            }
        }
        
    }
    
    
}
