using System.Collections;
using UnityEngine;

public abstract class BaseLogic {

	//paramètres de la classe
	private int MazeRows;
	private int MazeColumns;
	private MazeCell[,] Maze;

	public int RowCount{ get{ return MazeRows; } }
	public int ColumnCount { get { return MazeColumns; } }

	public BaseLogic (int rows, int columns){
		MazeRows = Mathf.Abs (rows);
		MazeColumns = Mathf.Abs (columns);
		if (MazeRows <= 0) {
			MazeRows = 1;
		}
		if (MazeColumns <= 0) {
			MazeColumns = 1;
		}
		Maze = new MazeCell[rows, columns];
		for (int row = 0; row < rows; row++) {
			for (int column = 0; column < columns; column++) {
				Maze [row, column] = new MazeCell ();
			}
		}
	}

	public abstract void GenerateMaze();

	//récupère la cellule du maze donnée
	public MazeCell GetMazeCell(int row, int column){
		if (row >= 0 && column >= 0 && row < MazeRows && column < MazeColumns) {
			return Maze[row,column];
		}else{
			Debug.Log(row+" "+column);
			throw new System.ArgumentOutOfRangeException();
		}
	}

	protected void SetMazeCell(int row, int column, MazeCell cell){
		if (row >= 0 && column >= 0 && row < MazeRows && column < MazeColumns) {
			Maze[row,column] = cell;
		}else{
			throw new System.ArgumentOutOfRangeException();
		}
	}
}
