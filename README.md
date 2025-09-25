# 🎮 2D Character Movement System for Unity

A modular and extensible 2D character movement framework for Unity, designed to streamline the setup of basic movement mechanics. This system emphasizes clean architecture, separation of concerns, and scalability.

---

## ✨ Features

- **Modular Architecture**: Decoupled components for input handling, movement logic, and data configuration.
- **Event-Driven Input**: Utilizes interfaces and delegates to manage player input, allowing for flexible input sources.
- **Data-Driven Configuration**: Employs ScriptableObjects for easy tuning of movement parameters.
- **Ground Detection**: Implements raycasting for accurate ground checks.
- **Clean Codebase**: Follows best practices for readability and maintainability.

---

## 📁 Project Structure

<pre> 
  Assets/
  ├── Scripts/ 
       └── Character/ 
        └── Player/ 
           ├── CharacterMovementBase.cs 
           ├── PlayerMovement.cs
           ├── PlayerController.cs 
           ├── PlayerData.cs
           ├── InputMappingRefs
           ├── PlayerDataAsset.cs 
           └── IPlayerInputEvents.cs 
  ├── SO_Data/ 
     └── PlayerData.asset 

</pre>

---

## 🧩 Architecture Overview

### PlayerController *(MonoBehaviour)*
- Initializes and manages:
  - `PlayerMovement`
  - `PlayerData`
  - `InputActionAsset`
- Implements `IPlayerInputEvents`
  - `OnMove`
  - `OnInteract`

### PlayerMovement *(inherits from CharacterMovementBase)*
- Handles movement logic in `FixedUpdate`
- Performs ground checks using raycasting
- Subscribes to `IPlayerInputEvents` for input data

### CharacterMovementBase *(abstract class)*
- Stores movement-related properties:
  - `MoveInput`
  - `IsGrounded`
  - `GroundNormal`

### PlayerDataAsset *(ScriptableObject)*
- Configurable parameters:
  - `MoveSpeed`
  - `EnableGroundCheck`
  - `GroundLayerMask`
  - `GroundCheckDistance`

### PlayerData
- Runtime wrapper for `PlayerDataAsset`

---

## 🚀 Getting Started

1. **Clone the Repository:**

<pre>
git clone https://github.com/VishnuGameDesign/CharacterMovement2D.git
</pre>

2. **Open in Unity**
- Open the project in Unity Editor.
- Ensure you have the Input System package installed via the Package Manager.

3. **Demo Scene**
- Check out the DemoScene in the Assets/Scenes folder to view the player setup.

## 🧪 Customization

🔄 Extending Movement
- Inherit from CharacterMovementBase to create new movement behaviors, e.g., EnemyMovement.

🕹️ Adding New Inputs
- Implement additional events in IPlayerInputEvents and handle them in PlayerController.

🗂️ Google Sheets Integration
- If you prefer maintaining character data in Google Sheets (e.g., for live updates or designer workflows):
  - Export your sheet to .json or .csv.
  - Parse it at runtime using Unity's JSON utilities or a CSV parser.
  - Replace or extend the PlayerData constructor to initialize from that data instead of PlayerDataAsset.

## 📄 License

This project is licensed under the MIT License. See the LICENSE file for details.

## 🙌 Acknowledgments

Inspired by best practices in Unity development and modular architecture design.

