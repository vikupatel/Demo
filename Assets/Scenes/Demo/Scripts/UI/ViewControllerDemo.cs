using UnityEngine;

  public class ViewControllerDemo : MonoBehaviour
    {

        //singletone instance
        public static ViewControllerDemo instance;

        //refrence to all the views
        public UIViewDemo viewMenu;
        public UIViewDemo viewInGame;
        public UIViewDemo viewGameOver;

        //currentview
        [HideInInspector]
        public UIViewDemo activeView;

        // Use this for initialization
        void Start()
        {
            SetActiveview(viewMenu);
        }

        /// <summary>
        /// Sets the activeView.
        /// </summary>
        /// <param name="targetView">Target view.</param>
        public void SetActiveview(UIViewDemo targetView)
        {
            if(activeView!= null)
            {
                activeView.HideView();
            }
            activeView = targetView;
            activeView.ShowView();
        }

    }

