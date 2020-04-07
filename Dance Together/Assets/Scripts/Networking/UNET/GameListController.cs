﻿using System.Collections.Generic;
using UnityEngine;

namespace App.Networking
{
    public class GameListController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform gameListContainer;
        [SerializeField]
        private NetworkJoinButton gameListButton;

        private NetworkController controller;

        public void Init(NetworkController _controller)
        {
            controller = _controller;
        }

        private Dictionary<LanConnectionInfo, NetworkJoinButton> availableGameButtons = new Dictionary<LanConnectionInfo, NetworkJoinButton>();

        public void RefreshGamesList(List<LanConnectionInfo> _availableConnections)
        {
            CreateNewButtons(_availableConnections);
            ClearOldButtons(_availableConnections);
        }

        private void CreateNewButtons(List<LanConnectionInfo> _availableConnections)
        {
            foreach (LanConnectionInfo connection in _availableConnections)
            {
                if (!availableGameButtons.ContainsKey(connection))
                {
                    // create button instance
                    NetworkJoinButton newButton = Instantiate(gameListButton, gameListContainer);
                    newButton.Init(connection, controller);
                    // add kvp
                    availableGameButtons.Add(connection, newButton);
                }
            }
        }

        private void ClearOldButtons(List<LanConnectionInfo> _availableConnections)
        {
            if (_availableConnections.Count == 0)
                Reset();


            foreach (var displayedConnection in availableGameButtons)
            {
                if (!_availableConnections.Contains(displayedConnection.Key))
                {
                    // destroy button instance
                    Destroy(availableGameButtons[displayedConnection.Key].gameObject);
                    // remove kvp
                    availableGameButtons.Remove(displayedConnection.Key);
                }
            }


        }

        private void OnEnable()
        {
            controller.NetworkDiscovery.UpdateMatchInfos();
        }

        private void OnDisable()
        {
            Reset();
        }

        public void Reset()
        {
            foreach (var kvp in availableGameButtons)
            {
                Destroy(kvp.Value.gameObject);

            }

            availableGameButtons.Clear();
        }
    }
}