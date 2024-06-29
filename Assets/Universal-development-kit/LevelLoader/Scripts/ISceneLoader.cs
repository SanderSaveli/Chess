namespace UDK.SceneLoad
{

    public interface ISceneLoader
    {
        /// <summary>
        /// Returns information saved from the previous scene or null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSceneData<T>() where T : ISceneData;

        /// <summary>
        /// Loading a scene with exitingt and entering animations.
        /// </summary>
        /// <param name="sceneName">Name of the scene in the build settings.</param>
        /// <param name="transitionIndex">Animation index in TransitionAnimator, used for exit and entry, -1 to play without animation.</param>
        public void LoadScene(string sceneName, int transitionIndex = -1);

        /// <summary>
        /// Loading a scene with exitingt and entering animations.
        /// </summary>
        /// <param name="sceneName">Name of the scene in the build settings.</param>
        /// <param name="outTransitionIndex">Animation index in TransitionAnimator, used for exit, -1 to play without animation.</param>
        /// <param name="inTransitionIndex">Animation index in TransitionAnimator, used for enter, -1 to play without animation.</param>
        public void LoadScene(string sceneName, int outTransitionIndex, int inTransitionIndex);

        /// <summary>
        /// Loads the target scene through a loading window with transition animations.
        /// </summary>
        /// <param name="targetSceneName">Name of the target scene in the build settings.</param>
        /// <param name="loadSceneName">Name of the loading window scene in the build settings.</param>
        /// <param name="transitionIndex">Animation index in TransitionAnimator, used for all animation, -1 to play without animation.</param>
        public void LoadSceneWithLoadScreen(string targetSceneName, string loadSceneName, int transitionIndex = -1);

        /// <summary>
        /// Loads the target scene through a loading window with transition animations.
        /// </summary>
        /// <param name="targetSceneName">Name of the target scene in the build settings.</param>
        /// <param name="loadSceneName">Name of the loading window scene in the build settings.</param>
        /// <param name="loadScreenTransitionIndex">Animation index in TransitionAnimator, used to enter and exit the loading screen scene, -1 to play without animation.</param>
        /// <param name="scenesTransitionIndex">Animation index in TransitionAnimator, used to exit from current scene and enter the target scene, -1 to play without animation.</param>
        public void LoadSceneWithLoadScreen(string targetSceneName, string loadSceneName, int scenesTransitionIndex, int loadScreenTransitionIndex);

        /// <summary>
        /// Loads the target scene through a loading window with transition animations.
        /// </summary>
        /// <param name="targetSceneName">Name of the target scene in the build settings.</param>
        /// <param name="loadSceneName">Name of the loading window scene in the build settings.</param>
        /// <param name="firstIndex">Animation index in TransitionAnimator, used for exit from current scene, -1 to play without animation.</param>
        /// <param name="secondIndex">Animation index in TransitionAnimator, used for enter to loading screen scene, -1 to play without animation.</param>
        /// <param name="thirdIndex">Animation index in TransitionAnimator, used for exit from loading screen scene, -1 to play without animation.</param>
        /// <param name="fourthIndex">Animation index in TransitionAnimator, used for enter to target scene, -1 to play without animation.</param>
        public void LoadSceneWithLoadScreen(string targetSceneName, string loadSceneName, int firstIndex, int secondIndex, int thirdIndex, int fourthIndex);

        /// <summary>
        /// Loading a scene with exitingt and entering animations. Passes information to the loaded scene.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sceneName">Name of the scene in the build settings.</param>
        /// <param name="data">Information to be sent to the loading scene.</param>
        /// <param name="transitionIndex">Animation index in TransitionAnimator, used for exit and entry, -1 to play without animation.</param>
        public void LoadScene<T>(string sceneName, T data, int transitionIndex = -1) where T : ISceneData;

        /// <summary>
        /// Loading a scene with exitingt and entering animations. Passes information to the loaded scene.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sceneName">Name of the scene in the build settings.</param>
        /// <param name="data">Information to be sent to the loading scene.</param>
        /// <param name="inTransitionIndex">Animation index in TransitionAnimator, used for exit, -1 to play without animation.</param>
        /// <param name="outTransitionIndex">Animation index in TransitionAnimator, used for enter, -1 to play without animation.</param>
        public void LoadScene<T>(string sceneName, T data, int outTransitionIndex, int inTransitionIndex) where T : ISceneData;

        /// <summary>
        /// Loads the target scene through a loading window with transition animations. Passes information to the loaded scene.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetSceneName">Name of the target scene in the build settings.</param>
        /// <param name="loadSceneName">Name of the loading window scene in the build settings.</param>
        /// <param name="data">Information to be sent to the loading scene.</param>
        /// <param name="transitionIndex">Animation index in TransitionAnimator, used for all animation, -1 to play without animation.</param>
        public void LoadSceneWithLoadScreen<T>(string targetSceneName, string loadSceneName, T data, int transitionIndex = -1) where T : ISceneData;

        /// <summary>
        /// Loads the target scene through a loading window with transition animations. Passes information to the loaded scene.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetSceneName">Name of the target scene in the build settings.</param>
        /// <param name="loadSceneName">Name of the loading window scene in the build settings.</param>
        /// <param name="data">Information to be sent to the loading scene.</param>
        /// <param name="loadScreenTransitionIndex">Animation index in TransitionAnimator, used to enter and exit the loading screen scene, -1 to play without animation.</param>
        /// <param name="scenesTransitionIndex">Animation index in TransitionAnimator, used to exit from current scene and enter the target scene, -1 to play without animation.</param>
        public void LoadSceneWithLoadScreen<T>(string targetSceneName, string loadSceneName, T data, int scenesTransitionIndex, int loadScreenTransitionIndex) where T : ISceneData;

        /// <summary>
        /// Loads the target scene through a loading window with transition animations. Passes information to the loaded scene.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetSceneName">Name of the target scene in the build settings.</param>
        /// <param name="loadSceneName">Name of the loading window scene in the build settings.</param>
        /// <param name="data">Information to be sent to the loading scene.</param>
        /// <param name="firstIndex">Animation index in TransitionAnimator, used for exit from current scene, -1 to play without animation.</param>
        /// <param name="secondIndex">Animation index in TransitionAnimator, used for enter to loading screen scene, -1 to play without animation.</param>
        /// <param name="thirdIndex">Animation index in TransitionAnimator, used for exit from loading screen scene, -1 to play without animation.</param>
        /// <param name="fourthIndex">Animation index in TransitionAnimator, used for enter to target scene, -1 to play without animation.</param>
        public void LoadSceneWithLoadScreen<T>(string targetSceneName, string loadSceneName, T data, int firstIndex, int secondIndex, int thirdIndex, int fourthIndex) where T : ISceneData;

    }
}
