using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Stack : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameScore;
    public GameObject endPanel;
    public Color32[] gameColors = new Color32[4];
    public Material stackMat;
    private const float BoundSize = 3f;
    private const float StackMovingSpeed = 5.0f;
    private const float ErrorMargin = 0.1f;
    private const float StackBoundsGain = 0.25f;
    private const int ComboStartGain = 3;
    private GameObject[] stack;
    private Vector2 stackBounds = new Vector2(BoundSize,BoundSize);
    private int stackIndex;
    public int scoreCount = 0;
    private int combo = 0;
    private float tileTransition =0.0f;
    private float tileSpeed = 2.5f;
    private bool isMovingOnX = true;
    private bool gameOver = false;
    private float SecondaryPosition;
    private Vector3 desiredPosition;
    private Vector3 lastTilePosition;
    Rigidbody rb;
    [SerializeField] GameObject PauseButton;
    public int highscore;
    [SerializeField] Button touchButton;
    [SerializeField] GameObject particlesTail;
    [SerializeField] GameObject CameraShake;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stack = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            stack[i] = transform.GetChild(i).gameObject;
            ColorMesh(stack[i].GetComponent<MeshFilter>().mesh);
        }
        stackIndex = transform.childCount - 1;
    }
    private void CreateRubble(Vector3 pos,Vector3 scale)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        go.AddComponent<Rigidbody>();
        go.GetComponent<MeshRenderer>().material = stackMat;
        ColorMesh(go.GetComponent<MeshFilter>().mesh);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            

                if (gameOver)
                {
                    return;
                }
            if (mousePos.y < 8)
            {
                if (PlaceTile())
                {
                    SpawnTile();
                    scoreCount++;
                    scoreText.text = scoreCount.ToString();
                    gameScore.text = scoreCount.ToString();
                }
                else
                {
                    Endgame();
                }
            }
            }
            
            MoveTile();
            transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);
        
        }
        
    /*public void TouchInput()
    {
        if (gameOver)
        {
            return;
        }
        if (PlaceTile())
        {
            SpawnTile();
            scoreCount++;
            scoreText.text = scoreCount.ToString();
            gameScore.text = scoreCount.ToString();
        }
        else
        {
            Endgame();
        }
    }*/


    private void MoveTile()
    {
        tileTransition += Time.deltaTime * tileSpeed;
        if(isMovingOnX)
            stack[stackIndex].transform.localPosition = new Vector3(Mathf.Sin(tileTransition )* BoundSize, scoreCount, SecondaryPosition);
        else
            stack[stackIndex].transform.localPosition = new Vector3(SecondaryPosition, scoreCount, Mathf.Sin(tileTransition) * BoundSize);
    }
    private void SpawnTile()
    {
        lastTilePosition = stack[stackIndex].transform.position;
        stackIndex--;
        if (stackIndex < 0)
            stackIndex = transform.childCount - 1;
        desiredPosition = (Vector3.down) * scoreCount;
        stack[stackIndex].transform.localPosition = new Vector3(0, scoreCount, 0);
        stack[stackIndex].transform.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
        ColorMesh(stack[stackIndex].GetComponent<MeshFilter>().mesh);
    }
    private bool PlaceTile()
    {
        Transform t = stack[stackIndex].transform;
        if (isMovingOnX)
        {
            float deltaX = lastTilePosition.x - t.position.x;
            if (Mathf.Abs(deltaX) > ErrorMargin)
            {
                //cutting cube
                combo = 0;
                stackBounds.x -= Mathf.Abs(deltaX);
                if (stackBounds.x <= 0)
                {
                    return false;
                }
                float middle = lastTilePosition.x + t.localPosition.x / 2;
                t.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
                CreateRubble(new Vector3((t.position.x > 0)
                    ? t.position.x + (t.localScale.x / 2)
                    : t.position.x - (t.localScale.x/2),
                    t.position.y,
                    t.position.z),
                    new Vector3(Mathf.Abs(deltaX), 1, t.localScale.z)
                    );
                t.localPosition = new Vector3(middle - (lastTilePosition.x / 2), scoreCount, lastTilePosition.z);
            }
            else
            {
                if (combo > ComboStartGain)
                {
                    stackBounds.x += StackBoundsGain;
                    if(stackBounds.x > BoundSize)
                        stackBounds.x = BoundSize;
                    float middle = lastTilePosition.x + t.localPosition.x / 2;
                    t.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
                    t.localPosition = new Vector3(middle - (lastTilePosition.x / 2), scoreCount, lastTilePosition.z);
                }
                combo++;
                t.localPosition = new Vector3(lastTilePosition.x, scoreCount, lastTilePosition.z);
            }
        }
        else
        {
            float deltaz = lastTilePosition.z - t.position.z;
            if (Mathf.Abs(deltaz) > ErrorMargin)
            {
                //cutting cube
                combo = 0;

                stackBounds.y -= Mathf.Abs(deltaz);
                if (stackBounds.y <= 0)
                {
                    return false;
                }
                float middle = lastTilePosition.z + t.localPosition.z / 2;
                t.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
                CreateRubble(new Vector3(t.position.x
                    , t.position.y
                    , (t.position.z > 0)
                    ? t.position.z + (t.localScale.z / 2)
                    : t.position.z - (t.localScale.z / 2)),
                    new Vector3(Mathf.Abs(deltaz), 1, t.localScale.z));
                t.localPosition = new Vector3(lastTilePosition.x , scoreCount, middle - lastTilePosition.z / 2);
            }
            else
            {
                if(combo > ComboStartGain)
                {
                    if (stackBounds.y > BoundSize)
                        stackBounds.y = BoundSize;
                    stackBounds.y += StackBoundsGain;
                    float middle = lastTilePosition.z + t.localPosition.z / 2;
                    t.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
                    t.localPosition = new Vector3(lastTilePosition.x, scoreCount, middle - lastTilePosition.z / 2);
                }
                combo++;
                t.localPosition = new Vector3(lastTilePosition.x, scoreCount, lastTilePosition.z);
            }
        }
        SecondaryPosition = (isMovingOnX)
            ? t.localPosition.x
             :t.localPosition.z;
        isMovingOnX = !isMovingOnX;
        return true;
    }

    private void ColorMesh(Mesh mesh)
    {
        Vector3[] vertices = mesh.vertices;
        Color32[] colors = new Color32[vertices.Length];
        float f = Mathf.Sin(scoreCount * 0.25f);
        for (int i = 0; i < vertices.Length; i++)
            colors[i] = Lerp4(gameColors[0], gameColors[1], gameColors[2], gameColors[3],f);
        mesh.colors32 = colors;
   }

    private Color32 Lerp4(Color32 a,Color32 b,Color32 C,Color32 d,float t)
    {
        if (t < 0.33f)
            return Color.Lerp(a, b, t / 0.33f);
        else if (t < 0.66f)
            return Color.Lerp(b, C, (t - 0.33f) / 0.33f);
        else
            return Color.Lerp(C, d, (t - 0.66f) / 0.66f);
        
    }

    private void Endgame()
    {
        if (PlayerPrefs.GetInt("score") < scoreCount)
            PlayerPrefs.SetInt("score", scoreCount);
        highscore = PlayerPrefs.GetInt("score", scoreCount);
        stack[stackIndex].AddComponent<Rigidbody>();
        gameOver = true;
        stack[stackIndex].SetActive(false);
        PauseButton.SetActive(false);
        particlesTail.SetActive(true);
        Vibration.Vibrate(400);
        CameraShake.SetActive(true);
        AdsForLose.instance.CalculatingScene();

        //endPanel.GetComponent<Animator>().Play("PanelAnimation");
        StartCoroutine(Animation());
    }
    public void OnButtonClick(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    IEnumerator Animation()
    {
        yield return new WaitForSeconds(1.1f);
        endPanel.GetComponent<Animator>().Play("PanelAnimation");
    }
}
