using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using MelonLoader;
using SG.Claymore.Interaction;//Where the Name and Description hide
using SG.Claymore.Entities;//Health hides here
using SG.GlobalEvents.Variables;
using SG.Claymore.Combat.Combatants;
using SG.Claymore.UI;

namespace SimpleInfo
{
    public class MyMod : MelonMod
    {
        static string modFolder = MelonUtils.UserDataDirectory + "\\SimpleInfo\\";
        string reloadScript = "<script>function timedRefresh(timeoutPeriod) {setTimeout(\"location.reload(true); \",timeoutPeriod);} window.onload = timedRefresh(1500);</script>";
        static void Blank(string file)
        {
            if (File.ReadAllText(modFolder + file) != null)//If not empty, empty it
            {
                File.WriteAllText(modFolder + file, null);
            }
        }
        static void CreateIfMissing(string file)
        {
            if (File.Exists(modFolder + file) == false)//If missing create text based file
            {
                File.CreateText(modFolder + file);
            }
        }
        static void WriteReward(string position)//Write Reward info to their txt files 
        {
            string pattern = "(<script(\\s|\\S)*?<\\/script>)|(<style(\\s|\\S)*?<\\/style>)|(<!--(\\s|\\S)*?-->)|(<\\/?(\\s|\\S)*?>)";
            GameObject gameObject = GameObject.Find("RewardSpawner/SpawnPoints/" + position);
            if (gameObject != null)
            {
                RewardInteractable rewardInteractable = gameObject.GetComponentInChildren<RewardInteractable>();
                if (rewardInteractable != null)
                {
                    if (rewardInteractable.DisplayName.ToString() != File.ReadAllText(modFolder + "DisplayName" + position + ".txt"))
                    {
                        File.WriteAllText(modFolder + "DisplayName" + position + ".txt", rewardInteractable.DisplayName.ToString());
                        MelonLogger.Msg(rewardInteractable.DisplayName.ToString());
                        File.WriteAllText(modFolder + "DisplayDescription" + position + ".txt", Regex.Replace(rewardInteractable.DisplayDescription.ToString(), pattern, ""));
                        MelonLogger.Msg(Regex.Replace(rewardInteractable.DisplayDescription.ToString(), pattern, ""));
                    }
                }
            }
        }
        public override void OnLateUpdate()
        {
            GameObject playerInteractor = GameObject.Find("Player/PlayerInteractor");
            if (playerInteractor != null)
            {
                PlayerHealth playerHealth = playerInteractor.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    string healthPip = "<img src=\"HealthPip.png\">";
                    string healthPipCracked = "<img src=\"HealthPipCracked.png\">";
                    int currentHealth = (int)Math.Ceiling(playerHealth.CurrentHealth);
                    int maxHealth = (int)Math.Ceiling(playerHealth.MaxHealth);
                    int emptyHealth = maxHealth - currentHealth;
                    if (playerHealth.MaxHealth.ToString() != File.ReadAllText(modFolder + "MaxHealth.txt"))
                    {
                        File.WriteAllText(modFolder + "MaxHealth.txt", playerHealth.MaxHealth.ToString());
                        File.WriteAllText(modFolder + "Health.html", "<center>" + String.Concat(Enumerable.Repeat(healthPip, currentHealth)) + String.Concat(Enumerable.Repeat(healthPipCracked, emptyHealth)) + "</center>" + reloadScript);
                        MelonLogger.Msg(playerHealth.CurrentHealth.ToString() + "/" + playerHealth.MaxHealth.ToString());
                    }
                    if (playerHealth.CurrentHealth.ToString() != File.ReadAllText(modFolder + "CurrentHealth.txt"))
                    {
                        File.WriteAllText(modFolder + "CurrentHealth.txt", playerHealth.CurrentHealth.ToString());
                        File.WriteAllText(modFolder + "Health.html", "<center>" + String.Concat(Enumerable.Repeat(healthPip, currentHealth)) + String.Concat(Enumerable.Repeat(healthPipCracked, emptyHealth)) + "</center>" + reloadScript);
                        MelonLogger.Msg(playerHealth.CurrentHealth.ToString() + "/" + playerHealth.MaxHealth.ToString());
                    }
                }
            }

            WriteReward("Left");
            WriteReward("Center");
            WriteReward("Right");

            GameObject dashUI_Instanced = GameObject.Find("DashUI_Instanced");
            if (dashUI_Instanced != null)
            {
                DashUIInstanced dashUIInstanced = dashUI_Instanced.GetComponent<DashUIInstanced>();
                if (dashUIInstanced != null)
                {
                    string dashPip = "<img src=\"DashPip.png\">";
                    string dashPipEmpty = "<img src=\"DashPipEmpty.png\">";
                    int emptyDash = dashUIInstanced._cachedMaxValue - dashUIInstanced._cachedCurrentValue;
                    if (dashUIInstanced._cachedMaxValue.ToString() != File.ReadAllText(modFolder + "MaxDash.txt"))
                    {
                        File.WriteAllText(modFolder + "MaxDash.txt", dashUIInstanced._cachedMaxValue.ToString());
                        File.WriteAllText(modFolder + "Dash.html", "<center>" + String.Concat(Enumerable.Repeat(dashPip, dashUIInstanced._cachedCurrentValue)) + String.Concat(Enumerable.Repeat(dashPipEmpty, emptyDash)) + "</center>" + reloadScript);
                        MelonLogger.Msg(dashUIInstanced._cachedCurrentValue.ToString() + "/" + dashUIInstanced._cachedMaxValue.ToString());
                    }
                    if (dashUIInstanced._cachedCurrentValue.ToString() != File.ReadAllText(modFolder + "CurrentDash.txt"))
                    {
                        File.WriteAllText(modFolder + "CurrentDash.txt", dashUIInstanced._cachedCurrentValue.ToString());
                        File.WriteAllText(modFolder + "Dash.html", "<center>" + String.Concat(Enumerable.Repeat(dashPip, dashUIInstanced._cachedCurrentValue)) + String.Concat(Enumerable.Repeat(dashPipEmpty, emptyDash)) + "</center>" + reloadScript);
                        MelonLogger.Msg(dashUIInstanced._cachedCurrentValue.ToString() + "/" + dashUIInstanced._cachedMaxValue.ToString());
                    }
                }
            }
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            Blank("DisplayNameLeft.txt");//Remove the reward info when changing room
            Blank("DisplayNameCenter.txt");
            Blank("DisplayNameRight.txt");
            Blank("DisplayDescriptionLeft.txt");
            Blank("DisplayDescriptionCenter.txt");
            Blank("DisplayDescriptionRight.txt");
        }
        public override void OnApplicationStart()
        {
            Directory.CreateDirectory(modFolder);//Create a folder just for me <3
            CreateIfMissing("MaxHealth.txt");
            CreateIfMissing("CurrentHealth.txt");
            CreateIfMissing("DisplayNameLeft.txt");
            CreateIfMissing("DisplayDescriptionLeft.txt");
            CreateIfMissing("DisplayNameCenter.txt");
            CreateIfMissing("DisplayDescriptionCenter.txt");
            CreateIfMissing("DisplayNameRight.txt");
            CreateIfMissing("DisplayDescriptionRight.txt");
            CreateIfMissing("MaxDash.txt");
            CreateIfMissing("CurrentDash.txt");
            if (File.Exists(modFolder + "Health.html") == false)//If missing put in all these text based files
            {
                File.WriteAllText(modFolder + "Health.html", reloadScript);
            }
            if (File.Exists(modFolder + "HealthPip.png") == false)
            {
                Bitmap img = Properties.Resources.HealthPip;
                img.Save(modFolder + "HealthPip.png", ImageFormat.Png);
            }
            if (File.Exists(modFolder + "HealthPipCracked.png") == false)
            {
                Bitmap img = Properties.Resources.HealthPipCracked;
                img.Save(modFolder + "HealthPipCracked.png", ImageFormat.Png);
            }
            if (File.Exists(modFolder + "Dash.html") == false)//If missing put in all these text based files
            {
                File.WriteAllText(modFolder + "Dash.html", reloadScript);
            }
            if (File.Exists(modFolder + "DashPip.png") == false)
            {
                Bitmap img = Properties.Resources.DashPip;
                img.Save(modFolder + "DashPip.png", ImageFormat.Png);
            }
            if (File.Exists(modFolder + "DashPipEmpty.png") == false)
            {
                Bitmap img = Properties.Resources.DashPipEmpty;
                img.Save(modFolder + "DashPipEmpty.png", ImageFormat.Png);
            }
        }
        public override void OnApplicationQuit()
        {
            Blank("DisplayNameLeft.txt");
            Blank("DisplayNameCenter.txt");
            Blank("DisplayNameRight.txt");
            Blank("DisplayDescriptionLeft.txt");
            Blank("DisplayDescriptionCenter.txt");
            Blank("DisplayDescriptionRight.txt");
            Blank("MaxHealth.txt");
            Blank("CurrentHealth.txt");
            File.WriteAllText(modFolder + "Health.html", reloadScript);
        }
    }
}