using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class BattleController : StateMachine
{
    [HideInInspector] public MonsterController monsterController;

    [HideInInspector] public bool isTimeLineActive = true;
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public TileSelectionToggle tileSelectionToggle;
    public GameObject tileSpriteGhostImage;
    public Point pos;

    public ConsumableBackpack backpackInventory;
    //public ConsumableInventoryDemo inventory;

    public LootSystem lootSystem;
    public PlayerUnit currentUnit;
    public EnemyUnit currentEnemyUnit;
    [HideInInspector] public EnemyController currentEnemyController;
   


    [HideInInspector] public ItemElements currentItem;
    [HideInInspector] public int itemIndexToRemove;

    [Space]
    [Header("Unit lists")]
    public List<Unit> unitsInGame;
    public List<Unit> unitsWithActions;

    public List<Unit> playerUnits;
    public List<Unit> enemyUnits;

    [Space]
    [Header("UI references")]
    public OptionSelection actionSelectionUI;
    public OptionSelection abilitySelectionUI;
    public OptionSelection itemSelectionUI;
    public UnitStatusList unitStatusUI;
    public TurnStatusUI turnStatusUI;
    public AbilityDetailsUI abilityDetailsUI;
    public AttackUI attackUI;
    public SpriteRenderer ghostImage;
    public TimelineUI timelineUI;
    public ExpandedUnitStatus expandedUnitStatus;
    public UIController uiController;
    public LootUIManager lootUIManager;
    public AbilityTargets targets;
    [Space]
    [Header("Combat Variables")]
    [HideInInspector] public int attackChosen;
    public List<TimelineElements> timelineElements;

    public Tile currentTile { get { return board.GetTile(pos); } }
    [Space]
    [Header("Events")]
    public RealTimeEvents environmentEvent;
    [HideInInspector] public MonsterEvent currentMonsterEvent;

    //Item variables
    [HideInInspector] public int itemChosen;

  

    [HideInInspector] public bool moveActionSelector = false;
    [HideInInspector] public bool moveAbilitySelector = false;
    [HideInInspector] public bool moveItemSelector = false;
    [HideInInspector] public bool win;
    [HideInInspector] public bool lose;


    [SerializeField] GameObject placeholderCanvas;
    public GameObject placeholderWinScreen;

    public CinemachineVirtualCamera cinemachineCamera;

    [Header("Playtesting")]
    [SerializeField] Playtest playtestingFunctions;


    public void BeginGame()
    {
        cinemachineCamera.m_Lens.NearClipPlane = -1f;
        Destroy(placeholderCanvas.gameObject);
        levelData = GameManager.instance.currentMission;
        
        ChangeState<InitBattleState>();
    }
    //public bool IsInMenu()
    //{
    //    return CurrentState is SelectActionState || CurrentState is SelectAbilityState || CurrentState is SelectItemState || CurrentState is SelectItemState;

    //}


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Battle");
            backpackInventory.RefillBackpack();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach(TimelineElements e in timelineElements)
            {
                e.fTimelineVelocity = 0;
            }
        }

        playtestingFunctions.elements = timelineElements;

    }

    public virtual void SelectTile(Point p)
    {
        if (pos == p || !board.playableTiles.ContainsKey(p))
        {
            return;
        }

        pos = p;
        tileSelectionIndicator.localPosition = board.playableTiles[p].center;
    }


    public void ActivateTileSelector()
    {
        tileSelectionIndicator.gameObject.SetActive(true);
    }

    public void DeactivateTileSelector()
    {
        tileSelectionIndicator.gameObject.SetActive(false);

    }

    public void CheckAllUnits()
    {
        if(playerUnits.Count == 0)
        {
            ChangeState<DefeatState>();
        }
    }

    public void FadeUnits()
    {
        foreach (Unit u in unitsInGame)
        {
            if (u == currentUnit)
                continue;

            u.unitSprite.color = new Color(u.unitSprite.color.r, u.unitSprite.color.g, u.unitSprite.color.b, 0.5f);
        }
    }


    public void ResetUnits()
    {
        foreach (Unit u in unitsInGame)
        {
            if (u == currentUnit)
                continue;

            u.unitSprite.color = new Color(u.unitSprite.color.r, u.unitSprite.color.g, u.unitSprite.color.b, 1f);
        }
    }
}
