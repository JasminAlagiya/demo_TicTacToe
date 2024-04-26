using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject CrossPrefab, NoughtPrefab, CrossPrefabSeven, NoughtPrefabSeven, ResultPanel;

    public static Manager instance;

    public TurnSelection turnSelection;

    public TextMeshProUGUI TurnTxt, ScoreTxt;

    public GameObject[] GridPrefab;

    public List<GameObject> AllBox = new List<GameObject>();

    public TextMeshProUGUI ResultScoreTxt,ResultTxt;

    //public TurnSelection[] AllBoxSelectedTurn;


    public List<TurnSelection> AllBoxSelectedTurn = new List<TurnSelection>();

    public bool isGameWin = false;
    public bool isGameWinFive = false;


    GameObject GeneratedGrid;

    public LineRenderer Line;

    public bool isDraw = false;

    private void Awake()
    {
        instance = this;
       // ScoreTxt.text = PlayerPrefs.GetInt("Srossj").ToString() + ":" + PlayerPrefs.GetInt("Noughtj");
    }

    private void Start()
    {
        AllBoxSelectedTurn.Clear();

        turnSelection = TurnSelection.Cross;

        TurnTxt.color = new Color32(198, 219, 90, 255);

        
        ScoreTxt.text = PlayerPrefs.GetInt("Srossj").ToString() + ":" + PlayerPrefs.GetInt("Noughtj");


        if (DDOL.instance.selectionGrid == SelectionGridModa.ThreeVsThree)
        {
            GeneratedGrid = Instantiate(GridPrefab[0]);
            foreach (Transform go in GeneratedGrid.transform)
            {
                AllBox.Add(go.gameObject);
            }
            for (int i = 0; i < AllBox.Count; i++)
            {
                AllBoxSelectedTurn.Add(TurnSelection.none);
            }
        }
        if (DDOL.instance.selectionGrid == SelectionGridModa.FiveVsFive)
        {
            GeneratedGrid = Instantiate(GridPrefab[1]);
            foreach (Transform go in GeneratedGrid.transform)
            {
                AllBox.Add(go.gameObject);
            }
            for (int i = 0; i < AllBox.Count; i++)
            {
                AllBoxSelectedTurn.Add(TurnSelection.none);
            }
        }
        if (DDOL.instance.selectionGrid == SelectionGridModa.SevenVsSeven)
        {
            GeneratedGrid = Instantiate(GridPrefab[2]);
            foreach (Transform go in GeneratedGrid.transform)
            {
                AllBox.Add(go.gameObject);
            }
            for (int i = 0; i < AllBox.Count; i++)
            {
                AllBoxSelectedTurn.Add(TurnSelection.none);
            }
        }
        GeneratedGrid.GetComponent<SpriteRenderer>().color = new Color32(198, 219, 90, 255);
    }

    public void BoxClick(GameObject G)
    {
        SoundManager.Instance.ObjectPutSound();

        Debug.Log(G.gameObject.name);
        if (turnSelection == TurnSelection.Cross)
        {
            GameObject G1 = Instantiate(CrossPrefab);
            GameObject G2 = Instantiate(CrossPrefabSeven);

            G1.transform.position = G.transform.position;
            G2.transform.position = G.transform.position;



            TurnTxt.text = "Second Player Turn";

            TurnTxt.color = new Color32(238, 123, 48, 255);
            GeneratedGrid.GetComponent<SpriteRenderer>().color = new Color32(198, 219, 90, 255);
            G.GetComponent<BoxCollider2D>().enabled = false;


            AllBoxSelectedTurn[int.Parse(G.gameObject.name)] = TurnSelection.Cross;


            // Draw Game
            int counter = 0;

            for (int i = 0; i < AllBoxSelectedTurn.Count; i++)
            {
                if (AllBoxSelectedTurn[i] == TurnSelection.none)
                {
                    // Debug.Log("Game is Continues");
                    counter++;
                }

            }
            if (counter == 0)
            {
                SoundManager.Instance.WinClickSound();

                Debug.Log("DrawGame");
                isDraw=true;
                ResultPanel.SetActive(true);
                Invoke("ScenesLod", 2f);
                ResultScoreTxt.text = PlayerPrefs.GetInt("Srossj").ToString() + ":" + PlayerPrefs.GetInt("Noughtj");
                ResultTxt.text = "DrawGame";
            }

            if (DDOL.instance.selectionGrid == SelectionGridModa.ThreeVsThree && WinCheck() == true)
            {
                SoundManager.Instance.WinClickSound();

                Debug.Log("Three Cross Win");
                for (int i = 0; i < AllBox.Count; i++)
                {
                    AllBox[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                ResultPanel.SetActive(true);
                Invoke("ScenesLod", 2f);

                PlayerPrefs.SetInt("Srossj", PlayerPrefs.GetInt("Srossj") + 1);

                ScoreTxt.text = PlayerPrefs.GetInt("Srossj").ToString() + ":" + PlayerPrefs.GetInt("Noughtj");
                ResultScoreTxt.text = PlayerPrefs.GetInt("Srossj").ToString() + ":" + PlayerPrefs.GetInt("Noughtj");
                Debug.Log("AA" + ScoreTxt.text);
                ResultTxt.text = "Cross Win";

            }
            else if (DDOL.instance.selectionGrid == SelectionGridModa.FiveVsFive && WinCheckFive() == true)
            {
                SoundManager.Instance.WinClickSound();

                Debug.Log("Five Cross Win");
                for (int i = 0; i < AllBox.Count; i++)
                {
                    AllBox[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                ResultPanel.SetActive(true);
                Invoke("ScenesLod", 2f);
                PlayerPrefs.SetInt("Srossj", PlayerPrefs.GetInt("Srossj") + 1);

                ScoreTxt.text = PlayerPrefs.GetInt("Srossj").ToString();
                ResultScoreTxt.text = PlayerPrefs.GetInt("Srossj").ToString() + ":" + PlayerPrefs.GetInt("Noughtj");
                ResultTxt.text = "Cross Win";

            }
            else if(DDOL.instance.selectionGrid == SelectionGridModa.SevenVsSeven && WinCheckSeven() == true)
            {
                SoundManager.Instance.WinClickSound();

                Debug.Log("Seven Cross Win");
                for (int i = 0; i < AllBox.Count; i++)
                {
                    AllBox[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                ResultPanel.SetActive(true);
                Invoke("ScenesLod", 2f);
                PlayerPrefs.SetInt("Srossj", PlayerPrefs.GetInt("Srossj") + 1);

                ScoreTxt.text = PlayerPrefs.GetInt("Srossj").ToString();
                ResultScoreTxt.text = PlayerPrefs.GetInt("Srossj").ToString() + ":" + PlayerPrefs.GetInt("Noughtj");
                ResultTxt.text = "Cross Win";
            }
            else
            {
                turnSelection = TurnSelection.Nought;
                if(DDOL.instance.selection == SelectionMode.PlayerVsBot && DDOL.instance.selectionGrid == SelectionGridModa.ThreeVsThree && isDraw == false)
                {
                    //StartCoroutine(BotTurn());
                    StartCoroutine(BotTurnMinMax());

                }
                else if(DDOL.instance.selection == SelectionMode.PlayerVsBot && DDOL.instance.selectionGrid == SelectionGridModa.FiveVsFive && isDraw == false)
                {
                    //StartCoroutine(BotTurnFive());
                    StartCoroutine(BotTurnMinMaxFive());
                }
            }
          

            //else
            //{
            //    turnSelection = TurnSelection.Nought;
            //    //if (DDOL.instance.selection == SelectionMode.PlayerVsBot && isDraw == false)
            //    //{
            //    //    //StartCoroutine(BotTurn());
            //    //    StartCoroutine(BotTurnMinMax());
            //    //}
            //}

        }
        else if (turnSelection == TurnSelection.Nought && DDOL.instance.selection == SelectionMode.PlayerVsPlayer)
        {
            GameObject G1 = Instantiate(NoughtPrefab);
            G1.transform.position = G.transform.position;


            AllBoxSelectedTurn[int.Parse(G.gameObject.name)] = TurnSelection.Nought;

            if (DDOL.instance.selectionGrid == SelectionGridModa.ThreeVsThree && WinCheck() == true)
            {
                SoundManager.Instance.WinClickSound();

                Debug.Log("Nought Win");
                for (int i = 0; i < AllBox.Count; i++)
                {
                    AllBox[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                ResultPanel.SetActive(true);
                Invoke("ScenesLod", 2f);
                PlayerPrefs.SetInt("Noughtj", PlayerPrefs.GetInt("Noughtj") + 1);

                ScoreTxt.text = PlayerPrefs.GetInt("Srossj") + ":" + PlayerPrefs.GetInt("Noughtj").ToString();
                ResultScoreTxt.text = PlayerPrefs.GetInt("Srossj") + ":" + PlayerPrefs.GetInt("Noughtj").ToString();
                Debug.LogError( "AAaA" + PlayerPrefs.GetInt("Srossj") + ":" + PlayerPrefs.GetInt("Noughtj").ToString());
                ResultTxt.text = "Nought Win";

            }
            else if(DDOL.instance.selectionGrid == SelectionGridModa.SevenVsSeven && WinCheckSeven() == true)
            {
                SoundManager.Instance.WinClickSound();

                Debug.Log("Five Nought Win");
                for (int i = 0; i < AllBox.Count; i++)
                {
                    AllBox[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                ResultPanel.SetActive(true);
                Invoke("ScenesLod", 2f);
                PlayerPrefs.SetInt("Noughtj", PlayerPrefs.GetInt("Noughtj") + 1);

                ScoreTxt.text = PlayerPrefs.GetInt("Noughtj").ToString();
                ResultScoreTxt.text = PlayerPrefs.GetInt("Srossj") + ":" + PlayerPrefs.GetInt("Noughtj").ToString();
                ResultTxt.text = "Nought Win";

            }
            else if (DDOL.instance.selectionGrid == SelectionGridModa.SevenVsSeven && WinCheckSeven() == true)
            {
                SoundManager.Instance.WinClickSound();

                Debug.Log("Seven Nought Win");
                for (int i = 0; i < AllBox.Count; i++)
                {
                    AllBox[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                ResultPanel.SetActive(true);
                Invoke("ScenesLod", 2f);
                PlayerPrefs.SetInt("Noughtj", PlayerPrefs.GetInt("Noughtj") + 1);
                ScoreTxt.text = PlayerPrefs.GetInt("Noughtj").ToString();
                ResultScoreTxt.text = PlayerPrefs.GetInt("Srossj") + ":" + PlayerPrefs.GetInt("Noughtj").ToString();
                ResultTxt.text = "Nought Win";
            }
            else
            {
                turnSelection = TurnSelection.Cross;

                TurnTxt.text = "First Player Turn";

                TurnTxt.color = new Color32(198, 219, 90, 255);
                GeneratedGrid.GetComponent<SpriteRenderer>().color = new Color32(238, 123, 48, 255);

                G.GetComponent<BoxCollider2D>().enabled = false;
            }
           
        }
       
    }
    public void ScenesLod()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public List<int> AllEmptyPos = new List<int>();

    public IEnumerator BotTurn()
    {
        AllEmptyPos.Clear();
        yield return new WaitForSeconds(1f);
        for(int i=0; i< AllBoxSelectedTurn.Count; i++) 
        {
            if (AllBoxSelectedTurn[i] == TurnSelection.none) 
            {
                AllEmptyPos.Add(i);
            }
        }
        int RadomPos = UnityEngine.Random.Range(0,AllEmptyPos.Count);

        GameObject G1 = Instantiate(NoughtPrefab);
        G1.transform.position = AllBox[AllEmptyPos[RadomPos]].transform.position;
        AllBox[AllEmptyPos[RadomPos]].GetComponent<BoxCollider2D>().enabled = false;
        AllBoxSelectedTurn[AllEmptyPos[RadomPos]] = TurnSelection.Nought;
        if (DDOL.instance.selectionGrid == SelectionGridModa.ThreeVsThree && WinCheck() == true)
        {
            SoundManager.Instance.WinClickSound();

            Debug.Log("Nought Win");

            for (int i = 0; i < AllBox.Count; i++)
            {
                AllBox[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            turnSelection = TurnSelection.Cross;

            TurnTxt.text = "First Player Turn";

            TurnTxt.color = new Color32(198, 219, 90, 255);

            GeneratedGrid.GetComponent<SpriteRenderer>().color = new Color32(238, 123, 48, 255);

            // G.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public List<int> AllEmptyPosFive = new List<int>();

    //public IEnumerator BotTurnFive()
    //{
    //    AllEmptyPosFive.Clear();
    //    yield return new WaitForSeconds(1f);
    //    for (int i = 0; i < AllBoxSelectedTurn.Count; i++)
    //    {
    //        if (AllBoxSelectedTurn[i] == TurnSelection.none)
    //        {
    //            AllEmptyPosFive.Add(i);
    //        }
    //    }
    //    int RadomPosFive = UnityEngine.Random.Range(0, AllEmptyPos.Count);

    //    GameObject G1 = Instantiate(NoughtPrefab);
    //    G1.transform.position = AllBox[AllEmptyPosFive[RadomPosFive]].transform.position;
    //    AllBox[AllEmptyPosFive[RadomPosFive]].GetComponent<BoxCollider2D>().enabled = false;
    //    AllBoxSelectedTurn[AllEmptyPosFive[RadomPosFive]] = TurnSelection.Nought;
    //    if (DDOL.instance.selectionGrid == SelectionGridModa.FiveVsFive && WinCheckFive() == true)
    //    {
    //        Debug.Log("Five Nought Win");
    //        for (int i = 0; i < AllBox.Count; i++)
    //        {
    //            AllBox[i].GetComponent<BoxCollider2D>().enabled = false;
    //        }
    //        ResultPanel.SetActive(true);
    //        Invoke("ScenesLod", 2f);
    //        PlayerPrefs.SetInt("Noughtj", PlayerPrefs.GetInt("Noughtj") + 1);

    //        ScoreTxt.text = PlayerPrefs.GetInt("Noughtj").ToString();
    //        ResultScoreTxt.text = PlayerPrefs.GetInt("Srossj") + ":" + PlayerPrefs.GetInt("Noughtj").ToString();
    //        ResultTxt.text = "Nought Win";

    //    }
    //    else
    //    {
    //        turnSelection = TurnSelection.Cross;

    //        TurnTxt.text = "First Player Turn";

    //        TurnTxt.color = new Color32(198, 219, 90, 255);
    //        GeneratedGrid.GetComponent<SpriteRenderer>().color = new Color32(238, 123, 48, 255);

    //      //  G.GetComponent<BoxCollider2D>().enabled = false;
    //    }

    //}

    public IEnumerator BotTurnMinMax()
    {
        counterValue = 0;
        yield return new WaitForSeconds(1f);
        char[,] board = new char[3, 3];

        int counter = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (AllBoxSelectedTurn[counter] == TurnSelection.none)
                {
                    board[i, j] = '_';
                }
                else if (AllBoxSelectedTurn[counter] == TurnSelection.Cross)
                {
                    board[i, j] = 'x';
                }
                else if (AllBoxSelectedTurn[counter] == TurnSelection.Nought)
                {
                    board[i, j] = 'o';
                }

                counter++;
            }
        }

        Move bestMove = findBestMove(board);

        Debug.Log("The Optimal Move is :\n");
        Debug.Log("ROW: {0} COL: {1}\n\n" + bestMove.row + "::" + bestMove.col);


        int FinalMovePos = 0;

        int MovableValue = 0;


        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (bestMove.row == i && bestMove.col == j)
                {
                    //Debug.Log("Final Move " + FinalMovePos);

                    MovableValue = FinalMovePos;
                }

                FinalMovePos++;
            }
        }

        GameObject G1 = Instantiate(NoughtPrefab);

        G1.transform.position = AllBox[MovableValue].transform.position;

        AllBox[MovableValue].GetComponent<BoxCollider2D>().enabled = false;

        AllBoxSelectedTurn[MovableValue] = TurnSelection.Nought;


        if (WinCheck() == true)
        {
            SoundManager.Instance.WinClickSound();

            Debug.Log("Nought Win");

            for (int i = 0; i < AllBox.Count; i++)
            {
                AllBox[i].GetComponent<BoxCollider2D>().enabled = false;
            }
            ResultPanel.SetActive(true);
            Invoke("ScenesLod", 2f);
            PlayerPrefs.SetInt("Noughtj", PlayerPrefs.GetInt("Noughtj") + 1);

            ScoreTxt.text = PlayerPrefs.GetInt("Noughtj").ToString();
            ResultScoreTxt.text = PlayerPrefs.GetInt("Srossj") + ":" + PlayerPrefs.GetInt("Noughtj").ToString();
            ResultTxt.text = "Nought Win";

        }
        else
        {
            turnSelection = TurnSelection.Cross;

            TurnTxt.text = "First Player Turn";

            TurnTxt.color = new Color32(198, 219, 90, 255);

            GeneratedGrid.GetComponent<SpriteRenderer>().color = new Color32(238, 123, 48, 255);

            // G.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public IEnumerator BotTurnMinMaxFive()
    {
        counterValue = 0;
        yield return new WaitForSeconds(1f);
        char[,] board = new char[5, 5];

        int counter = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (AllBoxSelectedTurn[counter] == TurnSelection.none)
                {
                    board[i, j] = '_';
                }
                else if (AllBoxSelectedTurn[counter] == TurnSelection.Cross)
                {
                    board[i, j] = 'x';
                }
                else if (AllBoxSelectedTurn[counter] == TurnSelection.Nought)
                {
                    board[i, j] = 'o';
                }

                counter++;
            }
        }

        Move bestMoveFive = findBestMoveFive(board);

        Debug.Log("The Optimal Move is :\n");
        Debug.Log("ROW: {0} COL: {1}\n\n" + bestMoveFive.row + "::" + bestMoveFive.col);


        int FinalMovePosFive = 0;

        int MovableValueFive = 0;


        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (bestMoveFive.row == i && bestMoveFive.col == j)
                {
                    //Debug.Log("Final Move " + FinalMovePos);

                    MovableValueFive = FinalMovePosFive;
                }

                FinalMovePosFive++;
            }
        }

        GameObject G1 = Instantiate(NoughtPrefab);

        G1.transform.position = AllBox[MovableValueFive].transform.position;

        AllBox[MovableValueFive].GetComponent<BoxCollider2D>().enabled = false;

        AllBoxSelectedTurn[MovableValueFive] = TurnSelection.Nought;


        if (DDOL.instance.selectionGrid == SelectionGridModa.FiveVsFive && WinCheckFive() == true)
        {
            SoundManager.Instance.WinClickSound();

            Debug.Log("Five Nought Win");
            for (int i = 0; i < AllBox.Count; i++)
            {
                AllBox[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            turnSelection = TurnSelection.Cross;

            TurnTxt.text = "First Player Turn";

            TurnTxt.color = new Color32(198, 219, 90, 255);
            GeneratedGrid.GetComponent<SpriteRenderer>().color = new Color32(238, 123, 48, 255);

            // G.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    #region MinMax 

    static char player = 'o', opponent = 'x';

    // This function returns true if there are moves 
    // remaining on the board. It returns false if 
    // there are no moves left to play. 
    static Boolean isMovesLeft(char[,] board)
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (board[i, j] == '_')
                    return true;
        return false;
    }

    // This is the evaluation function as discussed 
    // in the previous article ( http://goo.gl/sJgv68 ) 
    static int evaluate(char[,] b)
    {
        // Checking for Rows for X or O victory. 
        for (int row = 0; row < 3; row++)
        {
            if (b[row, 0] == b[row, 1] &&
                b[row, 1] == b[row, 2])
            {
                if (b[row, 0] == player)
                    return +10;
                else if (b[row, 0] == opponent)
                    return -10;
            }
        }

        // Checking for Columns for X or O victory. 
        for (int col = 0; col < 3; col++)
        {
            if (b[0, col] == b[1, col] &&
                b[1, col] == b[2, col])
            {
                if (b[0, col] == player)
                    return +10;

                else if (b[0, col] == opponent)
                    return -10;
            }
        }

        // Checking for Diagonals for X or O victory. 
        if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2])
        {
            if (b[0, 0] == player)
                return +10;
            else if (b[0, 0] == opponent)
                return -10;
        }

        if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0])
        {
            if (b[0, 2] == player)
                return +10;
            else if (b[0, 2] == opponent)
                return -10;
        }

        // Else if none of them have won then return 0 
        return 0;
    }


    static int counterValue = 0;
    // This is the minimax function. It considers all 
    // the possible ways the game can go and returns 
    // the value of the board 
    static int minimax(char[,] board,
                    int depth, Boolean isMax)
    {
        int score = evaluate(board);

        // If Maximizer has won the game 
        // return his/her evaluated score 
        if (score == 10)
            return score;

        // If Minimizer has won the game 
        // return his/her evaluated score 
        if (score == -10)
            return score;

        // If there are no more moves and 
        // no winner then it is a tie 
        if (isMovesLeft(board) == false)
            return 0;

        // If this maximizer's move 
        if (isMax)
        {
            int best = -1000;

            // Traverse all cells 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty 
                    if (board[i, j] == '_')
                    {
                        // Make the move 
                        board[i, j] = player;

                        counterValue++;

                       // Debug.Log("Counter Value is " + counterValue);

                        // Call minimax recursively and choose 
                        // the maximum value 
                        best = Math.Max(best, minimax(board,
                                        depth + 1, !isMax));

                        // Undo the move 
                        board[i, j] = '_';
                    }
                }
            }
            return best;
        }

        // If this minimizer's move 
        else
        {
            int best = 1000;

            // Traverse all cells 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty 
                    if (board[i, j] == '_')
                    {
                        // Make the move 
                        board[i, j] = opponent;

                        // Call minimax recursively and choose 
                        // the minimum value 
                        best = Math.Min(best, minimax(board,
                                        depth + 1, !isMax));

                        // Undo the move 
                        board[i, j] = '_';
                    }
                }
            }
            return best;
        }
    }

    // This will return the best possible 
    // move for the player 
    static Move findBestMove(char[,] board)
    {
        int bestVal = -1000;
        Move bestMove = new Move();
        bestMove.row = -1;
        bestMove.col = -1;

        // Traverse all cells, evaluate minimax function 
        // for all empty cells. And return the cell 
        // with optimal value. 
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // Check if cell is empty 
                if (board[i, j] == '_')
                {
                    // Make the move 
                    board[i, j] = player;

                    // compute evaluation function for this 
                    // move. 
                    int moveVal = minimax(board, 0, false);

                    // Undo the move 
                    board[i, j] = '_';

                    // If the value of the current move is 
                    // more than the best value, then update 
                    // best/ 
                    if (moveVal > bestVal)
                    {
                        bestMove.row = i;
                        bestMove.col = j;
                        bestVal = moveVal;
                    }
                }
            }
        }

        Console.Write("The value of the best Move " +
                            "is : {0}\n\n", bestVal);

        return bestMove;
    }

    #endregion

    #region MinMax Five

    static char player1 = 'o', opponent1 = 'x';

    // This function returns true if there are moves 
    // remaining on the board. It returns false if 
    // there are no moves left to play. 
    static Boolean isMovesLeftFive(char[,] board)
    {
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                if (board[i, j] == '_')
                    return true;
        return false;
    }

    // This is the evaluation function as discussed 
    // in the previous article ( http://goo.gl/sJgv68 ) 
    static int evaluateFive(char[,] b)
    {
        // Checking for Rows for X or O victory. 
        for (int row = 0; row < 5; row++)
        {
            if (b[row, 0] == b[row, 1] &&
                b[row, 1] == b[row, 2] &&
                b[row, 2] == b[row, 3] &&
                b[row, 3] == b[row, 4])
            {
                if (b[row, 0] == player)
                    return +10;
                else if (b[row, 0] == opponent)
                    return -10;
            }
        }

        // Checking for Columns for X or O victory. 
        for (int col = 0; col < 5; col++)
        {
            if (b[0, col] == b[1, col] &&
                b[1, col] == b[2, col] &&
                b[2, col] == b[3, col] &&
                b[3, col] == b[4, col])
            {
                if (b[0, col] == player)
                    return +10;

                else if (b[0, col] == opponent)
                    return -10;
            }
        }

        // Checking for Diagonals for X or O victory. 
        if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2] && b[2, 2] == b[3, 3] && b[3, 3] == b[4, 4])
        {
            if (b[0, 0] == player)
                return +10;
            else if (b[0, 0] == opponent)
                return -10;
        }

        if (b[0, 4] == b[1, 3] && b[1, 3] == b[2, 2] && b[2, 2] == b[3, 1] && b[3, 1] == b[4, 0])
        {
            if (b[0, 4] == player)
                return +10;
            else if (b[0, 4] == opponent)
                return -10;
        }

        // Else if none of them have won then return 0 
        return 0;
    }


    static int counterValueFive = 0;
    // This is the minimax function. It considers all 
    // the possible ways the game can go and returns 
    // the value of the board 
    static int minimaxFive(char[,] board,
                    int depth, Boolean isMax)
    {
        int score = evaluate(board);

        // If Maximizer has won the game 
        // return his/her evaluated score 
        if (score == 10)
            return score;

        // If Minimizer has won the game 
        // return his/her evaluated score 
        if (score == -10)
            return score;

        // If there are no more moves and 
        // no winner then it is a tie 
        if (isMovesLeft(board) == false)
            return 0;

        // If this maximizer's move 
        if (isMax)
        {
            int best = -1000;

            // Traverse all cells 
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    // Check if cell is empty 
                    if (board[i, j] == '_')
                    {
                        // Make the move 
                        board[i, j] = player;

                        counterValue++;

                        Debug.Log("Counter Value is " + counterValue);

                        // Call minimax recursively and choose 
                        // the maximum value 
                        best = Math.Max(best, minimax(board,
                                        depth + 1, !isMax));

                        // Undo the move 
                        board[i, j] = '_';
                    }
                }
            }
            return best;
        }

        // If this minimizer's move 
        else
        {
            int best = 1000;

            // Traverse all cells 
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    // Check if cell is empty 
                    if (board[i, j] == '_')
                    {
                        // Make the move 
                        board[i, j] = opponent;

                        // Call minimax recursively and choose 
                        // the minimum value 
                        best = Math.Min(best, minimax(board,
                                        depth + 1, !isMax));

                        // Undo the move 
                        board[i, j] = '_';
                    }
                }
            }
            return best;
        }
    }

    // This will return the best possible 
    // move for the player 
    static Move findBestMoveFive(char[,] board)
    {
        int bestVal = -1000;
        Move bestMove = new Move();
        bestMove.row = -1;
        bestMove.col = -1;

        // Traverse all cells, evaluate minimax function 
        // for all empty cells. And return the cell 
        // with optimal value. 
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                // Check if cell is empty 
                if (board[i, j] == '_')
                {
                    // Make the move 
                    board[i, j] = player;

                    // compute evaluation function for this 
                    // move. 
                    int moveVal = minimax(board, 0, false);

                    // Undo the move 
                    board[i, j] = '_';

                    // If the value of the current move is 
                    // more than the best value, then update 
                    // best/ 
                    if (moveVal > bestVal)
                    {
                        bestMove.row = i;
                        bestMove.col = j;
                        bestVal = moveVal;
                    }
                }
            }
        }

        Console.Write("The value of the best Move " +
                            "is : {0}\n\n", bestVal);

        return bestMove;
    }

    #endregion
    public bool WinCheck()
    {
        int[,] WinCondition = new int[8, 3]
                                     {
                                     { 0, 1, 2 } ,
                                     { 3, 4, 5 } ,
                                     { 6, 7, 8 } ,
                                     { 0, 3, 6 } ,
                                     { 1, 4, 7 } ,
                                     { 2, 5, 8 } ,
                                     { 0, 4, 8 } ,
                                     { 2, 4, 6 } };
        for (int i = 0; i < 8; i++)
        {
            if (AllBoxSelectedTurn[WinCondition[i, 0]] == turnSelection
                && AllBoxSelectedTurn[WinCondition[i, 1]] == turnSelection
                && AllBoxSelectedTurn[WinCondition[i, 2]] == turnSelection)
            {
                Line.positionCount = 3;

                Line.SetPosition(0, AllBox[WinCondition[i, 0]].transform.position);
                Line.SetPosition(1, AllBox[WinCondition[i, 1]].transform.position);
                Line.SetPosition(2, AllBox[WinCondition[i, 2]].transform.position);


                return true;
            }
        }
        return false;
    }
    public bool WinCheckFive()
    {
        int[,] WinConditionfive = new int[,]
                                     {
                                     {0,1,2,3 },
                                     {1,2,3,4},
                                     {5,6,7,8},
                                     {6,7,8,9},
                                     {10,11,12,13},
                                     {11,12,13,14},
                                     {15,16,17,18},
                                     {16,17,18,19},
                                     {20,21,22,23},
                                     {21,22,23,24},
                                     {0,5,10,15},
                                     {5,0,15,20},
                                     {1,6,11,16},
                                     {6,11,16,21},
                                     {2,7,12,17},
                                     {7,12,17,22},
                                     {3,8,13,18},
                                     {8,13,18,23},
                                     {4,9,14,19},
                                     {9,14,19,24},
                                     {0,6,12,18},
                                     {6,12,18,24},
                                     {1,7,13,19},
                                     {5,11,17,23},
                                     {4,8,12,16},
                                     {8,12,16,20},
                                     {3,7,11,15},
                                     {9,13,17,21}
                                     };
        for (int i = 0; i < 28; i++)
        {
            if (AllBoxSelectedTurn[WinConditionfive[i, 0]] == turnSelection
                && AllBoxSelectedTurn[WinConditionfive[i, 1]] == turnSelection
                && AllBoxSelectedTurn[WinConditionfive[i, 2]] == turnSelection
                && AllBoxSelectedTurn[WinConditionfive[i, 3]] == turnSelection)
            {
                Debug.Log("Win");
                Line.positionCount = 4;

                Line.SetPosition(0, AllBox[WinConditionfive[i, 0]].transform.position);
                Line.SetPosition(1, AllBox[WinConditionfive[i, 1]].transform.position);
                Line.SetPosition(2, AllBox[WinConditionfive[i, 2]].transform.position);
                Line.SetPosition(3, AllBox[WinConditionfive[i, 3]].transform.position);


                return true;
            }
        }
        return false;
    }
    public bool WinCheckSeven()
    {
        int[,] WinConditionSeven = new int[,] 
                                          {
                                              {0,1,2,3},
                                              {1,2,3,4},
                                              {2,3,4,5},
                                              {3,4,5,6},
                                              {7,8,9,10},
                                              {8,9,10,11},
                                              {9,10,11,12},
                                              {10,11,12,13},
                                              {14,15,16,17},
                                              {15,16,17,18},
                                              {16,17,18,19},
                                              {17,18,19,20},
                                              {21,22,23,24},
                                              {22,23,24,25},
                                              {23,24,25,26},
                                              {24,25,26,27},
                                              {28,29,30,31},
                                              {29,30,31,32},
                                              {30,31,32,33},
                                              {31,32,33,34},
                                              {35,36,37,38},
                                              {36,37,38,39},
                                              {37,38,39,40},
                                              {38,39,40,41},
                                              {42,43,44,45},
                                              {43,44,45,46},
                                              {44,45,46,47},
                                              {45,46,47,49},
                                              {0,7,14,21},
                                              {7,14,21,28},
                                              {14,21,28,35},
                                              {21,28,35,42},
                                              {1,8,15,22},
                                              {8,15,22,29},
                                              {15,22,29,36},
                                              {22,29,36,43},
                                              {2,9,16,23},
                                              {9,16,23,30},
                                              {16,23,30,37},
                                              {23,30,37,44},
                                              {3,10,17,24},
                                              {10,17,24,31},
                                              {17,24,31,38},
                                              {24,31,38,45},
                                              {4,11,18,25},
                                              {11,18,25,32},
                                              {18,25,32,39},
                                              {25,32,39,46},
                                              {5,12,19,26},
                                              {12,19,26,33},
                                              {19,26,33,40},
                                              {26,33,40,47},
                                              {6,13,20,27},
                                              {13,20,27,34},
                                              {20,27,34,41},
                                              {27,34,41,48},
                                              {0,8,16,24},
                                              {8,16,24,32},
                                              {16,24,32,40},
                                              {24,32,40,48},
                                              {7,15,23,31},
                                              {15,23,31,39},
                                              {23,31,39,47},
                                              {14,22,30,38},
                                              {22,30,38,46},
                                              {21,29,37,45},
                                              {1,9,17,25},
                                              {9,17,25,33},
                                              {17,25,33,41},
                                              {2,10,18,26},
                                              {10,18,26,34},
                                              {3,11,19,27},
                                              {6,12,18,24},
                                              {12,18,24,30},
                                              {18,24,30,36},
                                              {24,30,36,42},
                                              {5,11,17,23},
                                              {11,17,23,29},
                                              {17,23,29,35},
                                              {4,10,16,22},
                                              {10,16,22,28},
                                              {3,9,15,21},
                                              {13,19,25,31},
                                              {19,25,31,37},
                                              {25,31,37,43},
                                              {20,26,32,38},
                                              {26,32,38,44},
                                              {27,33,39,45}
                                              };
        for (int i = 0; i < 88; i++)
        {
            if (AllBoxSelectedTurn[WinConditionSeven[i, 0]] == turnSelection
                && AllBoxSelectedTurn[WinConditionSeven[i, 1]] == turnSelection
                && AllBoxSelectedTurn[WinConditionSeven[i, 2]] == turnSelection
                && AllBoxSelectedTurn[WinConditionSeven[i, 3]] == turnSelection)
            {
                Debug.Log("Win");
                Line.positionCount = 4;

                Line.SetPosition(0, AllBox[WinConditionSeven[i, 0]].transform.position);
                Line.SetPosition(1, AllBox[WinConditionSeven[i, 1]].transform.position);
                Line.SetPosition(2, AllBox[WinConditionSeven[i, 2]].transform.position);
                Line.SetPosition(3, AllBox[WinConditionSeven[i, 3]].transform.position);


                return true;
            }
        }
        return false;
    }
    public void BackBtnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        SoundManager.Instance.BtnClickSound();

    }
}

public class Move
{
    public int row, col;
};
