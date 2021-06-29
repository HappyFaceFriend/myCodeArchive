# myCodeArchive
scripts I made/got while making games

## Unity
>### 2D  
> >### AutoFlipping  
> > >FlipObjectToPoint.cs: Flips the object towards the direction. Used when there are only left/right images.  
> > >FlipSpriteToPoint.cs: Flips the sprite towards the direction. Used when there are only left/right images.  
> >### Effects  
> > >DisableWithAnimationEnd.cs: Disables object when animator's animation ends. Used for simple effects.  
> > >Effector.cs (needs correction): Used for effects like scale/move/rotate.  
> >### FSM  
> > >FSMBase.cs: This is a sample FSM  
> > >### Movement  
> > >Usage: Put MovementController and other MovementBases as components.  
> > > >MovementBase.cs: All movement components should derive this class to use ApplyMovement().  
> > > >MovementController: Adds all movement components' moving vectors and apply them to a rigidbody.  
> > > >DirectionMovement.cs: Moves towards a certain direction.  
> > > >Knockback.cs: Enables knockback.  
>### Android  
> >AdManager.cs: Shows video/picture/rewarded ads in android.  
> >AndroidShare.cs: Makes use of android's sharing functionality.  
>### Camera  
> >CameraAutoSize.cs (needs correcion): Resizes camera size to desired resolution.  
>### Inspector  
> >ReadOnlyDrawer.cs: Disables modifying a field in the inspector. Use \[ReadOnly\].  
>### UI  
> >### StatusBar  
> >Usage: Add StatusBar as component to a slider. It will be spawned as child of HUDManager.  
> > >HUDManager.cs: Spawns StatusBar objects.  
> > >StatusBar.cs: Offers functionallity simmilar to an HP bar.  
>### Utils  
> >### FunctionCollections  
> >These are static classes that offers frequently used functions.  
> > >AnimUtils.cs: Functions related to animation.  
> > >FileUtils.cs: Functions related to File I/O.  
> > >VectorUtils.cs: Functions related to vectors and transforms.  
> > >Funcs.cs: Just functions I used in projects with no particular classification.  
> > >EaseFunctions.cs: Easing functions.  
> >SingletonBehaviour.cs: Makes a component/gameobject a singleton.  
