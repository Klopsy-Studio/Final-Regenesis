using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class BattleController : StateMachine
{
    [HideInInspector] public MonsterController monsterController;

    public bool isTimeLineActive = true;
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public TileSelectionToggle tileSelectionToggle;
    public GameObject tileSpriteGhostImage;
    public Point pos;

    public ConsumableBackpack backpackInventory;
    //public ConsumableInventoryDemo inventory;

    public GameObject heroPrefab;
    public GameObject enemyPrefab;
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
    public UnitUI unitStatusUI;
    public TurnStatusUI turnStatusUI;
    public AbilityDetailsUI abilityDetailsUI;
    public AttackUI attackUI;
    public SpriteRenderer ghostImage;
    public TimelineUI timelineUI;
    public ExpandedUnitStatus expandedUnitStatus;

    [Header("Combat Variables")]
    [HideInInspector] public int attackChosen;
    public Tile currentTile { get { return board.GetTile(pos); } }
    [Space]
    public RealTimeEvents environmentEvent;


    //Item variables
    [HideInInspector] public int itemChosen;

  

    [HideInInspector] public bool moveActionSelector = false;
    [HideInInspector] public bool moveAbilitySelector = false;
    [HideInInspector] public bool moveItemSelector = false;

    public List<TimelineElements> timelineElements;

    [SerializeField] GameObject placeholderCanvas;
    public GameObject placeholderWinScreen;

    public CinemachineVirtualCamera cinemachineCamera;
    public void BeginGame()
    {
        cinemachineCamera.m_Lens.NearClipPlane = -1f;
        AudioManager.instance.Play("Music");
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
        }
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
}
