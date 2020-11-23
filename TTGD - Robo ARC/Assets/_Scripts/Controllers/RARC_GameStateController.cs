using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RARC_GameStateController : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_GameStateController Instance;

    ////////////////////////////////

    [Header("System Ready States")]
    public bool isReady_Launch;
    public bool isReady_Navigation;
    public bool isReady_Explore;
    public bool isReady_Event;
    public bool isReady_Research;
    public bool isReady_Contruction;
    public bool isReady_Crew;

    [Header("Interatablity Cover")]
    public Image blackoutCurtain_Image;

    [Header("Animators")]
    public Animator blacokoutCurtain_Animator;
    public Animator ship_Animator;
    public Animator cutScene_Animator;
    public Animator gameover_Animator;
    public Animator win_Animator;
    public GameObject gameoverAnimations_GO;

    public IEnumerator currentCutscene_IEnum;

    [Header("Navigation Options")]
    public List<RARC_Planet> navigationPossiblePlanets_List;

    [Header("Per Turn Consumables")]
    public readonly int fuelRequired = 5;
    public readonly int foodRequired = 10;

    [Header("Raycast Blocker")]
    public GameObject raycastBlocker_GO;

    [Header("BLANKVAR")]
    public RARC_RoomTab[] allShipRoomTabs_Arr;

    [Header("BLANKVAR")]
    public int currentSongPlayLength;

    public GameObject MainCanvas;

    /////////////////////////////////////////////////////////////////

    [Header("Crafting Per Turn")]
    public int allowedCraftingPerTurn_Fuel;
    public int currentCraftingPerTurn_Fuel;

    [Header("Crafting Per Turn")]
    public int allowedCraftingPerTurn_Food;
    public int currentCraftingPerTurn_Food;

    [Header("Crafting Per Turn")]
    public int allowedCraftingPerTurn_Organics;
    public int currentCraftingPerTurn_Organics;

    [Header("Crafting Per Turn")]
    public int allowedCraftingPerTurn_Crew;
    public int currentCraftingPerTurn_Crew;

    [Header("Crafting Per Turn")]
    public int allowedCraftingPerTurn_Bots;
    public int currentCraftingPerTurn_Bots;

    [Header("Planet Refresh Per Turn")]
    public int allowedRefreshPerTurn_Planets;
    public int currentRefreshPerTurn_Planets;

    /////////////////////////////////////////////////////////////////

    public readonly float eventChance_Travel = 0.45f;
    public readonly float eventChance_Planet = 0.25f;

    /////////////////////////////////////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;
    }

    private void Start()
    {
        //Check if its the first week On load
        if (RARC_DatabaseController.Instance.ship_SaveData.shipInfo_WeeksSurvived == 0)
        {
            System_GenerateNewWeek(true, true);
        }
        else
        {
            System_GenerateNewWeek(false, true);
        }

        //Refresh All UI On Startup As Well
        RARC_ButtonController_Game.Instance.RefreshUI_WeeksInSpace();
        RARC_ButtonController_Game.Instance.RefreshUI_NavigationDestination();
        RARC_ButtonController_Game.Instance.RefreshUI_UrgentIcons();
        RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
        RARC_ButtonController_Game.Instance.RefreshUI_ButtonInteractablity();
        RARC_ButtonController_Game.Instance.RefreshUI_LaunchResources();


        //Reload Ship
        ReloadShipRooms();

        //Load System Data


    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (RARC_ButtonController_Game.Instance.PauseMenu_Main.activeSelf != true)
            {
                RARC_ButtonController_Game.Instance.Button_Pause();
            }
            else
            {
                RARC_ButtonController_Game.Instance.Button_Pause_Close();
            }
        }
    }

    /////////////////////////////////////////////////////////////////

    public void System_GenerateNewWeek(bool isFirstWeek, bool isFirstLoad)
    {
        //All Bools default to Ready
        isReady_Navigation = true;
        isReady_Event = true;
        isReady_Crew = true;
        isReady_Explore = true;
        isReady_Research = true;
        isReady_Contruction = true;

        //Execpt Launch
        isReady_Launch = false;




        //Check For New Save File
        if (isFirstWeek)
        {
            //Play Cutscene As Enum
            currentCutscene_IEnum = Player_StartCutscene();
            StartCoroutine(Player_StartCutscene());

            //Set Planet BG / Space BG
            RARC_ButtonController_Game.Instance.space_Tab.spacePlanet_Tab.ClearPlanet();
            RARC_ButtonController_Game.Instance.space_Tab.SetSpace_Black();
            RARC_ButtonController_Game.Instance.RefreshUI_Tutorial();
        }
        else if (isFirstLoad)
        {

            //print("Test Code: Load Stuff Here");

            //Refresh All UI
            RARC_ButtonController_Game.Instance.RefreshUI_WeeksInSpace();
            RARC_ButtonController_Game.Instance.RefreshUI_ButtonInteractablity();
            RARC_ButtonController_Game.Instance.RefreshUI_ButtonAvailability_On();
            RARC_ButtonController_Game.Instance.RefreshUI_LaunchResources();
            RARC_ButtonController_Game.Instance.RefreshUI_NavigationDestination();
            RARC_ButtonController_Game.Instance.RefreshUI_UrgentIcons();
            RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();

            if (RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation != null)
            {
                //Set Planet BG / Space BG
                RARC_ButtonController_Game.Instance.space_Tab.spacePlanet_Tab.SetPlanet(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation);
                RARC_ButtonController_Game.Instance.space_Tab.PlayPlanetSpace();
                RARC_ButtonController_Game.Instance.space_Tab.PlanetAnimatorSpeeds();
            }
            else
            {
                //Set Planet BG / Space BG
                RARC_ButtonController_Game.Instance.space_Tab.spacePlanet_Tab.ClearPlanet();
                RARC_ButtonController_Game.Instance.space_Tab.SetSpace_Black();
                RARC_ButtonController_Game.Instance.space_Tab.EmptySpaceAnimatorSpeeds();
            }


            //Play First Track
            RARC_MusicController.Instance.PlayMusic_FirstTrack();
            currentSongPlayLength = 2;
        }
        else
        {
            //Progress Time
            RARC_DatabaseController.Instance.ship_SaveData.shipInfo_WeeksSurvived++;
            RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress++;

            //Generate Possible Events
            GetPossibleNewEvent_Travel();
        }

        //Check Win Condition
        if (RARC_DatabaseController.Instance.ship_SaveData.shipInfo_WeeksSurvived >= 50)
        {
            if (CountRoomsOnShip(RARC_Room.RoomType.CLONING) >= 1)
            {
                System_Win();
            }
            else
            {
                System_Gameover("Cloning");
            }

            return;
        }



        //Loop Song after 3rd track
        if (currentSongPlayLength >= 3)
        {
            RARC_MusicController.Instance.PlayTrackMusic_NoLocation_RandomTrackList(RARC_DatabaseController.Instance.music_DB.musicGame_List);
            currentSongPlayLength = 0;
        }
        else
        {
            currentSongPlayLength++;
        }







        //Remove Old Current Location
        RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation = null;


        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination != null)
        {
            if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress >= RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination.planetTravelTime)
            {
                //Set Location
                RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation = RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination;

                //Set Explore Requirement
                isReady_Explore = false;

                //Reset Counter
                RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress = 0;

                //Remove Planet Destination
                RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination = null;
            }
        }


   

        //Crafting Amounts Reset
        Reset_CraftingAmounts();


        //Reset Navigation Planets
        navigationPossiblePlanets_List = new List<RARC_Planet>();
        navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.GetPlanetDifficulty().GenerateAnyPlanet());
        navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.GetPlanetDifficulty().GenerateAnyPlanet());
        navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.GetPlanetDifficulty().GenerateAnyPlanet());
    }

    /////////////////////////////////////////////////////////////////

    public IEnumerator Player_StartCutscene()
    {
        cutScene_Animator.gameObject.SetActive(true);


        RARC_MusicController.Instance.PlayMusic_Cutscene();


        RARC_ButtonController_Game.Instance.RefreshUI_ButtonAvailability_Off();

        yield return new WaitForSeconds(30.3f);

        if (currentCutscene_IEnum == null)
        {
            yield break;
        }


        StartCoroutine(Player_EndCutscene());

   
        //Break Coroutine
        yield break;
    }

    public IEnumerator Player_EndCutscene()
    {
        cutScene_Animator.gameObject.SetActive(false);
        blacokoutCurtain_Animator.Play("Fade Out");
        currentCutscene_IEnum = null;

        yield return new WaitForSeconds(1f);

        RARC_ButtonController_Game.Instance.RefreshUI_ButtonAvailability_On();
        //RARC_MusicController.Instance.PlayMusic_FirstTrack();
        currentSongPlayLength = 3;

        //Break Coroutine
        yield break;
    }

    public IEnumerator Player_StartWeek()
    {
        //Do All Visuals Here




        //Generate a New Week of Data
        System_GenerateNewWeek(false, false);



        //Wait Till Screen is black
        yield return new WaitForSeconds(1.90f);

        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation != null)
        {
            //Set Planet BG / Space BG
            RARC_ButtonController_Game.Instance.space_Tab.spacePlanet_Tab.SetPlanet(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation);
            RARC_ButtonController_Game.Instance.space_Tab.PlayPlanetSpace();
            RARC_ButtonController_Game.Instance.space_Tab.PlanetAnimatorSpeeds();
        }
        else
        {
            //Set Planet BG / Space BG
            RARC_ButtonController_Game.Instance.space_Tab.spacePlanet_Tab.ClearPlanet();
            RARC_ButtonController_Game.Instance.space_Tab.SetSpace_Black();
            RARC_ButtonController_Game.Instance.space_Tab.EmptySpaceAnimatorSpeeds();
        }


        //Refresh All UI
        RARC_ButtonController_Game.Instance.RefreshUI_WeeksInSpace();
        RARC_ButtonController_Game.Instance.RefreshUI_NavigationDestination();
        RARC_ButtonController_Game.Instance.RefreshUI_UrgentIcons();
        RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
        RARC_ButtonController_Game.Instance.RefreshUI_ButtonAvailability_Off();

        yield return new WaitForSeconds(1.4f);

        RARC_ButtonController_Game.Instance.RefreshUI_ButtonAvailability_On();

        //Add Interatablity
        blackoutCurtain_Image.raycastTarget = false;


        //Break Coroutine
        yield break;
    }

    public void Player_FinishWeek()
    {
        //Remove Interatablity
        RARC_ButtonController_Game.Instance.LaunchButton_Main.interactable = false;
        blackoutCurtain_Image.raycastTarget = true;

        //Play ANimaitons
        ship_Animator.Play("Travel");
        blacokoutCurtain_Animator.Play("Fade In");
    }

    /////////////////////////////////////////////////////////////////

    public void LoadBackground()
    {

        //if ()
        {

        }


    }

    /////////////////////////////////////////////////////////////////

    public void GetPossibleNewEvent_Travel()
    {
        //Break out if not possible events left
        if (RARC_DatabaseController.Instance.ship_SaveData.shipAvalibleTravelEvents_List.Count == 0)
        {
            return;
        }

        //Use Chance to find an event
        float currentEventChance = eventChance_Travel * RARC_DatabaseController.Instance.ship_SaveData.travelEventsMissed;
        float randomChance = UnityEngine.Random.Range(0f, 1f);
        if (randomChance < currentEventChance)
        {
            //Chose Num, Add Event, Blacklist, Remove, Reset
            int chosenEventNO = UnityEngine.Random.Range(0, RARC_DatabaseController.Instance.ship_SaveData.shipAvalibleTravelEvents_List.Count);
            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List.Add(RARC_DatabaseController.Instance.ship_SaveData.shipAvalibleTravelEvents_List[chosenEventNO]);
            RARC_DatabaseController.Instance.ship_SaveData.shipBlacklistTravelEvents_List.Add(RARC_DatabaseController.Instance.ship_SaveData.shipAvalibleTravelEvents_List[chosenEventNO]);
            RARC_DatabaseController.Instance.ship_SaveData.shipAvalibleTravelEvents_List.RemoveAt(chosenEventNO);
            RARC_DatabaseController.Instance.ship_SaveData.travelEventsMissed = 0;
        }
        else
        {
            RARC_DatabaseController.Instance.ship_SaveData.travelEventsMissed += 1;
        }
    }

    public void GetPossibleNewEvent_Planet()
    {
        //Break out if not possible events left
        if (RARC_DatabaseController.Instance.ship_SaveData.shipAvaliblePlanetEvents_List.Count == 0)
        {
            return;
        }

        //Use Chance to find an event
        float currentEventChance = eventChance_Planet * RARC_DatabaseController.Instance.ship_SaveData.planetEventsMissed;
        float randomChance = UnityEngine.Random.Range(0f, 1f);
        if (randomChance < currentEventChance)
        {
            //Chose Num, Add Event, Blacklist, Remove, Reset
            int chosenEventNO = UnityEngine.Random.Range(0, RARC_DatabaseController.Instance.ship_SaveData.shipAvaliblePlanetEvents_List.Count);
            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentPlanetEvents_List.Add(RARC_DatabaseController.Instance.ship_SaveData.shipAvaliblePlanetEvents_List[chosenEventNO]);
            RARC_DatabaseController.Instance.ship_SaveData.shipBlacklistPlanetEvents_List.Add(RARC_DatabaseController.Instance.ship_SaveData.shipAvaliblePlanetEvents_List[chosenEventNO]);
            RARC_DatabaseController.Instance.ship_SaveData.shipAvaliblePlanetEvents_List.RemoveAt(chosenEventNO);
            RARC_DatabaseController.Instance.ship_SaveData.planetEventsMissed = 0;
        }
        else
        {
            RARC_DatabaseController.Instance.ship_SaveData.planetEventsMissed += 1;
        }
    }

    /////////////////////////////////////////////////////////////////

    public void ReloadShipRooms()
    {
        //Reload Ship
        RARC_Room.RoomType[] shipRooms_Arr = RARC_DatabaseController.Instance.ship_SaveData.shipData_Rooms_Arr;
        for (int i = 0; i < shipRooms_Arr.Length; i++)
        {
            //Load Room By Type
            allShipRoomTabs_Arr[i].LoadRoom(RARC_DatabaseController.Instance.room_DB.FindRoomType(shipRooms_Arr[i]));
        }
    }

    public void Reset_CraftingAmounts()
    {
        //Bots
        allowedCraftingPerTurn_Bots = CountRoomsOnShip(RARC_DatabaseController.Instance.crafting_DB.bots_Crafting.roomRequired) * RARC_DatabaseController.Instance.crafting_DB.bots_Crafting.resourceCraftsPerRoom;
        currentCraftingPerTurn_Bots = allowedCraftingPerTurn_Bots;

        //Crew
        allowedCraftingPerTurn_Crew= CountRoomsOnShip(RARC_DatabaseController.Instance.crafting_DB.crew_Crafting.roomRequired) * RARC_DatabaseController.Instance.crafting_DB.crew_Crafting.resourceCraftsPerRoom;
        currentCraftingPerTurn_Crew = allowedCraftingPerTurn_Crew;

        //Food 
        allowedCraftingPerTurn_Food = CountRoomsOnShip(RARC_DatabaseController.Instance.crafting_DB.food_Crafting.roomRequired) * RARC_DatabaseController.Instance.crafting_DB.food_Crafting.resourceCraftsPerRoom;
        currentCraftingPerTurn_Food = allowedCraftingPerTurn_Food;

        //Fuel
        allowedCraftingPerTurn_Fuel = CountRoomsOnShip(RARC_DatabaseController.Instance.crafting_DB.fuel_Crafting.roomRequired) * RARC_DatabaseController.Instance.crafting_DB.fuel_Crafting.resourceCraftsPerRoom;
        currentCraftingPerTurn_Fuel = allowedCraftingPerTurn_Fuel;

        //Organics
        allowedCraftingPerTurn_Organics = CountRoomsOnShip(RARC_DatabaseController.Instance.crafting_DB.organics_Crafting.roomRequired) * RARC_DatabaseController.Instance.crafting_DB.organics_Crafting.resourceCraftsPerRoom;
        currentCraftingPerTurn_Organics = allowedCraftingPerTurn_Organics;

        //Planet Refresh
        allowedRefreshPerTurn_Planets = CountRoomsOnShip(RARC_Room.RoomType.ASTROMETRICS);
        currentRefreshPerTurn_Planets = allowedRefreshPerTurn_Planets;
    } 

    public int CountRoomsOnShip(RARC_Room.RoomType findingRoomType)
    {
        int roomsFound = 0;

        foreach (RARC_Room.RoomType shipRoomType in RARC_DatabaseController.Instance.ship_SaveData.shipData_Rooms_Arr)
        {
            if (findingRoomType == shipRoomType)
            {
                roomsFound++;
            }
        }

        return roomsFound;
    }

    public void RoomCraftingValuesAddedWhenBuilding(RARC_Room.RoomType builtRoomType)
    {
        switch (builtRoomType)
        {
            case RARC_Room.RoomType.EMPTY:
                break;
            case RARC_Room.RoomType.ASTROMETRICS:
                allowedRefreshPerTurn_Planets += 1;
                currentRefreshPerTurn_Planets += 1;
                break;
            case RARC_Room.RoomType.CLONING:
                allowedCraftingPerTurn_Crew += RARC_DatabaseController.Instance.crafting_DB.crew_Crafting.resourceCraftsPerRoom;
                currentCraftingPerTurn_Crew += RARC_DatabaseController.Instance.crafting_DB.crew_Crafting.resourceCraftsPerRoom;
                break;
            case RARC_Room.RoomType.FACTORY:
                allowedCraftingPerTurn_Bots += RARC_DatabaseController.Instance.crafting_DB.bots_Crafting.resourceCraftsPerRoom;
                currentCraftingPerTurn_Bots += RARC_DatabaseController.Instance.crafting_DB.bots_Crafting.resourceCraftsPerRoom;

                allowedCraftingPerTurn_Fuel += RARC_DatabaseController.Instance.crafting_DB.fuel_Crafting.resourceCraftsPerRoom;
                currentCraftingPerTurn_Fuel += RARC_DatabaseController.Instance.crafting_DB.fuel_Crafting.resourceCraftsPerRoom;

                break;
            case RARC_Room.RoomType.HYDROPONICS:
                allowedCraftingPerTurn_Organics += RARC_DatabaseController.Instance.crafting_DB.organics_Crafting.resourceCraftsPerRoom;
                currentCraftingPerTurn_Organics += RARC_DatabaseController.Instance.crafting_DB.organics_Crafting.resourceCraftsPerRoom;
                break;
            case RARC_Room.RoomType.KITCHEN:
                allowedCraftingPerTurn_Food += RARC_DatabaseController.Instance.crafting_DB.food_Crafting.resourceCraftsPerRoom;
                currentCraftingPerTurn_Food += RARC_DatabaseController.Instance.crafting_DB.food_Crafting.resourceCraftsPerRoom;
                break;
            case RARC_Room.RoomType.MEDBAY:
                break;
            case RARC_Room.RoomType.QUARTERS:
                break;
            case RARC_Room.RoomType.STORAGE:
                break;
            default:
                break;
        }
    }

    public void AddRoom(RARC_Room.RoomType roomType)
    {
        throw new NotImplementedException();
    }

    public void RemoveRoom(RARC_Room.RoomType roomType)
    {
        for (int i = 0; i < RARC_DatabaseController.Instance.ship_SaveData.shipData_Rooms_Arr.Length; i++)
        {
            if (RARC_DatabaseController.Instance.ship_SaveData.shipData_Rooms_Arr[i] == roomType)
            {
                RARC_DatabaseController.Instance.ship_SaveData.shipData_Rooms_Arr[i] = RARC_Room.RoomType.EMPTY;
                ReloadShipRooms();
                return;
            }
        }
    }

    public void RemoveRoom_Any()
    {
        for (int i = 0; i < RARC_DatabaseController.Instance.ship_SaveData.shipData_Rooms_Arr.Length; i++)
        {
            if (RARC_DatabaseController.Instance.ship_SaveData.shipData_Rooms_Arr[i] != RARC_Room.RoomType.EMPTY)
            {
                RARC_DatabaseController.Instance.ship_SaveData.shipData_Rooms_Arr[i] = RARC_Room.RoomType.EMPTY;
                ReloadShipRooms();
                return;
            }
        }
    }

    /////////////////////////////////////////////////////////////////

    public RARC_Resource.ResourceType ConvertTypes(RARC_EventOutcome_SO.OutcomeType outcomeType)
    {
        //Resource Types
        RARC_Resource.ResourceType resourceType;

        switch (outcomeType)
        {
            case RARC_EventOutcome_SO.OutcomeType.SCRAP_CHANGE:
                resourceType = RARC_Resource.ResourceType.Scrap;
                break;
            case RARC_EventOutcome_SO.OutcomeType.FUEL_CHANGE:
                resourceType = RARC_Resource.ResourceType.Fuel;
                break;
            case RARC_EventOutcome_SO.OutcomeType.FOOD_CHANGE:
                resourceType = RARC_Resource.ResourceType.Food;
                break;
            case RARC_EventOutcome_SO.OutcomeType.TITANIUM_CHANGE:
                resourceType = RARC_Resource.ResourceType.Titanium;
                break;
            case RARC_EventOutcome_SO.OutcomeType.SILICON_CHANGE:
                resourceType = RARC_Resource.ResourceType.Silicon;
                break;
            case RARC_EventOutcome_SO.OutcomeType.CARBON_CHANGE:
                resourceType = RARC_Resource.ResourceType.Carbon;
                break;
            case RARC_EventOutcome_SO.OutcomeType.ORGANICS_CHANGE:
                resourceType = RARC_Resource.ResourceType.Organics;
                break;
            case RARC_EventOutcome_SO.OutcomeType.MEDKITS_CHANGE:
                resourceType = RARC_Resource.ResourceType.Medkit;
                break;
            case RARC_EventOutcome_SO.OutcomeType.HYDROGEN_CHANGE:
                resourceType = RARC_Resource.ResourceType.Hydrogen;
                break;
            case RARC_EventOutcome_SO.OutcomeType.NITROGEN_CHANGE:
                resourceType = RARC_Resource.ResourceType.Nitrogen;
                break;
            default:
                resourceType = RARC_Resource.ResourceType.NULL;
                break;
        }

        //Return
        return resourceType;
    }

    public void ChangeResources(string resourceName, RARC_Resource.ResourceType resourceType, int resourceCount)
    {
        switch (resourceType)
        {
            case RARC_Resource.ResourceType.Scrap:
                RARC_DatabaseController.Instance.ship_SaveData.shipResource_Scrap.resourceCount += resourceCount;

                //Refresh Then Update Resource Tab Visuals
                RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
                RARC_ButtonController_Game.Instance.storageResourceTabs_List[2].SpawnChangesText(resourceCount);
                break;

            case RARC_Resource.ResourceType.Fuel:
                RARC_DatabaseController.Instance.ship_SaveData.shipResource_Fuel.resourceCount += resourceCount;

                //Refresh Then Update Resource Tab Visuals
                RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
                RARC_ButtonController_Game.Instance.storageResourceTabs_List[0].SpawnChangesText(resourceCount);
                break;

            case RARC_Resource.ResourceType.Food:
                RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food.resourceCount += resourceCount;
                
                //Refresh Then Update Resource Tab Visuals
                RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
                RARC_ButtonController_Game.Instance.storageResourceTabs_List[1].SpawnChangesText(resourceCount);
                break;

            default:
                int resourceSlot = 99;
                int i = 0;

                foreach (RARC_Resource resourceInShip in RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List)
                {
                    if (resourceInShip.resourceType == resourceType)
                    {
                        resourceSlot = i;
                    }
                    i++;
                }

                if (resourceSlot != 99)
                {
                    RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List[resourceSlot].resourceCount += resourceCount;

                    //Refresh Then Update Resource Tab Visuals
                    RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
                    RARC_ButtonController_Game.Instance.storageResourceTabs_List[resourceSlot + 3].SpawnChangesText(resourceCount);
                }
                else
                {
                    RARC_Resource resource = new RARC_Resource(resourceName, resourceCount, resourceType);
                    RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List.Add(resource);

                    //Refresh Then Update Resource Tab Visuals
                    RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
                    RARC_ButtonController_Game.Instance.storageResourceTabs_List[RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List.Count + 3].SpawnChangesText(resourceCount);
                }

           
                break;
        }
    }

    public void ChangeHull(int amount)
    {
        //Refresh
        RARC_DatabaseController.Instance.ship_SaveData.shipHullHealth = Mathf.Clamp(RARC_DatabaseController.Instance.ship_SaveData.shipHullHealth + amount, 0, 100);
        RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
    }

    public void ChangeCrew(int amount)
    {
        //int normalizedAmount

        if (amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                RARC_CrewBotsController.Instance.AddNewCrew();
            }
        }
        else if (amount < 0)
        {
            for (int i = 0; i > amount; i--)
            {
                RARC_CrewBotsController.Instance.RemoveCrewMember();
            }
        }
    }

    public void ChangeBots(int amount)
    {
        if (amount == 0)
        {

        }
        else if (amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                RARC_CrewBotsController.Instance.AddNewBot();
            }
        }
        else if (amount < 0)
        {
            for (int i = 0; i > amount; i--)
            {
                RARC_CrewBotsController.Instance.RemoveBotMember();
            }
        }
    }

    public void ChangeAllRooms(int amount)
    {
        if (amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                //AddRoom();
            }
        }
        else if (amount < 0)
        {
            for (int i = 0; i > amount; i--)
            {
                RemoveRoom_Any();
            }
        }
    }

    public void ChangeCertainRooms(RARC_Room.RoomType roomType, int amount)
    {
        if (amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                AddRoom(roomType);
            }
        }
        else if (amount < 0)
        {
            for (int i = 0; i > amount; i--)
            {
                RemoveRoom(roomType);
            }
        }
    }

    /////////////////////////////////////////////////////////////////

    public bool CheckForResources(RARC_Resource.ResourceType resourceType, int resourceAmount)
    {
        bool isResourceOwned = false;

        switch (resourceType)
        {
            case RARC_Resource.ResourceType.NULL:
                isResourceOwned = true;
                break;

            case RARC_Resource.ResourceType.Scrap:
                if (RARC_DatabaseController.Instance.ship_SaveData.shipResource_Scrap.resourceCount >= resourceAmount)
                {
                    isResourceOwned = true;
                }
                break;

            case RARC_Resource.ResourceType.Fuel:
                if (RARC_DatabaseController.Instance.ship_SaveData.shipResource_Fuel.resourceCount >= resourceAmount)
                {
                    isResourceOwned = true;
                }
                break;

            case RARC_Resource.ResourceType.Food:
                if (RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food.resourceCount >= resourceAmount)
                {
                    isResourceOwned = true;
                }
                break;

            default:
                int resourceSlot = 99;
                int i = 0;

                //Search For Containing Slot
                foreach (RARC_Resource resourceInShip in RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List)
                {
                    if (resourceInShip.resourceType == resourceType)
                    {
                        //Found Slot
                        resourceSlot = i;
                        break;
                    }
                    i++;
                }

                //Check if slot is found
                if (resourceSlot != 99)
                {
                    if (RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List[resourceSlot].resourceCount >= resourceAmount)
                    {
                        isResourceOwned = true;
                    }
                }
                break;
        }

        return isResourceOwned;
    }

    public int GetResoucesCount(RARC_Resource.ResourceType resourceType)
    {
        int resourceAmount = 0;

        switch (resourceType)
        {
            case RARC_Resource.ResourceType.NULL:
                resourceAmount = 0;
                break;

            case RARC_Resource.ResourceType.Scrap:
                if (RARC_DatabaseController.Instance.ship_SaveData.shipResource_Scrap.resourceCount >= resourceAmount)
                {
                    resourceAmount = RARC_DatabaseController.Instance.ship_SaveData.shipResource_Scrap.resourceCount;
                }
                break;

            case RARC_Resource.ResourceType.Fuel:
                if (RARC_DatabaseController.Instance.ship_SaveData.shipResource_Fuel.resourceCount >= resourceAmount)
                {
                    resourceAmount = RARC_DatabaseController.Instance.ship_SaveData.shipResource_Fuel.resourceCount;
                }
                break;

            case RARC_Resource.ResourceType.Food:
                if (RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food.resourceCount >= resourceAmount)
                {
                    resourceAmount = RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food.resourceCount;
                }
                break;

            default:
                int resourceSlot = 99;
                int i = 0;

                //Search For Containing Slot
                foreach (RARC_Resource resourceInShip in RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List)
                {
                    if (resourceInShip.resourceType == resourceType)
                    {
                        //Found Slot
                        resourceSlot = i;
                        break;
                    }
                    i++;
                }

                //Check if slot is found
                if (resourceSlot != 99)
                {
                    if (RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List[resourceSlot].resourceCount >= resourceAmount)
                    {
                        resourceAmount = RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List[resourceSlot].resourceCount;
                    }
                }
                break;
        }

        return resourceAmount;
    }

    /////////////////////////////////////////////////////////////////

    public void EnableRaycastBlocker()
    {
        raycastBlocker_GO.SetActive(true);
    }

    public void DisableRaycastBlocker()
    {
        raycastBlocker_GO.SetActive(false);
    }

    /////////////////////////////////////////////////////////////////

    public void System_Gameover(string reason)
    {
        //Setup
        RARC_MusicController.Instance.PlayMusic_Gameover();
        RARC_ButtonController_Game.Instance.gameoverContainer_GO.SetActive(true);
        EnableRaycastBlocker();

        //Turn Off Main UI
        MainCanvas.SetActive(false);
        ship_Animator.gameObject.SetActive(false);
        gameoverAnimations_GO.SetActive(true);


        switch (reason)
        {
            case "Crew":
                RARC_ButtonController_Game.Instance.gameoverImage_Crew.SetActive(true);
                RARC_ButtonController_Game.Instance.gameoverImage_Hull.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Food.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Fuel.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Cloning.SetActive(false);

                break;

            case "Hull":
                RARC_ButtonController_Game.Instance.gameoverImage_Crew.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Hull.SetActive(true);
                RARC_ButtonController_Game.Instance.gameoverImage_Food.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Fuel.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Cloning.SetActive(false);

                break;

            case "Food":
                RARC_ButtonController_Game.Instance.gameoverImage_Crew.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Hull.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Food.SetActive(true);
                RARC_ButtonController_Game.Instance.gameoverImage_Fuel.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Cloning.SetActive(false);

                break;

            case "Fuel":
                RARC_ButtonController_Game.Instance.gameoverImage_Crew.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Hull.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Food.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Fuel.SetActive(true);
                RARC_ButtonController_Game.Instance.gameoverImage_Cloning.SetActive(false);

                break;

            case "Cloning":
                RARC_ButtonController_Game.Instance.gameoverImage_Crew.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Hull.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Food.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Fuel.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Cloning.SetActive(true);

                break;
        }

      


        //Play Animation
        gameover_Animator.Play("Fade In Gameover");
    }

    public void System_Win()
    {
        //Setup
        RARC_MusicController.Instance.PlayMusic_Gameover();
        RARC_ButtonController_Game.Instance.winContainer_GO.SetActive(true);
        EnableRaycastBlocker();

        //Turn Off Main UI
        MainCanvas.SetActive(false);

        //Winning
        RARC_ButtonController_Game.Instance.winImage_All.SetActive(true);
        win_Animator.Play("Fade In Gameover");
    }

    /////////////////////////////////////////////////////////////////
}
