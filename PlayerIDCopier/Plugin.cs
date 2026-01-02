using BepInEx;
using GorillaNetworking;
using System.Collections;
using UnityEngine;

namespace PlayerIDCopier
{

    [BepInPlugin(Constants.GUID, Constants.NAME, Constants.VERS)]
    public class Plugin : BaseUnityPlugin
    {
        void Awake() => GorillaTagger.OnPlayerSpawned(delegate
        {
            StartCoroutine(CopyPlayerID());
        });

        IEnumerator CopyPlayerID()
        {
            while (PlayFabAuthenticator.instance == null || string.IsNullOrEmpty(PlayFabAuthenticator.instance.GetPlayFabPlayerId()))
                yield return null;

            GUIUtility.systemCopyBuffer = PlayFabAuthenticator.instance.GetPlayFabPlayerId();

            Debug.Log($"[{Constants.NAME}]: Copied {PlayFabAuthenticator.instance.GetPlayFabPlayerId()} to clipboard.");
        }
    }
    class Constants
    {
        public const string GUID = "kinomonke.playeridcopier",
                            NAME = "PlayerIDCopier",
                            VERS = "1.0";
    }
}