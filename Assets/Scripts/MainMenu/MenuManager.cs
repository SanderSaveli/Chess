using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

namespace OFG.Chess
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera[] virtualCameras;
        [SerializeField] public GameObject MenuButtons;
        [SerializeField] public GameObject LevelButtons;
        private int currentCameraIndex;
        public void NewGame()
        {
             SwitchCameraMenu();
            
             
              
           
        }
         public void Level()
         {
            SwitchCameraLevel();
         }
        public void Quit()
        {
            Application.Quit();
        }
        
        
           
        
        public void SwitchCameraMenu()
        {
            virtualCameras[currentCameraIndex].gameObject.SetActive(false);
            currentCameraIndex++;

            if (currentCameraIndex >= virtualCameras.Length)
                currentCameraIndex=0;

            virtualCameras[currentCameraIndex].gameObject.SetActive(true);

            
                MenuButtons.SetActive(false);
                LevelButtons.SetActive(true);
        }
        public void SwitchCameraLevel()
        {
            virtualCameras[currentCameraIndex].gameObject.SetActive(false);
            currentCameraIndex++;

            if (currentCameraIndex >= virtualCameras.Length)
                currentCameraIndex=0;

            virtualCameras[currentCameraIndex].gameObject.SetActive(true);

            
                LevelButtons.SetActive(false);
                MenuButtons.SetActive(true);
        }
    
    }
}
