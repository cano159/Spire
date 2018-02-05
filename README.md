# Spire
A work-in-progress modding framework for TowerFall: Ascension. 

##Installation. 

1. Drag and drop SpireModLoader.exe into your TowerFall Ascension directory. 
2. Drag and drop any mods you want to use into your "Mods" folder in your TowerFall Ascension directory. 
3. Run SpireModLoader.exe. 

Any errors encountered while loading the game will be saved to the "loadlog.txt" file in your TowerFall Ascension directory.

##Creating Mods

Currently, making a mod requires creating a class that inherits from the abstract "Mod" class in the Spire namespace.

Any additions to the game currently require you to register your object with the corrosponding ObjectRegistrar in the SpireController class. 

An example of ObjectRegistrar registration is shown below: 

```
public override void OnModLoad()
{
    //Register custom console command with the ConsoleCommandsRegistrar
    SpireController.Instance.ConsoleCommandsRegistrar.Add(this, new UnlockEverythingCommand());
}
```

###Events

A Mod-derived class can subscribe to events in the EventsController class: 

```
EventController.Instance.OnGameUpdate += Instance_OnGameUpdate;
```

As well as events already in the game: 

```
TFGame.Instance.Window.ClientSizeChanged += Window_ClientSizeChanged;
```
