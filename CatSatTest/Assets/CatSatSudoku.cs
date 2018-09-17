using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CatSAT;
using static CatSAT.Language;


public class CatSatSudoku : MonoBehaviour {

	void Start () {

        //for(int i =0;i<50;i++)
        //    SudokuTest();

        for (int i = 0; i < 50; i++)
            IanSudokuTest();



    }

    public void AdamSudokuTest() {
        var problem = new Problem("Sudoku");
        Proposition[,,] cube = new Proposition[9, 9, 9]; // row, column, value 

        // Construct the cube of propositions
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                for (int k = 0; k < 9; k++) {
                    cube[i, j, k] = (Proposition)($"{i + 1},{j + 1}={k + 1}");
                }
            }
        }

        //Unique within a single cell.
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                Proposition[] valuesWithinCell = new Proposition[9];
                for (int k = 0; k < 9; k++) {
                    valuesWithinCell[k] = cube[i, j, k];
                }
                problem.Unique(valuesWithinCell);
            }
        }

        // Unique within the column.
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                Proposition[] column = new Proposition[9];
                for (int k = 0; k < 9; k++) {
                    column[k] = cube[k, i, j];
                }
                problem.Unique(column);
            }
        }

        // Unique within the row.
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                Proposition[] row = new Proposition[9];
                for (int k = 0; k < 9; k++) {
                    row[k] = cube[j, k, i];
                }
                problem.Unique(row);
            }
        }

        // Unique within the cage.
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                Proposition[] cage = new Proposition[9];
                for (int k = 0; k < 9; k++) {
                    cage[k] = cube[3 * (j % 3) + k % 3, 3 * (j / 3) + k / 3, i];
                }
                problem.Unique(cage);
            }
        }


        // Create the puzzle 'easy #1' from proofdoku.com
        // http://proofdoku.com/#100329600089160070602000409078016000400700160300000058903001806021090040004283000

        string puzzle = "100329600089160070602000409078016000400700160300000058903001806021090040004283000";
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                int puzzlePiece;
                int.TryParse(puzzle.Substring(9 * i + j, 1), out puzzlePiece);
                if (puzzlePiece > 0) {
                    problem[cube[i, j, puzzlePiece - 1]] = true;
                }
            }
        }

        var watch = System.Diagnostics.Stopwatch.StartNew();
        var solution = problem.Solve();
        watch.Stop();
        var elapsedMs = watch.ElapsedTicks;


        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                for (int k = 0; k < 9; k++) {
                    if (solution[cube[i, j, k]]) {
                        //Debug.Log(cube[i, j, k]);
                    }
                }
            }
        }
        print($"Solution time: {elapsedMs}");
    }
    public void IanSudokuTest() {

        var p = new Problem("Sudoku");
        var digits = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var cell = Predicate<int, int, int>("cell");
        foreach (var rank in digits)
            foreach (var d in digits) {
                p.Unique(digits, row => cell(row, rank, d));
                p.Unique(digits, column => cell(rank, column, d));
            }
        foreach (var row in digits)
            foreach (var col in digits)
                p.Unique(digits, d => cell(row, col, d));

        var watch2 = System.Diagnostics.Stopwatch.StartNew();
        p.Solve();
        watch2.Stop();
        var elapsedMs2 = watch2.ElapsedTicks;
        print(elapsedMs2);
        
            

    }
}
