CONTENTS OF THIS FILE
---------------------
   
 * Introduction
 * Requirements
 * Installation
 * Configuration
 * Troubleshooting
 * FAQ
 * Maintainers


 INTRODUCTION
------------

This project implements the EECS 481 Virtual Aquarium Project, which aims to project a virtual aquarium environment onto a knitted tunnel-structure.

This project is developed in Unity and C#. This repository contains a Microsoft Kinect connecter to use hand gestures as input into the environment.

 * Github: https://github.com/EECS481AquaTeam/Aquarium.git


 REQUIREMENTS
------------

This module requires the following components:

 * The lastest version of Unity 
 * Microsoft Kinect 1.8
 * Kinect Scripts in the Assets/ folder
 * Humpback_whale, fishing_boat and Crucarp animations in the /Assets/ folder
 * *.cs, *.mp3 in the /Assets/ folder


 INSTALLATION
------------

 * Download each of the components in the REQUIREMENTS sections
 * Open GameWithKinect.unity
 * The following scripts must be attached to the main camera. They can be found in the inspector:
 	* Underwater.cs 
 	* Main.cs
 	* GrowingTeamGame.cs
 	* LineGame.cs
 	* AquariumGame.cs
 	* AquariumMusic.cs
 * Add the desired prefabs and music scripts into each of these games.
 * Attach the Kinect through the USB port.

TROUBLESHOOTING
---------------

 * The programs is not able to be installed or configured contact the project owners:
 	* Sam Devaprasad (unclesam@umich.edu)
 	* Nadia Dubovitsky (ndubovit@umich.edu)
 	* Bradley Goldstein (bradgold@umich.edu)
 	* Kevin Silang (silanke@umich.edu)