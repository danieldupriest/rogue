﻿#region copyright
// Copyright (C) 2018 "Daniel Bramblett" <bram4@pdx.edu>, "Daniel Dupriest" <kououken@gmail.com>, "Brandon Goldbeck" <bpg@pdx.edu>
// This software is licensed under the MIT License. See LICENSE file for the full text.
#endregion

using Ecs;
using Game.Interfaces;
using Game.Interfaces.Markers;

namespace Game.Components
{
    class Door : Component, IInteractable
    {
        private bool locked = false;

        public bool IsInteractable
        {
            get
            {
                return true;
            }
        }

        public Door(bool locked = false)
        {
            this.locked = locked;
        }

        public override void Start()
        {
            return;
        }

        public override void Update()
        {
            return;
        }

        public override void Render()
        {
            return;
        }

        public void OnInteract(GameObject objectInteracting, object interactorType)
        {
            // This line makes only actors with the DoorOpener component can open doors.
            if (objectInteracting != null && !(interactorType is IDoorOpener) 
                && ! ((interactorType is IRage) && ((IRage)interactorType).isRaging)) {  return; }

            bool destroyDoor = false;
            if (locked)
            {
                if (objectInteracting.GetComponent<Player>() != null)
                {
                    Inventory inv = (Inventory)Player.MainPlayer().GetComponent<Inventory>();
                    if (inv.Find("Key") != null)
                    {
                        inv.Remove("Key");
                        MapManager.CurrentMap().PopObject(transform.position.x, transform.position.y);
                        MapManager.CurrentNavigationMap().RemoveObject(transform.position);

                        HUD.Append(objectInteracting.Name + " unlocked and opened a door.");
                        destroyDoor = true;
                    }
                    else
                    {
                        HUD.Append(objectInteracting.Name + " tried to open door, but is was locked.");
                        //Console.Beep(80, 100);
                    }
                }
            }
            else
            {
                MapManager.CurrentMap().PopObject(transform.position.x, transform.position.y);
                MapManager.CurrentNavigationMap().RemoveObject(transform.position);
                if (objectInteracting.GetComponent<Player>() != null)
                {
                    HUD.Append(objectInteracting.Name + " opened a door.");
                    destroyDoor = true;
                }
            }

            if (destroyDoor)
            {
                GameObject.Destroy(gameObject);
                //Console.Beep(100, 100);
            }
        }
        
    }
}
