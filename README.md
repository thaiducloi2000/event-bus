# 🎯 Event Bus for Unity

**Package Name**: `com.littlekid.event-bus`  
**Display Name**: Event Bus  
**Version**: `0.0.1`  
**Unity Version**: `2022.3+`

A lightweight, strongly-typed and extensible Event Bus system for Unity. Designed to decouple gameplay systems with minimal effort while remaining modular and easy to integrate in existing projects.

---

## 📦 Installation

You can add this package via **Git URL** in Unity Package Manager:

1. Open Unity → `Window > Package Manager`
2. Click the `+` icon → `Add package from Git URL`
3. Paste:

```
https://github.com/thaiducloi2000/event-bus
```

Alternatively, you can add it directly in `manifest.json`:

```json
{
  "dependencies": {
    "com.littlekid.event-bus": "https://github.com/thaiducloi2000/event-bus.git"
  }
}
```

> Replace with the actual repository URL if hosted on GitHub or GitLab.

---

## 🧩 Features

- ✅ **Generic and strongly-typed** event system  
- ✅ **Minimal boilerplate**, easy to use  
- ✅ **No dependencies**, works with Unity out of the box  
- ✅ Interface-driven design for scalability  
- ✅ Supports **ScriptableObject**-based event buses  
- ✅ Utility extension methods for mass registration

---

## 🔧 Usage Guide

### 1. Define Your Event Data

All data types sent through the event system must implement `IBusdata<T>`:

```csharp
public struct ChessPositionChangeParam : IBusdata<ChessPositionChangeParam>
{
    public Dictionary<int, Chess> chessInBoard;
    public List<Position> positions;
}
```

### 2. Create a Listener

Implement `IEvenListener` for any class that listens to events:

```csharp
public class CoverManager : MonoBehaviour, IEvenListener
{
    private GameplayEvent Event;

    private void OnEnable()
    {
        Event = GameplayManager.GameEvent;
        SetupEventListener();
    }

    private void OnDisable()
    {
        RemoveEventListener();
    }

    public void SetupEventListener()
    {
        Event.AddListener<ChessPositionChangeParam>(
            (int)GameplayEventID.OnChessesPositionChange, OnPositionChanged);
    }

    public void RemoveEventListener()
    {
        Event.RemoveListener<ChessPositionChangeParam>(
            (int)GameplayEventID.OnChessesPositionChange, OnPositionChanged);
    }

    private void OnPositionChanged(ChessPositionChangeParam param)
    {
        // Handle logic here
    }
}
```

### 3. Dispatch an Event

Post events using the EventBus:

```csharp
var data = new ChessPositionChangeParam
{
    chessInBoard = someDictionary,
    positions = someList
};

GameplayManager.GameEvent.PostEvent((int)GameplayEventID.OnChessesPositionChange, data);
```

### 4. Register All Listeners Automatically (Optional)

If your listeners are children of a common GameObject:

```csharp
this.gameObject.RegisterAllListeners();
this.gameObject.UnregisterAllListeners();
```

Uses `EventBusHelper` for convenience and cleanup.

---

## 🗂 Project Structure

```
event-bus/
├── Runtime/
│   ├── EventBus.cs
│   ├── GameplayEvent.cs
│   ├── IEventBus.cs
│   ├── IEvenListener.cs
│   ├── IBusdata.cs
│   └── EventBusHelper.cs
├── Editor/
├── package.json
└── README.md
```

---

## 🧪 Unit Testing

Planned for a future release to cover:

- Listener registration/unregistration
- Event dispatch and data correctness
- Multi-listener broadcasting

---

## 🚧 Roadmap

- [ ] Add unit tests using Unity Test Framework  
- [ ] Support async events  
- [ ] DOTS-compatible version  
- [ ] Custom event inspectors (Editor support)

---

## 📜 License

MIT License

---

## 👤 Author

Developed by [Little Kid]  
Feel free to contribute or submit issues on GitHub.
