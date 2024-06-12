# VREyeTracking

Eye Interactable and Eye Tracking Ray Unity Components

EyeInteractable
The EyeInteractable script defines an interactable game object that responds to eye gaze or hover events. It requires a Collider and Rigidbody component to function properly. Key features include:

Hover Effects: Changes material and triggers UnityEvents when the object is hovered over.
Customizable Materials: Supports customization of materials for both active and inactive hover states.
UnityEvent Support: Provides UnityEvent for handling hover events with other game logic.
EyeTrackingRay
The EyeTrackingRay script generates a raycast from the user's viewpoint to detect interactable objects within the scene. Notable features include:

Raycasting: Casts a ray from the camera's position to simulate eye tracking.
Hover Detection: Identifies interactable objects within the ray's path and triggers hover effects.
Line Renderer Visualization: Renders the ray for visual feedback.
Layer Masking: Allows filtering of interactable layers for efficient detection.
This set of scripts enables the implementation of interactive elements in Unity projects, particularly suitable for VR or gaze-based interaction systems.

